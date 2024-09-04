/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

using SIPSorcery.SIP.App;
using SIPSorcery.SIP;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.X509;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Reflection.Metadata.Ecma335;

namespace Tortoise912
{
	internal class Siphandle
	{
		private static Microsoft.Extensions.Logging.ILogger Log = NullLogger.Instance;

		internal static SIPTransport _sipTransport = new SIPTransport();

		public static SIPTransport sipTransport
		{
			set { _sipTransport = value; }
			get { return _sipTransport; }
		}

		internal static SIPRegistrationUserAgent _regUserAgent;

		internal static SIPRegistrationUserAgent regUserAgent
		{
			get { return _regUserAgent; }
			set { _regUserAgent = value; }
		}

		internal static ManualResetEvent _exitMre = new ManualResetEvent(false);

		internal static ManualResetEvent exitMre
		{
			get { return _exitMre; }
			set { _exitMre = value; }
		}

		/// <summary>
		/// Reg Sip
		/// </summary>
		/// <param name="sipTransport"></param>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="server"></param>

		internal static void SippyReg(SIPTransport sipTransport, string username, string password, string server)
		{
			//Log = AddConsoleLogger(LogEventLevel.Verbose);
			_regUserAgent = new SIPRegistrationUserAgent(sipTransport, username, password, server, 850);
			sipTransport.EnableTraceLogs();

			// Create a client user agent to maintain a periodic registration with a SIP server.

			// Event handlers for the different stages of the registration.
			regUserAgent.RegistrationFailed += (uri, resp, err) => Log.LogWarning($"{uri}: {err}");
			regUserAgent.RegistrationTemporaryFailure += (uri, resp, msg) => Log.LogWarning($"{uri}: {msg}");
			regUserAgent.RegistrationRemoved += (uri, resp) => Log.LogWarning($"{uri} registration failed.");
			regUserAgent.RegistrationSuccessful += (uri, resp) => Log.LogInformation($"{uri} registration succeeded.");
		}

		internal static bool SippyPWCheck(SIPTransport sipTransport, string username, string password, string server)
		{
			//Log = AddConsoleLogger(LogEventLevel.Verbose);
			_regUserAgent = new SIPRegistrationUserAgent(sipTransport, username, password, server, 5);
			sipTransport.EnableTraceLogs();

			int stat = 69;

			// Event handlers for the different stages of the registration.
			regUserAgent.RegistrationFailed += (uri, resp, err) => stat = 1;
			regUserAgent.RegistrationTemporaryFailure += (uri, resp, msg) => stat = 2;
			regUserAgent.RegistrationRemoved += (uri, resp) => stat = 9;
			regUserAgent.RegistrationSuccessful += (uri, resp) => stat = 0;
			regUserAgent.Stop();

			switch (stat)
			{
				case 1:

				//Fail
				case 2:
				case 9:
					return false;

				case 0:
					return true;

				case 69:
				default:
					return false;
			}
		}
	}
}
