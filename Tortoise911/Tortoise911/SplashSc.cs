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
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tortoise911
{
	public partial class SplashSc : Form
	{
		/// <summary>
		/// Splash Screen Thing
		/// </summary>
		public SplashSc()
		{
			InitializeComponent();
			ConfigParse.parse();

			//Call Config Parser here
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OperatorLogin OL = new OperatorLogin();
			OL.Show();
			this.Hide();
		}
	}
}
