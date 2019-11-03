using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ContentAdministratorCommonLibrary.Data
{
  public static class ContextExtensions
  {
    //public static void AttachModified<T>(this DbSet<T> dbSet, T data)  where T : class
    //{
    //  dbSet.Attach(data);

    //  dbSet.GetContext().Entry(data).State = EntityState.Modified;
      
    //  //context.Entry(data).State = EntityState.Modified;
    //}

    //public static void AttachDeleted<T>(this DbSet<T> dbSet, T data)  where T : class
    //{
    //  dbSet.Attach(data);

    //  dbSet.GetContext().Entry(data).State = EntityState.Deleted;
      
    //  //context.Entry(data).State = EntityState.Deleted;
    //}

    //public static DbContext GetContext<TEntity>(this DbSet<TEntity> dbSet)
    //    where TEntity: class
    //{ 
    //    object internalSet = dbSet
    //        .GetType()
    //        .GetField("_internalSet", BindingFlags.NonPublic | BindingFlags.Instance)
    //        .GetValue(dbSet);

    //    object internalContext = internalSet
    //        .GetType()
    //        .BaseType
    //        .GetField("_internalContext", BindingFlags.NonPublic | BindingFlags.Instance)
    //        .GetValue(internalSet); 

    //    return (DbContext)internalContext
    //        .GetType()
    //        .GetProperty("Owner", BindingFlags.Instance | BindingFlags.Public)
    //        .GetValue(internalContext,null); 
    //} 

    //

    public static void Attach<T>(this DbContext context, DbSet<T> dbSet, T data)  where T : class
    {
      dbSet.Attach(data);
    }

    public static void AttachModified<T>(this DbContext context, DbSet<T> dbSet, T data)  where T : class
    {
      dbSet.Attach(data);
     
      context.Entry(data).State = EntityState.Modified;
    }

    public static void AttachDeleted<T>(this DbContext context, DbSet<T> dbSet, T data)  where T : class
    {
      dbSet.Attach(data);
      
      context.Entry(data).State = EntityState.Deleted;
    }

    public static void AttachAdded<T>(this DbContext context, DbSet<T> dbSet, T data)  where T : class
    {
      dbSet.Attach(data);
      
      context.Entry(data).State = EntityState.Added;
    }
  }
}
