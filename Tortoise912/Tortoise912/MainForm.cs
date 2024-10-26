/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Logging;
using SIPSorcery.SIP;
using SIPSorcery.SIP.App;
using SIPSorcery.SoftPhone;
using SIPSorcery.Sys;
using SIPSorceryMedia.Abstractions;

namespace Tortoise912
{
	/// <summary>
	/// Class for the Main Window
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Is the Screen Being cleaned?
		/// </summary>
		public bool CLEANINGSCREEN = false;
		private int cleancount = 20;
		private int SIP_CLIENT_COUNT = CONFstor.LINE_LIM;
		private const int ZINDEX_TOP = 10;
		private const int REGISTRATION_EXPIRY = 180;
		private string m_sipUsername = CONFstor.SUS;
		private string m_sipPassword = CONFstor.SPW;
		private string m_sipServer = CONFstor.SIP_CONTROLLER_LIST;
		private SIPTransportManager _sipTransportManager;
		private List<SIPClient> _sipClients;
		private SoftphoneSTUNClient _stunClient;                    // STUN client to periodically check the public IP address.
		private SIPRegistrationUserAgent _sipRegistrationClient;
		internal bool L1Act = false;
		internal bool L2Act = false;
		internal bool L3Act = false;
		internal bool L4Act = false;
		internal bool L5Act = false;
		internal bool L6Act = false;
		internal bool L1FT = false;
		internal bool L2FT = false;
		internal bool L3FT = false;
		internal bool L4FT = false;
		internal bool L5FT = false;
		internal bool L6FT = false;
		internal bool L1H = false;
		internal bool L2H = false;
		internal bool L3H = false;
		internal bool L4H = false;
		internal bool L5H = false;
		internal bool L6H = false;
		internal CallsRegStor.L1Call CRS1 = new CallsRegStor.L1Call();
		internal CallsRegStor.L2Call CRS2 = new CallsRegStor.L2Call();
		internal CallsRegStor.L3Call CRS3 = new CallsRegStor.L3Call();
		internal CallsRegStor.L4Call CRS4 = new CallsRegStor.L4Call();
		internal CallsRegStor.L5Call CRS5 = new CallsRegStor.L5Call();
		internal CallsRegStor.L6Call CRS6 = new CallsRegStor.L6Call();
		internal int actline = 99;

		/// <summary>
		/// CleanTimer
		/// </summary>
		public System.Windows.Forms.Timer cleantimer = new System.Windows.Forms.Timer();

		/// <summary>
		/// The Main Window
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			_sipTransportManager = new SIPTransportManager();
			_sipTransportManager.IncomingCall += SIPCallIncoming;

			_sipClients = new List<SIPClient>();
			if (CONFstor.LOGUSR != null)
			{
				usersnamelab.Text = CONFstor.LOGUSR;
			}
			RefreshButtons();

			if (varstore.tauth == true && Settings.Default.Dbug == false)
			{
				this.Text = $"Tortoise 912 *TEMPORARY LOGIN* User:" + usersnamelab.Text;
			}
			else if (varstore.tauth == true && Settings.Default.Dbug == true)
			{
				this.Text = $"Tortoise 912 *DEBUG TEMPORARY LOGIN* User:" + usersnamelab.Text;
			}
			else if (varstore.tauth == false && Settings.Default.Dbug == false)
			{
				this.Text = $"Tortoise 912 User:" + usersnamelab.Text;
			}

			System.Windows.Forms.Timer timetimer = new System.Windows.Forms.Timer();
			timetimer.Interval = (1000);
			timetimer.Tick += new EventHandler(timetimer_Tick);
			timetimer.Start();
			timetimer.Enabled = true;

