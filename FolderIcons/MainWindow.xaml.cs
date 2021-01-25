using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Main window class
	/// </summary>
	public partial class MainWindow : Window
	{

		#region General properties

		/// <summary>
		/// Application
		/// </summary>
		protected TheApplication Application { get { return (TheApplication)System.Windows.Application.Current; } }

		/// <summary>
		/// Executing
		/// </summary>
		protected bool Executing { get; set; }

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		public MainWindow()
		{
			try
			{ 
				this.Executing = false;
				this.InitializeComponent();
				this.UpdateWindow(string.Empty);
				this.ClearCounters();
				this.Application.OnCountersChange += this.ApplicationOnCountersChange;
			}
			catch (Exception lException)
			{
				MessageBox.Show(ErrorToolkit.CreateErrorMessage(lException));
			}
		}

		#endregion

		#region Actions

		/// <summary>
		/// Loading defaults
		/// </summary>
		protected void LoadDefaults()
		{
			try
			{
				this.UpdateWindow(JHJ.FolderIcons.Resources.MainWindow.LoadingDefaults);
				this.Application.LoadDefaults();
				this.UpdateSetupTreeView();
				this.ClearCounters();
			}
			catch (Exception lException)
			{
				ErrorToolkit.ShowErrorMessage(lException);
			}
			finally
			{
				this.UpdateWindow(string.Empty);
			}
		}
				
		/// <summary>
		/// Loading icons
		/// </summary>
		protected void LoadIcons()
		{
			string lXmlFilePath = this.SelectXmlFile();
			if (lXmlFilePath.Length > 0)
				try
				{
					this.UpdateWindow(JHJ.FolderIcons.Resources.MainWindow.LoadingIcons);
					this.Application.LoadIcons(lXmlFilePath, true);
					this.UpdateSetupTreeView();
					this.ClearCounters();
				}
				catch (Exception lException)
				{
					ErrorToolkit.ShowErrorMessage(lException);
				}
				finally
				{
					this.UpdateWindow(string.Empty);
				}
		}

		/// <summary>
		/// Loading file system
		/// </summary>
		protected void LoadFileSystem()
		{
			string lXmlFilePath = this.SelectXmlFile();
			if (lXmlFilePath.Length > 0)
				try
				{
					this.UpdateWindow(JHJ.FolderIcons.Resources.MainWindow.LoadingFileSystem);
					this.Application.LoadFileSystem(lXmlFilePath, true);
					this.UpdateSetupTreeView();
					this.ClearCounters();
				}
				catch (Exception lException)
				{
					ErrorToolkit.ShowErrorMessage(lException);
				}
				finally
				{
					this.UpdateWindow(string.Empty);
				}
		}

		/// <summary>
		/// Executing folder icon changer
		/// </summary>
		protected void Execute()
		{
			try
			{
				this.Executing = true;
				this.UpdateWindow(JHJ.FolderIcons.Resources.MainWindow.Executing);
				this.ClearCounters();
				this.Application.Execute();
				this.ClearCurrentlyProcessing();
			}
			catch (Exception lException)
			{
				ErrorToolkit.ShowErrorMessage(lException);
			}
			finally
			{
				this.Executing = false;
				this.UpdateWindow(string.Empty);
			}
		}

		/// <summary>
		/// Showing log
		/// </summary>
		protected void ShowLog()
		{
			LogWindow lLogWindow = new LogWindow(this.Application.Log);
			lLogWindow.Show();
		}

		/// <summary>
		/// Showing "About" window
		/// </summary>
		protected void About()
		{
			AboutWindow lAboutWindow = new AboutWindow();
			lAboutWindow.Show();
		}

		/// <summary>
		/// Exiting the application
		/// </summary>
		protected void Exit()
		{
			this.Application.Shutdown();
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Selecting XML file
		/// </summary>
		/// <returns>XML file path (or empty string if cancelled)</returns>
		protected string SelectXmlFile()
		{
			string lXmlFilePath = string.Empty;
			OpenFileDialog lOpenFileDialog = new OpenFileDialog();
			lOpenFileDialog.Filter = string.Format("{0} (*.xml)|*.xml|{1} (*.*)|*.*", JHJ.FolderIcons.Resources.MainWindow.XmlFilesFilter,
				JHJ.FolderIcons.Resources.MainWindow.AllFilesFilter);
			if (lOpenFileDialog.ShowDialog() == true)
				lXmlFilePath = lOpenFileDialog.FileName;
			return lXmlFilePath;
		}

		/// <summary>
		/// Updating setup tree view
		/// </summary>
		protected void UpdateSetupTreeView()
		{
			this.SetupTreeView.Items.Clear();
			if (this.Application.FolderIconChanger != null)
			{ 
				TreeViewBuilder lTreeViewBuilder = new TreeViewBuilder(this.SetupTreeView);
				TreeViewItem lIconsTreeViewItem = lTreeViewBuilder.AddItemToRoot(JHJ.FolderIcons.Resources.MainWindow.Icons, @"Resources\Images\Icons24.png");
				Icons lIcons = this.Application.FolderIconChanger.Icons;
				if (lIcons != null)
				{ 
					foreach (Icon lIcon in lIcons.Items)
						lTreeViewBuilder.AddItem(lIconsTreeViewItem, lIcon.ToString(), @"Resources\Images\Icon16.png");
					lTreeViewBuilder.SetItemHeader(lIconsTreeViewItem, lIcons.ToString());
				}
				TreeViewItem lFileSystemTreeViewItem = lTreeViewBuilder.AddItemToRoot(JHJ.FolderIcons.Resources.MainWindow.FileSystem, @"Resources\Images\FileSystem24.png");
				FileSystem lFileSystem = this.Application.FolderIconChanger.FileSystem;
				if (lFileSystem != null)
				{
					foreach (Drive lDrive in this.Application.FolderIconChanger.FileSystem.Drives)
					{
						TreeViewItem lDriveTreeViewItem = lTreeViewBuilder.AddItem(lFileSystemTreeViewItem, lDrive.ToString(), @"Resources\Images\Drive16.png");
						foreach (Folder lFolder in lDrive.Folders)
							lTreeViewBuilder.AddItem(lDriveTreeViewItem, lFolder.ToString(), @"Resources\Images\Folder16.png");
					}
					lTreeViewBuilder.SetItemHeader(lFileSystemTreeViewItem, lFileSystem.ToString());
				}
			}
		}

		/// <summary>
		/// Updating the window
		/// </summary>
		/// <param name="pActivity">Activity</param>
		protected void UpdateWindow(string pActivity)
		{
			this.LoadIconsRibbonButton.IsEnabled = (!this.Executing);
			this.LoadFileSystemRibbonButton.IsEnabled = (!this.Executing);
			this.ExecuteRibbonButton.IsEnabled = ((this.Application.IconsLoaded) && (this.Application.FileSystemLoaded) && (!this.Executing));
			this.ShowLogRibbonButton.IsEnabled = ((this.Application.Log != null) && (!this.Executing));
			this.ExitRibbonButton.IsEnabled = (!this.Executing);

			if (this.Application.IconsLoaded)
			{
				string lFileName = System.IO.Path.GetFileName(this.Application.FolderIconChanger.Icons.FilePath);
				this.IconsStatusBarTextBlock.Text = string.Format(JHJ.FolderIcons.Resources.MainWindow.IconsLoaded, lFileName);
			}
			else
				this.IconsStatusBarTextBlock.Text = JHJ.FolderIcons.Resources.MainWindow.NoIconsLoaded;

			if (this.Application.FileSystemLoaded)
			{
				string lFileName = System.IO.Path.GetFileName(this.Application.FolderIconChanger.FileSystem.FilePath);
				this.FileSystemStatusBarTextBlock.Text = string.Format(JHJ.FolderIcons.Resources.MainWindow.FileSystemLoaded, lFileName);
			}
			else
				this.FileSystemStatusBarTextBlock.Text = JHJ.FolderIcons.Resources.MainWindow.NoFileSystemLoaded;

			if (pActivity.Length > 0)
				this.ActivityStatusBarTextBlock.Text = pActivity;
			else
				this.ActivityStatusBarTextBlock.Text = JHJ.FolderIcons.Resources.MainWindow.Idle;

			this.InvalidateVisual();
			this.UpdateLayout();
		}

		/// <summary>
		/// Clearing counters
		/// </summary>
		protected void ClearCounters()
		{
			this.UpdateCounters(string.Empty, 0, 0, 0, 0, 0);
		}

		/// <summary>
		/// Clearing currently processing counter
		/// </summary>
		protected void ClearCurrentlyProcessing()
		{
			this.CurrentlyProcessingLabel.Content = string.Empty;
		}

		/// <summary>
		/// Updating counters
		/// </summary>
		/// <param name="pCurrentlyProcessing">Currently processing</param>
		/// <param name="pDrivesProcessed">Drives processed</param>
		/// <param name="pFoldersProcessed">Folders processed</param>
		/// <param name="pIconsAlreadyOk">Icons already OK</param>
		/// <param name="pIconsChanged">Icons changed</param>
		/// <param name="pErrors">Errors</param>
		protected void UpdateCounters(string pCurrentlyProcessing, int pDrivesProcessed, int pFoldersProcessed, int pIconsAlreadyOk,
			int pIconsChanged, int pErrors)
		{
			this.CurrentlyProcessingLabel.Content = pCurrentlyProcessing;
			this.DrivesProcessedLabel.Content = pDrivesProcessed.ToString();
			this.FoldersProcessedLabel.Content = pFoldersProcessed.ToString();
			this.IconsAlreadyOkLabel.Content = pIconsAlreadyOk.ToString();
			this.IconsChangedLabel.Content = pIconsChanged.ToString();
			this.ErrorsLabel.Content = pErrors.ToString();
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
			this.LoadDefaults();
		}

		/// <summary>
		/// On "Load Icons" ribbon button click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void LoadIconsRibbonButtonClick(object sender, RoutedEventArgs e)
		{
			this.LoadIcons();
		}

		/// <summary>
		/// On "Load File System" ribbon button click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void LoadFileSystemRibbonButtonClick(object sender, RoutedEventArgs e)
		{
			this.LoadFileSystem();
		}

		/// <summary>
		/// On "Execute" ribbon button click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ExecuteRibbonButtonClick(object sender, RoutedEventArgs e)
		{
			this.Execute();
		}

		/// <summary>
		/// On "Show Log" ribbon button click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ShowLogRibbonButtonClick(object sender, RoutedEventArgs e)
		{
			this.ShowLog();
		}

		/// <summary>
		/// On "Exit" ribbon button click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ExitRibbonButtonClick(object sender, RoutedEventArgs e)
		{
			this.Exit();
		}

		/// <summary>
		/// On "About" ribbon application menu item click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void AboutRibbonApplicationMenuItemClick(object sender, RoutedEventArgs e)
		{
			this.About();
		}

		/// <summary>
		/// On "Exit" ribbon application menu item click
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ExitRibbonApplicationMenuItemClick(object sender, RoutedEventArgs e)
		{
			this.Exit();
		}

		#endregion

		#region Application event handlers

		/// <summary>
		/// Application on counters change event handler
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ApplicationOnCountersChange(object sender, FolderIconChangerCountersEventArgs e)
		{
			this.UpdateCounters(e.Counters.CurrentlyProcessing, e.Counters.DrivesProcessed, e.Counters.FoldersProcessed,
				e.Counters.IconsAlreadyOk, e.Counters.IconsChanged, e.Counters.Errors);
		}

		#endregion

	}

}
