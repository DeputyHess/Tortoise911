namespace Tortoise911
{
	partial class OperatorLogin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperatorLogin));
			pictureBox1 = new PictureBox();
			label1 = new Label();
			label2 = new Label();
			usernameBOX = new TextBox();
			passwordBOX = new TextBox();
			label3 = new Label();
			label4 = new Label();
			button1 = new Button();
			button2 = new Button();
			uidlab = new Label();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.BackgroundImage = Properties.Resources.R__21_;
			pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
			pictureBox1.Location = new Point(48, 20);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(182, 122);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Comic Sans MS", 30F);
			label1.Location = new Point(18, 140);
			label1.Name = "label1";
			label1.Size = new Size(255, 56);
			label1.TabIndex = 1;
			label1.Text = "Tortoise 911";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.BackColor = Color.Transparent;
			label2.Font = new Font("Segoe UI", 20F);
			label2.Location = new Point(147, 63);
			label2.Name = "label2";
			label2.Size = new Size(32, 37);
			label2.TabIndex = 2;
			label2.Text = "2";
			// 
			// usernameBOX
			// 
			usernameBOX.Location = new Point(41, 241);
			usernameBOX.Name = "usernameBOX";
			usernameBOX.Size = new Size(189, 23);
			usernameBOX.TabIndex = 3;
			// 
			// passwordBOX
			// 
			passwordBOX.Location = new Point(41, 310);
			passwordBOX.Name = "passwordBOX";
			passwordBOX.PasswordChar = '*';
			passwordBOX.Size = new Size(189, 23);
			passwordBOX.TabIndex = 4;
			passwordBOX.UseSystemPasswordChar = true;
			passwordBOX.KeyDown += passwordBOX_KeyDown;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 15F);
			label3.Location = new Point(95, 279);
			label3.Name = "label3";
			label3.Size = new Size(93, 28);
			label3.TabIndex = 5;
			label3.Text = "Password";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 15F);
			label4.Location = new Point(95, 210);
			label4.Name = "label4";
			label4.Size = new Size(99, 28);
			label4.TabIndex = 6;
			label4.Text = "Username";
			// 
			// button1
			// 
			button1.Location = new Point(41, 356);
			button1.Name = "button1";
			button1.Size = new Size(189, 55);
			button1.TabIndex = 7;
			button1.Text = "Login";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.BackColor = Color.Red;
			button2.Location = new Point(41, 435);
			button2.Name = "button2";
			button2.Size = new Size(189, 33);
			button2.TabIndex = 8;
			button2.Text = "Exit";
			button2.UseVisualStyleBackColor = false;
			button2.Click += button2_Click;
			// 
			// uidlab
			// 
			uidlab.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			uidlab.AutoSize = true;
			uidlab.Location = new Point(-1, 483);
			uidlab.Name = "uidlab";
			uidlab.Size = new Size(27, 15);
			uidlab.TabIndex = 9;
			uidlab.Text = "ERR";
			// 
			// OperatorLogin
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(294, 498);
			Controls.Add(uidlab);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(passwordBOX);
			Controls.Add(usernameBOX);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(pictureBox1);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "OperatorLogin";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Operator Login";
			Shown += OperatorLogin_Shown;
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictureBox pictureBox1;
		private Label label1;
		private Label label2;
		private TextBox usernameBOX;
		private TextBox passwordBOX;
		private Label label3;
		private Label label4;
		private Button button1;
		private Button button2;
		private Label uidlab;
	}
}