/**
 * Date extension
 * @class Date
 * @version 1.0.1.0 JH 2016-11-06 Created
 * @version 2.0.1.0 JH 2019-10-28 Node.JS version
 */

/**
 * Formatting date using specified pattern
 * The function understands only "yyyy", "yy", "MM", "dd", "hh", "mm", "ss" and "zzz" placeholders
 * @param pPattern: Pattern
 * @returns Final string
 */
Date.prototype.Format = function(pPattern)
{
	var lYearText = this.getFullYear().Pad(4);
	var lMonthText = (this.getMonth() + 1).Pad(2);
	var lDayText = this.getDay().Pad(2);
	var lHoursText = this.getHours().Pad(2);
	var lMinutesText = this.getMinutes().Pad(2);
	var lSecondsText = this.getSeconds().Pad(2);
	var lMillisecondsText = this.getMilliseconds().Pad(2);
	var lDateText = pPattern;
	lDateText = lDateText.replace("yyyy", lYearText);
	lDateText = lDateText.replace("yy", lYearText.substr(2));
	lDateText = lDateText.replace("MM", lMonthText);
	lDateText = lDateText.replace("dd", lDayText);
	lDateText = lDateText.replace("hh", lHoursText);
	lDateText = lDateText.replace("mm", lMinutesText);
	lDateText = lDateText.replace("ss", lSecondsText);
	lDateText = lDateText.replace("zzz", lMillisecondsText);
	return lDateText;
}