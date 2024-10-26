/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

namespace Tortoise912
{
	internal class ConfigFileBullshit
	{
		internal string Provurl { get; set; }
		internal string Provgrp { get; set; }
		internal void setconf() 
		{
			string[] line = new string[3];
			line [0] = "NYXTEL TORTOISE 912 INIT CFG";
			line [1] = Provurl;
			line[2] = Provgrp;
			try
			{
				File.WriteAllLines(@"C:\NyxTel\config.nxt", line);
			}
			catch (Exception ex) 
			{
				System.IO.Directory.CreateDirectory(@"C:\NyxTel\");
				File.WriteAllLines(@"C:\NyxTel\config.nxt", line);
			}
		}

		internal void getconf() 
		{
			string[] cfgshit = File.ReadAllLines(@"C:\NyxTel\config.nxt");
			Provurl = cfgshit[1];
			Provgrp = cfgshit[2];
		}
	}
}
