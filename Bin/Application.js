/**
 * Folder icons class
 * @class FolderIcons
 * @version 1.0.1.0 JH 06/11/2016 Created
 * @version 1.0.2.0 JH 24/07/2018 New folder arrangment, fixes
 * @version 1.0.3.0 JH 30/08/2018 Fixes
 */

const ImagePage = require("./ImagePage.js");
const ImageProcessor = require("./ImageProcessor.js");
const Path = require("./Toolkit/Path.js");

class Application {
    /**
     * The constructor
     * @constructor
     * @param pImageProcessorFolderPath: Image processor folder path
     * @param pImageProcessorConvertExecutable: Image processor "Convert" executable name
     * @param pImageFormats: An array of image formats
     * @param pCleanupMask: Mask for files to be deleted after
     */
    constructor(pImageProcessorFolderPath, pImageProcessorConvertExecutable, pImageFormats, pCleanupMask) {
        // Constants
        this.IgnoreFoldersWithPrefix = ".";
        this.EmptyFolderName = this.IgnoreFoldersWithPrefix + "Empty";
        this.EmptyBackFileNameTemplate = "{0}B.{1}";
        this.EmptyFrontFileNameTemplate = "{0}F.{1}";
        this.FinalImageName = "FolderIcon.ico";
        this.FinalFolderName = this.IgnoreFoldersWithPrefix + "Final";

        // General properties
        this.ImageProcessor = new ImageProcessor(pImageProcessorFolderPath, pImageProcessorConvertExecutable);
        this.ImageFormats = pImageFormats;
        this.RootFolder = null;
        this.EmptyImageFolderPath = null;
        this.CleanupMask = pCleanupMask;

        // Internal properties
        this.Shell = new ActiveXObject("WScript.Shell");
        this.FileSystem = new ActiveXObject("Scripting.FileSystemObject");
    }

    /**
     * Running the engine
     * @public
     */
    Run() {
        this.Initialise();
        this.ProcessImages();
    }

    /**
     * Initialisation
     * @public
     */
    Initialise() {
        var lScriptFile = this.FileSystem.getFile(WScript.ScriptFullName);
        this.RootFolder = lScriptFile.ParentFolder;
        this.EmptyImageFolderPath = Path.Combine(this.RootFolder.path, this.EmptyFolderName);
    }

    /**
     * Processing images
     * @public
     */
    ProcessImages() {
        var lFolders = new Enumerator(this.RootFolder.subFolders);
        var lFolder = null;
        for (; !lFolders.atEnd(); lFolders.moveNext()) {
            lFolder = lFolders.item();
            if (!lFolder.name.StartsWith(this.IgnoreFoldersWithPrefix))
                this.ProcessImage(lFolder);
        }
    }

    /**
     * Processing one image
     * @public
     * @param pFolder: Image folder
     */
    ProcessImage(pFolder) {
        var lFolderPath = pFolder.path;
        var lImageFormatIndex = 0;
        var lImageFormat = null;
        var lMergedImagePath = "";
        var lImagePaths = [];
        for (lImageFormatIndex = 0; lImageFormatIndex < this.ImageFormats.length; lImageFormatIndex++) {
            lImageFormat = this.ImageFormats[lImageFormatIndex];
            lSourceFilePath = Path.Combine(lFolderPath, lImageFormat.SourceFileName);
            lDestinationFilePath = Path.Combine(lFolderPath, lImageFormat.DestinationFileName);
            this.ImageProcessor.ExtractImage(lSourceFilePath, lImageFormat.Index, lDestinationFilePath);
            this.ImageProcessor.ResizeImage(lDestinationFilePath, lDestinationFilePath, lImageFormat.ResizeTo, lImageFormat.ResizeTo);
            this.MergeImages(lImageFormat, lDestinationFilePath);
            lMergedImagePath = Path.Combine(lFolderPath, lImageFormat.MergedFileName);
            lImagePaths.push(lMergedImagePath);
        }
        this.CombineImages(lImagePaths, pFolder);
        this.Cleanup(pFolder.path);
        this.CopyToFinalFolder(pFolder);
    }

    /**
     * Merging images
     * @public
     * @param pImageFormat: Image format
     * @param pDestinationFilePath: Destination file path
     */
    MergeImages(pImageFormat, pDestinationFilePath) {
        var lEmptyBackFilePath = this.ConstructEmptyFilePath(pImageFormat, this.EmptyBackFileNameTemplate);
        var lEmptyFrontFilePath = this.ConstructEmptyFilePath(pImageFormat, this.EmptyFrontFileNameTemplate);
        var lImagePages =
            [
                new ImagePage(lEmptyBackFilePath, 0, 0),
                new ImagePage(pDestinationFilePath, pImageFormat.MergeXOffset, pImageFormat.MergeYOffset),
                new ImagePage(lEmptyFrontFilePath, 0, 0)
            ];
        this.ImageProcessor.MergeImages(lImagePages, pDestinationFilePath);
    }

    /**
     * Constructing empty file path
     * @public
     * @param pImageFormat: Image format
     * @param pTemplate: Template to use
     */
    ConstructEmptyFilePath(pImageFormat, pTemplate) {
        var lFileNameWithoutExtension = Path.GetFileNameWithoutExtension(pImageFormat.DestinationFileName);
        var lFileExtension = Path.GetExtension(pImageFormat.DestinationFileName);
        var lFileName = pTemplate.Format(lFileNameWithoutExtension, lFileExtension);
        var lFilePath = Path.Combine(this.EmptyImageFolderPath, lFileName);
        return lFilePath;
    }

    /**
     * Combining images
     * @public
     * @param pImagePaths: An array of image paths
     */
    CombineImages(pImagePaths, pFolder) {
        var lDestinationFilePath = Path.Combine(pFolder, this.FinalImageName);
        this.ImageProcessor.CombineImages(pImagePaths, lDestinationFilePath);
    }

    /**
     * Performing cleanups
     * @public
     * @param pFolderPath: Folder path to perform cleanups in
     */
    Cleanup(pFolderPath) {
        var lCleanupMask = Path.Combine(pFolderPath, this.CleanupMask);
        this.FileSystem.deleteFile(lCleanupMask, true);
    }

    /**
     * Copying final image to final folder
     * @public
     * @param pFolder: The folder
     */
    CopyToFinalFolder(pFolder) {
        var lFinalExtension = Path.GetExtension(this.FinalImageName);
        var lSourceFilePath = Path.Combine(pFolder, this.FinalImageName);
        var lDestinationFolderPath = Path.Combine(this.RootFolder.path, this.FinalFolderName);
        var lDestinationFileName = "{0}.{1}".Format(pFolder.name, lFinalExtension);
        var lDestinationFilePath = Path.Combine(lDestinationFolderPath, lDestinationFileName);
        this.FileSystem.copyFile(lSourceFilePath, lDestinationFilePath);
    }
}

module.exports.Application = Application;