namespace Tortoise912
{
	partial class NoteWindow
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
			richTextBox1 = new RichTextBox();
			notesaveBUT = new Button();
			SuspendLayout();
			// 
			// richTextBox1
			// 
			richTextBox1.Location = new Point(1, 1);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.Size = new Size(442, 277);
			richTextBox1.TabIndex = 0;
			richTextBox1.Text = "";
			// 
			// notesaveBUT
			// 
			notesaveBUT.Location = new Point(170, 281);
			notesaveBUT.Name = "notesaveBUT";
			notesaveBUT.Size = new Size(75, 23);
			notesaveBUT.TabIndex = 1;
			notesaveBUT.Text = "Save";
			notesaveBUT.UseVisualStyleBackColor = true;
			// 
			// NoteWindow
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(444, 307);
			Controls.Add(notesaveBUT);
			Controls.Add(richTextBox1);
			Name = "NoteWindow";
			Text = "NoteWindow";
			ResumeLayout(false);
		}

		#endregion

		private RichTextBox richTextBox1;
		private Button notesaveBUT;
	}
}