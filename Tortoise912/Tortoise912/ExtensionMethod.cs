using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Tortoise912
{
	/// <summary>
	/// Ext Methods
	/// </summary>
	public static class ExtensionMethod
	{
		/// <summary>
		/// Do Shit on UI Thread
		/// </summary>
		/// <param name="dispatcher"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public static void DoOnUiThread(this Dispatcher dispatcher, Action action)
		{
			dispatcher.Invoke(DispatcherPriority.Render, (SendOrPostCallback)delegate { action(); }, null);
		}
		public static void DoOnUIThread(this Dispatcher dispatcher) { }
		public static void DoOnUIThread(this Dispatcher dispatcher, Action action) { }
		public static void DUIT(this Dispatcher dispatcher, Action action) { }
	}
	class Test 
	{
		void Testt()
		{
		}
	}
}
