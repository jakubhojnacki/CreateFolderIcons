using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Windows.Threading;

namespace JHJ.FolderIcons
{

	/// <summary>
	/// Folder icon changer class
	/// </summary>
	public class FolderIconChanger
	{

		#region Constants

		/// <summary>
		/// Configuration file name
		/// </summary>
		private const string ConfigurationFileName = "desktop.ini";

		/// <summary>
		/// Configuration file backup name
		/// </summary>
		private const string ConfigurationFileBackupName = "desktop.bak.ini";

		/// <summary>
		/// Configuration file section name
		/// </summary>
		private const string ConfigurationFileSecionName = ".ShellClassInfo";

		/// <summary>
		/// Configuration file property name
		/// </summary>
		private const string ConfigurationFilePropertyName = "IconResource";

		#endregion

		#region General properties

		/// <summary>
		/// Application
		/// </summary>
		protected TheApplication Application { get { return (TheApplication)System.Windows.Application.Current; } }

		/// <summary>
		/// Icons
		/// </summary>
		public Icons Icons { get; protected set; }

		/// <summary>
		/// File system
		/// </summary>
		public FileSystem FileSystem { get; protected set; }

		/// <summary>
		/// Counters
		/// </summary>
		public FolderIconChangerCounters Counters { get; protected set; }

		#endregion

		#region Events

		/// <summary>
		/// Icon set event
		/// </summary>
		public event EventHandler<IconsEventArgs> OnIconSet;
		public void TriggerOnIconSet() 
		{ 
			if (this.OnIconSet != null) 
				this.OnIconSet(this, new IconsEventArgs(this.Icons)); 
		}

		/// <summary>
		/// File system event
		/// </summary>
		public event EventHandler<FileSystemEventArgs> OnFileSystem;
		public void TriggerOnFileSystem()
		{
			if (this.OnFileSystem != null)
				this.OnFileSystem(this, new FileSystemEventArgs(this.FileSystem));
		}

		/// <summary>
		/// Drive event
		/// </summary>
		public event EventHandler<DriveEventArgs> OnDrive;
		public void TriggerOnDrive(Drive pDrive)
		{
			if (this.OnDrive != null)
				this.OnDrive(this, new DriveEventArgs(pDrive));
		}

		/// <summary>
		/// Folder event
		/// </summary>
		public event EventHandler<FolderEventArgs> OnFolder;
		public void TriggerOnFolder(Folder pFolder)
		{
			if (this.OnFolder != null)
				this.OnFolder(this, new FolderEventArgs(pFolder));
		}

		/// <summary>
		/// Folder status event
		/// </summary>
		public event EventHandler<FolderStatusEventArgs> OnFolderStatus;
		public void TriggerOnFolderStatus(FolderStatus pFolderStatus)
		{
			if (this.OnFolderStatus != null)
				this.OnFolderStatus(this, new FolderStatusEventArgs(pFolderStatus));
		}

		/// <summary>
		/// Error event
		/// </summary>
		public event EventHandler<ErrorEventArgs> OnError;
		public void TriggerOnError(Exception pException)
		{
			if (this.OnError != null)
				this.OnError(this, new ErrorEventArgs(pException));
		}

		/// <summary>
		/// Counters change event
		/// </summary>
		public event EventHandler<FolderIconChangerCountersEventArgs> OnCountersChange;
		public void TriggerOnCountersChange()
		{
			if (this.OnCountersChange != null)
				this.OnCountersChange(this, new FolderIconChangerCountersEventArgs(this.Counters));
		}

		#endregion

		#region Construction and destruction

		/// <summary>
		/// Standard constructor
		/// </summary>
		public FolderIconChanger()
		{
			this.Icons = null;
			this.FileSystem = null;
			this.Counters = new FolderIconChangerCounters();
		}

		#endregion

		#region General methods

		/// <summary>
		/// Loading icons
		/// </summary>
		/// <param name="pFilePath">File path</param>
		public void LoadIcons(string pFilePath)
		{
			this.Icons = XmlSerialiser<Icons>.ReadFromXmlFile(pFilePath);
			this.Icons.FilePath = pFilePath;
			this.TriggerOnIconSet();
		}

		/// <summary>
		/// Reading file system
		/// </summary>
		/// <param name="pFilePath">File path</param>
		public void LoadFileSystem(string pFilePath)
		{
			this.FileSystem = XmlSerialiser<FileSystem>.ReadFromXmlFile(pFilePath);
			this.FileSystem.FilePath = pFilePath;
			this.TriggerOnFileSystem();
		}

