/**
 * Number extension
 * @class Number
 * @version 1.0.1.0 JH 2016-11-06 Created
 * @version 2.0.1.0 JH 2019-10-28 Node.JS version
 */

/**
 * Padding number with leading zeros
 * @param pLength: Length of padded string
 * @returns Final string
 */
Number.prototype.Pad = function (pLength) {
    var lNumberString = "00000000000000000000" + this;
    return lNumberString.substr(lNumberString.length - pLength);
}