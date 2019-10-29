/**
 * Main entry starting icons creation
 * @version 1.0.1.0 JH 05/11/2016 Created
 * @version 1.0.2.0 JH 24/07/2018 New folder arrangment, fixes
 * @version 1.0.3.0 JH 30/08/2018 Fixes
 * @version 2.0.1.0 JH 28/10/2019 Node.JS version
 */

const Application = require("./Bin/Application.js").Application;
const ImageFormat = require("./Bin/ImageFormat.js").ImageFormat;

(function Main() {
    let ImageProcessorFolderPath = "";
    let ImageProcessorConvertExecutable = "";
    let UseBatch = false;
    
    if (UseBatch) 
    {
        ImageProcessorFolderPath = "C:\\Development\\JavaScript\\CreateFolderIcons\\.Engine";
        ImageProcessorConvertExecutable = "convert.cmd";
    }
    else
    {
        ImageProcessorFolderPath = "C:\\Program Files\\ImageMagick";
        ImageProcessorConvertExecutable = "convert.exe";
    }

    const ImageFormats = 
    [
        new ImageFormat("Image.ico", 0, "256.png", 192, 48, 32, "256-2.png"),
        new ImageFormat("Image.ico", 1, "128.png", 96, 24, 16, "128-2.png"),
        new ImageFormat("Image.ico", 2, "064.png", 48, 12, 8, "064-2.png"),
        new ImageFormat("Image.ico", 3, "048.png", 36, 9, 6, "048-2.png"),
        new ImageFormat("Image.ico", 4, "032.png", 24, 6, 4, "032-2.png"),
        new ImageFormat("Image.ico", 5, "024.png", 18, 5, 3, "024-2.png"),
        new ImageFormat("Image.ico", 6, "016.png", 12, 3, 2, "016-2.png")
    ];

    let TheApplication = new Application(ImageProcessorFolderPath, ImageProcessorConvertExecutable, ImageFormats, "*.png");
    TheApplication.Run();
})();