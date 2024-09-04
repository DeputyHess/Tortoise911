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

namespace Tortoise912
{
	public partial class FirstLaunch : Form
	{
		public FirstLaunch()
		{
			InitializeComponent();
			System.Windows.Forms.Timer timetimer = new System.Windows.Forms.Timer();
			timetimer.Interval = (1000);
			timetimer.Tick += new EventHandler(timetimer_Tick);
			timetimer.Start();
			timetimer.Enabled = true;
		}

		private void timetimer_Tick(object sender, EventArgs e)
		{
			try
			{
				var ping2 = new System.Net.NetworkInformation.Ping();
				string pingurl = provurlBOX.Text.Replace($"https://", "");
				pingurl = pingurl.Replace($"/", "");
				var result2 = ping2.Send(pingurl);

				if (result2.Status == System.Net.NetworkInformation.IPStatus.Success)
				{
					colorbox.BackColor = Color.Green;
				}
				else
				{
					colorbox.BackColor = Color.Red;
				}
			}
			catch (Exception ex)
			{
				colorbox.BackColor = Color.Red;
			}
		}

		private void subBUT_Click(object sender, EventArgs e)
		{
			Application.UserAppDataRegistry.SetValue("PROV_GRP", provgrpBOX.Text);
			Application.UserAppDataRegistry.SetValue("PROV_URL", provurlBOX.Text);
			this.Close();
		}

		private void provurlBOX_Leave(object sender, EventArgs e)
		{
		}
	}
}
