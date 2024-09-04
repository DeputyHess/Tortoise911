/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

namespace Tortoise912
{
	public partial class OperatorLogin : Form
	{
		private bool grant = false;

		private int ecod = 0;

		public OperatorLogin()
		{
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			ExitTask.Exit();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Application.UserAppDataRegistry.SetValue("CUSR", usernameBOX.Text);

			//logic to verify sip login with the sip server
			try
			{
				if (Application.UserAppDataRegistry.GetValue("SIPSV") != null)
				{
					Siphandle.SippyPWCheck(Siphandle.sipTransport, usernameBOX.Text, passwordBOX.Text, Application.UserAppDataRegistry.GetValue("SIPSV").ToString());
				}
				else
				{
					ecod = 1;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			if (grant == true)
			{
				this.Hide();
				MainForm MF = new MainForm();
				MF.Show();
			}
			else
			{
				if (ecod == 99)
				{
					MessageBox.Show("Login Failure");
				}
				else if (ecod == 1)
				{
#if DEBUG
					MessageBox.Show("No Server Has Been Setup. Granting Temp Login ");
					grant = true;
					button1.Text = "Confirm Login";
					varstore.tauth = true;

#else
					if (settings.Default.Dbug == true)
					{
						MessageBox.Show("No Server Has Been Setup. Granting Debug Login ");
						varstore.tauth = true;
						grant = true;
						button1.Text = "Confirm Login";
					}
#endif
				}
			}
		}

		private void OperatorLogin_Shown(object sender, EventArgs e)
		{
			button1.Text = "Login";
		}
	}
}