		/// <summary>
		/// Executing the folder icon changer
		/// </summary>
		public void Execute()
		{
			if (this.Icons == null)
				throw new Exception(Resources.FolderIconChanger.IconsNotLoaded);
			if (this.FileSystem == null)
				throw new Exception(Resources.FolderIconChanger.FileSystemNotLoaded);

			this.Counters = new FolderIconChangerCounters();

			this.Icons.BuildDictionary();

			BackgroundWorker lExecuteBackgroundWorker = new BackgroundWorker();
			lExecuteBackgroundWorker.WorkerReportsProgress = true;
			lExecuteBackgroundWorker.WorkerSupportsCancellation = false;
			lExecuteBackgroundWorker.DoWork += this.ExecuteBackgroundWorkerDoWork;
			lExecuteBackgroundWorker.ProgressChanged += this.ExecuteBackgroundWorkerProgressChanged;
			lExecuteBackgroundWorker.RunWorkerAsync();
		}

		#endregion

		#region Background worker event handlers

		/// <summary>
		/// On execute background worker do work
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ExecuteBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
		{
			this.ProcessDrives((BackgroundWorker)sender);
		}

		/// <summary>
		/// On execute background worker progress changed
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ExecuteBackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.UserState == null)
				return;
			if (!(e.UserState is FolderIconChangerState))
				return;

			FolderIconChangerState lState = (FolderIconChangerState)e.UserState;
			bool lWaitForIntefaceUpdate = false;

			switch (lState.ContentType)
			{
				case FolderIconChangerStateContentType.Drive:
					{ 
						Drive lDrive = (Drive)lState.Content;
						this.TriggerOnDrive(lDrive);
						this.Counters.CurrentlyProcessing = lDrive.ToString();
						this.Counters.DrivesProcessed += 1;
						this.TriggerOnCountersChange();
						lWaitForIntefaceUpdate = true;
					}
					break;
				case FolderIconChangerStateContentType.Folder:
					{ 
						Folder lFolder = (Folder)lState.Content;
						this.TriggerOnFolder(lFolder);
						this.Counters.CurrentlyProcessing = lFolder.ToString();
						this.Counters.FoldersProcessed += 1;
						this.TriggerOnCountersChange();
						lWaitForIntefaceUpdate = true;
					}
					break;
				case FolderIconChangerStateContentType.FolderStatus:
					{ 
						FolderStatus lFolderStatus = (FolderStatus)lState.Content;
						this.TriggerOnFolderStatus(lFolderStatus);
						if (lFolderStatus.IconAlreadyOk)
							this.Counters.IconsAlreadyOk += 1;
						else if (lFolderStatus.IconChanged)
							this.Counters.IconsChanged += 1;
						this.TriggerOnCountersChange();
						lWaitForIntefaceUpdate = true;
					}
					break;
				case FolderIconChangerStateContentType.Error:
					{
						this.TriggerOnError((FolderIconsException)lState.Content);
						this.Counters.Errors += 1;
						this.TriggerOnCountersChange();
						lWaitForIntefaceUpdate = true;
					}
					break;
			}

			//^^^if (lWaitForIntefaceUpdate)
			//^^^	Dispatcher.CurrentDispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Processing drives
		/// </summary>
		protected void ProcessDrives(BackgroundWorker pBackgroundWorker)
		{
			foreach (Drive lDrive in this.FileSystem.Drives)
			{
				pBackgroundWorker.ReportProgress(0, new FolderIconChangerState(FolderIconChangerStateContentType.Drive, lDrive));
				DriveInfo lDriveInfo = new DriveInfo(lDrive.Name);
				this.ProcessFolder(pBackgroundWorker, lDrive, lDriveInfo.RootDirectory, null, 0, 0);
			}
		}

