/*
 * Created by SharpDevelop.
 * User: U09477
 * Date: 25/03/2009
 * Time: 14:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using AwkEverywhere.Frontend;
using AwkEverywhere.Config;

namespace AwkEverywhere
{
	public sealed class NotificationIcon
	{
		private NotifyIcon notifyIcon;
		private ContextMenu notificationMenu;
		
		private AwkEverywhere.Forms.NppAwkPluginMainForm moMain;
		private AwkEverywhere.Forms.WSForms.WSBrowser moWSForm;
		
		
		#region Initialize icon and menu
		public NotificationIcon()
		{
			notifyIcon = new NotifyIcon();
			notificationMenu = new ContextMenu(InitializeMenu());
			
			notifyIcon.DoubleClick += IconDoubleClick;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationIcon));
			notifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
			notifyIcon.ContextMenu = notificationMenu;
			
			IFrontEnd oFrontEnd = new AwkFrontEnd();
				IFrontEndConfig oConfig = new NppAwkPluginConfig();
				moMain = new AwkEverywhere.Forms.NppAwkPluginMainForm(oFrontEnd,oConfig);
				moMain.CopyFromNpp += new EventHandler(oMain_CopyFromNpp);
				moMain.CopyToNpp += new EventHandler(oMain_CopyToNpp);
		}

		
		private MenuItem[] InitializeMenu()
		{
			MenuItem[] menu = new MenuItem[] {
				new MenuItem("Open", IconDoubleClick),
				new MenuItem("About", menuAboutClick),
				new MenuItem("WS Scripts", menuWSClick),
				new MenuItem("Exit", menuExitClick)
			};
			return menu;
		}
		#endregion
		
		#region Main - Program entry point
		/// <summary>Program entry point.</summary>
		/// <param name="args">Command Line Arguments</param>
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			bool isFirstInstance;
			// Please use a unique name for the mutex to prevent conflicts with other programs
			using (Mutex mtx = new Mutex(true, "AwkEverywhere", out isFirstInstance)) {
				if (isFirstInstance) {
					NotificationIcon notificationIcon = new NotificationIcon();
					notificationIcon.notifyIcon.Visible = true;
					Application.Run();
					notificationIcon.notifyIcon.Dispose();
				} else {
					// The application is already running
					// TODO: Display message box or change focus to existing application instance
				}
			} // releases the Mutex
		}
		#endregion
		
		#region Event Handlers
		private void menuAboutClick(object sender, EventArgs e)
		{
			AwkEverywhere.Forms.AboutForm oAbout = new AwkEverywhere.Forms.AboutForm();
			oAbout.ShowDialog();
		}
		
		private void menuExitClick(object sender, EventArgs e)
		{
			moMain.HideOnClose = false;
			Application.Exit();
		}
		
		private void menuWSClick(object sender, EventArgs e){
			if(moWSForm==null){
				IFrontEndConfig oConfig = new NppAwkPluginConfig();
				moWSForm = new AwkEverywhere.Forms.WSForms.WSBrowser(oConfig);
			}
			moWSForm.Show();
		}
		
		private void IconDoubleClick(object sender, EventArgs e)
		{
			moMain.Show();
		}

		void oMain_CopyToNpp(object sender, EventArgs e)
		{
			Clipboard.SetText(moMain.GetResultString());
		}

		void oMain_CopyFromNpp(object sender, EventArgs e)
		{
			moMain.SetDataString(Clipboard.GetText());
		}
		

		#endregion
	}
}
