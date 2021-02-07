//#define INMEMORORY_DATABASE_TEST

using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.CommandLineUtils;

using ContentAdministratorCommonLibrary;
using ContentAdministratorCommonLibrary.Data;

using eMeL_Common;
using System.Threading;
using System.Threading.Tasks;


// Install-Package Microsoft.Extensions.CommandLineUtils
// https://gist.github.com/iamarcel/8047384bfbe9941e52817cf14a79dc34

// Install-Package FirebirdSql.Data.FirebirdClient
// Install-Package FirebirdSql.EntityFrameworkCore.Firebird 
// Install-Package firebird.embedded
// Install-Package Microsoft.EntityFrameworkCore.InMemory

namespace ContentAdministrator
{
    class Program
    {
        #region variables

        public static ExitCode exitCode = ExitCode.OK;

        private const string DefaultDatabaseConnectionStringFilename = "Default_ConnectionString.txt";

        private const string HelpOptionWords = "-?|-h|--h|--help";

        #endregion

        static void Main(string[] args)
        {
            #if INMEMORORY_DATABASE_TEST && DEBUG
            CADB_Context.inMemoryDatabaseName = "TEST";                                         // not empty value switch on inMemoryDatabase test case
            #endif

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("ContentAdministratorScanAndDelete --- (c) eMeL, www.emel.hu  --- " + GetVersion() + Environment.NewLine + Environment.NewLine);
            Console.ResetColor();

            //{   // Set 'IDSequence.firstInstance'
            //    GetSequenceNextDelegate getSequenceNext = DbHelpers.GetSequenceNext<CADB_Context>;
            //    var temp = new IDSequence(getSequenceNext);

            //    //var temp = new IDSequence(getSequenceNext, 10, 5);                // TEST 1.
            //    //var temp = new IDSequence(getSequenceNext, 10, 5, true);          // TEST 2.
            //    //for (int i = 0; i < 100; i++)
            //    //{
            //    //    var next = IDSequence.next;
            //    //}                
            //}

            ProcessParameters(args);
        }

        #region static variables

        static CommandLineApplication commandLineApp = new CommandLineApplication();

        #endregion

