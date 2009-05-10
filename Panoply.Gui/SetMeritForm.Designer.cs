namespace Panoply.Gui
{
    partial class SetMeritForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetMeritForm));
            this.label1 = new System.Windows.Forms.Label();
            this.filterNameLabel = new System.Windows.Forms.Label();
            this.meritSwCompressorRadioButton = new System.Windows.Forms.RadioButton();
            this.meritNormalPlus1RadioButton = new System.Windows.Forms.RadioButton();
            this.meritUnlikelyRadioButton = new System.Windows.Forms.RadioButton();
            this.meritNormalRadioButton = new System.Windows.Forms.RadioButton();
            this.meritDoNotUseRadioButton = new System.Windows.Forms.RadioButton();
            this.meritHwCompressorRadioButton = new System.Windows.Forms.RadioButton();
            this.meritPreferredRadioButton = new System.Windows.Forms.RadioButton();
            this.meritPreferredPlusOneRadioButton = new System.Windows.Forms.RadioButton();
            this.meritPreferredPlus255RadioButton = new System.Windows.Forms.RadioButton();
            this.meritCustomRadioButton = new System.Windows.Forms.RadioButton();
            this.customMeritTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(409, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // filterNameLabel
            // 
            this.filterNameLabel.AutoSize = true;
            this.filterNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterNameLabel.Location = new System.Drawing.Point(1, 73);
            this.filterNameLabel.Name = "filterNameLabel";
            this.filterNameLabel.Size = new System.Drawing.Size(83, 13);
            this.filterNameLabel.TabIndex = 1;
            this.filterNameLabel.Text = "<Filter name>";
            // 
            // meritSwCompressorRadioButton
            // 
            this.meritSwCompressorRadioButton.AutoSize = true;
            this.meritSwCompressorRadioButton.Location = new System.Drawing.Point(12, 95);
            this.meritSwCompressorRadioButton.Name = "meritSwCompressorRadioButton";
            this.meritSwCompressorRadioButton.Size = new System.Drawing.Size(184, 17);
            this.meritSwCompressorRadioButton.TabIndex = 2;
            this.meritSwCompressorRadioButton.TabStop = true;
            this.meritSwCompressorRadioButton.Text = "Merit Level \'Software Compressor\'";
            this.meritSwCompressorRadioButton.UseVisualStyleBackColor = true;
            // 
            // meritNormalPlus1RadioButton
            // 
            this.meritNormalPlus1RadioButton.AutoSize = true;
            this.meritNormalPlus1RadioButton.Location = new System.Drawing.Point(12, 210);
            this.meritNormalPlus1RadioButton.Name = "meritNormalPlus1RadioButton";
            this.meritNormalPlus1RadioButton.Size = new System.Drawing.Size(135, 17);
            this.meritNormalPlus1RadioButton.TabIndex = 3;
            this.meritNormalPlus1RadioButton.TabStop = true;
            this.meritNormalPlus1RadioButton.Text = "Merit Level \'Normal\' + 1";
            this.meritNormalPlus1RadioButton.UseVisualStyleBackColor = true;
            // 
            // meritUnlikelyRadioButton
            // 
            this.meritUnlikelyRadioButton.AutoSize = true;
            this.meritUnlikelyRadioButton.Location = new System.Drawing.Point(12, 164);
            this.meritUnlikelyRadioButton.Name = "meritUnlikelyRadioButton";
            this.meritUnlikelyRadioButton.Size = new System.Drawing.Size(121, 17);
            this.meritUnlikelyRadioButton.TabIndex = 4;
            this.meritUnlikelyRadioButton.TabStop = true;
            this.meritUnlikelyRadioButton.Text = "Merit Level \'Unlikely\'";
            this.meritUnlikelyRadioButton.UseVisualStyleBackColor = true;
            // 
            // meritNormalRadioButton
            // 
            this.meritNormalRadioButton.AutoSize = true;
            this.meritNormalRadioButton.Location = new System.Drawing.Point(12, 187);
            this.meritNormalRadioButton.Name = "meritNormalRadioButton";
            this.meritNormalRadioButton.Size = new System.Drawing.Size(117, 17);
            this.meritNormalRadioButton.TabIndex = 5;
            this.meritNormalRadioButton.TabStop = true;
            this.meritNormalRadioButton.Text = "Merit Level \'Normal\'";
            this.meritNormalRadioButton.UseVisualStyleBackColor = true;
            // 
            // meritDoNotUseRadioButton
            // 
            this.meritDoNotUseRadioButton.AutoSize = true;
            this.meritDoNotUseRadioButton.Location = new System.Drawing.Point(12, 141);
            this.meritDoNotUseRadioButton.Name = "meritDoNotUseRadioButton";
            this.meritDoNotUseRadioButton.Size = new System.Drawing.Size(140, 17);
            this.meritDoNotUseRadioButton.TabIndex = 6;
            this.meritDoNotUseRadioButton.TabStop = true;
            this.meritDoNotUseRadioButton.Text = "Merit Level \'Do Not Use\'";
            this.meritDoNotUseRadioButton.UseVisualStyleBackColor = true;
            // 
            // meritHwCompressorRadioButton
            // 
            this.meritHwCompressorRadioButton.AutoSize = true;
            this.meritHwCompressorRadioButton.Location = new System.Drawing.Point(12, 118);
            this.meritHwCompressorRadioButton.Name = "meritHwCompressorRadioButton";
            this.meritHwCompressorRadioButton.Size = new System.Drawing.Size(188, 17);
            this.meritHwCompressorRadioButton.TabIndex = 7;
            this.meritHwCompressorRadioButton.TabStop = true;
            this.meritHwCompressorRadioButton.Text = "Merit Level \'Hardware Compressor\'";
            this.meritHwCompressorRadioButton.UseVisualStyleBackColor = true;
            // 
            // meritPreferredRadioButton
            // 
            this.meritPreferredRadioButton.AutoSize = true;
            this.meritPreferredRadioButton.Location = new System.Drawing.Point(12, 233);
            this.meritPreferredRadioButton.Name = "meritPreferredRadioButton";
            this.meritPreferredRadioButton.Size = new System.Drawing.Size(127, 17);
            this.meritPreferredRadioButton.TabIndex = 8;
            this.meritPreferredRadioButton.TabStop = true;
            this.meritPreferredRadioButton.Text = "Merit Level \'Preferred\'";
            this.meritPreferredRadioButton.UseVisualStyleBackColor = true;
            // 
            // meritPreferredPlusOneRadioButton
            // 
            this.meritPreferredPlusOneRadioButton.AutoSize = true;
            this.meritPreferredPlusOneRadioButton.Location = new System.Drawing.Point(12, 256);
            this.meritPreferredPlusOneRadioButton.Name = "meritPreferredPlusOneRadioButton";
            this.meritPreferredPlusOneRadioButton.Size = new System.Drawing.Size(145, 17);
            this.meritPreferredPlusOneRadioButton.TabIndex = 9;
            this.meritPreferredPlusOneRadioButton.TabStop = true;
            this.meritPreferredPlusOneRadioButton.Text = "Merit Level \'Preferred\' + 1";
            this.meritPreferredPlusOneRadioButton.UseVisualStyleBackColor = true;
            // 
            // meritPreferredPlus255RadioButton
            // 
            this.meritPreferredPlus255RadioButton.AutoSize = true;
            this.meritPreferredPlus255RadioButton.Location = new System.Drawing.Point(12, 279);
            this.meritPreferredPlus255RadioButton.Name = "meritPreferredPlus255RadioButton";
            this.meritPreferredPlus255RadioButton.Size = new System.Drawing.Size(157, 17);
            this.meritPreferredPlus255RadioButton.TabIndex = 10;
            this.meritPreferredPlus255RadioButton.TabStop = true;
            this.meritPreferredPlus255RadioButton.Text = "Merit Level \'Preferred\' + 255";
            this.meritPreferredPlus255RadioButton.UseVisualStyleBackColor = true;
            // 
            // meritCustomRadioButton
            // 
            this.meritCustomRadioButton.AutoSize = true;
            this.meritCustomRadioButton.Location = new System.Drawing.Point(12, 302);
            this.meritCustomRadioButton.Name = "meritCustomRadioButton";
            this.meritCustomRadioButton.Size = new System.Drawing.Size(189, 17);
            this.meritCustomRadioButton.TabIndex = 11;
            this.meritCustomRadioButton.TabStop = true;
            this.meritCustomRadioButton.Text = "Custom Merit Value (Hexadecimal):";
            this.meritCustomRadioButton.UseVisualStyleBackColor = true;
            this.meritCustomRadioButton.CheckedChanged += new System.EventHandler(this.meritCustomRadioButton_CheckedChanged);
            // 
            // customMeritTextBox
            // 
            this.customMeritTextBox.Enabled = false;
            this.customMeritTextBox.Location = new System.Drawing.Point(208, 302);
            this.customMeritTextBox.Name = "customMeritTextBox";
            this.customMeritTextBox.Size = new System.Drawing.Size(100, 20);
            this.customMeritTextBox.TabIndex = 12;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(121, 338);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 13;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(217, 338);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SetMeritForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 364);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.customMeritTextBox);
            this.Controls.Add(this.meritCustomRadioButton);
            this.Controls.Add(this.meritPreferredPlus255RadioButton);
            this.Controls.Add(this.meritPreferredPlusOneRadioButton);
            this.Controls.Add(this.meritPreferredRadioButton);
            this.Controls.Add(this.meritHwCompressorRadioButton);
            this.Controls.Add(this.meritDoNotUseRadioButton);
            this.Controls.Add(this.meritNormalRadioButton);
            this.Controls.Add(this.meritUnlikelyRadioButton);
            this.Controls.Add(this.meritNormalPlus1RadioButton);
            this.Controls.Add(this.meritSwCompressorRadioButton);
            this.Controls.Add(this.filterNameLabel);
            this.Controls.Add(this.label1);
            this.Name = "SetMeritForm";
            this.Text = "Change Filter Merit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label filterNameLabel;
        private System.Windows.Forms.RadioButton meritSwCompressorRadioButton;
        private System.Windows.Forms.RadioButton meritNormalPlus1RadioButton;
        private System.Windows.Forms.RadioButton meritUnlikelyRadioButton;
        private System.Windows.Forms.RadioButton meritNormalRadioButton;
        private System.Windows.Forms.RadioButton meritDoNotUseRadioButton;
        private System.Windows.Forms.RadioButton meritHwCompressorRadioButton;
        private System.Windows.Forms.RadioButton meritPreferredRadioButton;
        private System.Windows.Forms.RadioButton meritPreferredPlusOneRadioButton;
        private System.Windows.Forms.RadioButton meritPreferredPlus255RadioButton;
        private System.Windows.Forms.RadioButton meritCustomRadioButton;
        private System.Windows.Forms.TextBox customMeritTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}