using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JHJ.FolderIcons
{
	/// <summary>
	/// Interaction logic for LogWindow.xaml
	/// </summary>
	public partial class LogWindow : Window
	{

		#region General properties

		/// <summary>
		/// Log
		/// </summary>
		public Log Log { get; set; }

		#endregion

		#region Construction and destruction
		
		/// <summary>
		/// Standard constructor
		/// </summary>
		public LogWindow()
		{
			this.Log = null;
			this.InitializeComponent();
		}

		/// <summary>
		/// Constructor with log
		/// </summary>
		public LogWindow(Log pLog)
			: this()
		{
			this.Log = pLog;
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Updating the window
		/// </summary>
		protected void UpdateWindow()
		{
			if (this.Log != null)
				this.LogTextBox.Text = this.Log.Content;
			else
				this.LogTextBox.Text = string.Empty;
			this.UpdateLayout();
		}

		#endregion

		#region Window event handlers

		/// <summary>
		/// On window loaded
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			this.UpdateWindow();
		}

		/// <summary>
		/// On "Load Icons" ribbon button click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ClearLogRibbonButtonClick(object sender, RoutedEventArgs e)
		{
			if (this.Log != null)
			{ 
				this.Log.Clear();
				this.UpdateWindow();
			}
		}

		/// <summary>
		/// On "Exit" ribbon button click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ExitRibbonButtonClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// On "Exit" ribbon application menu item click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ExitRibbonApplicationMenuItemClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		#endregion
	}

}
