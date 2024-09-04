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

namespace Tortoise912
{
	public partial class configform : Form
	{
		public configform()
		{
			InitializeComponent();
			try
			{
				if (Application.UserAppDataRegistry.GetValue("PROV_URL") != null)
				{
					provTXT.Text = Application.UserAppDataRegistry.GetValue("PROV_URL").ToString();
				}

				if (Application.UserAppDataRegistry.GetValue("PROV_GRP") != null)
				{
					provgrpTXT.Text = Application.UserAppDataRegistry.GetValue("PROV_GRP").ToString();
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
				Application.UserAppDataRegistry.SetValue("PROV_URL", provTXT.Text);
				Application.UserAppDataRegistry.SetValue("PROV_GRP", provgrpTXT.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.Close();
		}
	}
}