		/// <summary>
		/// Processing a folder
		/// </summary>
		/// <param name="pBackgroundWorker">Background worker</param>
		/// <param name="pDrive">Drive</param>
		/// <param name="pDirectoryInfo">The directory info</param>
		/// <param name="pParentFolderStatus">Parent folder status</param>
		/// <param name="pDepth">Depth</param>
		/// <param name="pSubFolderDepth">Sub-folder depth</param>
		protected void ProcessFolder(BackgroundWorker pBackgroundWorker, Drive pDrive, DirectoryInfo pDirectoryInfo, 
			FolderStatus pParentFolderStatus, int pDepth, int pSubFolderDepth)
		{
			if (!FolderIconChanger.FolderIsToBeProcessed(pDirectoryInfo))
				return;

			pBackgroundWorker.ReportProgress(0, new FolderIconChangerState(FolderIconChangerStateContentType.Folder, 
				new Folder() { Name = pDirectoryInfo.FullName }));

			string lFolderPath = FileSystemToolkit.GetPathWithoutDrive(pDirectoryInfo.FullName);
			string lFolderPathIdentifier = FileSystemToolkit.GetPathIdentifier(lFolderPath);
			Folder lFolder = pDrive.Folders.FirstOrDefault<Folder>(pFolder => FileSystemToolkit.GetPathIdentifier(pFolder.Name) == lFolderPathIdentifier);
			FolderStatus lFolderStatus = new FolderStatus(pDirectoryInfo.FullName);
			if (pDepth == 0)
			{ 
				lFolderStatus.SubFolderProcessing = SubFolderProcessing.Icons;
				lFolderStatus.SubFolderDepth = 1;
			}
			if (lFolder != null)
			{
				lFolderStatus.Icon = lFolder.Icon;
				lFolderStatus.SubFolderProcessing = lFolder.SubFolderProcessing;
				lFolderStatus.SubFolderDepth = lFolder.SubFolderDepth;
				pSubFolderDepth = 0;
			}
			else if (pParentFolderStatus != null)
			{
				if (pParentFolderStatus.SubFolderProcessing == SubFolderProcessing.SameIcon)
				{ 
					lFolderStatus.Icon = pParentFolderStatus.Icon;
					lFolderStatus.IconPath = pParentFolderStatus.IconPath;
				}
				lFolderStatus.SubFolderProcessing = pParentFolderStatus.SubFolderProcessing;
				lFolderStatus.SubFolderDepth = pParentFolderStatus.SubFolderDepth;
			}
			
			if (lFolderStatus.Icon.Length == 0)
			{ 
				lFolderStatus.Icon = pDirectoryInfo.Name.Trim().ToLower().Replace(" ", "");
				lFolderStatus.IconPath = string.Empty;
			}
			if (pDepth > 0)
				this.ChangeFolderIcon(pBackgroundWorker, pDirectoryInfo, lFolderStatus);

			string lSubFoldersPattern = lFolderPathIdentifier + @"\";
			bool lProcessAllSubFolders = ((lFolderStatus.ProcessSubFolders) && ((lFolderStatus.SubFolderDepth < 0) || (lFolderStatus.SubFolderDepth > pSubFolderDepth)));
			bool lProcessSomeSubFolders = ((lProcessAllSubFolders) || (pDrive.Folders.Any<Folder>(pFolder => pFolder.Name.Trim().ToLower().StartsWith(lSubFoldersPattern))));
			bool lProcessSubFolder = false;
			string lSubFolderPathIdentifier = string.Empty;
			if ((lProcessAllSubFolders) || (lProcessSomeSubFolders))
				foreach (DirectoryInfo lSubDirectoryInfo in pDirectoryInfo.GetDirectories())
				{
					lProcessSubFolder = lProcessAllSubFolders;
					if (!lProcessSubFolder)
					{
						lSubFolderPathIdentifier = FileSystemToolkit.GetPathIdentifier(FileSystemToolkit.GetPathWithoutDrive(lSubDirectoryInfo.FullName));
						lProcessSubFolder = (pDrive.Folders.FirstOrDefault<Folder>(pFolder => FileSystemToolkit.GetPathIdentifier(pFolder.Name) == lSubFolderPathIdentifier) != null);
					}
					if (lProcessSubFolder)
						this.ProcessFolder(pBackgroundWorker, pDrive, lSubDirectoryInfo, lFolderStatus, pDepth + 1, pSubFolderDepth + 1);
				}
		}

