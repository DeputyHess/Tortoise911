namespace Tortoise912
{
	partial class FirstLaunch
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstLaunch));
			subBUT = new Button();
			label1 = new Label();
			provurlBOX = new TextBox();
			provgrpBOX = new TextBox();
			label2 = new Label();
			colorbox = new Panel();
			label3 = new Label();
			SuspendLayout();
			// 
			// subBUT
			// 
			subBUT.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			subBUT.Location = new Point(99, 167);
			subBUT.Name = "subBUT";
			subBUT.Size = new Size(179, 44);
			subBUT.TabIndex = 0;
			subBUT.Text = "Submit";
			subBUT.UseVisualStyleBackColor = true;
			subBUT.Click += subBUT_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 54);
			label1.Name = "label1";
			label1.Size = new Size(97, 15);
			label1.TabIndex = 1;
			label1.Text = "Provisioning URL";
			// 
			// provurlBOX
			// 
			provurlBOX.Location = new Point(115, 51);
			provurlBOX.Name = "provurlBOX";
			provurlBOX.Size = new Size(199, 23);
			provurlBOX.TabIndex = 2;
			provurlBOX.Leave += provurlBOX_Leave;
			// 
			// provgrpBOX
			// 
			provgrpBOX.Location = new Point(115, 131);
			provgrpBOX.Name = "provgrpBOX";
			provgrpBOX.Size = new Size(47, 23);
			provgrpBOX.TabIndex = 4;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(0, 135);
			label2.Name = "label2";
			label2.Size = new Size(109, 15);
			label2.TabIndex = 3;
			label2.Text = "Provisioning Group";
			// 
			// colorbox
			// 
			colorbox.BackColor = Color.Red;
			colorbox.Location = new Point(328, 52);
			colorbox.Name = "colorbox";
			colorbox.Size = new Size(24, 19);
			colorbox.TabIndex = 5;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(35, 77);
			label3.Name = "label3";
			label3.Size = new Size(289, 15);
			label3.TabIndex = 6;
			label3.Text = "URL *MUST* have https:// at the start and / at the end";
			// 
			// FirstLaunch
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.DarkGray;
			ClientSize = new Size(371, 223);
			Controls.Add(label3);
			Controls.Add(colorbox);
			Controls.Add(provgrpBOX);
			Controls.Add(label2);
			Controls.Add(provurlBOX);
			Controls.Add(label1);
			Controls.Add(subBUT);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FirstLaunch";
			StartPosition = FormStartPosition.CenterParent;
			Text = "FirstLaunch";
			TopMost = true;
			FormClosing += FirstLaunch_FormClosing;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button subBUT;
		private Label label1;
		private TextBox provurlBOX;
		private TextBox provgrpBOX;
		private Label label2;
		private Panel colorbox;
		private Label label3;
	}
}