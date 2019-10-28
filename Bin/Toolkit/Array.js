/**
 * Array class extensions
 * @class Array
 * @version 1.0.1.0 JH 2016-11-05 Created
 * @version 2.0.1.0 JH 2019-10-28 Node.JS version
 */

/**
 * Returning if array contains the value
 * @param pValue: The value
 * @returns The answer
 */
Array.prototype.Contains = function(pValue) 
{
	var lIndex = this.length;
	while (lIndex--) 
		if (this[lIndex] == pValue)
			return true;
	return false;
}