		/// <summary>
		/// Checking whether the folder is to be processed
		/// </summary>
		/// <param name="pDirectoryInfo">Directory info of the folder</param>
		/// <returns>The answer</returns>
		private static bool FolderIsToBeProcessed(DirectoryInfo pDirectoryInfo)
		{
			if (pDirectoryInfo == null)
				return false;
			if ((pDirectoryInfo.Attributes.HasFlag(FileAttributes.Hidden)) && (pDirectoryInfo.Parent != null))
				return false;

			try
			{ 
				DirectorySecurity lDirectorySecurity = Directory.GetAccessControl(pDirectoryInfo.FullName);
				pDirectoryInfo.GetDirectories();
			}
			catch (UnauthorizedAccessException)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Changing folder icon
		/// </summary>
		/// <param name="pBackgroundWorker">Background worker</param>
		/// <param name="pDirectoryInfo">Directory info</param>
		/// <param name="pFolderStatus">Folder status</param>
		protected void ChangeFolderIcon(BackgroundWorker pBackgroundWorker, DirectoryInfo pDirectoryInfo, FolderStatus pFolderStatus)
		{
			try
			{ 
				if (this.FindIcon(pFolderStatus))
					this.ChangeFolderConfigurationFile(pDirectoryInfo, pFolderStatus);
				pBackgroundWorker.ReportProgress(0, new FolderIconChangerState(FolderIconChangerStateContentType.FolderStatus, 
					pFolderStatus));
			}
			catch (Exception lException)
			{
				FolderIconsException lFolderIconsException = new FolderIconsException(lException);
				pBackgroundWorker.ReportProgress(0, new FolderIconChangerState(FolderIconChangerStateContentType.Error, lFolderIconsException));
			}
		}

		/// <summary>
		/// Trying to find an icon to apply it to a folder
		/// </summary>
		/// <param name="pFolderStatus">Folder status</param>
		protected bool FindIcon(FolderStatus pFolderStatus)
		{
			bool lResult = (pFolderStatus.IconPath.Length > 0);
			if (!lResult)
			{ 
				string lIconKeyword = pFolderStatus.Icon.Trim().ToLower();
				if (lIconKeyword.Length > 0)
					if (this.Icons.Dictionary.ContainsKey(lIconKeyword))
					{
						Icon lIcon = this.Icons.Dictionary[lIconKeyword];
						pFolderStatus.IconPath = this.Icons.IconPath(lIcon);
						lResult = true;
					}
			}
			return lResult;
		}

		/// <summary>
		/// Changing folder configuration file
		/// </summary>
		/// <param name="pBackgroundWorker">Background worker</param>
		/// <param name="pDirectoryInfo">Directory info</param>
		/// <param name="pFolderStatus">Folder status</param>
		protected void ChangeFolderConfigurationFile(DirectoryInfo pDirectoryInfo, FolderStatus pFolderStatus)
		{
			if (pFolderStatus.IconPath.Length == 0)
				return;

			string lIconPropertyValue = Icons.IconConfigurationFilePropertyValue(pFolderStatus.IconPath);
			string lConfigurationFilePath = Path.Combine(pDirectoryInfo.FullName, FolderIconChanger.ConfigurationFileName);
			string lConfigurationFileBackupPath = Path.Combine(pDirectoryInfo.FullName, FolderIconChanger.ConfigurationFileBackupName);
			ConfigurationFileSerialiser lConfigurationFileSerialiser = new ConfigurationFileSerialiser();
			ConfigurationFile lConfigurationFile = null;
			bool lNewConfigurationFile = false;
			bool lSaveConfigurationFile = false;

			if (File.Exists(lConfigurationFilePath))
				lConfigurationFile = lConfigurationFileSerialiser.Read(lConfigurationFilePath);
			else
			{
				lConfigurationFile = new ConfigurationFile();
				lNewConfigurationFile = true;
			}

			lConfigurationFile.SetSection(FolderIconChanger.ConfigurationFileSecionName);

			if (lNewConfigurationFile)
			{
				lConfigurationFile.SetProperty(FolderIconChanger.ConfigurationFilePropertyName, lIconPropertyValue);
				lSaveConfigurationFile = true;
			}
			else
			{
				string lPropertyValue = lConfigurationFile.GetProperty(FolderIconChanger.ConfigurationFilePropertyName);
				if (lPropertyValue != lIconPropertyValue)
				{
					lConfigurationFile.SetProperty(FolderIconChanger.ConfigurationFilePropertyName, lIconPropertyValue);
					lSaveConfigurationFile = true;
				}
				else
					pFolderStatus.IconAlreadyOk = true;
			}

			if (lSaveConfigurationFile)
			{
				FileAttributes lConfigurationFileAttributes = FileAttributes.Archive | FileAttributes.Hidden | FileAttributes.System;
				if (!lNewConfigurationFile)
				{
					if (File.Exists(lConfigurationFileBackupPath))
						File.Delete(lConfigurationFileBackupPath);
					File.Copy(lConfigurationFilePath, lConfigurationFileBackupPath);
					lConfigurationFileAttributes = File.GetAttributes(lConfigurationFilePath);
					File.SetAttributes(lConfigurationFilePath, FileAttributes.Normal);
				}
				lConfigurationFileSerialiser.Write(lConfigurationFile, lConfigurationFilePath);
				File.SetAttributes(lConfigurationFilePath, lConfigurationFileAttributes);

				pFolderStatus.IconChanged = true;
			}

			FileAttributes lFolderAttributes = pDirectoryInfo.Attributes;
			FileAttributes lFolderNewAttributes = lFolderAttributes | FileAttributes.ReadOnly;
			if (lFolderAttributes != lFolderNewAttributes)
				File.SetAttributes(pDirectoryInfo.FullName, lFolderNewAttributes);
		}

		#endregion

	}

}
