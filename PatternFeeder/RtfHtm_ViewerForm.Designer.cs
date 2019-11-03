namespace PatternFeeder
{
  partial class ViewerForm
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
      System.Windows.Forms.Button exitButton;
      this.richTextBox = new System.Windows.Forms.RichTextBox();
      this.controlPanel = new System.Windows.Forms.Panel();
      this.saveButton = new System.Windows.Forms.Button();
      this.printButton = new System.Windows.Forms.Button();
      this.viewButton = new System.Windows.Forms.Button();
      this.webBrowser = new System.Windows.Forms.WebBrowser();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      exitButton = new System.Windows.Forms.Button();
      this.controlPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // exitButton
      // 
      exitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      exitButton.Dock = System.Windows.Forms.DockStyle.Right;
      exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      exitButton.ForeColor = System.Drawing.Color.Maroon;
      exitButton.Location = new System.Drawing.Point(803, 0);
      exitButton.Name = "exitButton";
      exitButton.Size = new System.Drawing.Size(96, 32);
      exitButton.TabIndex = 0;
      exitButton.Text = "&Exit";
      exitButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
      exitButton.UseVisualStyleBackColor = true;
      // 
      // richTextBox
      // 
      this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox.Cursor = System.Windows.Forms.Cursors.Default;
      this.richTextBox.Location = new System.Drawing.Point(12, 12);
      this.richTextBox.Margin = new System.Windows.Forms.Padding(4);
      this.richTextBox.Name = "richTextBox";
      this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
      this.richTextBox.Size = new System.Drawing.Size(570, 98);
      this.richTextBox.TabIndex = 0;
      this.richTextBox.Text = "";
      this.richTextBox.WordWrap = false;
      // 
      // controlPanel
      // 
      this.controlPanel.Controls.Add(this.saveButton);
      this.controlPanel.Controls.Add(this.printButton);
      this.controlPanel.Controls.Add(this.viewButton);
      this.controlPanel.Controls.Add(exitButton);
      this.controlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.controlPanel.Location = new System.Drawing.Point(0, 487);
      this.controlPanel.Name = "controlPanel";
      this.controlPanel.Size = new System.Drawing.Size(899, 32);
      this.controlPanel.TabIndex = 1;
      // 
      // saveButton
      // 
      this.saveButton.Dock = System.Windows.Forms.DockStyle.Right;
      this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.saveButton.ForeColor = System.Drawing.Color.DarkBlue;
      this.saveButton.Location = new System.Drawing.Point(515, 0);
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(96, 32);
      this.saveButton.TabIndex = 3;
      this.saveButton.Text = "&Save";
      this.saveButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
      this.saveButton.UseVisualStyleBackColor = true;
      this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
      // 
      // printButton
      // 
      this.printButton.Dock = System.Windows.Forms.DockStyle.Right;
      this.printButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.printButton.ForeColor = System.Drawing.Color.DarkBlue;
      this.printButton.Location = new System.Drawing.Point(611, 0);
      this.printButton.Name = "printButton";
      this.printButton.Size = new System.Drawing.Size(96, 32);
      this.printButton.TabIndex = 2;
      this.printButton.Text = "&Print";
      this.printButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
      this.printButton.UseVisualStyleBackColor = true;
      this.printButton.Click += new System.EventHandler(this.printButton_Click);
      // 
      // viewButton
      // 
      this.viewButton.Dock = System.Windows.Forms.DockStyle.Right;
      this.viewButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.viewButton.ForeColor = System.Drawing.Color.DarkBlue;
      this.viewButton.Location = new System.Drawing.Point(707, 0);
      this.viewButton.Name = "viewButton";
      this.viewButton.Size = new System.Drawing.Size(96, 32);
      this.viewButton.TabIndex = 1;
      this.viewButton.Text = "&View";
      this.viewButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
      this.viewButton.UseVisualStyleBackColor = true;
      this.viewButton.Click += new System.EventHandler(this.viewButton_Click);
      // 
      // webBrowser
      // 
      this.webBrowser.Location = new System.Drawing.Point(12, 122);
      this.webBrowser.Margin = new System.Windows.Forms.Padding(4);
      this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser.Name = "webBrowser";
      this.webBrowser.Size = new System.Drawing.Size(570, 85);
      this.webBrowser.TabIndex = 2;
      // 
      // ViewerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(899, 519);
      this.Controls.Add(this.webBrowser);
      this.Controls.Add(this.controlPanel);
      this.Controls.Add(this.richTextBox);
      this.Name = "ViewerForm";
      this.Text = "RTF or Html Viewer ";
      this.Load += new System.EventHandler(this.ViewerForm_Load);
      this.Resize += new System.EventHandler(this.ViewerForm_Resize);
      this.controlPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox richTextBox;
    private System.Windows.Forms.Panel controlPanel;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.Button printButton;
    private System.Windows.Forms.Button viewButton;
    private System.Windows.Forms.WebBrowser webBrowser;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
  }
}