        private static void ProcessParameters(string[] args)
        {
            commandLineApp.Name = "ContentAdministratorScanAndDelete";
            commandLineApp.Description = "Scan files/directories and build a file-information database and delete duplicated files.";
            commandLineApp.ExtendedHelpText = "\nThis is a simple app to scan files/directories and store file's information to database." + Environment.NewLine +
                                              "This program can delete duplicated files based on information of previous scans of momentarily attached and/or detached volumes.\n\n" +
                                              "Examples:" + Environment.NewLine +
                                              "ContentAdministratorScanAndDelete.exe  createdatabase default" + Environment.NewLine +
                                              "ContentAdministratorScanAndDelete.exe  scan --help" + Environment.NewLine +
                                              "ContentAdministratorScanAndDelete.exe  scan \\work\\test\\*.*  \\work\\test2\\  --strategy=Removable ";

            commandLineApp.HelpOption(HelpOptionWords);

            commandLineApp.OnExecute(() =>
                {
                    commandLineApp.ShowHelp();
                    return 0;
                });

            commandLineApp.VersionOption("-v|--version", () => GetVersion());

            #if DEBUG
            commandLineApp.Command("test", (command) =>
            {
                command.Description = "<<<TEST>>>";

                command.OnExecute(() =>
                {
                    Console.WriteLine($"*** {command.Description} ***" + Environment.NewLine);                   

                    var connStr = Database.ReadConnectionString();

                    CADB_Context.Init(connStr, true);

                    using (var db = new CADB_Context())
                    {
                        foreach (var item in db.logs)
                        {
                            Console.WriteLine($"id:{item.id} group:{item.group} time:{item.logTime} text:{item.logText}");
                        }

                        var log = new Logs("TEST", $"{Environment.UserName} at {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");

                        log.lastModTime = DateTime.Now;
                        log.lastModLogin = 0;

                        db.logs.Add(log);

                        db.SaveChanges();
                    }

                    //

                    Win32VolumeInfo[] win32VolumeInfos =  Win32VolumeManage.VolumesInfo;                    

                    var pathInfo = new UncouplePathParts(@"c:\_work_\atnezni\utal_alap\Bérjegyzék 2019.04.pdf");

                    return (int)ExitCode.OK;
                });
            });
            #endif

            commandLineApp.Command("volumes", (command) =>
            {
                command.Description = "Display available volumes' informations.";

                command.OnExecute(() =>
                {
                    const string line = "--------------------------------------------------------------------------";

                    Console.WriteLine($"*** {command.Description} ***" + Environment.NewLine);   

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nDriveInfo C# class");
                    Console.ResetColor();    

                    DriveInfo[] allDrives = DriveInfo.GetDrives();

                    foreach (DriveInfo d in allDrives)
                    {
                        Console.WriteLine(line);

                        Console.WriteLine("Drive {0}", d.Name);
                        Console.WriteLine("  Drive type: {0}", d.DriveType);
                        if (d.IsReady == true)
                        {
                            Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                            Console.WriteLine("  File system:  {0}", d.DriveFormat);
                            Console.WriteLine(
                                "  Available space to current user:{0, 15} bytes", 
                                d.AvailableFreeSpace);

                            Console.WriteLine(
                                "  Total available space:          {0, 15} bytes",
                                d.TotalFreeSpace);

                            Console.WriteLine(
                                "  Total size of drive:            {0, 15} bytes ",
                                d.TotalSize);
                        }
                    }
                    
                    Console.WriteLine(line); 
                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nThe Win32_Volume class represents an area of storage on a hard disk.\nThe class returns local volumes that are formatted, unformatted, mounted, or offline.");
                    Console.ResetColor();                                  
                    
                    foreach (var info in Win32VolumeManage.GetVolumesInfo())
                    {
                        Console.WriteLine(line);  

                        Console.WriteLine($"Automount                    : {info.automount                   }");
                        Console.WriteLine($"Availability                 : {info.availability                }");
                        Console.WriteLine($"BootVolume                   : {info.bootVolume                  }");
                        Console.WriteLine($"BlockSize                    : {info.blockSize                   }");
                        Console.WriteLine($"Caption                      : {info.caption                     }");
                        Console.WriteLine($"Capacity                     : {info.capacity                    }");
                        Console.WriteLine($"Compressed                   : {info.compressed                  }");
                        Console.WriteLine($"Description                  : {info.description                 }");
                        Console.WriteLine($"DeviceID                     : {info.deviceID                    }");
                        Console.WriteLine($"DriveLetter                  : {info.driveLetter                 }");
                        Console.WriteLine($"DriveType                    : {info.driveType                   }");
                        Console.WriteLine($"FileSystem                   : {info.fileSystem                  }");
                        Console.WriteLine($"FreeSpace                    : {info.freeSpace                   }");
                        Console.WriteLine($"IndexingEnabled              : {info.indexingEnabled             }");
                        Console.WriteLine($"Label                        : {info.label                       }");
                        Console.WriteLine($"MaximumFileNameLength        : {info.maximumFileNameLength       }");
                        Console.WriteLine($"Name                         : {info.name                        }");
                        Console.WriteLine($"SerialNumber                 : {info.serialNumber                }");
                        Console.WriteLine($"SupportsFileBasedCompression : {info.supportsFileBasedCompression}");
                        Console.WriteLine($"SystemName                   : {info.systemName                  }");
                        Console.WriteLine($"SystemVolume                 : {info.systemVolume                }");
                        Console.WriteLine($"Guid                         : {info.guid                        }");
                    }
                    Console.WriteLine(line);

                    //

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nWin32_MappedLogicalDisk WMI class represents network storage devices\nthat are mapped as logical disks on the computer system.");
                    Console.ResetColor();

                    foreach (var info in Win32MappedLogicalDiskManage.GetMappedLogicalDiskInfo())
                    {
                        Console.WriteLine(line);

                        Console.WriteLine($"Access                      : {info.access                      }");
                        Console.WriteLine($"Availability                : {info.availability                }");
                        Console.WriteLine($"BlockSize                   : {info.blockSize                   }");
                        Console.WriteLine($"Caption                     : {info.caption                     }");
                        Console.WriteLine($"Compressed                  : {info.compressed                  }");
                        Console.WriteLine($"Description                 : {info.description                 }");
                        Console.WriteLine($"DeviceID                    : {info.deviceID                    }");
                        Console.WriteLine($"FileSystem                  : {info.fileSystem                  }");
                        Console.WriteLine($"FreeSpace                   : {info.freeSpace                   }");
                        Console.WriteLine($"InstallDate                 : {info.installDate                 }");
                        Console.WriteLine($"Name                        : {info.name                        }");
                        Console.WriteLine($"NumberOfBlocks              : {info.numberOfBlocks              }");
                        Console.WriteLine($"PNPDeviceID                 : {info.pnpDeviceID                 }");
                        Console.WriteLine($"ProviderName                : {info.providerName                }");
                        Console.WriteLine($"Purpose                     : {info.purpose                     }");
                        Console.WriteLine($"SessionID                   : {info.sessionID                   }");
                        Console.WriteLine($"Size                        : {info.size                        }");
                        Console.WriteLine($"Status                      : {info.status                      }");
                        Console.WriteLine($"StatusInfo                  : {info.statusInfo                  }");
                        Console.WriteLine($"SupportsFileBasedCompression: {info.supportsFileBasedCompression}");
                        Console.WriteLine($"SystemName                  : {info.systemName                  }");
                        Console.WriteLine($"VolumeName                  : {info.volumeName                  }");
                        Console.WriteLine($"VolumeSerialNumber          : {info.volumeSerialNumber          }");
                    }
                    Console.WriteLine(line);

                    return (int)ExitCode.OK;
                });
            });

            commandLineApp.Command("createdatabase", (command) =>
            {
                command.Description = "Create database and save connection string";
                command.HelpOption(HelpOptionWords);
                command.ExtendedHelpText =
                    $"Database is FirebirdSql or MS  SQlServer and there is an example connection string in attached file '{DefaultDatabaseConnectionStringFilename}'." + Environment.NewLine +
                    "You can type connection string to command line, or type filename of connection string or type 'default' word and use filename above." + Environment.NewLine +
                    "Examples:" + Environment.NewLine +
                    "  ContentAdministratorScanAndDelete.exe  createdatabase default" + Environment.NewLine +
                    "  ContentAdministratorScanAndDelete.exe  createdatabase \".\\CA_ConnectionString.txt\"";

                var connectionStringArgument = command.Argument("connectionString", "Connection string to create database.");                

                command.OnExecute(async () =>
                {
                    Console.WriteLine($"*** {command.Description} ***" + Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  ~ current directory: " + System.IO.Directory.GetCurrentDirectory());
                    Console.ResetColor();

                    exitCode = ExitCode.Error;

                    var connectionString = RetrieveConnectionString(connectionStringArgument.Value);

                    Task[] tasks = new Task[2];

                    tasks[0] = Database.CreateDatabaseAsync(connectionString);
                    tasks[1] = Database.SaveConnectionStringAsync(connectionString);

                    await Task.WhenAll(tasks);

                    return (int)ExitCode.OK;
                });
            });

            commandLineApp.Command("saveconnectionstring", (command) =>
            {
                command.Description = "Save connection string for this user";
                command.HelpOption(HelpOptionWords);
                command.ExtendedHelpText =
                    $"Database is FirebirdSql or MS  SQlServer and there is an example connection string in attached file '{DefaultDatabaseConnectionStringFilename}'." + Environment.NewLine +
                    "You can type connection string to command line, or type filename of connection string or type 'default' word and use filename above." + Environment.NewLine +
                    "Examples:" + Environment.NewLine +
                    "  ContentAdministratorScanAndDelete.exe  saveconnectionstring default" + Environment.NewLine +
                    "  ContentAdministratorScanAndDelete.exe  saveconnectionstring \".\\CA_ConnectionString.txt\"";

                var connectionStringArgument = command.Argument("connectionString", "Connection string for connect to database.");

                command.OnExecute(async () =>
                {
                    Console.WriteLine($"*** {command.Description} ***" + Environment.NewLine);

                    exitCode = ExitCode.Error;

                    var connectionString = RetrieveConnectionString(connectionStringArgument.Value);
                    await Database.SaveConnectionStringAsync(connectionString);

                    return (int)ExitCode.OK;
                });
            });

            commandLineApp.Command("scan", (command) =>
            {
                command.Description = "Scan directories and files and store fileinfo to database.";
                command.HelpOption(HelpOptionWords);
                command.ExtendedHelpText =
                    "Scan directories and files defined in parameters." + Environment.NewLine +
                    "Store to database path and checksum informations for coordination of files." + Environment.NewLine +
                    "Examples:" + Environment.NewLine +
                    "  ContentAdministratorScanAndDelete.exe  scan *.*" + Environment.NewLine +
                    "  ContentAdministratorScanAndDelete.exe  scan C:\\mediacontents\\  --strategy reverse" + Environment.NewLine +
                    "  ContentAdministratorScanAndDelete.exe  scan C:\\mediacontents\\*.jpg --subdir";

                var pathDefinitionArguments = command.Argument("path", "Path and filename definition for scan these files.", true);
                
                var subdirectoryOption      = command.Option("-s|--subdir",                "Scan subdirectories",       CommandOptionType.NoValue);
                var includeFilesOption      = command.Option("-i|--include <wildcards>",   "Include files's wildcards", CommandOptionType.MultipleValue);
                var excludeFilesOption      = command.Option("-e|--exclude <wildcards>",   "Exclude files's wildcards", CommandOptionType.MultipleValue);
                var deletingDuplicateOption = command.Option("-dd|--deldup",               "Delete duplicates",         CommandOptionType.NoValue);
                var strategyOption          = command.Option("-st|--strategy <enumValue>", "Strategy for lifetime of files in directory", CommandOptionType.SingleValue);
                var threadsOption           = command.Option("-th|--threads <number>",    $"Number of thread for scan of files in directory (1..{ScanManager.threadsCountMax})", CommandOptionType.SingleValue);


                command.OnExecute(() =>
                {
                    Console.WriteLine($"*** {command.Description} ***" + Environment.NewLine);                   

                    var connStr = Database.ReadConnectionString();

                    CADB_Context.Init(connStr, true);

                    Console.WriteLine();
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;                  
                    Console.Write("Starts scan and store to database...    ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;       
                    Console.WriteLine("(you can break run with Ctrl+C or Ctrl+Break key)");
                    Console.ResetColor();

                    if (strategyOption.HasValue())
                    {
                        ScanManager.SetStrategy(strategyOption.Value());
                    }

                    if (threadsOption.HasValue())
                    {
                        int threadsCount;

                        if (Int32.TryParse(threadsOption.Value(), out threadsCount))
                        {
                            ScanManager.threadsCount = threadsCount; 
                        }
                        else
                        {
                            throw new EMEL_ParameterException("The parameter of --threads isn't a number!", 
                                                              new Guid("C2CBD49D-B97D-4EE7-8B6B-FE503EB2974A"));
                        }
                    }

                    ScanManager.deletingDuplicate = deletingDuplicateOption.HasValue();
                    ScanManager.subdirectories    = subdirectoryOption.HasValue();

                    Console.CancelKeyPress += OnCancelKeyPressed;

                    foreach (var path in pathDefinitionArguments.Values)
                    {
                        var parameters = new ScanParameters(path, includeFilesOption.Values, excludeFilesOption.Values);

                        if (ScanManager.threadsCount < 2)
                        {
                            Console.WriteLine($" » Scanning of {parameters.directory} is started.");
                            DoScan(parameters);                            
                        }
                        else
                        {
                            ScanManager.QueueScan(parameters);
                            Console.WriteLine($" » {parameters.directory} has queued.");
                        }                        
                    }

                    while (! cancelled && ! ScanManager.hasFinished)
                    {
                        Console.Write('.');
                        Thread.Sleep(1000);
                    };

                    Console.WriteLine($"\n{ScanManager.scannedDirs} directories scanned.");

                    return (int)ExitCode.OK;
                });
            });
            //

            exitCode = ExitCode.ParameterErr1;

            try
            {
                commandLineApp.Execute(args);

                exitCode = ExitCode.ParameterErr2;
            } 
            catch (Exception e)
            {
                string errtxt = e.Message;

                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    errtxt += "\n" + e.Message;
                }

                errtxt += "\n" + e.StackTrace;

                ShowErrorMessageAndUsage(errtxt, exitCode);
            }
        }

        private static void DoScan(ScanParameters parameters)
        {
            var scan = new Scan(parameters);
            scan.DoIt();
            ScanManager.scannedDirs++;

            if (ScanManager.subdirectories)
            {
                foreach (var subdir in System.IO.Directory.EnumerateDirectories(parameters.directory))
                {
                    ScanParameters subdirParameters = new ScanParameters(parameters, subdir);
                    DoScan(subdirParameters);                                           // recursion
                }
            }
        }

        private static volatile bool cancelled = false;

        private static void OnCancelKeyPressed(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("\n**** CANCELED by user ****  (please wait for terminate, max 10s)");

            e.Cancel = true;                                                // I want use own cancel(end of execution mechanism.

            ScanManager.CancelRun();                                        // cancel scan threads

            var cancelThread = new Thread(() => { Thread.Sleep(10_000); cancelled = true; });
            cancelThread.Start();                                           // emergency shutdown

            //Thread.Sleep(10_000); 
            //cancelled = true;
        }

        private static string RetrieveConnectionString(string parameterValue)
        {
            string filename;
            string connectionString = null;

            if (String.IsNullOrWhiteSpace(parameterValue) || parameterValue.ToUpper().Trim() == "DEFAULT")
            {
                filename = @".\" + DefaultDatabaseConnectionStringFilename;
            }
            else
            {
                filename = parameterValue.Trim();
            }

            if (System.IO.File.Exists(filename))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  ~ connection string file: " + filename);
                Console.ResetColor();

                var lines = System.IO.File.ReadAllLines(filename);

                foreach (var line in lines) 
                {
                    if (! String.IsNullOrEmpty(line) && ! line.TrimStart().StartsWith("--"))
                    {
                        connectionString = line;
                        break;
                    }
                }               
            }
            else
            {
                connectionString = parameterValue;
            }

            return connectionString;
        }

