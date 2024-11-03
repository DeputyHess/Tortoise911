/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

namespace Tortoise911
{
	public partial class configform : Form
	{
		public configform()
		{
			InitializeComponent();
			try
			{
				ConfigFileBullshit CFB = new ConfigFileBullshit();
				CFB.getconf();
				if (CFB.Provurl != null)
				{
					provTXT.Text = CFB.Provurl;
				}

				if (CFB.Provgrp != null)
				{
					provgrpTXT.Text = CFB.Provgrp;
				}
				if (CFB.AwnKey != null) { awnkeydrop.Text = CFB.AwnKey; }
				if(CFB.RelKey != null) { relkeydrop.Text = CFB.RelKey; }
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void saveBUT_Click(object sender, EventArgs e)
		{
			try
			{
				ConfigFileBullshit CFB = new ConfigFileBullshit();
				CFB.Provurl= provTXT.Text;
				CFB.Provgrp = provgrpTXT.Text;
				CFB.AwnKey=awnkeydrop.Text;
				CFB.RelKey=relkeydrop.Text;
				CFB.setconf();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.Close();
		}
	}
}