			System.Windows.Forms.Timer conftimer = new System.Windows.Forms.Timer();
			try
			{
				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("POLLING_P") != null)
				{
					conftimer.Interval = (int.Parse(System.Windows.Forms.Application.UserAppDataRegistry.GetValue("POLLING_P").ToString()) * 60 * 1000);
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			conftimer.Tick += new EventHandler(conftimer_Tick);
			conftimer.Start();
			conftimer.Enabled = true;
			if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("CONFSTATUS") != null)
			{
				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("CONFSTATUS").ToString() == "Unprovisioned")
				{
					statusTXT.BackColor = Color.Red; statusTXT.ForeColor = Color.Black;
				}
				else if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("CONFSTATUS").ToString() == "SIP IP Softphone")
				{
					statusTXT.BackColor = Color.Silver; statusTXT.ForeColor = Color.Black;
				}
			}
			else
			{
				System.Windows.Forms.Application.UserAppDataRegistry.SetValue("CONFSTATUS", "Unprovisioned");
				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("CONFSTATUS").ToString() == "Unprovisioned")
				{
					statusTXT.BackColor = Color.Red; statusTXT.ForeColor = Color.Black;
				}
				else if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("CONFSTATUS").ToString() == "SIP IP Softphone")
				{
					statusTXT.BackColor = Color.Silver; statusTXT.ForeColor = Color.Black;
				}
			}
			if (!CONFstor.STUNServerHostname.IsNullOrBlank())
			{
				_stunClient = new SoftphoneSTUNClient(CONFstor.STUNServerHostname);
				_stunClient.PublicIPAddressDetected += (ip) =>
				{
					CONFstor.PublicIPAddress = ip;
				};
				_stunClient.Run();
			}



			//other shit
		}

		/// <summary>
		/// Refreshes the buttons with text from the Cfg
		/// </summary>
		private void RefreshButtons()
		{
			//Logout Button
			if (CONFstor.PROVIDE_LOGOUT == true)
			{
				logoutbutton.Visible = true;
			}
			else
			{
				logoutbutton.Visible = false;
			}

			//L1
			try
			{
				if (CONFstor.ENABLE_L1 == false)
				{
					Line1BUT.Visible = false;
				}
				else
				{
					Line1BUT.Visible = true;
				}

				if (CONFstor.LINE_1_TXT != null)
				{
					Line1BUT.Text = CONFstor.LINE_1_TXT.ToString();
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//L2
			try
			{
				if (CONFstor.ENABLE_L2 == false)
				{
					Line2BUT.Visible = false;
				}
				else
				{
					Line2BUT.Visible = true;
				}
				if (CONFstor.LINE_2_TXT != null)
				{
					Line2BUT.Text = CONFstor.LINE_2_TXT;
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//L3
			try
			{
				if (CONFstor.ENABLE_L3 == false)
				{
					Line3BUT.Visible = false;
				}
				else
				{
					Line3BUT.Visible = true;
				}

				if (CONFstor.LINE_3_TXT != null)
				{
					Line3BUT.Text = CONFstor.LINE_3_TXT;
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//L4
			try
			{
				if (CONFstor.ENABLE_L4 == false)
				{
					Line4BUT.Visible = false;
				}
				else
				{
					Line4BUT.Visible = true;
				}

				if (CONFstor.LINE_4_TXT != null)
				{
					Line4BUT.Text = CONFstor.LINE_4_TXT;
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//L5
			try
			{
				if (CONFstor.ENABLE_L5 == false)
				{
					Line5BUT.Visible = false;
				}
				else
				{
					Line5BUT.Visible = true;
				}

				if (CONFstor.LINE_5_TXT != null)
				{
					Line5BUT.Text = CONFstor.LINE_5_TXT;
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//L6
			try
			{
				if (CONFstor.ENABLE_L6 == false)
				{
					Line6BUT.Visible = false;
				}
				else
				{
					Line6BUT.Visible = true;
				}

				if (CONFstor.LINE_6_TXT != null)
				{
					Line6BUT.Text = CONFstor.LINE_6_TXT;
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//XFER1
			try
			{
				if (CONFstor.ENABLE_XF1 != null)
				{
					if (CONFstor.ENABLE_XF1 == false)
					{
						xfer1BUT.Visible = false;
					}
					else
					{
						xfer1BUT.Visible = true;
					}
				}

				if (CONFstor.XF1_TXT != null)
				{
					xfer1BUT.Text = CONFstor.XF1_TXT;
				}

				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF1_CLR") != null)
				{
					string clr = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF1_CLR").ToString();
					switch (clr.ToLower())
					{
						case "green":
							xfer1BUT.BackColor = Color.Green;
							break;

						case "red":
							xfer1BUT.BackColor = Color.Red;
							break;

						case "blue":
							xfer1BUT.BackColor = Color.Blue;
							break;

						case "pink":
							xfer1BUT.BackColor = Color.Pink;
							break;

						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//XFER2
			try
			{
				if (CONFstor.ENABLE_XF2 != null)
				{
					if (CONFstor.ENABLE_XF2 == false)
					{
						xfer2BUT.Visible = false;
					}
					else
					{
						xfer2BUT.Visible = true;
					}
				}

				if (CONFstor.XF2_TXT != null)
				{
					xfer2BUT.Text = CONFstor.XF2_TXT;
				}
				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF2_CLR") != null)
				{
					string clr = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF2_CLR").ToString();
					switch (clr.ToLower())
					{
						case "green":
							xfer2BUT.BackColor = Color.Green;
							break;

						case "red":
							xfer2BUT.BackColor = Color.Red;
							break;

						case "blue":
							xfer2BUT.BackColor = Color.Blue;
							break;

						case "pink":
							xfer2BUT.BackColor = Color.Pink;
							break;

						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//XFER3
			try
			{
				if (CONFstor.ENABLE_XF3 == false)
				{
					xfer3BUT.Visible = false;
				}
				else
				{
					xfer3BUT.Visible = true;
				}

				if (CONFstor.XF3_TXT != null)
				{
					xfer3BUT.Text = CONFstor.XF3_TXT;
				}
				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF3_CLR") != null)
				{
					string clr = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF3_CLR").ToString();
					switch (clr.ToLower())
					{
						case "green":
							xfer3BUT.BackColor = Color.Green;
							break;

						case "red":
							xfer3BUT.BackColor = Color.Red;
							break;

						case "blue":
							xfer3BUT.BackColor = Color.Blue;
							break;

						case "pink":
							xfer3BUT.BackColor = Color.Pink;
							break;

						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//XFER4
			try
			{
				if (CONFstor.ENABLE_XF4 == false)
				{
					xfer4BUT.Visible = false;
				}
				else
				{
					xfer4BUT.Visible = true;
				}

				if (CONFstor.XF4_TXT != null)
				{
					xfer4BUT.Text = CONFstor.XF4_TXT;
				}
				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF4_CLR") != null)
				{
					string clr = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF4_CLR").ToString();
					switch (clr.ToLower())
					{
						case "green":
							xfer4BUT.BackColor = Color.Green;
							break;

						case "red":
							xfer4BUT.BackColor = Color.Red;
							break;

						case "blue":
							xfer4BUT.BackColor = Color.Blue;
							break;

						case "pink":
							xfer4BUT.BackColor = Color.Pink;
							break;

						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//XFER5
			try
			{
				if (CONFstor.ENABLE_XF5 == false)
				{
					xfer5BUT.Visible = false;
				}
				else
				{
					xfer5BUT.Visible = true;
				}

				if (CONFstor.XF5_TXT != null)
				{
					xfer5BUT.Text = CONFstor.XF5_TXT;
				}
				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF5_CLR") != null)
				{
					string clr = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF5_CLR").ToString();
					switch (clr.ToLower())
					{
						case "green":
							xfer5BUT.BackColor = Color.Green;
							break;

						case "red":
							xfer5BUT.BackColor = Color.Red;
							break;

						case "blue":
							xfer5BUT.BackColor = Color.Blue;
							break;

						case "pink":
							xfer5BUT.BackColor = Color.Pink;
							break;

						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//XFER6
			try
			{
				if (CONFstor.ENABLE_XF6 == false)
				{
					xfer6BUT.Visible = false;
				}
				else
				{
					xfer6BUT.Visible = true;
				}

				if (CONFstor.XF6_TXT != null)
				{
					xfer6BUT.Text = CONFstor.XF6_TXT;
				}
				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF6_CLR") != null)
				{
					string clr = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF6_CLR").ToString();
					switch (clr.ToLower())
					{
						case "green":
							xfer6BUT.BackColor = Color.Green;
							break;

						case "red":
							xfer6BUT.BackColor = Color.Red;
							break;

						case "blue":
							xfer6BUT.BackColor = Color.Blue;
							break;

						case "pink":
							xfer6BUT.BackColor = Color.Pink;
							break;

						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//XFER7
			try
			{
				if (CONFstor.ENABLE_XF7 == false)
				{
					xfer7BUT.Visible = false;
				}
				else
				{
					xfer7BUT.Visible = true;
				}

				if (CONFstor.XF7_TXT != null)
				{
					xfer7BUT.Text = CONFstor.XF7_TXT;
				}
				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF7_CLR") != null)
				{
					string clr = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF7_CLR").ToString();
					switch (clr.ToLower())
					{
						case "green":
							xfer7BUT.BackColor = Color.Green;
							break;

						case "red":
							xfer7BUT.BackColor = Color.Red;
							break;

						case "blue":
							xfer7BUT.BackColor = Color.Blue;
							break;

						case "pink":
							xfer7BUT.BackColor = Color.Pink;
							break;

						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//XFER8
			try
			{
				if (CONFstor.ENABLE_XF8 == false)
				{
					xfer8BUT.Visible = false;
				}
				else
				{
					xfer8BUT.Visible = true;
				}

				if (CONFstor.XF8_TXT != null)
				{
					xfer8BUT.Text = CONFstor.XF8_TXT;
				}

				if (System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF8_CLR") != null)
				{
					string clr = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("XF8_CLR").ToString();
					switch (clr.ToLower())
					{
						case "green":
							xfer8BUT.BackColor = Color.Green;
							break;

						case "red":
							xfer8BUT.BackColor = Color.Red;
							break;

						case "blue":
							xfer8BUT.BackColor = Color.Blue;
							break;

						case "pink":
							xfer8BUT.BackColor = Color.Pink;
							break;

						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			//Status box
			try
			{
				if (CONFstor.CONFSTATUS != null)
				{
					statusTXT.Text = CONFstor.CONFSTATUS;
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Initialises the SIP clients and transport.
		/// </summary>
		private async Task Initialize()
		{
			await _sipTransportManager.InitialiseSIP();

			for (int i = 0; i < SIP_CLIENT_COUNT; i++)
			{
				var sipClient = new SIPClient(_sipTransportManager.SIPTransport);

				sipClient.CallAnswer += SIPCallAnswered;
				sipClient.CallEnded += ResetToCallStartState;

				_sipClients.Add(sipClient);
			}

			string listeningEndPoints = null;

			foreach (var sipChannel in _sipTransportManager.SIPTransport.GetSIPChannels())
			{
				SIPEndPoint sipChannelEP = sipChannel.ListeningSIPEndPoint.CopyOf();
				sipChannelEP.ChannelID = null;
				listeningEndPoints += (listeningEndPoints == null) ? sipChannelEP.ToString() : $", {sipChannelEP}";
			}

			_sipRegistrationClient = new SIPRegistrationUserAgent(
				_sipTransportManager.SIPTransport,
				m_sipUsername,
				m_sipPassword,
				m_sipServer,
				REGISTRATION_EXPIRY);

			_sipRegistrationClient.Start();
		}

		/// <summary>
		/// Clock Update timer, Ever 1 S
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timetimer_Tick(object sender, EventArgs e)
		{
			timeLAB.Text = DateTime.Now.ToString("HH:MM:ss");
			dateLAB.Text = DateTime.Now.ToString("MM/dd/yyyy");
			if (L1FT == true)
			{
				if (Line1BUT.BackColor == Color.Red) { Line1BUT.BackColor = Color.Blue; }
				else { Line1BUT.BackColor = Color.Red; }
			}
			else if (L2FT == true)
			{
				if (Line2BUT.BackColor == Color.Red) { Line2BUT.BackColor = Color.Blue; }
				else { Line2BUT.BackColor = Color.Red; }
			}
			else if (L3FT == true)
			{
				if (Line3BUT.BackColor == Color.Red) { Line3BUT.BackColor = Color.Blue; }
				else { Line3BUT.BackColor = Color.Red; }
			}
			else if (L4FT == true)
			{
				if (Line4BUT.BackColor == Color.Red) { Line4BUT.BackColor = Color.Blue; }
				else { Line4BUT.BackColor = Color.Red; }
			}
			else if (L5FT == true)
			{
				if (Line5BUT.BackColor == Color.Red) { Line5BUT.BackColor = Color.Blue; }
				else { Line5BUT.BackColor = Color.Red; }
			}
			else if (L6FT == true)
			{
				if (Line6BUT.BackColor == Color.Red) { Line6BUT.BackColor = Color.Blue; }
				else { Line6BUT.BackColor = Color.Red; }
			}
		}

		/// <summary>
		/// Config Refresh Timer Tick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void conftimer_Tick(object sender, EventArgs e)
		{
			try
			{
				ConfigParse.parse();
				Invoke(() =>
				{
					RefreshButtons();
				});
			}
			catch (Exception ex) { }

		}

		/// <summary>
		/// Quit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void quitbutton_Click(object sender, EventArgs e)
		{
			if (CLEANINGSCREEN == false)
			{
				System.Windows.MessageBox.Show("Exiting");
				ExitTask.Exit();
			}
		}

		/// <summary>
		/// Notes Button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button19_Click(object sender, EventArgs e)
		{
			if (CLEANINGSCREEN == false) { }

			//Open Notes Panel
		}

		/// <summary>
		/// Config/Settings button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void configBUT_Click(object sender, EventArgs e)
		{
			if (CLEANINGSCREEN == false)
			{
				configform CF = new configform();
				CF.ShowDialog();
			}
		}

		/// <summary>
		/// About Button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void aboutBUT_Click(object sender, EventArgs e)
		{
			if (CLEANINGSCREEN == false)
			{
				About AB = new About();
				AB.ShowDialog();
			}
		}

		/// <summary>
		/// Logout Button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button10_Click(object sender, EventArgs e)
		{
			if (CLEANINGSCREEN == false)
			{
				if (CONFstor.PROVIDE_LOGOUT == true)
				{
					var confirmResult = System.Windows.Forms.MessageBox.Show("Are you sure you want to logout", "Confirm Logout!!", MessageBoxButtons.YesNo);
					if (confirmResult == DialogResult.Yes)
					{
						OperatorLogin OL = new OperatorLogin();
						OL.Show();
						this.Hide();
					}
					else
					{
					}
				}
			}
		}

		/// <summary>
		/// Disable All Buttons
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void screenclean1BUT_Click(object sender, EventArgs e)
		{
			CLEANINGSCREEN = true;

			cleantimer.Interval = (1000);
			cleantimer.Tick += new EventHandler(cleantimer_Tick);
			cleantimer.Start();
			cleantimer.Enabled = true;
		}

		/// <summary>
		/// ScreenCleanTimer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cleantimer_Tick(object sender, EventArgs e)
		{
			if (cleancount > 0)
			{
				cleanBANNER.Visible = true;
				cleancount--;
				cleanBANNER.Text = "SCREEN CLEAN ACTIVE " + cleancount + " SECONDS REMAINING";
			}
			else
			{
				cleantimer.Enabled = false;
				cleantimer.Stop();

				cleanBANNER.Visible = false;
				cleancount = 20;
				CLEANINGSCREEN = false;
			}

			//Make a Flag that all buttons check
		}

		/// <summary>
		/// Window has loaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void MainForm_Load(object sender, EventArgs e)
		{
			await Initialize();
		}

		/// <summary>
		/// Window Is Closing Task
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (var sipClient in _sipClients)
			{
				sipClient.Shutdown();
			}

			_sipTransportManager.Shutdown();
			_stunClient?.Stop();
		}

		/// <summary>
		/// Reset the UI elements to their initial state at the end of a call.
		/// </summary>
		private void ResetToCallStartState(SIPClient sipClient)
		{
			extNUMTXT.Text = "";
			arrCdeTXT.Text = "";
			provideridTXT.Text = "";
			uriTXT.Text = "";
			phNUMTXT.Text = "";
			addrbox.Text = "";
			statusTXT.Text = "";
			methodTXT.Text = "";
			mobilityTXT.Text = "";
			callerbox.Text = "";
			XText.Text = "";
			yTXT.Text = "";
			zTXT.Text = "";

		}

		/// <summary>
		/// Update Text Box Text
		/// </summary>
		/// <param name="box"></param>
		/// <param name="text"></param>
		public void UpdateTextBox(TextBox box,string text)
		{
			Invoke((MethodInvoker)delegate {
				box.Text = text;
			});
		}

		/// <summary>
		/// UpDate Rich Text Box
		/// </summary>
		/// <param name="box"></param>
		/// <param name="text"></param>
		public void UpdateTextBox(RichTextBox box, string text)
		{
			Invoke((MethodInvoker)delegate {
				box.Text = text;
			});
		}
		/// <summary>
		/// update back color
		/// </summary>
		/// <param name="box"></param>
		/// <param name="Color"></param>
		public void UpdateTextBox(TextBox box, Color Color)
		{
			Invoke((MethodInvoker)delegate {
				box.BackColor = Color;
			});
		}

		/// <summary>
		/// Checks if there is a client that can accept the call and if so sets up the UI
		/// to present the handling options to the user.
		/// </summary>
		private bool SIPCallIncoming(SIPRequest sipRequest)
		{
			Invoke((MethodInvoker)delegate
			{
				UpdateTextBox(callerbox, sipRequest.Header.From.FriendlyDescription());
			});

			string tmptpn = sipRequest.Header.CallId;
			tmptpn = tmptpn.Substring(3, tmptpn.Length - 3);
			string arcd = tmptpn.Substring(0, 3);

			Invoke((MethodInvoker)delegate
			{
				UpdateTextBox(arrCdeTXT, arcd);
			});
			Invoke((MethodInvoker)delegate
			{
				UpdateTextBox(phNUMTXT, tmptpn);
			});


			Random rnd = new Random();
			int rn = rnd.Next(1, 3);
			//Set mobility text
			string MBT = "Unknown";
			Color MBC = Color.Red;
			switch (rn)
			{
				case 1:
					MBT = "Cellular";
					MBC = Color.LightCyan;
					break;
				case 2:
					MBT = "Landline";
					MBC = Color.OrangeRed;
					break;
				case 3:
					MBT = "Satilite";
					MBC = Color.RebeccaPurple;
					break;
				default:
					MBT = "Unknown";
					MBC = Color.Red;
					break;
			}
			Invoke((MethodInvoker)delegate
			{
				UpdateTextBox(mobilityTXT, MBT);
			});
			Invoke((MethodInvoker)delegate
			{
				UpdateTextBox(mobilityTXT, MBC);
			});


			//TODO: Add Call to Active Call List

			//Flash Line Button
			if (L1Act == false && Line1BUT.Visible == true)
			{
				L1FT = true;
			}
			else if (L2Act == true && Line2BUT.Visible == true)
			{
				L1FT = true;
			}
			else if (L3Act == true && Line3BUT.Visible == true)
			{
				L1FT = true;
			}
			else if (L4Act == true && Line4BUT.Visible == true)
			{
				L1FT = true;
			}
			else if (L5Act == true && Line5BUT.Visible == true)
			{
				L1FT = true;
			}
			else if (L6Act == true && Line6BUT.Visible == true)
			{
				L1FT = true;
			}
			try 
			{
				if (!_sipClients[0].IsCallActive)
				{
					_sipClients[0].Accept(sipRequest);

					//Set Call Screen up for Call

					return true;
				}
				else if (!_sipClients[1].IsCallActive)
				{
					_sipClients[1].Accept(sipRequest);

					//Set Call Screen up for Call

					return true;
				}
				else if (!_sipClients[2].IsCallActive)
				{
					_sipClients[2].Accept(sipRequest);

					//Set Call Screen up for Call

					return true;
				}
				else if (!_sipClients[3].IsCallActive)
				{
					_sipClients[3].Accept(sipRequest);

					//Set Call Screen up for Call

					return true;
				}
				else if (!_sipClients[4].IsCallActive)
				{
					_sipClients[4].Accept(sipRequest);

					//Set Call Screen up for Call

					return true;
				}
				else if (!_sipClients[5].IsCallActive)
				{
					_sipClients[5].Accept(sipRequest);

					//Set Call Screen up for Call

					return true;
				}
				else
				{
					return false;
				}
			} catch (Exception ex) 
			{ return false; }
			
		}

		/// <summary>
		/// Set up the UI to present options for an established SIP call, i.e. hide the cancel 
		/// button and display they hangup button.
		/// </summary>
		private async void SIPCallAnswered(SIPClient client)
		{

				//Gen Addr, Provider, Etc
		}

		/// <summary>
		/// StartHanging Up Call
		/// </summary>
		/// <param name="sender"></param>
		private void HngUpCall(object sender)
		{
			var client = _sipClients[0];
			switch (actline) 
			{
				case 1:
					client = _sipClients[0];
					break;
				case 2:
					client = _sipClients[1];
					break;
				case 3:
					client = _sipClients[2];
					break;
				case 4:
					client = _sipClients[3];
					break;
				case 5:
					client = _sipClients[4];
					break;
				case 6:
					client = _sipClients[5];
					break;
				default:
					break;
			}
			client.Hangup();

			ResetToCallStartState(client);
		}

		/// <summary>
		/// Pickup Call
		/// </summary>
		/// <param name="sender"></param>
		private async void AwnCall(object sender)
		{
			var client = _sipClients[0];
			if (sender == Line1BUT)
			{
				client = _sipClients[0];
			}
			if (sender == Line2BUT)
			{
				client = _sipClients[1];
			}
			if (sender == Line3BUT)
			{
				client = _sipClients[2];
			}
			if (sender == Line4BUT)
			{
				client = _sipClients[3];
			}
			if (sender == Line5BUT)
			{
				client = _sipClients[4];
			}
			if (sender == Line6BUT)
			{
				client = _sipClients[5];
			}

			await AnswerCallAsync(client);
		}

		/// <summary>
		/// HangUp Call
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void byebutton_Click(object sender, EventArgs e)
		{
			HngUpCall(sender);
		}

		/// <summary>
		/// Answer an incoming call on the SipClient
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		private async Task AnswerCallAsync(SIPClient client)
		{
			bool result = await client.Answer();

			if (result)
			{
				SIPCallAnswered(client);
			}
			else
			{
				ResetToCallStartState(client);
			}
		}

		private void Line1BUT_Click(object sender, EventArgs e)
		{
			//If - Active = False
			//Pickup Call
			//If- Other Button = Active = True
			//Hold Other button Pickup or Unhold This call if was on hold
			//If - Active = False and New Call = faslse..
			//Do nothing

			if (L1Act == false && L1H == false)
			{
				AwnCall(sender);
			}
			else if (L1Act == true && L1H == true)
			{
				//Unhold call
			}
			else if (L1Act == false && L2Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L1Act == false && L3Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L1Act == false && L4Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L1Act == false && L5Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L1Act == false && L6Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}


		}

		private void Line2BUT_Click(object sender, EventArgs e)
		{
			if (L2Act == false && L2H == false)
			{
				AwnCall(sender);
			}
			else if (L2Act == true && L2H == true)
			{
				//Unhold call
			}
			else if (L2Act == false && L1Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L2Act == false && L3Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L2Act == false && L4Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L2Act == false && L5Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L2Act == false && L6Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
		}

		private void Line3BUT_Click(object sender, EventArgs e)
		{
			if (L3Act == false && L3H == false)
			{
				AwnCall(sender);
			}
			else if (L3Act == true && L3H == true)
			{
				//Unhold call
			}
			else if (L3Act == false && L1Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L3Act == false && L2Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L3Act == false && L4Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L3Act == false && L5Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L3Act == false && L6Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
		}

		private void Line4BUT_Click(object sender, EventArgs e)
		{
			if (L4Act == false && L4H == false)
			{
				AwnCall(sender);
			}
			else if (L4Act == true && L4H == true)
			{
				//Unhold call
			}
			else if (L4Act == false && L1Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L4Act == false && L2Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L4Act == false && L3Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L4Act == false && L5Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L4Act == false && L6Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
		}

		private void Line5BUT_Click(object sender, EventArgs e)
		{
			if (L5Act == false && L5H == false)
			{
				AwnCall(sender);
			}
			else if (L5Act == true && L5H == true)
			{
				//Unhold call
			}
			else if (L5Act == false && L1Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L5Act == false && L2Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L5Act == false && L3Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L5Act == false && L4Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L5Act == false && L6Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
		}

		private void Line6BUT_Click(object sender, EventArgs e)
		{
			if (L6Act == false && L6H == false)
			{
				AwnCall(sender);
			}
			else if (L6Act == true && L6H == true)
			{
				//Unhold call
			}
			else if (L6Act == false && L1Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L6Act == false && L2Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L6Act == false && L3Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L6Act == false && L4Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
			else if (L6Act == false && L5Act == true)
			{
				//Hold Other Call
				//then
				Archivecallstatus(sender);
				AwnCall(sender);
			}
		}

		/// <summary>
		/// Take all the Call Specific Values and store it away
		/// </summary>
		/// <param name="sender"></param>
		private void Archivecallstatus(object sender)
		{
			if (sender == Line1BUT) 
			{
				CRS1.Ext = extNUMTXT.Text;
				CRS1.AreaCode = arrCdeTXT.Text;
				CRS1.Provider = provideridTXT.Text;
				CRS1.URI = uriTXT.Text;
				CRS1.PhoneNumb = phNUMTXT.Text;
				CRS1.Addr = addrbox;
				CRS1.STATBOX = statusTXT.Text;
				CRS1.Method = methodTXT.Text;
				CRS1.Mobility = mobilityTXT.Text;
				CRS1.CallerID = callerbox.Text;
				CRS1.XCORD = XText.Text;
				CRS1.YCORD = yTXT.Text;
				CRS1.ZCORD = zTXT.Text;
			}
			else if (sender == Line2BUT) 
			{
				CRS2.Ext = extNUMTXT.Text;
				CRS2.AreaCode = arrCdeTXT.Text;
				CRS2.Provider = provideridTXT.Text;
				CRS2.URI = uriTXT.Text;
				CRS2.PhoneNumb = phNUMTXT.Text;
				CRS2.Addr = addrbox;
				CRS2.STATBOX = statusTXT.Text;
				CRS2.Method = methodTXT.Text;
				CRS2.Mobility = mobilityTXT.Text;
				CRS2.CallerID = callerbox.Text;
				CRS2.XCORD = XText.Text;
				CRS2.YCORD = yTXT.Text;
				CRS2.ZCORD = zTXT.Text;
			}
			else if (sender == Line3BUT) 
			{
				CRS3.Ext = extNUMTXT.Text;
				CRS3.AreaCode = arrCdeTXT.Text;
				CRS3.Provider = provideridTXT.Text;
				CRS3.URI = uriTXT.Text;
				CRS3.PhoneNumb = phNUMTXT.Text;
				CRS3.Addr = addrbox;
				CRS3.STATBOX = statusTXT.Text;
				CRS3.Method = methodTXT.Text;
				CRS3.Mobility = mobilityTXT.Text;
				CRS3.CallerID = callerbox.Text;
				CRS3.XCORD = XText.Text;
				CRS3.YCORD = yTXT.Text;
				CRS3.ZCORD = zTXT.Text;
			}
			else if (sender == Line4BUT) 
			{
				CRS4.Ext = extNUMTXT.Text;
				CRS4.AreaCode = arrCdeTXT.Text;
				CRS4.Provider = provideridTXT.Text;
				CRS4.URI = uriTXT.Text;
				CRS4.PhoneNumb = phNUMTXT.Text;
				CRS4.Addr = addrbox;
				CRS4.STATBOX = statusTXT.Text;
				CRS4.Method = methodTXT.Text;
				CRS4.Mobility = mobilityTXT.Text;
				CRS4.CallerID = callerbox.Text;
				CRS4.XCORD = XText.Text;
				CRS4.YCORD = yTXT.Text;
				CRS4.ZCORD = zTXT.Text;
			}
			else if (sender == Line5BUT) 
			{
				CRS5.Ext = extNUMTXT.Text;
				CRS5.AreaCode = arrCdeTXT.Text;
				CRS5.Provider = provideridTXT.Text;
				CRS5.URI = uriTXT.Text;
				CRS5.PhoneNumb = phNUMTXT.Text;
				CRS5.Addr = addrbox;
				CRS5.STATBOX = statusTXT.Text;
				CRS5.Method = methodTXT.Text;
				CRS5.Mobility = mobilityTXT.Text;
				CRS5.CallerID = callerbox.Text;
				CRS5.XCORD = XText.Text;
				CRS5.YCORD = yTXT.Text;
				CRS5.ZCORD = zTXT.Text;
			}
			else if (sender == Line6BUT) 
			{
				CRS6.Ext = extNUMTXT.Text;
				CRS6.AreaCode = arrCdeTXT.Text;
				CRS6.Provider = provideridTXT.Text;
				CRS6.URI = uriTXT.Text;
				CRS6.PhoneNumb = phNUMTXT.Text;
				CRS6.Addr = addrbox;
				CRS6.STATBOX = statusTXT.Text;
				CRS6.Method = methodTXT.Text;
				CRS6.Mobility = mobilityTXT.Text;
				CRS6.CallerID = callerbox.Text;
				CRS6.XCORD = XText.Text;
				CRS6.YCORD = yTXT.Text;
				CRS6.ZCORD = zTXT.Text;
			}
		}

		/// <summary>
		/// Take all the Call Specific Values from store and display them
		/// </summary>
		/// <param name="sender"></param>
		private void RestoreCallStatus(object sender) 
		{
			if (sender == Line1BUT) 
			{
				extNUMTXT.Text = CRS1.Ext;
				arrCdeTXT.Text = CRS1.AreaCode;
				provideridTXT.Text = CRS1.Provider;
				uriTXT.Text = CRS1.URI;
				phNUMTXT.Text = CRS1.PhoneNumb;
				addrbox = CRS1.Addr;
				statusTXT.Text = CRS1.STATBOX;
				methodTXT.Text = CRS1.Method;
				mobilityTXT.Text = CRS1.Mobility;
				callerbox.Text = CRS1.CallerID;
				XText.Text = CRS1.XCORD;
				yTXT.Text = CRS1.YCORD;
				zTXT.Text = CRS1.ZCORD;
			}
			else if (sender == Line2BUT) 
			{
				extNUMTXT.Text = CRS2.Ext;
				arrCdeTXT.Text = CRS2.AreaCode;
				provideridTXT.Text = CRS2.Provider;
				uriTXT.Text = CRS2.URI;
				phNUMTXT.Text = CRS2.PhoneNumb;
				addrbox = CRS2.Addr;
				statusTXT.Text = CRS2.STATBOX;
				methodTXT.Text = CRS2.Method;
				mobilityTXT.Text = CRS2.Mobility;
				callerbox.Text = CRS2.CallerID;
				XText.Text = CRS2.XCORD;
				yTXT.Text = CRS2.YCORD;
				zTXT.Text = CRS2.ZCORD;
			}
			else if (sender == Line3BUT) 
			{
				extNUMTXT.Text = CRS3.Ext;
				arrCdeTXT.Text = CRS3.AreaCode;
				provideridTXT.Text = CRS3.Provider;
				uriTXT.Text = CRS3.URI;
				phNUMTXT.Text = CRS3.PhoneNumb;
				addrbox = CRS3.Addr;
				statusTXT.Text = CRS3.STATBOX;
				methodTXT.Text = CRS3.Method;
				mobilityTXT.Text = CRS3.Mobility;
				callerbox.Text = CRS3.CallerID;
				XText.Text = CRS3.XCORD;
				yTXT.Text = CRS3.YCORD;
				zTXT.Text = CRS3.ZCORD;
			}
			else if (sender == Line4BUT) 
			{
				extNUMTXT.Text = CRS4.Ext;
				arrCdeTXT.Text = CRS4.AreaCode;
				provideridTXT.Text = CRS4.Provider;
				uriTXT.Text = CRS4.URI;
				phNUMTXT.Text = CRS4.PhoneNumb;
				addrbox = CRS4.Addr;
				statusTXT.Text = CRS4.STATBOX;
				methodTXT.Text = CRS4.Method;
				mobilityTXT.Text = CRS4.Mobility;
				callerbox.Text = CRS4.CallerID;
				XText.Text = CRS4.XCORD;
				yTXT.Text = CRS4.YCORD;
				zTXT.Text = CRS4.ZCORD;
			}
			else if (sender == Line5BUT) 
			{
				extNUMTXT.Text = CRS5.Ext;
				arrCdeTXT.Text = CRS5.AreaCode;
				provideridTXT.Text = CRS5.Provider;
				uriTXT.Text = CRS5.URI;
				phNUMTXT.Text = CRS5.PhoneNumb;
				addrbox = CRS5.Addr;
				statusTXT.Text = CRS5.STATBOX;
				methodTXT.Text = CRS5.Method;
				mobilityTXT.Text = CRS5.Mobility;
				callerbox.Text = CRS5.CallerID;
				XText.Text = CRS5.XCORD;
				yTXT.Text = CRS5.YCORD;
				zTXT.Text = CRS5.ZCORD;
			}
			else if (sender == Line6BUT) 
			{
				extNUMTXT.Text = CRS6.Ext;
				arrCdeTXT.Text = CRS6.AreaCode;
				provideridTXT.Text = CRS6.Provider;
				uriTXT.Text = CRS6.URI;
				phNUMTXT.Text = CRS6.PhoneNumb;
				addrbox = CRS6.Addr;
				statusTXT.Text = CRS6.STATBOX;
				methodTXT.Text = CRS6.Method;
				mobilityTXT.Text = CRS6.Mobility;
				callerbox.Text = CRS6.CallerID;
				XText.Text = CRS6.XCORD;
				yTXT.Text = CRS6.YCORD;
				zTXT.Text = CRS6.ZCORD;
			}
		}

		/// <summary>
		/// Put the Current Call on Hold
		/// </summary>
		/// <param name="sender"></param>
		private async void HldCll(object sender)
		{
			SIPClient client = _sipClients[0];
			if (sender == Line1BUT)
			{
				client = _sipClients[0];
				await client.PutOnHold();
			}
			else if (sender == Line2BUT)
			{
				client = _sipClients[1];
				await client.PutOnHold();
			}
			else if (sender == Line3BUT)
			{
				client = _sipClients[2];
				await client.PutOnHold();
			}
			else if (sender == Line4BUT)
			{
				client = _sipClients[3];
				await client.PutOnHold();
			}
			else if (sender == Line5BUT)
			{
				client = _sipClients[4];
				await client.PutOnHold();
			}
			else if (sender == Line6BUT)
			{
				client = _sipClients[5];
				await client.PutOnHold();
			}
			else if (sender == holdBUT)
			{
				switch (actline)
				{
					case 1:
						client = _sipClients[0];
						await client.PutOnHold();
						break;
					case 2:
						client = _sipClients[1];
						await client.PutOnHold();
						break;
					case 3:
						client = _sipClients[2];
						await client.PutOnHold();
						break;
					case 4:
						client = _sipClients[3];
						await client.PutOnHold();
						break;
					case 5:
						client = _sipClients[4];
						await client.PutOnHold();
						break;
					case 6:
						client = _sipClients[5];
						await client.PutOnHold();
						break;
					default: break;
				}
			}

		}

		/// <summary>
		/// THE HOLD BUTTON HAS BEEN CLICKED
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void holdBUT_Click(object sender, EventArgs e)
		{
			HldCll(sender);
		}

		/// <summary>
		/// Take the Current Call Off Hold
		/// </summary>
		/// <param name="sender"></param>
		private void UHldCll(object sender)
		{
			SIPClient client = _sipClients[0];
			if (sender == Line1BUT)
			{
				client = _sipClients[0];
				client.TakeOffHold();
			}
			else if (sender == Line2BUT)
			{
				client = _sipClients[1];
				client.TakeOffHold();
			}
			else if (sender == Line3BUT)
			{
				client = _sipClients[2];
				client.TakeOffHold();
			}
			else if (sender == Line4BUT)
			{
				client = _sipClients[3];
				client.TakeOffHold();
			}
			else if (sender == Line5BUT)
			{
				client = _sipClients[4];
				client.TakeOffHold();
			}
			else if (sender == Line6BUT)
			{
				client = _sipClients[5];
				client.TakeOffHold();
			}
			else if (sender == holdBUT)
			{
				switch (actline)
				{
					case 1:
						client = _sipClients[0];
						client.TakeOffHold();
						break;
					case 2:
						client = _sipClients[1];
						client.TakeOffHold();
						break;
					case 3:
						client = _sipClients[2];
						client.TakeOffHold();
						break;
					case 4:
						client = _sipClients[3];
						client.TakeOffHold();
						break;
					case 5:
						client = _sipClients[4];
						client.TakeOffHold();
						break;
					case 6:
						client = _sipClients[5];
						client.TakeOffHold();
						break;
					default: break;
				}
			}

		}
	}
	
}

		
