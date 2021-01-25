using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace JHJ.FolderIcons
{
	/// <summary>
	/// The application
	/// </summary>
	public partial class TheApplication : Application
	{

		#region Constantas

		/// <summary>
		/// Default icons path
		/// </summary>
		private const string DefaultIconsPath = @"Setup\Icons.xml";

		/// <summary>
		/// Default file system path
		/// </summary>
		private const string DefaultFileSystemPath = @"Setup\FileSystem.xml";

		#endregion

		#region General properties

		/// <summary>
		/// Arguments
		/// </summary>
		public Arguments Arguments { get; protected set; }

		/// <summary>
		/// Folder icon changer
		/// </summary>
		public FolderIconChanger FolderIconChanger { get; protected set; }

		/// <summary>
		/// Returning if icons have been loaded
		/// </summary>
		public bool IconsLoaded { get { return this.FolderIconChanger != null ? this.FolderIconChanger.Icons != null : false; } }

		/// <summary>
		/// Returning if file system has been loaded
		/// </summary>
		public bool FileSystemLoaded { get { return this.FolderIconChanger != null ? this.FolderIconChanger.FileSystem != null : false; } }

		/// <summary>
		/// Log
		/// </summary>
		public Log Log { get; protected set; }

		#endregion

		#region Events

		/// <summary>
		/// Counters change event
		/// </summary>
		public event EventHandler<FolderIconChangerCountersEventArgs> OnCountersChange;
		public void TriggerOnCountersChange(FolderIconChangerCounters pCounters)
		{
			if (this.OnCountersChange != null)
				this.OnCountersChange(this, new FolderIconChangerCountersEventArgs(pCounters));
		}

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		public TheApplication()
			: base()
		{
			this.Arguments = new Arguments();
			this.FolderIconChanger = null;
			this.Log = null;
		}

		#endregion

		#region General methods

		/// <summary>
		/// Loading defaults (icons, file system)
		/// </summary>
		public void LoadDefaults()
		{
			this.LoadIcons(ApplicationToolkit.ApplicationFilePath(TheApplication.DefaultIconsPath), false);
			this.LoadFileSystem(ApplicationToolkit.ApplicationFilePath(TheApplication.DefaultFileSystemPath), false);
		}

		/// <summary>
		/// Loading icons
		/// </summary>
		/// <param name="pFilePath">File path</param>
		/// <param name="pErrorIfFileDoesntExist">Flag to throw an error if file doesn't exist</param>
		public void LoadIcons(string pFilePath, bool pErrorIfFileDoesntExist)
		{
			pFilePath = ApplicationToolkit.VerifyFilePath(pFilePath);
			if (TheApplication.CheckIfFileExists(pFilePath, pErrorIfFileDoesntExist))
			{ 
				this.InitialiseFolderIconChanger();
				this.Log = null;
				this.FolderIconChanger.LoadIcons(pFilePath);
			}
		}

		/// <summary>
		/// Loading file system
		/// </summary>
		/// <param name="pFilePath">File path</param>
		/// <param name="pErrorIfFileDoesntExist">Flag to throw an error if file doesn't exist</param>
		public void LoadFileSystem(string pFilePath, bool pErrorIfFileDoesntExist)
		{
			pFilePath = ApplicationToolkit.VerifyFilePath(pFilePath);
			if (TheApplication.CheckIfFileExists(pFilePath, pErrorIfFileDoesntExist))
			{ 
				this.InitialiseFolderIconChanger();
				this.Log = null;
				this.FolderIconChanger.LoadFileSystem(pFilePath);
			}
		}

		/// <summary>
		/// Executing folder icon changer
		/// </summary>
		public void Execute()
		{
			this.CheckFolderIconChanger();
			this.Log = new Log();
			this.FolderIconChanger.Execute();
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Initialising folder icon changer
		/// </summary>
		protected void InitialiseFolderIconChanger()
		{
			if (this.FolderIconChanger == null)
			{ 
				this.FolderIconChanger = new FolderIconChanger();
				this.FolderIconChanger.OnDrive += FolderIconChangerOnDrive;
				this.FolderIconChanger.OnFolder += FolderIconChangerOnFolder;
				this.FolderIconChanger.OnFolderStatus += FolderIconChangerOnFolderStatus;
				this.FolderIconChanger.OnError += FolderIconChangerOnError;
				this.FolderIconChanger.OnCountersChange += FolderIconChangerOnCountersChange;
			}
		}

		/// <summary>
		/// Checking folder icon changer
		/// </summary>
		protected void CheckFolderIconChanger()
		{
			//TODO Check if everything is OK here
		}

		/// <summary>
		/// Checking if file exists and - if not and the "pErrorIfFileDoesntExist" is set - throwing an error
		/// </summary>
		/// <param name="pFilePath">File path</param>
		/// <param name="pErrorIfFileDoesntExist">The flag</param>
		/// <returns>Answer</returns>
		private static bool CheckIfFileExists(string pFilePath, bool pErrorIfFileDoesntExist)
		{
			bool lFileExists = File.Exists(pFilePath);
			if ((!lFileExists) && (pErrorIfFileDoesntExist))
				throw new Exception(string.Format(JHJ.FolderIcons.Resources.TheApplication.FileDoesntExist, pFilePath));
			return lFileExists;
		}

		#endregion

		#region Folder icon changer event handlers

		/// <summary>
		/// Folder icon changer "On Drive" event handler
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void FolderIconChangerOnDrive(object sender, DriveEventArgs e)
		{
			this.Log.Add(e.Drive.ToString());
		}

		/// <summary>
		/// Folder icon changer "On Folder" event handler
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void FolderIconChangerOnFolder(object sender, FolderEventArgs e)
		{
			this.Log.Add(e.Folder.ToString());
		}

		/// <summary>
		/// Folder icon changer "On Folder Status" event handler
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void FolderIconChangerOnFolderStatus(object sender, FolderStatusEventArgs e)
		{
			FolderStatus lFolderStatus = e.FolderStatus;
			string lAction = string.Empty;
			if (lFolderStatus.IconAlreadyOk)
				lAction = FolderIcons.Resources.TheApplication.IconAlready;
			else if (lFolderStatus.IconChanged)
				lAction = FolderIcons.Resources.TheApplication.IconChanged;
			if (lAction.Length > 0)
				this.Log.Add("  " + string.Format(lAction, e.FolderStatus.Icon));
		}

		/// <summary>
		/// Folder icon changer "On Folder Status" event handler
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void FolderIconChangerOnError(object sender, ErrorEventArgs e)
		{
			this.Log.Add(string.Format("ERROR!!! {0}", e.ErrorMessage));
		}

		/// <summary>
		/// Folder icon changer "On Counters Change" event handler
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void FolderIconChangerOnCountersChange(object sender, FolderIconChangerCountersEventArgs e)
		{
			this.TriggerOnCountersChange(e.Counters);
		}
		
		#endregion

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			// hook on error before app really starts
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			//base.OnStartup(e);
		}

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			// put your tracing or logging code here (I put a message box as an example)
			MessageBox.Show(e.ExceptionObject.ToString());
		}
	}

}
