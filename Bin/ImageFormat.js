/**
 * Class to keep information about image format
 * @class ImageFormat
 * @version 1.0.1.0 JH 16/11/2016 Created
 * @version 1.0.3.0 JH 30/08/2018 Fixes
 */

class ImageFormat {
    /**
     * The constructor
     * @constructor
     * @param pSourceFileName: Source file name
     * @param pIndex: Index (in source file)
     * @param pDestinationFileName: Destination file name
     * @param pResizeTo: Size the image should be resized to
     * @param pMergeXOffset: Merge X offset
     * @param pMergeYOffset: Merge Y offset
     * @param pMergedFileName: Merged file name
     */
    constructor(pSourceFileName, pIndex, pDestinationFileName, pResizeTo, pMergeXOffset, pMergeYOffset, pMergedFileName) {
        this.SourceFileName = pSourceFileName;
        this.Index = pIndex;
        this.DestinationFileName = pDestinationFileName;
        this.ResizeTo = pResizeTo;
        this.MergeXOffset = pMergeXOffset;
        this.MergeYOffset = pMergeYOffset;
        this.MergedFileName = pMergedFileName;
    }
}

module.exports.ImageFormat = ImageFormat;