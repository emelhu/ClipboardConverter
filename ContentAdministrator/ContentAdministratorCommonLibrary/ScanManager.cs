using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ContentAdministratorCommonLibrary
{
    using ContentAdministratorCommonLibrary.Data;
    using eMeL_Common;

    public static class ScanManager
    {
        #region private variables
        private static Thread                           manager      = new Thread (() => ManageScans(cancelSource.Token));
        private static ConcurrentQueue<ScanParameters>  scanQueue    = new ConcurrentQueue<ScanParameters>();

        private static CancellationTokenSource          cancelSource = new CancellationTokenSource();
        private const  int                              waitUnit     = 100;   
        #endregion

        static ScanManager()
        {
            //manager.Start();                  -- ! problem !                        
        }

        #region engine
        public static void QueueScan(ScanParameters parameters)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("!!!!!!!!!!! ManageScans/QueueScan nem működik még !!!!!!!!!!");

            scanQueue.Enqueue(parameters);
  
            if (! manager.IsAlive)
            {
                manager.Start();       
            }            
        }

        public static void ManageScans(CancellationToken cancelToken)
        {
            do 
            {
                bool isNext = scanQueue.TryDequeue(out ScanParameters parameters);

                if (isNext)
                {
                    // TODO: start new thread       ...!!!...       ...!!!...       ...!!!...       ...!!!...       ...!!!...       ...!!!...
                    // _scannedDirs++;

                    if (ScanManager.subdirectories)
                    {
                        new Thread(() => AddSubdirectories(cancelSource.Token, parameters)).Start();
                    }
                }
                else
                {
                    Thread.Sleep(waitUnit * 3);   

                    bool allThreadFinished = false;             // TODO: ...!!!...      ...!!!...       ...!!!...       ...!!!...

                    //allThreadFinished = ....

                    if (scanQueue.IsEmpty && allThreadFinished)             
                    {   // if all thread finished and no more request
                        break;
                    }
                }
            } while (! cancelToken.IsCancellationRequested);

            hasFinished = true;
        }

        public static void AddSubdirectories(CancellationToken cancelToken, ScanParameters parameters)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            foreach (var subdir in System.IO.Directory.EnumerateDirectories(parameters.directory))
            {
                Thread.Sleep(random.Next(waitUnit, waitUnit * 2));                          // for scramble subdirectories of multiple command line directory parameter

                ScanParameters subdirParameters = new ScanParameters(parameters, subdir);

                scanQueue.Enqueue(subdirParameters);
            }
        }        

        public static void CancelRun()
        {
            cancelSource.Cancel();
        }
        #endregion

        #region interface
        public static bool      deletingDuplicate = false;

        public static bool      subdirectories    = false;

        public  static Strategy strategy { get; private set;} = Strategy.Calculated;

        public  const  int      threadsCountMax = 8;                        // too much paralell disk I/O don't improves performance
        private static int     _threadsCount    = 0;                        // 0 or 1 means "no threading" (all scans run in main console thread)
        public  static int      threadsCount { get => _threadsCount; set => _threadsCount = Math.Min(Math.Max(1, value), threadsCountMax); } 

        public static void      SetStrategy(string text)
        {
            Debug.Assert(! String.IsNullOrWhiteSpace(text));

            var names = Enum.GetNames(typeof(Strategy));

            for (int loop = 0; loop < names.Length; loop++)
            {
                if (string.Compare(names[loop], text, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Debug.Assert(loop >= 0 && loop <= 9);
                    strategy = (Strategy)loop;
                    return;
                }
            }

            var enabledNames = string.Join(",", names);

            throw new EMEL_ParameterException($"Strategy parameter value can be only one of {enabledNames}!", 
                                               new Guid("3EE86A20-904D-47DC-9E27-18CDB7B7DF6C"));
        }
        
        private static bool    _hasFinished = false;
        public  static bool     hasFinished { get => _hasFinished; private set => _hasFinished = value; }

        private static long    _scannedDirs = 0;
        public static long      scannedDirs { get => _scannedDirs; set { if (threadsCount < 2) _scannedDirs = value; else Debug.Fail("scannedDirs!"); } }
        #endregion
    }

    //

    public enum Strategy
    {
        Calculated = 0,             // auto 1..5
        Volatile   = 1,             // Volatile volume
        Regular    = 2,             // Regular volume
        Removable  = 3,             // Removable volume
        Archive    = 4,             // Archive volume
        ReadOnly   = 5              // Readonly volume
    };
}
