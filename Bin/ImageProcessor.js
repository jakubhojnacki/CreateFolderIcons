/**
 * Image processing engine
 * @class ImageProcessor
 * @version 1.0.1.0 JH 06/11/2016 Created
 * @version 1.0.2.0 JH 24/07/2018 "Run" method added
 * @version 1.0.3.0 JH 30/08/2018 Fixes
 */

const fs = require('fs');

class ImageProcessor {
    /**
     * Constructor</summary>
     * @constructor
     * @param pFolderPath: Image processor folder path
     * @param pConvertExecutable: "Convert" executable
     */
    constructor(pFolderPath, pConvertExecutable)
    {
        this.FolderPath = pFolderPath;
        this.ConvertPath = fs.Combine(this.FolderPath, pConvertExecutable);
    }

    /**
     * Resizing an image</summary>
     * @public
     * @param pSourceFilePath: Source file path
     * @param pDestinationFilePath: Destination file path
     * @param pWidth: New width of image</param->
     * @param pHeight: New height of image
     */
    ResizeImage(pSourceFilePath, pDestinationFilePath, pWidth, pHeight)
    {
        var lTemplate = "\"{0}\" \"{1}\" -resize {3}x{4} \"{2}\"";
        var lCommand = lTemplate.Format(this.ConvertPath, pSourceFilePath, pDestinationFilePath, pWidth, pHeight);
        this.Run(lCommand);	
    }

    /**
     * Extracting an image fro mmulti-image file</summary>
     * @public
     * @param pSourceFilePath: Source file path
     * @param pImageIndex: Image index within source file
     * @param pDestinationFilePath: Destination file path
     */
    ExtractImage(pSourceFilePath, pImageIndex, pDestinationFilePath)
    {
        var lTemplate = "\"{0}\" \"{1}\"[{2}] \"{3}\"";
        var lCommand = lTemplate.Format(this.ConvertPath, pSourceFilePath, pImageIndex, pDestinationFilePath);
        this.Run(lCommand);	
    }

    /**
     * Merging images together</summary>
     * @public
     * @param pImagePages: An array of image pages
     * @param pDestinationFilePath: Destination file path+
     */
    MergeImages(pImagePages, pDestinationFilePath)
    {
        var lTemplate = "\"{0}\" {1} -layers coalesce \"{2}\"";
        var lPageTemplate = "-page +{0}+{1} \"{2}\"";
        var lCommand = "";
        var lPageCommand = "";
        var lImagePage = null;
        var lImagePageIndex = 0;
        for (lImagePageIndex = 0; lImagePageIndex < pImagePages.length; lImagePageIndex++)
        {
            lImagePage = pImagePages[lImagePageIndex];
            if (lPageCommand.length > 0)
                lPageCommand += " ";
            lPageCommand += lPageTemplate.Format(lImagePage.XOffset, lImagePage.YOffset, lImagePage.ImagePath);
        }
        lCommand = lTemplate.Format(this.ConvertPath, lPageCommand, pDestinationFilePath);
        this.Run(lCommand);
    }

    /**
     * Combining images together to form a new multi-page image</summary>
     * @public
     * @param pImagePaths: An array of image paths
     * @param pDestinationFilePath: Destination file path
     */
    CombineImages(pImagePaths, pDestinationFilePath)
    {
        var lTemplate = "\"{0}\" {1} \"{2}\"";
        var lCommand = "";
        var lImagesCommand = "";
        var lImagePathIndex = 0;
        for (lImagePathIndex = 0; lImagePathIndex < pImagePaths.length; lImagePathIndex++)
        {
            if (lImagesCommand.length > 0)
                lImagesCommand += " ";
            lImagesCommand += "\"{0}\"".Format(pImagePaths[lImagePathIndex]);
        }
        lCommand = lTemplate.Format(this.ConvertPath, lImagesCommand, pDestinationFilePath);
        this.Run(lCommand);
    }

    /**
     * Running the passed command
     * @public
     * @param pCommand - The command
     */
    Run(pCommand)
    {
        this.Shell.run(pCommand, 0, true);
    }
}

module.exports.ImageProcessor = ImageProcessor;