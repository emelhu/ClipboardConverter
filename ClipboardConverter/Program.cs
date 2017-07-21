using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Linq;

namespace ClipboardConverter
{
  class Program
  {
    static string         lastClipboardText = null;
    static ConversionType conversionType    = ConversionType.RemoveDotAndBlank;

    [STAThread]
    static void Main(string[] args)
    {
      var prog = new Program();
      
      prog.Start(args);

    }

    
    public void Start(string[] args)
    {
      //Thread.SetApartmentState(ApartmentState.STA);

      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.BackgroundColor = ConsoleColor.DarkGray;
      Console.WriteLine("   *** Clipboard content converter 1.01 *** (c) eMeL *** www.emel.hu ***   ");
      Console.ResetColor();
      Console.WriteLine();

      //

      if (args.Length > 0)
      { // first parameter [index: 0] is the name of program
        if (args[0].Length != 1)
        {
          Console.WriteLine("Error! The first parameter isn't a single karakter of code of operation!");
          Console.WriteLine();
          DisplayHelp(true);
          Console.WriteLine();
          Console.ReadKey();
          Environment.Exit(2);
        }

        conversionType = Helper.CheckTypeCode(args[0][0]);                                        // First char of first command line parameter
            
        if (conversionType == 0)
        {
          Console.WriteLine("Error! The parameter isn't a valid karakter of operations!");
          Console.WriteLine();
          DisplayHelp(true);
          Console.WriteLine();
          Console.ReadKey();
          Environment.Exit(3);
        }
      }
      else
      {
        while (true)
        {
          Console.WriteLine("\n\nSelect an operation code:\n");
          DisplayHelp(false);
          Console.Write("\nOperation: ");

          char operation = (char)Console.ReadKey().KeyChar;

          if (operation == (char)27)
          { // Escape char
            Environment.Exit(1);
          }

          conversionType = Helper.CheckTypeCode(operation);
            
          if (conversionType != 0)
          { // chacked a valid code
            break;
          }
        }
      }


      Console.WriteLine();
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.BackgroundColor = ConsoleColor.DarkGray;
      Console.WriteLine("selected conversion type: " + Helper.GetEnumDescription(conversionType));
      Console.ResetColor();
      Console.WriteLine();
      Console.WriteLine("... to break press ESC...");
      Console.WriteLine();
      Console.WriteLine();


      bool breaked;

      do
      {
        DoClipboardContentConvert();

        breaked = IsBreaked();

        Thread.Sleep(200);
      }
      while (!breaked);
    }

    private static bool IsBreaked()
    {
      if (Console.KeyAvailable)
      {
        var keyInfo = Console.ReadKey();

        if (keyInfo.Key == ConsoleKey.Escape)
        {
          return true;
        }
      }

      return false;
    }

    private static void DoClipboardContentConvert()
    {
      if (Clipboard.ContainsText())
      {
        string clipboardText = Clipboard.GetText();

        if (clipboardText != lastClipboardText)
        {
          lastClipboardText = ClipboardContentConverter(clipboardText);

          if (clipboardText != lastClipboardText)
          {
            Console.WriteLine("[" + clipboardText + "] --> [" + lastClipboardText + "]");

            //Clipboard.SetText(lastClipboardText);                                                 // EXCEPTION: Az OpenClipboard hívás végrehajtása sikertelen (A kivétel HRESULT-értéke: 0x800401D0 (CLIPBRD_E_CANT_OPEN))    

            NativeClipboard.SetText(lastClipboardText);                                             // troubleshooting because error above
          }
        }
      }
    }

    private static string ClipboardContentConverter(string clipboardText)
    {
      switch (conversionType)
      {
        case ConversionType.RemoveDot:
          if (clipboardText.Contains("."))
          {
            clipboardText = clipboardText.Replace(".", "");
          }
          break;

        case ConversionType.RemoveDotAndSpace:
          if (clipboardText.Contains(".") || clipboardText.Contains(" "))
          {
            clipboardText = clipboardText.Replace(".", "");
            clipboardText = clipboardText.Replace(" ", "");
          }
          break;

        case ConversionType.RemoveDotAndBlank:
          StringBuilder sb    = new StringBuilder(clipboardText);
          int           index = 0;

          while (index < sb.Length)
          {
            if (sb[index] == '.')
            {
              sb.Remove(index, 1);
            }
            else if (Char.IsWhiteSpace(sb[index]))
            {
              sb.Remove(index, 1);
            }
            else
            {
              index++;
            }
          }

          if (sb.Length < clipboardText.Length)
          { // Has changed
            clipboardText = sb.ToString();
          }
          break;

        default:
          Console.WriteLine("Invalid conversion type!");
          break;
      }
      

      return clipboardText;
    }

    public static void DisplayHelp(bool fullText)
    {
      if (fullText)
      {
        Console.WriteLine("usage: ClipboardConverter  <operation code character>");
        Console.WriteLine("  Operation codes and meaning:");
      }

      var conversionTypeDescriptions = from ConversionType n in Enum.GetValues(typeof(ConversionType))
                                         select new { ID = (int)n, Name = Helper.GetEnumDescription(n) };

      foreach (var desc in conversionTypeDescriptions)
      {
        Console.WriteLine("  '" + desc.ID + "' : " + desc.Name + "");
      }

      if (! fullText)
      {
        Console.WriteLine("  ESC : Quit program.");
      }
    }    
  }

  public enum ConversionType 
  {
    [Description("Remove dot characters.")]
    RemoveDot = 1, 

    [Description("Remove dot and space characters.")]
    RemoveDotAndSpace, 

    [Description("Remove dot and blank (whitespace) characters.")]
    RemoveDotAndBlank};   
}
