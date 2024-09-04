/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

namespace Tortoise912
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			try
			{
				if (Application.UserAppDataRegistry.GetValue("PROV_URL") != null || Application.UserAppDataRegistry.GetValue("PROV_GRP") != null)
				{
				}
				else
				{
					FirstLaunch FL = new FirstLaunch();
					FL.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			Application.Run(new SplashSc());
		}
	}
}
