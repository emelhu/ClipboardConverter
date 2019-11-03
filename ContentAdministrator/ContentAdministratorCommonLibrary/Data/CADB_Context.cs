#nullable disable           // https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references

using System;
using System.Collections.Generic;
using System.Text;

//using FirebirdSql;
//using FirebirdSql.Data;
//using FirebirdSql.EntityFrameworkCore.Firebird;             // optionsBuilder.UseFirebird extension
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using eMeL_Common;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using Microsoft.Extensions.Logging;


// shema migration: Install-Package Microsoft.EntityFrameworkCore.Tools
//                  https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/
//                  https://stackoverflow.com/questions/43398483/command-line-connection-string-for-ef-core-database-update

// dotnet ef:       https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet

namespace ContentAdministratorCommonLibrary.Data
{
    public class CADB_Context : eMeL_Common.DbContextBase //, eMeL_Common.IDbContextBase
    {
        #region Database Tables
        public DbSet<Volume>            volume          { get; set; }
        public DbSet<Directory>         directory       { get; set; }
        public DbSet<File>              file            { get; set; }
        public DbSet<Logs>              logs            { get; set; }
        public DbSet<ParameterValue>    parameterValue  { get; set; }
        #endregion

        #region Sequence

        public static IDSequence    idSequence;
        public static IDSequence    strategySequence;

        public static long          nextID => idSequence.nextID;

        /// <summary>
        /// Get value for volume's strategy field for parametrized strategy.
        /// (information for "strategy algorithm" to select any of duplicated file to delete)
        /// </summary>
        /// <param name="prefix">0..9 value, tipically value of "enum Strategy" </param>
        /// <returns></returns>
        public static long          GetNextStrategyValue(int prefix)
        {
            Debug.Assert((prefix >= 0) && (prefix <= 9));
            
            var next = strategySequence.nextID;

            return (next * strategySequenceIncrementStep) + (prefix * strategySequencePrefixValue);            
        }

        public const string sequenceName  = "ID_Sequence";
        public const int    sequenceStart = 1000_000;

        public const string strategySequenceName   = "StrategySequence";        
        public const int    strategySequenceIncrementStep = 1000_000;
        public const long   strategySequencePrefixValue   = 1000_000_000_000_000;
        #endregion

        #region Constructor & init
        public CADB_Context()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new EMEL_DatabaseException("CADB_Context.connectionString is not defined!", 
                                                 new Guid("D2283323-A517-4534-90F9-1AD981BA50A7"));
            }
        }

        /// <summary>
        /// Initialize database connection/context informations before create first context instance.
        /// </summary>
        /// <param name = "connectionString" > Connection string to database(extend with provider info)</param>
        public static void Init(string connectionString)
        {
            DbContextBase.Init<CADB_Context>(connectionString);                       // Call base class's Init()

            GetSequenceNextDelegate getSequenceNext = DbHelpers.GetSequenceValue<CADB_Context>;
            
            idSequence       = new IDSequence(sequenceName,         getSequenceNext, 100, 10, true);
            strategySequence = new IDSequence(strategySequenceName, getSequenceNext, 10);
          }
        #endregion

        #region Configuring/ModelCreating
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
           
            switch (databaseServerType)
            {
                case DatabaseServerType.Firebird:
                    //options.UseFirebird(_connectionString);               // https://sourceforge.net/p/firebird/mailman/message/36776666/
                    break;
                case DatabaseServerType.SqlServer:
                    #if DEBUG
                    options.UseLoggerFactory(SqlCommandLoggerFactory);
                    #endif  
                    options.UseSqlServer(_connectionString);
                    break;
                default:
                    throw new Exception($"Database type invalid! [{databaseServerType}]");
            }            
        }    
        
        private static readonly ILoggerFactory SqlCommandLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => ((category == DbLoggerCategory.Database.Command.Name) && (level == LogLevel.Information)))
                   .AddConsole();
        });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<long>(CADB_Context.sequenceName).StartsAt(CADB_Context.sequenceStart);

            modelBuilder.HasSequence<long>(CADB_Context.strategySequenceName);

            //

            {   // --- Volume ---
                var e = modelBuilder.Entity<Volume>();
                e.Property(e => e.name).HasMaxLength(Volume.nameMaxLength);
                e.HasIndex(e => e.guid)
                 .HasName("Volume_Guid_Ix")
                 .IsUnique();
            }

            {   // --- Directory ---
                var e = modelBuilder.Entity<Directory>();
                e.Property(e => e.name).HasMaxLength(Directory.nameMaxLength);
                e.HasIndex(e => e.guid)
                 .HasName("Directory_Guid_Ix")
                 .IsUnique();
            }

            {   // --- File ---
                var e = modelBuilder.Entity<File>();
                e.Property(e => e.name).HasMaxLength(File.nameMaxLength);
                e.HasIndex(e => e.guid)
                 .HasName("File_Guid_Ix")
                 .IsUnique();
            }

            {   // --- Logs ---
                var e = modelBuilder.Entity<Logs>();
                e.Property(e => e.group).HasMaxLength(Logs.groupMaxLength);
                e.Property(e => e.logText).HasMaxLength(Logs.logTextMaxLength);
                e.HasIndex(e => new { e.group, e.logTime })               
                 .HasName("Logs_GroupLogTime_Ix")                       
                 .IsUnique();
            }

            {   // --- ParameterValue ---
                var e = modelBuilder.Entity<ParameterValue>();
                //e.Property(e => e.id).ValueGeneratedNever();
                e.HasIndex(e => new { e.group, e.name })
                 .HasName("ParameterValue_GroupName_Ix")
                 .IsUnique();

            }

            //

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
 }

/*
example:
------------------------------------------------------------------------
        modelBuilder.HasSequence<int>("OrderNumbers", schema: "shared")
            .StartsAt(1000)
            .IncrementsBy(5);

        modelBuilder.Entity<Order>()
            .Property(o => o.OrderNo)
            .HasDefaultValueSql("NEXT VALUE FOR shared.OrderNumbers");
------------------------------------------------------------------------
        modelBuilder.Entity<Logs>()
            .Property(p => p.logTime).ValueGeneratedOnAdd(); -or- ValueGeneratedNever();
------------------------------------------------------------------------
*/
