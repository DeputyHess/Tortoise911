/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

using Microsoft.Win32;
using Org.BouncyCastle.Ocsp;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tortoise912
{
	internal class ConfigParse
	{
		internal static void parse()
		{
			ConfigFileBullshit config = new ConfigFileBullshit();
			config.getconf();
			try
			{
				using (WebClient WC = new WebClient())
				{
					bool doneparse = false;
					string searchHeadder = "";
					bool searchmode = false;
					string newfile = "";
					bool finenewfile = false;
					bool gotoenable = false;
					string content = "null";

					try
					{
						if (config.Provurl != null)
						{
							content = WC.DownloadString(config.Provurl + "46xxsettings.txt");
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}

				N:

					if (finenewfile == true)
					{
						finenewfile = false;
						content = WC.DownloadString(config.Provurl + newfile);
					}
					var split = content.Split($"\r\n");
					foreach (string line in split)
					{
						//Get ready for a large switch statement
						var number = "";
						if (searchmode == false)
						{
							if (line.Length > 0) { number = line.Substring(0, 2); }
							switch (number)
							{
								case "##":

									//Ignore
									break;

								case "# ":

									//Headder
									if (line == "# END")
									{ //doneparse = true; break;
									}
									else
									{
										if (searchHeadder.Length > 0 && searchHeadder == line)
										{
											File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\Headder.txt", line);
										}
									};
									break;

								case "GE":
#if DEBUG
									File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\GET.txt", line);
#endif
									try
									{
										if (config.Provgrp != null)
										{
											//get cmd
											//Pull another file from server
											if (line.Contains("46xxsettings-" + config.Provgrp) == true)
											{
												finenewfile = true;
												if (line.Length > 0) { number = line.Substring(4, line.Length - 4); }
												newfile = number;
												goto N;
											}
										}
									}
									catch (Exception ex)
									{
										MessageBox.Show(ex.Message);
									}

									break;

								case "SE":

									//SET cmd
									//Set Setting

									if (line.Contains("SET GMTOFFSET") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, line.Length - 14);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.GMTOFFSET = number;
										}
									}
									if (line.Contains("SET DAYLIGHT_SAVING_SETTNG_MODE") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(32, line.Length - 32);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.DAYLIGHT_SAVING_SETTNG_MODE = number;
										}
									}
									if (line.Contains("SET COUNTRY") == true)
									{
										if (line.Contains("US") == true)
										{
											CONFstor.COUNTRY = "US";
										}
										else if (line.Contains("UK") == true)
										{
											CONFstor.COUNTRY = "UK";
										}
									}
									if (line.Contains("SET DATEFORMAT") == true) { }
									if (line.Contains("SET TIMEZONE America/Chicago") == true) { }
									if (line.Contains("SET ENABLE_TIMEZONE") == true) { }
									if (line.Contains("SET SIP_CONTROLLER_LIST") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(24, line.Length - 24);
											number = Regex.Replace(number, "[\"]", string.Empty);
											var spltscl = number.Split(";");
											spltscl = spltscl[0].Split(":");
											number = "sip:" + spltscl[0];
											CONFstor.SIP_CONTROLLER_LIST = number;
											try
											{
												Application.UserAppDataRegistry.SetValue("SIPSV", number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
									}
									if (line.Contains("SET SIPSIGNAL") == true) { }
									if (line.Contains("SET WAIT_FOR_REGISTRATION_TIMER") == true) { }
									if (line.Contains("SET DOMAIN") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(11, line.Length - 11);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.DOMAIN = number;
										}
									}
									if (line.Contains("SET SIPDOMAIN") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, line.Length - 14);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.SIPDOMAIN = number;
										}
									}
									if (line.Contains("SET ENABLE_PRESENCE") == true) { }
									if (line.Contains("SET PROVIDE_LOGOUT") == true)
									{
										if (line.Length > 0)
										{
											if (line.Contains("1"))
											{
												CONFstor.PROVIDE_LOGOUT = true;
											}
											else if (line.Contains("0"))
											{
												CONFstor.PROVIDE_LOGOUT = false;
											}
										}
									}
									if (line.Contains("SET SIPREGPROXYPOLICY") == true) { }
									if (line.Contains("SET SIMULTANEOUS_REGISTRATIONS") == true) { }
									if (line.Contains("SET ENABLE_G711A") == true) { }
									if (line.Contains("SET ENABLE_G711U") == true) { }
									if (line.Contains("SET CODEC_PRIORITY") == true) { }
									if (line.Contains("SET ENABLE_G722") == true) { }
									/*if (line.Contains("SET UPGRADE_POLLING_PERIOD") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(27, line.Length - 27);
											number = Regex.Replace(number, "[\"]", string.Empty);
											try
											{
												Application.UserAppDataRegistry.SetValue("POLLING_P",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}*/
									if (line.Contains("SET ENABLE_L1") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_L1 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_L1 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_L2") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_L2 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_L2 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_L3") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_L3 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_L3 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_L4") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_L4 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_L4 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_L5") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_L5 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_L5 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_L6") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_L6 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_L6 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_1_DN") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, line.Length - 14);
											try
											{
												Application.UserAppDataRegistry.SetValue("LINE_1_DN",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_2_DN") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, line.Length - 14);
											try
											{
												Application.UserAppDataRegistry.SetValue("LINE_2_DN",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_3_DN") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, line.Length - 14);
											try
											{
												Application.UserAppDataRegistry.SetValue("LINE_3_DN",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_4_DN") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, line.Length - 14);
											try
											{
												Application.UserAppDataRegistry.SetValue("LINE_4_DN",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_5_DN") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, line.Length - 14);
											try
											{
												Application.UserAppDataRegistry.SetValue("LINE_5_DN",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_6_DN") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(14, line.Length - 14);
											try
											{
												Application.UserAppDataRegistry.SetValue("LINE_6_DN",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_1_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, line.Length - 15);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.LINE_1_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_2_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, line.Length - 15);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.LINE_2_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_3_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, line.Length - 15);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.LINE_3_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_4_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, line.Length - 15);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.LINE_4_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_5_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, line.Length - 15);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.LINE_5_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET LINE_6_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, line.Length - 15);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.LINE_6_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_XF1") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_XF1 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_XF1 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_XF2") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_XF2 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_XF2 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_XF3") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_XF3 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_XF3 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_XF4") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_XF4 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_XF4 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_XF5") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_XF5 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_XF5 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_XF6") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_XF6 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_XF6 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_XF7") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_XF7 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_XF7 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET ENABLE_XF8") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, 1);
											if (number == "1")
											{
												CONFstor.ENABLE_XF8 = true;
											}
											else if (number == "0")
											{
												CONFstor.ENABLE_XF8 = false;
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF1_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.XF1_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF2_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.XF2_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF3_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.XF3_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF4_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.XF4_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF5_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.XF5_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF6_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.XF6_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF7_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.XF7_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF8_TXT") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											CONFstor.XF8_TXT = number;
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF1_CLR") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											try
											{
												Application.UserAppDataRegistry.SetValue("XF1_CLR",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF2_CLR") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											try
											{
												Application.UserAppDataRegistry.SetValue("XF2_CLR",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF3_CLR") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											try
											{
												Application.UserAppDataRegistry.SetValue("XF3_CLR",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF4_CLR") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											try
											{
												Application.UserAppDataRegistry.SetValue("XF4_CLR",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF5_CLR") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											try
											{
												Application.UserAppDataRegistry.SetValue("XF5_CLR",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF6_CLR") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											try
											{
												Application.UserAppDataRegistry.SetValue("XF6_CLR",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF7_CLR") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											try
											{
												Application.UserAppDataRegistry.SetValue("XF7_CLR",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF8_CLR") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(12, line.Length - 12);
											number = Regex.Replace(number, "[\"]", string.Empty);
											try
											{
												Application.UserAppDataRegistry.SetValue("XF8_CLR",
												  number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}
									if (line.Contains("SET XF1_NUMB") == true) { }
									if (line.Contains("SET XF2_NUMB") == true) { }
									if (line.Contains("SET XF3_NUMB") == true) { }
									if (line.Contains("SET XF4_NUMB") == true) { }
									if (line.Contains("SET XF5_NUMB") == true) { }
									if (line.Contains("SET XF6_NUMB") == true) { }
									if (line.Contains("SET XF7_NUMB") == true) { }
									if (line.Contains("SET XF8_NUMB") == true) { }
									if (line.Contains("SET CONFSTATUS") == true)
									{
										if (line.Length > 0)
										{
											number = line.Substring(15, line.Length - 15);
											number = Regex.Replace(number, "[\"]", string.Empty);
											try
											{
												Application.UserAppDataRegistry.SetValue("CONFSTATUS", number);
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.Message);
											}
										}
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\SET.txt", number);
#endif
									}

									break;

								case "GO":

									//GOTO cmd
									//Keep parseing untill you get to the headder the GOTO says to get to
									if (gotoenable == true)
									{
										searchmode = true;
										if (line.Length > 0) { number = line.Substring(4, line.Length - 4); }
										searchHeadder = $"# " + number;
#if DEBUG
										File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\" + number + ".txt", number);
#endif
									}
									break;

								case "IF":
									try
									{
										if (Application.UserAppDataRegistry.GetValue("PROV_GRP") != null)
										{
											if (Application.UserAppDataRegistry.GetValue("PROV_GRP").ToString() != "False")
											{
												//IF cmd
												if (line.Contains($"IF $GROUP SEQ " + Application.UserAppDataRegistry.GetValue("PROV_GRP").ToString()))
												{
													if (line.Length > 0) { number = line.Substring(23, line.Length - 23); }
													searchHeadder = $"# " + number;
													searchmode = true;
#if DEBUG
													File.WriteAllText(@"B:\Tortoise911\Tortoise912\DEBUGCONF\IF.txt", line + $"||||" + searchHeadder);
#endif
												}
											}
										}
									}
									catch (Exception ex)
									{
										MessageBox.Show(ex.Message);
									}

									break;

								default:

									//fuck
									break;
							}
						}
						else
						{
							if (line.Contains(searchHeadder) == true)
							{
								searchmode = false;
								searchHeadder = "";
							}
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				//MAKE AN ERROR WINDOW OR SOMETHING
			}
		}
	}
}

/*
 * Ignore everything with 2 or more #s
 * Items with 1 # is a header and used for GOTO jumps
 * IF is a IF statement
 * GOTO is a goto statement
 * SET is a setting that needs to be set
 * GET is a new file we need to pull
 * $VALUE is a variable that needs to be checked during If statements
 * # END means stop parse
 */
