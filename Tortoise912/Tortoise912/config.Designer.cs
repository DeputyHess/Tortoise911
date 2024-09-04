namespace Tortoise912
{
	partial class configform
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(configform));
			headsetdrop = new ComboBox();
			label1 = new Label();
			label2 = new Label();
			micdrop = new ComboBox();
			provTXT = new TextBox();
			provURLTXT = new Label();
			colour = new Label();
			colorLIST = new ComboBox();
			label3 = new Label();
			provgrpTXT = new TextBox();
			saveBUT = new Button();
			SuspendLayout();
			// 
			// headsetdrop
			// 
			headsetdrop.FormattingEnabled = true;
			headsetdrop.Location = new Point(62, 32);
			headsetdrop.Name = "headsetdrop";
			headsetdrop.Size = new Size(172, 23);
			headsetdrop.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(119, 14);
			label1.Name = "label1";
			label1.Size = new Size(50, 15);
			label1.TabIndex = 1;
			label1.Text = "Headset";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(135, 71);
			label2.Name = "label2";
			label2.Size = new Size(27, 15);
			label2.TabIndex = 3;
			label2.Text = "Mic";
			// 
			// micdrop
			// 
			micdrop.FormattingEnabled = true;
			micdrop.Location = new Point(62, 89);
			micdrop.Name = "micdrop";
			micdrop.Size = new Size(172, 23);
			micdrop.TabIndex = 2;
			// 
			// provTXT
			// 
			provTXT.Location = new Point(22, 155);
			provTXT.Name = "provTXT";
			provTXT.Size = new Size(248, 23);
			provTXT.TabIndex = 4;
			provTXT.TextAlign = HorizontalAlignment.Center;
			// 
			// provURLTXT
			// 
			provURLTXT.AutoSize = true;
			provURLTXT.Location = new Point(96, 137);
			provURLTXT.Name = "provURLTXT";
			provURLTXT.Size = new Size(97, 15);
			provURLTXT.TabIndex = 5;
			provURLTXT.Text = "Provisioning URL";
			// 
			// colour
			// 
			colour.AutoSize = true;
			colour.Location = new Point(119, 259);
			colour.Name = "colour";
			colour.Size = new Size(43, 15);
			colour.TabIndex = 7;
			colour.Text = "Colour";
			// 
			// colorLIST
			// 
			colorLIST.FormattingEnabled = true;
			colorLIST.Items.AddRange(new object[] { "Grey", "Black", "Blue", "Red", "Purple" });
			colorLIST.Location = new Point(53, 277);
			colorLIST.Name = "colorLIST";
			colorLIST.Size = new Size(172, 23);
			colorLIST.TabIndex = 6;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(96, 200);
			label3.Name = "label3";
			label3.Size = new Size(109, 15);
			label3.TabIndex = 9;
			label3.Text = "Provisioning Group";
			// 
			// provgrpTXT
			// 
			provgrpTXT.Location = new Point(119, 218);
			provgrpTXT.Name = "provgrpTXT";
			provgrpTXT.Size = new Size(54, 23);
			provgrpTXT.TabIndex = 8;
			provgrpTXT.TextAlign = HorizontalAlignment.Center;
			// 
			// saveBUT
			// 
			saveBUT.Location = new Point(108, 313);
			saveBUT.Name = "saveBUT";
			saveBUT.Size = new Size(75, 23);
			saveBUT.TabIndex = 10;
			saveBUT.Text = "Save";
			saveBUT.UseVisualStyleBackColor = true;
			saveBUT.Click += saveBUT_Click;
			// 
			// configform
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(282, 348);
			Controls.Add(saveBUT);
			Controls.Add(label3);
			Controls.Add(provgrpTXT);
			Controls.Add(colour);
			Controls.Add(colorLIST);
			Controls.Add(provURLTXT);
			Controls.Add(provTXT);
			Controls.Add(label2);
			Controls.Add(micdrop);
			Controls.Add(label1);
			Controls.Add(headsetdrop);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "configform";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Config";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox headsetdrop;
		private Label label1;
		private Label label2;
		private ComboBox micdrop;
		private TextBox provTXT;
		private Label provURLTXT;
		private Label colour;
		private ComboBox colorLIST;
		private Label label3;
		private TextBox provgrpTXT;
		private Button saveBUT;
	}
}