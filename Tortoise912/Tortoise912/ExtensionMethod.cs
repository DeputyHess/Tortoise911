using System.Windows.Threading;
namespace Tortoise912.Ext
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
		public static void DoOnUIThread(this Dispatcher dispatcher, Action action)
		{
			dispatcher.Invoke(DispatcherPriority.Render, (SendOrPostCallback)delegate { action(); }, null);
		}
	}
}
