/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortoise912
{
	internal class varstore
	{
		internal static bool _tauth = false;

		internal static bool tauth
		{
			get
			{
				return _tauth;
			}

			set
			{
				_tauth = value;
			}
		}
	}
}
