/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

using System.IO;

namespace Tortoise911
{
	internal class ConfigFileBullshit
	{
		internal string Provurl { get; set; }
		internal string Provgrp { get; set; }
		internal string Rngtne { get; set; }
		internal string RelKey { get; set; }
		internal string AwnKey { get; set; }
		internal string DBGLGN { get; set; }
		internal void setconf() 
		{
			string[] line = new string[6];
			line [0] = "NYXTEL TORTOISE 911 INIT CFG";
			line [1] = Provurl;
			line [2] = Provgrp;
			line [3] = Rngtne;
			line [4] = RelKey;
			line [5] = AwnKey;
			line [6] = DBGLGN;
			try
			{
				File.WriteAllLines(@"C:\ProgramData\NyxTel\config.nxt", line);
			}
			catch (Exception ex) 
			{
				System.IO.Directory.CreateDirectory(@"C:\ProgramData\NyxTel\");
				File.WriteAllLines(@"C:\ProgramData\NyxTel\config.nxt", line);
			}
		}

		internal void getconf() 
		{
			try
			{
				string[] cfgshit = File.ReadAllLines(@"C:\ProgramData\NyxTel\config.nxt");
				Provurl = cfgshit[1];
				Provgrp = cfgshit[2];
				Rngtne = cfgshit[3];
				RelKey = cfgshit[4];
				AwnKey = cfgshit[5];
				DBGLGN = cfgshit[6];
			}
			catch (Exception ex)
			{
				
			}
		}
	}
}
