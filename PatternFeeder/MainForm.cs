using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;  

//

namespace PatternFeeder
{
  using eMeL.PatternFeeder;
  using System.Diagnostics;
  using System.Threading;

  public partial class mainForm : Form
  {
    private PatternFeeder patternFeeder = null;
    
    //

    public mainForm()
    {
      InitializeComponent();

      SetControlPanelState(false);

      StateStore(false);                                      // read
    }

    private void exitButton_Click(object sender, EventArgs e)
    {
      Application.Exit();                                                         // It auto calls 'FormClosed' event.
    }

    private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      StateStore(true);                                     // save
    }

    //

    private void StateStore(bool save)
    {
      const string regKey = "HKEY_CURRENT_USER\\eMeL\\PatternFeeder";

      if (save)
      {
        string[] strings = { patternDocTextBox.Text, data1TextFileTextBox.Text, data2TextFileTextBox.Text };
        Registry.SetValue(regKey, "Files", strings);
      }
      else
      {
        string[] strings = (string[])Registry.GetValue(regKey, "Files", null);

        if ((strings != null) && (strings.Length >= 3))
        {
          patternDocTextBox.Text    = strings[0];
          data1TextFileTextBox.Text = strings[1];
          data2TextFileTextBox.Text = strings[2];
        }
      }
    }

    private void SetControlPanelState(bool enabled)
    {
      readButton.Enabled   = !enabled;
      controlPanel.Enabled = enabled;

      if (!enabled)
      { // free space
        patternFeeder = null;
      }
    }

    private bool DataRead()
    {
      try
      {
        string pattern = File.ReadAllText(patternDocTextBox.Text, Encoding.Default);

        patternFeeder = new PatternFeeder(pattern);
      }
      catch (Exception e)
      {
        messageLabel.Text = patternDocTextBox.Text + ": " + e.Message;
        return false;
      }


      try
      {
        string[] data1Text = File.ReadAllLines(data1TextFileTextBox.Text, Encoding.Default);

        patternFeeder.buildData1List(data1Text);
      }
      catch (Exception e)
      {
        messageLabel.Text = data1TextFileTextBox.Text + ": " + e.Message;
        return false;
      }


      if (! String.IsNullOrWhiteSpace(data2TextFileTextBox.Text))
      {
        try
        {
          string[] data2Text = File.ReadAllLines(data2TextFileTextBox.Text, Encoding.Default);

          patternFeeder.buildData2List(data2Text);
        }
        catch (Exception e)
        {
          messageLabel.Text = data2TextFileTextBox.Text + ": " + e.Message;
          return false;
        }
      }

      if (!String.IsNullOrWhiteSpace(data3TextFileTextBox.Text))
      {
        try
        {
          string[] data3Text = File.ReadAllLines(data3TextFileTextBox.Text, Encoding.Default);

          patternFeeder.buildData2List(data3Text);
        }
        catch (Exception e)
        {
          messageLabel.Text = data3TextFileTextBox.Text + ": " + e.Message;
          return false;
        }
      }

      //

      data1SelectNumericUpDown.Minimum = Math.Min(1, patternFeeder.data1List.Count);
      data1SelectNumericUpDown.Maximum = patternFeeder.data1List.Count;
      data1SelectNumericUpDown.Value   = data1SelectNumericUpDown.Minimum;


      if (patternFeeder.data2List == null)
      {
        data2SelectNumericUpDown.Visible = false;
        data2SelecLabel.Visible          = false;

        data2SelectNumericUpDown.Minimum = -1;
        data2SelectNumericUpDown.Maximum = -1;
        data2SelectNumericUpDown.Value   = -1;
      }
      else
      {
        data2SelectNumericUpDown.Visible = true;
        data2SelecLabel.Visible          = true;

        data2SelectNumericUpDown.Minimum = Math.Min(1, patternFeeder.data2List.Count);
        data2SelectNumericUpDown.Maximum = patternFeeder.data2List.Count;
        data2SelectNumericUpDown.Value = data2SelectNumericUpDown.Minimum;
      }


      if (patternFeeder.data3List == null)
      {
        data3SelectNumericUpDown.Visible = false;
        data3SelecLabel.Visible = false;

        data3SelectNumericUpDown.Minimum = -1;
        data3SelectNumericUpDown.Maximum = -1;
        data3SelectNumericUpDown.Value = -1;
      }
      else
      {
        data3SelectNumericUpDown.Visible = true;
        data3SelecLabel.Visible = true;

        data3SelectNumericUpDown.Minimum = Math.Min(1, patternFeeder.data2List.Count);
        data3SelectNumericUpDown.Maximum = patternFeeder.data2List.Count;
        data3SelectNumericUpDown.Value = data2SelectNumericUpDown.Minimum;
      }

      //

      return true;              
    }

    //

