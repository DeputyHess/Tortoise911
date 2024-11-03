/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;

namespace Tortoise911
{
	/// <summary>
	/// Config Form Class
	/// </summary>
	public partial class configform : Form
	{

		/// <summary>
		/// Config Form
		/// </summary>
		public configform()
		{
			Dictionary<int,string> DEVICESIN = new Dictionary<int,string>();
			Dictionary<int, string> DEVICESOUT = new Dictionary<int, string>();
			InitializeComponent();
			try {
				var enumerator = new MMDeviceEnumerator();
				//cycle through all audio devices
				for (int i = 0; i < WaveIn.DeviceCount; i++)
				{
					string SSSTR = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active)[i].ToString();
					DEVICESIN.Add(i, SSSTR);
				}
				enumerator.Dispose();
			}
			catch(Exception ex) { MessageBox.Show(ex.Message); }
			

			try {
				var enumeratorr = new MMDeviceEnumerator();
				//cycle through all audio devices
				for (int i = 0; i < WaveOut.DeviceCount; i++)
				{
					string SSSTR = enumeratorr.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)[i].ToString();
					DEVICESOUT.Add(i, SSSTR);
				}
				enumeratorr.Dispose();
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
			

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
				if (CFB.Rngtne != null) { ringtoneDROP.Text = CFB.Rngtne; }

				micdrop.Items.Clear();
				foreach (string s in DEVICESIN.Values)
				{
					micdrop.Items.Add(s);
				}
				if (CFB.ain != null) { micdrop.SelectedIndex = CFB.ain; }

				headsetdrop.Items.Clear();
				foreach (string s in DEVICESOUT.Values) 
				{
					headsetdrop.Items.Add(s);
				}
				if (CFB.aout != null) { headsetdrop.SelectedIndex = CFB.aout + 1; }
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
				CFB.Rngtne=ringtoneDROP.Text;
				CFB.AwnKey=awnkeydrop.Text;
				CFB.RelKey=relkeydrop.Text;
				CFB.ain = int.Parse(micdrop.Text)-1;
				CFB.aout = int.Parse(headsetdrop.Text)-1;
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