        private static string GetVersion()
        {
            return string.Format("Version {0}", Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
        }

        public static void ShowErrorMessageAndUsage(string errorMessage = null, ExitCode exitCode = ExitCode.OK, bool showHelp = true, bool onlyWarning = false)
        {
            if (showHelp)
            {
                commandLineApp.ShowHelp();
            }

            if (!String.IsNullOrWhiteSpace(errorMessage))
            {
                var colBG = Console.BackgroundColor;
                var colFG = Console.ForegroundColor;

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Error.WriteLine();
                if (!onlyWarning)
                {
                    Console.Error.WriteLine("-------------------------------- !!! ERROR !!! --------------------------------");
                }

                Console.Error.WriteLine(errorMessage);
                Console.Error.WriteLine();

                Console.BackgroundColor = colBG;
                Console.ForegroundColor = colFG;
            }

#if DEBUG
            Console.WriteLine(Environment.NewLine + Environment.NewLine + "Press any key to exit...");
            Console.ReadKey();
#endif

            if ((int)exitCode > 0)
            {
                Environment.Exit((int)exitCode);
            }
        }
    }

    //

    public enum ExitCode
    {
        OK              = 0,
        NormalExit      = 1,
        ParameterErr1   = 11,
        ParameterErr2   = 12,
        Error           = 100
    };
}
