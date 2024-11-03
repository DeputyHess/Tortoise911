/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

namespace Tortoise911
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
			CONFstor.LOGUSR = " " + usernameBOX.Text;

			//logic to verify sip login with the sip server
			try
			{
				if (Application.UserAppDataRegistry.GetValue("SIPSV") != null)
				{
					int rcod = 0;
					rcod = Siphandle.SippyPWCheck(Siphandle.sipTransport, usernameBOX.Text, passwordBOX.Text, Application.UserAppDataRegistry.GetValue("SIPSV").ToString());

					switch (rcod) 
					{
						case 0:
							grant = true;
							break;
						case 1:
							ecod = 81;
							break;
						case 2:
							ecod = 82;
							break;
						case 9:
							ecod = 89;
							break;
						case 69:
							ecod = 75;
							break;
						default:
							ecod = 75;
							break;
					}
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
				CONFstor.grant = true;
				CONFstor.SUS = usernameBOX.Text;
				CONFstor.SPW = passwordBOX.Text;
				this.Hide();
				MainForm MF = new MainForm();
				MF.Show();
			}
			else
			{
				if (ecod == 99)
				{
					MessageBox.Show("Login Failure - Check Ext and Password");
				}
				else if (ecod == 1)
				{
#if DEBUG
					MessageBox.Show("No Server Has Been Setup. Granting Temp Login ");
					grant = true;
					button1.Text = "Confirm Login";
					varstore.tauth = true;

#else
					ConfigFileBullshit CFB = new ConfigFileBullshit();
					CFB.getconf();
					if (CFB.DBGLGN == "DaU2sceJBj3x4ZJ0BO8VFYSHAsQRhCcw")
					{
						MessageBox.Show("No Server Has Been Setup. Granting Debug Login ");
						varstore.tauth = true;
						grant = true;
						button1.Text = "Confirm Login";
					}
#endif
				}
				else if (ecod == 81) { MessageBox.Show("Login Failure - Registration Failed"); }
				else if (ecod == 82) { MessageBox.Show("Login Failure - Registration Temporary Failure"); }
				else if (ecod == 89) { MessageBox.Show("Login Failure - Registration Removed"); }
				else if (ecod == 75) { MessageBox.Show("Login Failure - Unknown"); }
				else { MessageBox.Show("Login Failure - Unknown"); }
			}
		}

		private void OperatorLogin_Shown(object sender, EventArgs e)
		{
			button1.Text = "Login";
		}
	}
}
