namespace Tortoise912
{
	partial class SplashSc
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashSc));
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			button1 = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Rockwell", 50F, FontStyle.Bold | FontStyle.Italic);
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(424, 84);
			label1.TabIndex = 0;
			label1.Text = "Tortoise 912";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Rockwell Condensed", 21F, FontStyle.Italic);
			label2.Location = new Point(420, 46);
			label2.Name = "label2";
			label2.Size = new Size(109, 33);
			label2.TabIndex = 1;
			label2.Text = "v0.69 rev5";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 22F, FontStyle.Bold | FontStyle.Underline);
			label3.ForeColor = Color.Red;
			label3.Location = new Point(7, 198);
			label3.Name = "label3";
			label3.Size = new Size(700, 41);
			label3.TabIndex = 2;
			label3.Text = "NOT TO BE USED FOR LIFE SAFTEY OPERATIONS";
			// 
			// button1
			// 
			button1.BackColor = Color.LightSlateGray;
			button1.Font = new Font("Arial", 17F);
			button1.ForeColor = SystemColors.ControlText;
			button1.Location = new Point(273, 262);
			button1.Name = "button1";
			button1.Size = new Size(140, 42);
			button1.TabIndex = 3;
			button1.Text = "OK";
			button1.UseVisualStyleBackColor = false;
			button1.Click += button1_Click;
			// 
			// SplashSc
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Silver;
			ClientSize = new Size(715, 316);
			Controls.Add(button1);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MdiChildrenMinimizedAnchorBottom = false;
			MinimizeBox = false;
			Name = "SplashSc";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "SplashSc";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Label label2;
		private Label label3;
		private Button button1;
	}
}