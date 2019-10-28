/**
 * Path static class
 * @class Path
 * @version 1.0.1.0 JH 2016-11-05 Created
 * @version 2.0.1.0 JH 2019-10-28 Node.JS version
 */

class Path {
    /**
     * Constructor
     */
    constructor() {
    }

    /**
     * Extracting folder from path passed
     * @param pPath: The path
     * @returns The folder
     */
    static GetFolder(pPath) {
        var lIndex = pPath.lastIndexOf("\\");
        return lIndex >= 0 ? pPath.substr(0, lIndex) : pPath;
    }

    /**
     * Extracting file name from path passed
     * @param pPath: The path
     * @returns The file name
     */
    static GetFileName(pPath) {
        var lIndex = pPath.lastIndexOf("\\");
        return lIndex >= 0 ? pPath.substr(lIndex + 1) : pPath;
    }

    /**
     * Extracting file name without extension from path passed
     * @param pPath: The path
     * @returns The file name
     */
    static GetFileNameWithoutExtension(pPath) {
        var lFileName = Path.GetFileName(pPath);
        var lIndex = lFileName.lastIndexOf(".");
        return lIndex >= 0 ? lFileName.substr(0, lIndex) : lFileName;
    }

    /**
     * Extracting extension from path passed
     * @param pPath: The path
     * @returns The extension
     */
    static GetExtension(pPath) {
        var lFileName = Path.GetFileName(pPath);
        var lIndex = lFileName.lastIndexOf(".");
        return lIndex >= 0 ? lFileName.substr(lIndex + 1) : lFileName;
    }

    /**
     * Combining parts of path together
     * @param arguments: Variable list of arguments
     * @returns Path combined
     */
    static Combine(/*arguments*/)
    {
        var lCombinedPath = "";
        var lArgumentIndex = 0;
        var lArgumentValue = "";
        var lSeparator = "";
        for (lArgumentIndex = 0; lArgumentIndex < arguments.length; lArgumentIndex++) {
            if (lCombinedPath.length > 0) 
                if (lCombinedPath.substr(lCombinedPath.length - 1, 1) == "\\")
                    lCombinedPath = lCombinedPath.substr(0, lCombinedPath.length - 1);
            lArgumentValue = NotNull(arguments[lArgumentIndex], "");
            if (lArgumentValue.length > 0)
                if (lArgumentValue.substr(0, 1) == "\\")
                    lArgumentValue = lArgumentValue.substr(1);
            lSeparator = ((lCombinedPath.length > 0) && (lArgumentValue.length > 0)) ? "\\" : "";
            lCombinedPath += lSeparator + lArgumentValue;
        }
        return lCombinedPath;	
    }

    /**
     * Returning temporary folder
     * @returns The folder
     */
    static GetTemporaryFolder() {
        var lTemporaryFolder = 2;
        var lFileSystemObject = new ActiveXObject("Scripting.FileSystemObject");
        return lFileSystemObject.GetSpecialFolder(lTemporaryFolder);
    }

    /**
     * Returning temporary path for given extension
     * @param pExtension: The extension
     * @returns The path
     */
    static GetTemporaryPath(pExtension) {
        var lFolder = Path.GetTemporaryFolder();
        var lFileNameWithoutExtension = (new Date()).Format("yyyyMMddhhmmsszzz");
        var lFilePath = Path.Combine(lFolder, lFileNameWithoutExtension + "." + pExtension);
        var lFileIndex = 0;
        var lFileSystemObject = new ActiveXObject("Scripting.FileSystemObject");
        while (lFileSystemObject.fileExists(lFilePath)) {
            lFileIndex += 1;
            lFilePath = Path.Combine(lFolder, lFileNameWithoutExtension + "_" + lFileIndex.toString() + "." + pExtension);
        }
        return lFilePath;
    }
}

