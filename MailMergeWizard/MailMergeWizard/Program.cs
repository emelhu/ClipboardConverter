﻿using System;
using System.IO;
using System.Text;
using System.Reflection;

using DevLib.Csv;                               // Install-Package DevLib.Csv // https://devlib.codeplex.com/
using System.Diagnostics;

// Install-Package DevLib.Csv
// Install-Package System.Text.Encoding.CodePages

//

// see also: http://www.filehelpers.net/example/QuickStart/ReadFileDelimited/

namespace MailMergeWizard
{
  class Program
  {
    #region variables

    static string       rtfFilename = null;
    static string       csvFilename = null;

    static string       rtf;
    static CsvDocument  csv;

    static Encoding     csvEncoding = Encoding.UTF8;                                              // Default is UTF8 in DotNetCore

    #endregion

    #region constants

    const  string rtfExtension = ".rtf";
    const  string csvExtension = ".csv";

    #endregion

    static void Main(string[] args)
    {
      Console.ResetColor();
      Console.BackgroundColor = ConsoleColor.Gray;
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("Mail Merge Wizard | a simple solution without LibreOffice or Microsoft Office");
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.WriteLine("                                            (c) eMeL | www.emel.hu | freeware\n\n");
      Console.ResetColor();
      Console.BackgroundColor = ConsoleColor.White;
      Console.ForegroundColor = ConsoleColor.Black;

      ProcessArguments(args);

      //

      if (! File.Exists(rtfFilename))
      {
        DisplayUsage("Invalid RTF filename: " + rtfFilename);
      }

      if (!File.Exists(csvFilename))
      {
        DisplayUsage("Invalid CSV filename: " + csvFilename);
      }

      //

      try
      {
        rtf = File.ReadAllText(rtfFilename);

        string csvText = File.ReadAllText(csvFilename, csvEncoding);

        csv = new CsvDocument();
        csv.LoadCsv(csvText);                                                                      // csv[0, 0] = "hello";  csv.RowCount; csv.ColumnCount;
      }
      catch (Exception e)
      {
        DisplayUsage("Exception (rtf or csv): " + e.Message);
      }

      Console.WriteLine("CSV filename: " + csvFilename);
      Console.WriteLine("RTF filename: " + rtfFilename);

      if (! csv.HasHeader)
      {
        DisplayUsage("Threre isn't header in CSV file!");
      }

      if (csv.RowCount < 1)
      {
        DisplayUsage("Threre aren't data rows in CSV file!");
      }

      Console.WriteLine();

      //

      foreach (string columnName in csv.HeaderColumns)
      {
        bool existInRTF = rtf.Contains(columnName);

        Console.ForegroundColor = existInRTF ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed;
        Console.WriteLine(columnName + " column " + (existInRTF ? "exists" : "NOT exists") + " in RTF text.");
      }

      Console.ResetColor();
      Console.WriteLine();

      //

      for (int rowIndex = 0; rowIndex < csv.RowCount; rowIndex++)
      {
        string rtfMod = rtf;

        for (int colIndex = 0; colIndex < csv.HeaderColumnCount; colIndex++)
        {
          string name = ConvertToRtfText(csv.HeaderColumns[colIndex]);
          string data = ConvertToRtfText(csv[rowIndex, colIndex]);

          rtfMod = rtfMod.Replace(name, data);
        }

        string generatedRtfFilename = Path.GetFileNameWithoutExtension(rtfFilename) + "_" + (rowIndex + 1).ToString("D4") + rtfExtension;

        File.WriteAllText(generatedRtfFilename, rtfMod);

        Console.WriteLine(generatedRtfFilename + " file has writen.");
      }

      //

      #if DEBUG
      Console.ReadLine();
      #endif
    }

    public static string GetRtfEncoding(char c)
    {
      if (c == '\\')
      { 
        return "\\\\"; 
      }

      if (c == '{')
      {
        return "\\{";
      }

      if (c == '}')
      {
        return "\\}";
      }

      if (c == '\r')
      {
        return "";
      }

      if (c == '¶')                                                                               
      { // Pilcrow Sign U+00B6
        return "\r\n\\line ";                                                                          
      }

      

      int intCode = Convert.ToInt32(c);

      if ((intCode >= 0x20) && (intCode < 0x80))
      {
        return c.ToString();
      }

      return "\\u" + intCode + "?";
    }

    public static string ConvertToRtfText(string s)
    {
      StringBuilder rtfText = new StringBuilder();

      foreach (char c in s)
      {
        rtfText.Append(GetRtfEncoding(c));
      }

      return rtfText.ToString();
    }

    private static void ProcessArguments(string[] args)
    {
      if (args.Length < 1)
      {
        DisplayUsage();
      }
      
      if (String.IsNullOrWhiteSpace(args[0]))
      {
        DisplayUsage();
      }

      bool foundFilename = false;

      foreach (string arg in args)
      { // for extension, when we will use more arguments
        var argType = getArgumentType(arg);

        switch (argType)
        {
          case ArgumentType.Invalid:
            DisplayUsage("Invalid argument: " + arg);
            break;
          case ArgumentType.Filename:
            ProcessFilenameArgument(arg);
            foundFilename = true;
            break;
          case ArgumentType.Help:
            DisplayUsage();
            break;
          case ArgumentType.Example:
            StartExample();
            break;
          case ArgumentType.CodePage:
            var cp = arg.Substring(10);

            if (String.IsNullOrWhiteSpace(cp))
            {
              csvEncoding = Encoding.UTF8;
            }
            else
            {
              try
              { // dotnet add package System.Text.Encoding.CodePages
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                int cpInt;

                try
                {
                  cpInt = Convert.ToInt32(cp);
                }
                catch 
                {
                  cpInt = -1;
                }

                if (cpInt >= 0)
                {
                  csvEncoding = Encoding.GetEncoding(cpInt);
                }
                else
                {
                  csvEncoding = Encoding.GetEncoding(cp);                                          // DEFAULT is UTF8 in DotNetCore, so we need load.
                }
              }
              catch (Exception e)
              {
                DisplayUsage("Invalid codepage: " + cp + "\n" + e.Message);
              }
            }
            break;
          default:
            DisplayUsage("Internal error at argument: " + arg);
            break;
        }
      }

      if (!foundFilename)
      {
        DisplayUsage("Filename argument isn't found !");
      }
    }  

