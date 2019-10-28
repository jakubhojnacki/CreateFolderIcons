/**
 * Class to keep information about image page
 * @class ImagePage
 * @version 1.0.1.0 JH 17/11/2016 Created
 * @version 1.0.3.0 JH 30/08/2018 Fixes 
 */

class ImagePage {
    /**
     * The constructor
     * @constructor
     * @param pImagePath: Image path
     * @param pXOffset: X (horizontal) offset 
     * @param pYOffset: Y (vertical) offset
     */
    constructor(pImagePath, pXOffset, pYOffset) {
        this.ImagePath = pImagePath;
        this.XOffset = pXOffset;
        this.YOffset = pYOffset;
    }
}

module.exports.ImagePage = ImagePage;