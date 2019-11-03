using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace PatternFeeder
{
  public partial class ViewerForm : Form
  {
    private string showText;

    private enum TextType {Rtf, Htm, Txt};
    private TextType type;

    public ViewerForm(string showText, string labelText = null)
    {
      InitializeComponent();

      //

      if (labelText != null)
      {
        this.Text = labelText;
      }


      this.showText = showText;

      if (isRtfText(showText))
      { // View as RTF text
        webBrowser.Visible   = false;
        richTextBox.ReadOnly = true;
        richTextBox.Rtf      = showText;
        type                 = TextType.Rtf;
      }
      else if (isHtmText(showText))
      { // View as Html text
        richTextBox.Visible     = false;
        webBrowser.DocumentText = showText;
        type                    = TextType.Htm;
      }
      else
      { // View as pure text
        webBrowser.Visible   = false;
        richTextBox.ReadOnly = true;
        richTextBox.Text     = showText;
        type                 = TextType.Txt;
      }
    }

    private void ViewerForm_Resize(object sender, EventArgs e)
    {
      Control viewer = webBrowser.Visible ? (Control)webBrowser : (Control)richTextBox;

      viewer.Top    = 0;
      viewer.Left   = 0;
      viewer.Width  = this.Width;
      viewer.Height = controlPanel.Top;
    }

    //

    private void saveButton_Click(object sender, EventArgs e)
    {
      saveToFile(false);                                                    // false: ask filename
    }

    private void printButton_Click(object sender, EventArgs e)
    {
      var psi  = new ProcessStartInfo(saveToFile(true)); 
      psi.Verb = "Print";

      Process.Start(psi);
    }

    private void viewButton_Click(object sender, EventArgs e)
    {
      var psi = new ProcessStartInfo(saveToFile(true));      
      psi.Verb = "Open";

      Process.Start(psi);
    }

    private string saveToFile(bool temp)
    {
      string fileName;
 
      if (temp)
      {
        fileName = Path.GetTempPath() + @"\RtfViewerTemporaryFile_" + Environment.UserName + ".rtf";
      }
      else
      {
        switch (type)
        {
          case TextType.Rtf:
            saveFileDialog.DefaultExt = "rtf";
            break;

          case TextType.Htm:
            saveFileDialog.DefaultExt = "htm";
            break;

          case TextType.Txt:
            saveFileDialog.DefaultExt = "txt";
            break;

          default:
            saveFileDialog.DefaultExt = "!!!";
            break;
        }

        if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          fileName = saveFileDialog.FileName;
        }
        else
        {
          return null;
        }
      }

      File.WriteAllText(fileName, this.showText, Encoding.Default);

      return fileName;
    }

    //

    private static bool isRtfText(string text)
    {
      string start = text.Substring(0, 128).Trim().ToLower();

      return start.StartsWith(@"{\rtf");
    }

    private static bool isHtmText(string text)
    {
      if (isRtfText(text))
      {
        return false;
      }

      string start = text.Substring(0, 1024).Trim().ToLower();

      return start.Contains(@"<html");
    }

    private void ViewerForm_Load(object sender, EventArgs e)
    {
      ViewerForm_Resize(null, null);
    }
  }   
}