    private static void ProcessFilenameArgument(string arg)
    {
      arg = arg.Trim();

      string filename  = Path.GetFileNameWithoutExtension(arg);  
      string extension = Path.GetExtension(arg);

      if (String.IsNullOrWhiteSpace(rtfFilename))
      {
        rtfFilename = filename + rtfExtension;
      }

      if (String.IsNullOrWhiteSpace(csvFilename))
      {
        csvFilename = filename + csvExtension;
      }

      if (String.Compare(extension, rtfExtension, true) == 0)
      {
        rtfFilename = filename;
      }

      if (String.Compare(extension, csvExtension, true) == 0)
      {
        csvFilename = filename;
      }
    }

    enum ArgumentType
    {
      Invalid,
      Filename,
      Help,
      Example,
      CodePage                                                                                    // Encoding of CSV file
    }

    private static ArgumentType getArgumentType(string arg)
    {
      if (arg.Length > 0)
      { 
        if ((arg[0] == '/') || (arg[0] == '-'))
        { // accepted: -arg, --arg, /arg
          if ((arg.Length > 1) && (arg[1] == '-'))
          {
            arg = arg.Substring(2);
          }
          else
          {
            arg = arg.Substring(1);
          }

          if ((String.Compare(arg, "?", true) == 0) || (String.Compare(arg, "h", true) == 0) || (String.Compare(arg, "help", true) == 0))
          { /// ignore case
            return ArgumentType.Help;
          }
          else if (String.Compare(arg, "example", true) == 0)
          { /// ignore case
            return ArgumentType.Example;
          }
          else if (String.Compare(arg, 0, "CodePage:", 0, 9, true) == 0)
          { /// ignore case
            return ArgumentType.CodePage;
          }          
        }
        else
        { // filename
          if (String.IsNullOrWhiteSpace(arg))
          {
            return ArgumentType.Invalid;
          }

          arg = arg.Trim();     

          var invalidChars = Path.GetInvalidFileNameChars();

          Array.Sort(invalidChars);

          foreach (char c in arg)
          {
            int ix = Array.BinarySearch<char>(invalidChars, c);

            if (ix >= 0)
            {
              return ArgumentType.Invalid;
            }
          }

          return ArgumentType.Filename;
        }
      }

      return ArgumentType.Invalid;
    }

    private static void DisplayUsage(string errText = null)
    {
      Console.BackgroundColor = ConsoleColor.Yellow;
      Console.ForegroundColor = ConsoleColor.DarkBlue;

      Console.WriteLine("usage: MailMergeWizard <filename> [-CodePage:<CodePageNameOrNumber>] ");
      Console.WriteLine("usage: MailMergeWizard <filename.rtf> [<filename.csv>] [-CodePage:<CodePageNameOrNumber>] ");
      Console.WriteLine("usage: MailMergeWizard -help | -h ");
      Console.WriteLine("usage: MailMergeWizard -example \n\n");
      Console.WriteLine("<filename> is a name of a RTF and CSV file.\n" +
                        "  This program use <filename.rtf> to source of shema text and\n" + 
                        "  <filename.csv> for data of generated content.");
      Console.WriteLine("Files must have in current directory, the path (filename) can't contains directory definition.");
      Console.WriteLine("Accepted command line parameter signals: -arg, --arg, /arg");
      Console.WriteLine("The -CodePage arguments define encoding of CSV file content. Default: UTF8");

      if (errText != null)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nERROR!\n" + errText + "\n");
      }

      #if DEBUG
      Console.ReadLine();
      #endif

      Environment.Exit(1);
    }

    private static void StartExample()
    {
      Console.BackgroundColor = ConsoleColor.Gray;
      Console.ForegroundColor = ConsoleColor.DarkGray;

      Console.WriteLine("*** Example rtf + csv resource access:");

      try
      {
        var assembly = Assembly.GetExecutingAssembly();

        //var names = assembly.GetManifestResourceNames();

        string resourceNamesBegin = "MailMergeWizard.Example";

        using (Stream stream = assembly.GetManifestResourceStream(resourceNamesBegin + ".rtf"))
        using (StreamReader reader = new StreamReader(stream))
        {
          string rtfText = reader.ReadToEnd();
          Console.WriteLine("  Example rtf resource is readed.");

          File.WriteAllText(@"./Example.rtf", rtfText);
          Console.WriteLine("  Example.rtf file is written.");
        }

        using (Stream stream = assembly.GetManifestResourceStream(resourceNamesBegin + ".csv"))
        using (StreamReader reader = new StreamReader(stream))
        {
          string csvText = reader.ReadToEnd();
          Console.WriteLine("  Example csv resource is readed.");

          File.WriteAllText(@"./Example.csv", csvText);
          Console.WriteLine("  Example.csv file is written.");
        }

        Console.WriteLine("  Reastart as 'DotNet MailMergeWizard.dll Example'");
        Console.ResetColor();
        Console.WriteLine();

        Process.Start("dotnet", "MailMergeWizard.dll Example");
        Environment.Exit(2);
      }
      catch (Exception e)
      {
        DisplayUsage("Example rtf+csv resource access error!\n[" + e.Message + "]");
      }
    }
  }
}