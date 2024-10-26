/*
*   Copyright (C) 2024 by N5UWU
*   This program is distributed WITHOUT WARRANTY.
*/

using Microsoft.Win32;
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

		internal static int cunt { get; set; }
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

		internal static string VersionIndependentRegKey
		{
			get
			{
				string versionDependent = System.Windows.Forms.Application.UserAppDataRegistry.Name;
				string versionIndependent =
					   versionDependent.Substring(0, versionDependent.LastIndexOf("\\"));
				return versionIndependent;
			}
		}

		internal static object GetRegistryValue(string name, object defaultValue)
		{
			return Registry.GetValue(VersionIndependentRegKey, name, defaultValue);
		}

		internal static object GetRegistryValue(string name)
		{
			return GetRegistryValue(name, null);
		}

		internal static void SetRegistryValue(string name, object value, RegistryValueKind kind)
		{
			Registry.SetValue(VersionIndependentRegKey, name, value, kind);
		}
	}
}
