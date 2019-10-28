/**
 * String class extensions
 * @class String
 * @version 1.0.1.0 JH 2016-11-05 Created
 * @version 2.0.1.0 JH 2019-10-28 Node.JS version
 */

/**
 * Trimming a string
 * @returns Final string
 */
String.prototype.Trim = function () {
    return this.replace(/^\s+|\s+$/g, '');
}

/**
 * Removing all single spaces from a string
 * @returns Final string
 */
String.prototype.RemoveSignleSpaces = function () {
    return this.replace(/\s/g, '');
}

/**
 * Removing all spaces from a string
 * @returns Final string
 */
String.prototype.RemoveSpaces = function () {
    return this.replace(/\s+/g, ' ');
};

/**
 * Formatting a string (replacing {0} .. {9} templates with argument values
 * @param arguments: Flexible argument list
 * @returns Final string
 */
String.prototype.Format = function (/*arguments*/) {
    var lString = this;
    var lArgumentIndex = 0;
    var lArgumentPlaceholder = "";
    var lArgumentValue = "";
    for (lArgumentIndex = 0; lArgumentIndex < arguments.length; lArgumentIndex++) {
        lArgumentPlaceholder = "{" + lArgumentIndex.toString() + "}";
        lArgumentValue = arguments[lArgumentIndex];
        if (lArgumentValue == null)
            lArgumentValue = "null";
        lString = lString.replace(lArgumentPlaceholder, lArgumentValue);
    }
    return lString;
}

/**
 * Returing if string starts with the string passed
 * @param pText: Starting phase text
 * @returns The answer
 */
String.prototype.StartsWith = function (pText) {
    return ((this.length >= pText.length) && (this.substr(0, pText.length) == pText));
}

/**
 * Returing if string ends with the string passed
 * @param pText: Endinig phase text
 * @returns The answer
 */
String.prototype.EndsWith = function (pText) {
    return ((this.length >= pText.length) && (this.substr(this.length - pText.length, pText.length) == pText));
}
