/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