    private void patternDocPictureBox_Click(object sender, EventArgs e)
    {
      patternDocOpenFileDialog.FileName = patternDocTextBox.Text;

      if (patternDocOpenFileDialog.ShowDialog() == DialogResult.OK)
      {
        patternDocTextBox.Text = patternDocOpenFileDialog.FileName;
      }
    }

   
    private void data1TextFilePictureBox_Click(object sender, EventArgs e)
    {
      dataXOpenFileDialog.FileName = data1TextFileTextBox.Text;

      if (dataXOpenFileDialog.ShowDialog() == DialogResult.OK)
      {
        data1TextFileTextBox.Text = dataXOpenFileDialog.FileName;
      }
    }

    private void data2TextFilePictureBox_Click(object sender, EventArgs e)
    {
      dataXOpenFileDialog.FileName = data2TextFileTextBox.Text;

      if (dataXOpenFileDialog.ShowDialog() == DialogResult.OK)
      {
        data2TextFileTextBox.Text = dataXOpenFileDialog.FileName;
      }
    }

    private void data3TextFilePictureBox_Click(object sender, EventArgs e)
    {
      dataXOpenFileDialog.FileName = data3TextFileTextBox.Text;

      if (dataXOpenFileDialog.ShowDialog() == DialogResult.OK)
      {
        data3TextFileTextBox.Text = dataXOpenFileDialog.FileName;
      }
    } 


    private void patternDocTextBox_TextChanged(object sender, EventArgs e)
    {
      SetControlPanelState(false);
    }

    private void everyPageDataTextBox_TextChanged(object sender, EventArgs e)
    {
      SetControlPanelState(false);
    }

    private void eachPageDataTextBox_TextChanged(object sender, EventArgs e)
    {
      SetControlPanelState(false);
    }

    private void readButton_Click(object sender, EventArgs e)
    {
      SetControlPanelState(DataRead());
    }

    private void pageShowButton_Click(object sender, EventArgs e)
    {
      int ix1 = (int)data1SelectNumericUpDown.Value - 1; 
      int ix2 = (int)data2SelectNumericUpDown.Value - 1;
      int ix3 = (int)data3SelectNumericUpDown.Value - 1;

      string combined = patternFeeder.getCombinedPattern(ix1, ix2, ix3);

      if (patternFeeder.type == PatternFeeder.Type.Rtf)
      {
        var form = new ViewerForm(combined, "Generated page/document");

        form.ShowDialog();
      }
      else
      {
        MessageBox.Show("There isn't a viewer for this format of pattern!\n\n" + combined.Substring(0, 2048) + "...");
      }
    }

    private void printButton_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Start printing?", "Print documents.", MessageBoxButtons.YesNo) != DialogResult.Yes)
      {
        return;
      }

      //

      messageLabel.Text = "Print start...";
      this.Cursor = Cursors.WaitCursor;
      this.Refresh();

      Dictionary<string, string> combinedAll = patternFeeder.getCombinedPatternAll();

      List<string> filenames = new List<string>();

      foreach (var item in combinedAll)
      {
        string fileName = Path.GetTempPath() + @"\RtfViewerTemporaryFile_" + item.Key + ".rtf";

        File.WriteAllText(fileName, item.Value, Encoding.Default);

        filenames.Add(fileName);

        messageLabel.Text = "print: " + fileName;
        messageLabel.Refresh();

        Thread.Sleep(2000);

        var psi = new ProcessStartInfo(fileName);
        psi.Verb = "Print";

        Process.Start(psi);
      }

      foreach (var filename in filenames)
      {
        messageLabel.Text = "delete: " + filename;
        messageLabel.Refresh();

        File.Delete(filename);
      }

      messageLabel.Text = "Print OK";
      this.Cursor = Cursors.Default;
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Start saving?", "Save documents.", MessageBoxButtons.YesNo) != DialogResult.Yes)
      {
        return;
      }

      //
      if (saveAllFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        messageLabel.Text = "Save start...";
        this.Cursor = Cursors.WaitCursor;
        this.Refresh();

        string targetPath = saveAllFolderBrowserDialog.SelectedPath;

        Dictionary<string, string> combinedAll = patternFeeder.getCombinedPatternAll();

        foreach (var item in combinedAll)
        {
          string fileName = targetPath + @"\RtfViewerTemporaryFile_" + item.Key + ".rtf";

          File.WriteAllText(fileName, item.Value, Encoding.Default);

          messageLabel.Text = fileName;
          messageLabel.Refresh();
        }

        messageLabel.Text = "Save OK";
        this.Cursor = Cursors.Default;
      }
    }

    private void messageLabel_Click(object sender, EventArgs e)
    {
      MessageBox.Show(messageLabel.Text);
    }

    private void messageLabel_TextChanged(object sender, EventArgs e)
    {
      this.toolTip.SetToolTip(this.messageLabel, messageLabel.Text);
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {

    }

    

    

    //
  }
}
