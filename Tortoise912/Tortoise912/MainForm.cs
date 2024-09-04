/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

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

		public System.Windows.Forms.Timer cleantimer = new System.Windows.Forms.Timer();

		/// <summary>
		/// The Main Window
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			if (Application.UserAppDataRegistry.GetValue("CUSR") != null)
			{
				usersnamelab.Text = Application.UserAppDataRegistry.GetValue("CUSR").ToString();
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
				if (Application.UserAppDataRegistry.GetValue("POLLING_P") != null)
				{
					conftimer.Interval = (int.Parse(Application.UserAppDataRegistry.GetValue("POLLING_P").ToString()) * 60 * 1000);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			conftimer.Tick += new EventHandler(conftimer_Tick);
			conftimer.Start();
			conftimer.Enabled = true;
			if (Application.UserAppDataRegistry.GetValue("CONFSTATUS") != null)
			{
				if (Application.UserAppDataRegistry.GetValue("CONFSTATUS").ToString() == "Unprovisioned")
				{
					statusTXT.BackColor = Color.Red; statusTXT.ForeColor = Color.Black;
				}
				else if (Application.UserAppDataRegistry.GetValue("CONFSTATUS").ToString() == "SIP IP Softphone")
				{
					statusTXT.BackColor = Color.Silver; statusTXT.ForeColor = Color.Black;
				}
			}
			else
			{
				Application.UserAppDataRegistry.SetValue("CONFSTATUS", "Unprovisioned");
				if (Application.UserAppDataRegistry.GetValue("CONFSTATUS").ToString() == "Unprovisioned")
				{
					statusTXT.BackColor = Color.Red; statusTXT.ForeColor = Color.Black;
				}
				else if (Application.UserAppDataRegistry.GetValue("CONFSTATUS").ToString() == "SIP IP Softphone")
				{
					statusTXT.BackColor = Color.Silver; statusTXT.ForeColor = Color.Black;
				}
			}

			//other shit
		}

		private void RefreshButtons()
		{
			//L1
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_L1") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_L1").ToString() == "False")
					{
						Line1BUT.Visible = false;
					}
					else
					{
						Line1BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("LINE_1_TXT") != null)
				{
					Line1BUT.Text = Application.UserAppDataRegistry.GetValue("LINE_1_TXT").ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			//L2
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_L2") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_L2").ToString() == "False")
					{
						Line2BUT.Visible = false;
					}
					else
					{
						Line2BUT.Visible = true;
					}
				}
				if (Application.UserAppDataRegistry.GetValue("LINE_2_TXT") != null)
				{
					Line2BUT.Text = Application.UserAppDataRegistry.GetValue("LINE_2_TXT").ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			//L3
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_L3") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_L3").ToString() == "False")
					{
						Line3BUT.Visible = false;
					}
					else
					{
						Line3BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("LINE_3_TXT") != null)
				{
					Line3BUT.Text = Application.UserAppDataRegistry.GetValue("LINE_3_TXT").ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			//L4
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_L4") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_L4").ToString() == "False")
					{
						Line4BUT.Visible = false;
					}
					else
					{
						Line4BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("LINE_4_TXT") != null)
				{
					Line4BUT.Text = Application.UserAppDataRegistry.GetValue("LINE_4_TXT").ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			//L5
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_L5") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_L5").ToString() == "False")
					{
						Line5BUT.Visible = false;
					}
					else
					{
						Line5BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("LINE_5_TXT") != null)
				{
					Line5BUT.Text = Application.UserAppDataRegistry.GetValue("LINE_5_TXT").ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			//L6
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_L6") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_L6").ToString() == "False")
					{
						Line6BUT.Visible = false;
					}
					else
					{
						Line6BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("LINE_6_TXT") != null)
				{
					Line6BUT.Text = Application.UserAppDataRegistry.GetValue("LINE_6_TXT").ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			//XFER1
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_XF1") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_XF1").ToString() == "False")
					{
						xfer1BUT.Visible = false;
					}
					else
					{
						xfer1BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("XF1_TXT") != null)
				{
					xfer1BUT.Text = Application.UserAppDataRegistry.GetValue("XF1_TXT").ToString();
				}
				if (Application.UserAppDataRegistry.GetValue("XF1_CLR") != null)
				{
					string clr = Application.UserAppDataRegistry.GetValue("XF1_CLR").ToString();
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
				MessageBox.Show(ex.Message);
			}

			//XFER2
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_XF2") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_XF2").ToString() == "False")
					{
						xfer2BUT.Visible = false;
					}
					else
					{
						xfer2BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("XF2_TXT") != null)
				{
					xfer2BUT.Text = Application.UserAppDataRegistry.GetValue("XF2_TXT").ToString();
				}
				if (Application.UserAppDataRegistry.GetValue("XF2_CLR") != null)
				{
					string clr = Application.UserAppDataRegistry.GetValue("XF2_CLR").ToString();
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
				MessageBox.Show(ex.Message);
			}

			//XFER3
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_XF3") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_XF3").ToString() == "False")
					{
						xfer3BUT.Visible = false;
					}
					else
					{
						xfer3BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("XF3_TXT") != null)
				{
					xfer3BUT.Text = Application.UserAppDataRegistry.GetValue("XF3_TXT").ToString();
				}
				if (Application.UserAppDataRegistry.GetValue("XF3_CLR") != null)
				{
					string clr = Application.UserAppDataRegistry.GetValue("XF3_CLR").ToString();
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
				MessageBox.Show(ex.Message);
			}

			//XFER4
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_XF4") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_XF4").ToString() == "False")
					{
						xfer4BUT.Visible = false;
					}
					else
					{
						xfer4BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("XF4_TXT") != null)
				{
					xfer4BUT.Text = Application.UserAppDataRegistry.GetValue("XF4_TXT").ToString();
				}
				if (Application.UserAppDataRegistry.GetValue("XF4_CLR") != null)
				{
					string clr = Application.UserAppDataRegistry.GetValue("XF4_CLR").ToString();
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
				MessageBox.Show(ex.Message);
			}

			//XFER5
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_XF5") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_XF5").ToString() == "False")
					{
						xfer5BUT.Visible = false;
					}
					else
					{
						xfer5BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("XF5_TXT") != null)
				{
					xfer5BUT.Text = Application.UserAppDataRegistry.GetValue("XF5_TXT").ToString();
				}
				if (Application.UserAppDataRegistry.GetValue("XF5_CLR") != null)
				{
					string clr = Application.UserAppDataRegistry.GetValue("XF5_CLR").ToString();
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
				MessageBox.Show(ex.Message);
			}

			//XFER6
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_XF6") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_XF6").ToString() == "False")
					{
						xfer6BUT.Visible = false;
					}
					else
					{
						xfer6BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("XF6_TXT") != null)
				{
					xfer6BUT.Text = Application.UserAppDataRegistry.GetValue("XF6_TXT").ToString();
				}
				if (Application.UserAppDataRegistry.GetValue("XF6_CLR") != null)
				{
					string clr = Application.UserAppDataRegistry.GetValue("XF6_CLR").ToString();
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
				MessageBox.Show(ex.Message);
			}

			//XFER7
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_XF7") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_XF7").ToString() == "False")
					{
						xfer7BUT.Visible = false;
					}
					else
					{
						xfer7BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("XF7_TXT") != null)
				{
					xfer7BUT.Text = Application.UserAppDataRegistry.GetValue("XF7_TXT").ToString();
				}
				if (Application.UserAppDataRegistry.GetValue("XF7_CLR") != null)
				{
					string clr = Application.UserAppDataRegistry.GetValue("XF7_CLR").ToString();
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
				MessageBox.Show(ex.Message);
			}

			//XFER8
			try
			{
				if (Application.UserAppDataRegistry.GetValue("ENABLE_XF8") != null)
				{
					if (Application.UserAppDataRegistry.GetValue("ENABLE_XF8").ToString() == "False")
					{
						xfer8BUT.Visible = false;
					}
					else
					{
						xfer8BUT.Visible = true;
					}
				}

				if (Application.UserAppDataRegistry.GetValue("XF8_TXT") != null)
				{
					xfer8BUT.Text = Application.UserAppDataRegistry.GetValue("XF8_TXT").ToString();
				}

				if (Application.UserAppDataRegistry.GetValue("XF8_CLR") != null)
				{
					string clr = Application.UserAppDataRegistry.GetValue("XF8_CLR").ToString();
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
				MessageBox.Show(ex.Message);
			}

			//Status box
			try
			{
				if (Application.UserAppDataRegistry.GetValue("CONFSTATUS") != null)
				{
					statusTXT.Text = Application.UserAppDataRegistry.GetValue("CONFSTATUS").ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void timetimer_Tick(object sender, EventArgs e)
		{
			timeLAB.Text = DateTime.Now.ToString("HH:MM:ss");
			dateLAB.Text = DateTime.Now.ToString("MM/dd/yyyy");
		}

		private void conftimer_Tick(object sender, EventArgs e)
		{
			ConfigParse.parse();
			Invoke(() =>
			{
				RefreshButtons();
			});
		}

		private void quitbutton_Click(object sender, EventArgs e)
		{
			if (CLEANINGSCREEN == false)
			{
				ExitTask.Exit();
			}
		}

		private void button19_Click(object sender, EventArgs e)
		{
			if (CLEANINGSCREEN == false) { }

			//Open Notes Panel
		}

		private void configBUT_Click(object sender, EventArgs e)
		{
			if (CLEANINGSCREEN == false)
			{
				configform CF = new configform();
				CF.ShowDialog();
			}
		}

		private void aboutBUT_Click(object sender, EventArgs e)
		{
			if (CLEANINGSCREEN == false)
			{
				About AB = new About();
				AB.ShowDialog();
			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			if (CLEANINGSCREEN == false)
			{
				var confirmResult = MessageBox.Show("Are you sure you want to logout", "Confirm Logout!!", MessageBoxButtons.YesNo);
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
	}
}
