namespace Tortoise912
{
	partial class About
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Comic Sans MS", 30F, FontStyle.Bold | FontStyle.Italic);
			label1.Location = new Point(5, 14);
			label1.Name = "label1";
			label1.Size = new Size(273, 56);
			label1.TabIndex = 0;
			label1.Text = "Tortoise 912";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Comic Sans MS", 15F, FontStyle.Bold | FontStyle.Italic);
			label2.Location = new Point(54, 81);
			label2.Name = "label2";
			label2.Size = new Size(173, 29);
			label2.TabIndex = 1;
			label2.Text = "For Support Call";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Comic Sans MS", 15F, FontStyle.Bold | FontStyle.Italic);
			label3.Location = new Point(49, 110);
			label3.Name = "label3";
			label3.Size = new Size(181, 29);
			label3.TabIndex = 2;
			label3.Text = "1-800-438-3825";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Comic Sans MS", 10F, FontStyle.Bold | FontStyle.Italic);
			label4.Location = new Point(22, 191);
			label4.Name = "label4";
			label4.Size = new Size(248, 20);
			label4.TabIndex = 3;
			label4.Text = "Made by Nyx while Sleep Deprived";
			// 
			// About
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.IndianRed;
			ClientSize = new Size(287, 212);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "About";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "About";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
	}
}