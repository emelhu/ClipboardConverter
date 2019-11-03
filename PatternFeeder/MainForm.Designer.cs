namespace PatternFeeder
{
  partial class mainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
      this.patternDocLabel = new System.Windows.Forms.Label();
      this.patternDocTextBox = new System.Windows.Forms.TextBox();
      this.patternDocOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.data1TextFileTextBox = new System.Windows.Forms.TextBox();
      this.data1TextFileLabel = new System.Windows.Forms.Label();
      this.data2TextFileTextBox = new System.Windows.Forms.TextBox();
      this.data2TextFileLabel = new System.Windows.Forms.Label();
      this.dataXOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.readButton = new System.Windows.Forms.Button();
      this.controlPanel = new System.Windows.Forms.Panel();
      this.data2SelecLabel = new System.Windows.Forms.Label();
      this.data1SelectLabel = new System.Windows.Forms.Label();
      this.data2SelectNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.saveButton = new System.Windows.Forms.Button();
      this.printButton = new System.Windows.Forms.Button();
      this.pageShowButton = new System.Windows.Forms.Button();
      this.data1SelectNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.messageLabel = new System.Windows.Forms.Label();
      this.saveAllFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.data3TextFileTextBox = new System.Windows.Forms.TextBox();
      this.exitButton = new System.Windows.Forms.Button();
      this.data2TextFilePictureBox = new System.Windows.Forms.PictureBox();
      this.data1TextFilePictureBox = new System.Windows.Forms.PictureBox();
      this.patternDocPictureBox = new System.Windows.Forms.PictureBox();
      this.data3TextFilePictureBox = new System.Windows.Forms.PictureBox();
      this.data3TextFileLabel = new System.Windows.Forms.Label();
      this.data3SelecLabel = new System.Windows.Forms.Label();
      this.data3SelectNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.controlPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.data2SelectNumericUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.data1SelectNumericUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.data2TextFilePictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.data1TextFilePictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.patternDocPictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.data3TextFilePictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.data3SelectNumericUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // patternDocLabel
      // 
      this.patternDocLabel.AutoSize = true;
      this.patternDocLabel.Location = new System.Drawing.Point(12, 9);
      this.patternDocLabel.Name = "patternDocLabel";
      this.patternDocLabel.Size = new System.Drawing.Size(94, 13);
      this.patternDocLabel.TabIndex = 0;
      this.patternDocLabel.Text = "Pattern document:";
      // 
      // patternDocTextBox
      // 
      this.patternDocTextBox.ForeColor = System.Drawing.Color.Navy;
      this.patternDocTextBox.Location = new System.Drawing.Point(126, 6);
      this.patternDocTextBox.Name = "patternDocTextBox";
      this.patternDocTextBox.Size = new System.Drawing.Size(383, 20);
      this.patternDocTextBox.TabIndex = 1;
      this.toolTip.SetToolTip(this.patternDocTextBox, "Enter name of file that contains pattern document (it will combine data)");
      this.patternDocTextBox.TextChanged += new System.EventHandler(this.patternDocTextBox_TextChanged);
      // 
      // patternDocOpenFileDialog
      // 
      this.patternDocOpenFileDialog.DefaultExt = "rtf";
      this.patternDocOpenFileDialog.FileName = "PatternDoc.rtf";
      this.patternDocOpenFileDialog.Filter = "Reach text files (*.rtf)|*.rtf|Htm files (*.htm)|*.htm|Html files (*.html)|*.html" +
    "";
      // 
      // data1TextFileTextBox
      // 
      this.data1TextFileTextBox.ForeColor = System.Drawing.Color.Navy;
      this.data1TextFileTextBox.Location = new System.Drawing.Point(126, 32);
      this.data1TextFileTextBox.Name = "data1TextFileTextBox";
      this.data1TextFileTextBox.Size = new System.Drawing.Size(383, 20);
      this.data1TextFileTextBox.TabIndex = 4;
      this.toolTip.SetToolTip(this.data1TextFileTextBox, "Enter name of file that contains data for combine with pattern.");
      this.data1TextFileTextBox.TextChanged += new System.EventHandler(this.everyPageDataTextBox_TextChanged);
      // 
      // data1TextFileLabel
      // 
      this.data1TextFileLabel.AutoSize = true;
      this.data1TextFileLabel.Location = new System.Drawing.Point(12, 35);
      this.data1TextFileLabel.Name = "data1TextFileLabel";
      this.data1TextFileLabel.Size = new System.Drawing.Size(81, 13);
      this.data1TextFileLabel.TabIndex = 3;
      this.data1TextFileLabel.Text = "Data 1. text file:";
      // 
      // data2TextFileTextBox
      // 
      this.data2TextFileTextBox.ForeColor = System.Drawing.Color.Navy;
      this.data2TextFileTextBox.Location = new System.Drawing.Point(126, 58);
      this.data2TextFileTextBox.Name = "data2TextFileTextBox";
      this.data2TextFileTextBox.Size = new System.Drawing.Size(383, 20);
      this.data2TextFileTextBox.TabIndex = 7;
      this.toolTip.SetToolTip(this.data2TextFileTextBox, "Enter name of file that contains data for combine with pattern.");
      this.data2TextFileTextBox.TextChanged += new System.EventHandler(this.eachPageDataTextBox_TextChanged);
      // 
      // data2TextFileLabel
      // 
      this.data2TextFileLabel.AutoSize = true;
      this.data2TextFileLabel.Location = new System.Drawing.Point(12, 61);
      this.data2TextFileLabel.Name = "data2TextFileLabel";
      this.data2TextFileLabel.Size = new System.Drawing.Size(81, 13);
      this.data2TextFileLabel.TabIndex = 6;
      this.data2TextFileLabel.Text = "Data 2. text file:";
      // 
      // dataXOpenFileDialog
      // 
      this.dataXOpenFileDialog.DefaultExt = "txt";
      this.dataXOpenFileDialog.FileName = "data1.txt";
      this.dataXOpenFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
      // 
      // readButton
      // 
      this.readButton.ForeColor = System.Drawing.Color.Maroon;
      this.readButton.Location = new System.Drawing.Point(354, 110);
      this.readButton.Name = "readButton";
      this.readButton.Size = new System.Drawing.Size(155, 33);
      this.readButton.TabIndex = 9;
      this.readButton.Text = "Read pattern and data";
      this.readButton.UseVisualStyleBackColor = true;
      this.readButton.Click += new System.EventHandler(this.readButton_Click);
      // 
      // controlPanel
      // 
      this.controlPanel.Controls.Add(this.data3SelecLabel);
      this.controlPanel.Controls.Add(this.data3SelectNumericUpDown);
      this.controlPanel.Controls.Add(this.data2SelecLabel);
      this.controlPanel.Controls.Add(this.data1SelectLabel);
      this.controlPanel.Controls.Add(this.data2SelectNumericUpDown);
      this.controlPanel.Controls.Add(this.saveButton);
      this.controlPanel.Controls.Add(this.printButton);
      this.controlPanel.Controls.Add(this.pageShowButton);
      this.controlPanel.Controls.Add(this.data1SelectNumericUpDown);
      this.controlPanel.Location = new System.Drawing.Point(20, 161);
      this.controlPanel.Name = "controlPanel";
      this.controlPanel.Size = new System.Drawing.Size(512, 128);
      this.controlPanel.TabIndex = 10;
      // 
      // data2SelecLabel
      // 
      this.data2SelecLabel.AutoSize = true;
      this.data2SelecLabel.Location = new System.Drawing.Point(21, 47);
      this.data2SelecLabel.Name = "data2SelecLabel";
      this.data2SelecLabel.Size = new System.Drawing.Size(66, 13);
      this.data2SelecLabel.TabIndex = 7;
      this.data2SelecLabel.Text = "Data 2. part:";
      // 
      // data1SelectLabel
      // 
      this.data1SelectLabel.AutoSize = true;
      this.data1SelectLabel.Location = new System.Drawing.Point(21, 15);
      this.data1SelectLabel.Name = "data1SelectLabel";
      this.data1SelectLabel.Size = new System.Drawing.Size(66, 13);
      this.data1SelectLabel.TabIndex = 5;
      this.data1SelectLabel.Text = "Data 1. part:";
      // 
      // data2SelectNumericUpDown
      // 
      this.data2SelectNumericUpDown.Location = new System.Drawing.Point(93, 45);
      this.data2SelectNumericUpDown.Name = "data2SelectNumericUpDown";
      this.data2SelectNumericUpDown.Size = new System.Drawing.Size(48, 20);
      this.data2SelectNumericUpDown.TabIndex = 4;
      // 
      // saveButton
      // 
      this.saveButton.Location = new System.Drawing.Point(334, 69);
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(155, 33);
      this.saveButton.TabIndex = 3;
      this.saveButton.Text = "Save all page/document";
      this.saveButton.UseVisualStyleBackColor = true;
      this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
      // 
      // printButton
      // 
      this.printButton.Location = new System.Drawing.Point(334, 28);
      this.printButton.Name = "printButton";
      this.printButton.Size = new System.Drawing.Size(155, 35);
      this.printButton.TabIndex = 2;
      this.printButton.Text = "Print all page/document";
      this.printButton.UseVisualStyleBackColor = true;
      this.printButton.Click += new System.EventHandler(this.printButton_Click);
      // 
      // pageShowButton
      // 
      this.pageShowButton.Location = new System.Drawing.Point(162, 6);
      this.pageShowButton.Name = "pageShowButton";
      this.pageShowButton.Size = new System.Drawing.Size(155, 30);
      this.pageShowButton.TabIndex = 1;
      this.pageShowButton.Text = "Show page/document";
      this.pageShowButton.UseVisualStyleBackColor = true;
      this.pageShowButton.Click += new System.EventHandler(this.pageShowButton_Click);
      // 
      // data1SelectNumericUpDown
      // 
      this.data1SelectNumericUpDown.Location = new System.Drawing.Point(93, 13);
      this.data1SelectNumericUpDown.Name = "data1SelectNumericUpDown";
      this.data1SelectNumericUpDown.Size = new System.Drawing.Size(48, 20);
      this.data1SelectNumericUpDown.TabIndex = 0;
      // 
      // messageLabel
      // 
      this.messageLabel.BackColor = System.Drawing.Color.Moccasin;
      this.messageLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.messageLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.messageLabel.ForeColor = System.Drawing.Color.Teal;
      this.messageLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.messageLabel.Location = new System.Drawing.Point(0, 344);
      this.messageLabel.Name = "messageLabel";
      this.messageLabel.Size = new System.Drawing.Size(558, 13);
      this.messageLabel.TabIndex = 11;
      this.messageLabel.Text = "Select pattern and data files";
      this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.messageLabel.TextChanged += new System.EventHandler(this.messageLabel_TextChanged);
      this.messageLabel.Click += new System.EventHandler(this.messageLabel_Click);
      // 
      // saveAllFolderBrowserDialog
      // 
      this.saveAllFolderBrowserDialog.Description = "Directory to save combined pages/documents.";
      this.saveAllFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
      // 
      // data3TextFileTextBox
      // 
      this.data3TextFileTextBox.ForeColor = System.Drawing.Color.Navy;
      this.data3TextFileTextBox.Location = new System.Drawing.Point(126, 84);
      this.data3TextFileTextBox.Name = "data3TextFileTextBox";
      this.data3TextFileTextBox.Size = new System.Drawing.Size(383, 20);
      this.data3TextFileTextBox.TabIndex = 14;
      this.toolTip.SetToolTip(this.data3TextFileTextBox, "Enter name of file that contains data for combine with pattern.");
      this.data3TextFileTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
      // 
      // exitButton
      // 
      this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.exitButton.ForeColor = System.Drawing.Color.Blue;
      this.exitButton.Image = ((System.Drawing.Image)(resources.GetObject("exitButton.Image")));
      this.exitButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.exitButton.Location = new System.Drawing.Point(424, 295);
      this.exitButton.Name = "exitButton";
      this.exitButton.Size = new System.Drawing.Size(108, 28);
      this.exitButton.TabIndex = 12;
      this.exitButton.Text = "&Exit";
      this.exitButton.UseVisualStyleBackColor = true;
      this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
      // 
      // data2TextFilePictureBox
      // 
      this.data2TextFilePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.data2TextFilePictureBox.Image = global::PatternFeeder.Properties.Resources._043;
      this.data2TextFilePictureBox.InitialImage = null;
      this.data2TextFilePictureBox.Location = new System.Drawing.Point(515, 61);
      this.data2TextFilePictureBox.Name = "data2TextFilePictureBox";
      this.data2TextFilePictureBox.Size = new System.Drawing.Size(20, 17);
      this.data2TextFilePictureBox.TabIndex = 8;
      this.data2TextFilePictureBox.TabStop = false;
      this.data2TextFilePictureBox.Click += new System.EventHandler(this.data2TextFilePictureBox_Click);
      // 
      // data1TextFilePictureBox
      // 
      this.data1TextFilePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.data1TextFilePictureBox.Image = global::PatternFeeder.Properties.Resources._043;
      this.data1TextFilePictureBox.InitialImage = null;
      this.data1TextFilePictureBox.Location = new System.Drawing.Point(515, 35);
      this.data1TextFilePictureBox.Name = "data1TextFilePictureBox";
      this.data1TextFilePictureBox.Size = new System.Drawing.Size(20, 17);
      this.data1TextFilePictureBox.TabIndex = 5;
      this.data1TextFilePictureBox.TabStop = false;
      this.data1TextFilePictureBox.Click += new System.EventHandler(this.data1TextFilePictureBox_Click);
      // 
      // patternDocPictureBox
      // 
      this.patternDocPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.patternDocPictureBox.Image = global::PatternFeeder.Properties.Resources._043;
      this.patternDocPictureBox.InitialImage = null;
      this.patternDocPictureBox.Location = new System.Drawing.Point(515, 9);
      this.patternDocPictureBox.Name = "patternDocPictureBox";
      this.patternDocPictureBox.Size = new System.Drawing.Size(20, 17);
      this.patternDocPictureBox.TabIndex = 2;
      this.patternDocPictureBox.TabStop = false;
      this.patternDocPictureBox.Click += new System.EventHandler(this.patternDocPictureBox_Click);
      // 
      // data3TextFilePictureBox
      // 
      this.data3TextFilePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.data3TextFilePictureBox.Image = global::PatternFeeder.Properties.Resources._043;
      this.data3TextFilePictureBox.InitialImage = null;
      this.data3TextFilePictureBox.Location = new System.Drawing.Point(515, 87);
      this.data3TextFilePictureBox.Name = "data3TextFilePictureBox";
      this.data3TextFilePictureBox.Size = new System.Drawing.Size(20, 17);
      this.data3TextFilePictureBox.TabIndex = 15;
      this.data3TextFilePictureBox.TabStop = false;
      this.data3TextFilePictureBox.Click += new System.EventHandler(this.data3TextFilePictureBox_Click);
      // 
      // data3TextFileLabel
      // 
      this.data3TextFileLabel.AutoSize = true;
      this.data3TextFileLabel.Location = new System.Drawing.Point(12, 87);
      this.data3TextFileLabel.Name = "data3TextFileLabel";
      this.data3TextFileLabel.Size = new System.Drawing.Size(81, 13);
      this.data3TextFileLabel.TabIndex = 13;
      this.data3TextFileLabel.Text = "Data 3. text file:";
      // 
      // data3SelecLabel
      // 
      this.data3SelecLabel.AutoSize = true;
      this.data3SelecLabel.Location = new System.Drawing.Point(20, 79);
      this.data3SelecLabel.Name = "data3SelecLabel";
      this.data3SelecLabel.Size = new System.Drawing.Size(66, 13);
      this.data3SelecLabel.TabIndex = 9;
      this.data3SelecLabel.Text = "Data 3. part:";
      this.data3SelecLabel.Click += new System.EventHandler(this.label1_Click);
      // 
      // data3SelectNumericUpDown
      // 
      this.data3SelectNumericUpDown.Location = new System.Drawing.Point(92, 77);
      this.data3SelectNumericUpDown.Name = "data3SelectNumericUpDown";
      this.data3SelectNumericUpDown.Size = new System.Drawing.Size(48, 20);
      this.data3SelectNumericUpDown.TabIndex = 8;
      this.data3SelectNumericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
      // 
      // mainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(558, 357);
      this.Controls.Add(this.data3TextFilePictureBox);
      this.Controls.Add(this.data3TextFileTextBox);
      this.Controls.Add(this.data3TextFileLabel);
      this.Controls.Add(this.exitButton);
      this.Controls.Add(this.messageLabel);
      this.Controls.Add(this.controlPanel);
      this.Controls.Add(this.readButton);
      this.Controls.Add(this.data2TextFilePictureBox);
      this.Controls.Add(this.data2TextFileTextBox);
      this.Controls.Add(this.data2TextFileLabel);
      this.Controls.Add(this.data1TextFilePictureBox);
      this.Controls.Add(this.data1TextFileTextBox);
      this.Controls.Add(this.data1TextFileLabel);
      this.Controls.Add(this.patternDocPictureBox);
      this.Controls.Add(this.patternDocTextBox);
      this.Controls.Add(this.patternDocLabel);
      this.Name = "mainForm";
      this.Text = "Feed the pattern!   (c) eMeL";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
      this.controlPanel.ResumeLayout(false);
      this.controlPanel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.data2SelectNumericUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.data1SelectNumericUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.data2TextFilePictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.data1TextFilePictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.patternDocPictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.data3TextFilePictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.data3SelectNumericUpDown)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label patternDocLabel;
    private System.Windows.Forms.TextBox patternDocTextBox;
    private System.Windows.Forms.OpenFileDialog patternDocOpenFileDialog;
    private System.Windows.Forms.PictureBox patternDocPictureBox;
    private System.Windows.Forms.PictureBox data1TextFilePictureBox;
    private System.Windows.Forms.TextBox data1TextFileTextBox;
    private System.Windows.Forms.Label data1TextFileLabel;
    private System.Windows.Forms.PictureBox data2TextFilePictureBox;
    private System.Windows.Forms.TextBox data2TextFileTextBox;
    private System.Windows.Forms.Label data2TextFileLabel;
    private System.Windows.Forms.OpenFileDialog dataXOpenFileDialog;
    private System.Windows.Forms.Button readButton;
    private System.Windows.Forms.Panel controlPanel;
    private System.Windows.Forms.Label messageLabel;
    private System.Windows.Forms.Button exitButton;
    private System.Windows.Forms.Button pageShowButton;
    private System.Windows.Forms.NumericUpDown data1SelectNumericUpDown;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.Button printButton;
    private System.Windows.Forms.FolderBrowserDialog saveAllFolderBrowserDialog;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.NumericUpDown data2SelectNumericUpDown;
    private System.Windows.Forms.Label data2SelecLabel;
    private System.Windows.Forms.Label data1SelectLabel;
    private System.Windows.Forms.PictureBox data3TextFilePictureBox;
    private System.Windows.Forms.TextBox data3TextFileTextBox;
    private System.Windows.Forms.Label data3TextFileLabel;
    private System.Windows.Forms.Label data3SelecLabel;
    private System.Windows.Forms.NumericUpDown data3SelectNumericUpDown;
  }
}

