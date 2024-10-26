/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

namespace Tortoise912
{
	/// <summary>
	/// This Class Contains All The Tasks Needed For A Clean ShutDown Of The Application
	/// </summary>
	internal class ExitTask
	{
		internal static void Exit()
		{
			try
			{
				Siphandle.regUserAgent.Stop();

				// Allow for unregister request to be sent (REGISTER with 0 expiry)
				Task.Delay(1500).Wait();

				Siphandle.sipTransport.Shutdown();
				Task.Delay(1500).Wait();
			}
			catch (NullReferenceException ex) { }

			System.Windows.Forms.Application.Exit();
		}
	}
}
