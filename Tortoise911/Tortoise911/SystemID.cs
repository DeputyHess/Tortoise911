using Microsoft.Win32;
namespace Security
{
	/// <summary>
	/// Tools for stuff
	/// </summary>
	public static class FingerPrint
	{
		/// <summary>
		/// Gets GUID
		/// </summary>
		/// <returns></returns>
		/// <exception cref="KeyNotFoundException"></exception>
		/// <exception cref="IndexOutOfRangeException"></exception>
		public static string GetMachineGuid()
		{
			string location = @"SOFTWARE\Microsoft\Cryptography";
			string name = "MachineGuid";

			using (RegistryKey localMachineX64View =
				RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
			{
				using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
				{
					if (rk == null)
						throw new KeyNotFoundException(
							string.Format("Key Not Found: {0}", location));

					object machineGuid = rk.GetValue(name);
					if (machineGuid == null)
						throw new IndexOutOfRangeException(
							string.Format("Index Not Found: {0}", name));

					return machineGuid.ToString();
				}
			}
		}

		/// <summary>
		/// MAKE MD5
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string CreateMD5(string input)
		{
			// Use input string to calculate MD5 hash
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				return Convert.ToHexString(hashBytes);
			}
		}
	}
}