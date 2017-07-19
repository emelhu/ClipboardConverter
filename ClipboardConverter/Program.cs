using System;
using System.Threading;
using System.Windows;

namespace ClipboardConverter
{
  class Program
  {
    static string lastClipboardText = null;

    [STAThread]
    static void Main(string[] args)
    {
      var prog = new Program();
      
      prog.Start();

    }

    
    public void Start()
    {
      //Thread.SetApartmentState(ApartmentState.STA);

      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.BackgroundColor = ConsoleColor.DarkGray;
      Console.WriteLine("   *** Clipboard content converter *** (c) eMeL *** www.emel.hu ***   ");
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
      if (clipboardText.Contains("."))
      {
        clipboardText = clipboardText.Replace(".", "");
      }

      return clipboardText;
    }
  }
}
