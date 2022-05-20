using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SP_DAO_Generator.Utils
{
    public class StringUtil
    {
        public static string StripNonNumericChars(string input)
        {
            string output = "";

            if (input != null)
            {
                output = Regex.Replace(input, "[^0-9]", "");
            }

            return output;

        }

        public static DateTime GetDateTimeBoundary(DateTime input, string bound)
        {
            DateTime output = input;

            if (bound.Equals("MIN"))
                output = new DateTime(input.Year, input.Month, input.Day, 0, 0, 0);

            if (bound.Equals("MAX"))
                output = new DateTime(input.Year, input.Month, input.Day, 23, 59, 59);

            return output;
        }

        public static DateTime GetMilitaryTime(string time, int year, int month, int day)
        {
            //
            // Convert hour part of string to integer.
            //
            string hour = time.Substring(0, 2);
            int hourInt = int.Parse(hour);

            if (time.Equals("2400"))
            {
                return new DateTime(year, month, day, 0, 0, 0);
            }

            if (hourInt > 24)
            {
                throw new ArgumentOutOfRangeException("Invalid hour");
            }



            //
            // Convert minute part of string to integer.
            //
            string minute = time.Substring(2, 2);
            int minuteInt = int.Parse(minute);
            if (minuteInt >= 60)
            {
                throw new ArgumentOutOfRangeException("Invalid minute");
            }
            //
            // Return the DateTime.
            //
            return new DateTime(year, month, day, hourInt, minuteInt, 0);
        }

        public static string GetCreditCardHiddenNumber(string ccNumber)
        {
            string val = "";

            if (IsNullOrEmpty(ccNumber))
                return "";

            if (ccNumber.Length > 4)
            {
                //make all digits * except for last 4
                string firstPart = ccNumber.Substring(0, ccNumber.Length - 4);
                string secondPart = ccNumber.Substring(ccNumber.Length - 4);

                string stars = "";

                for (int i = 1; i <= firstPart.Length; i++)
                {
                    stars += "*";
                }

                val = stars + secondPart;
            }

            return val;
        }

        public static string[] SplitItem(string item)
        {
            List<string> items = new List<string>();

            if (!StringUtil.IsNullOrEmpty(item))
            {
                string[] itemComma = null;
                bool foundDelim = false;

                //replace spaces with commas
                item = item.Replace(" ", ",");

                //see if we are dlimitted by comma
                if (item.IndexOf(",") > 0)
                {
                    itemComma = item.Split(new char[] { ',' });
                    foundDelim = true;
                }

                if (!foundDelim)
                {
                    //treat as a single PO
                    itemComma = new string[1];
                    itemComma[0] = item;
                }

                if (itemComma != null)
                {
                    foreach (string s in itemComma)
                    {
                        if (StringUtil.ToString(s).Length > 0)
                        {
                            string s1 = s.Replace(",", "");
                            items.Add(s1);
                        }
                    }
                }
            }

            return items.ToArray();

        }




        public static DateTime GetDateValue(string input)
        {
            DateTime ret = DateTime.MinValue;

            try
            {
                ret = Convert.ToDateTime(input);
            }
            catch //(Exception ex)
            {
            }

            return ret;
        }

        public static double GetDoubleValue(string val)
        {
            double ret = 0.00;

            if (!IsNullOrEmpty(val))
            {
                if (IsNumeric(val))
                {
                    ret = Convert.ToDouble(ToString(val));
                }
            }

            return ret;

        }

        public static string GetLongDisplay(long val, bool zeroIsBlank)
        {
            string ret = "";

            if (val != -1)
            {
                ret = val.ToString();
            }

            if ((val == 0 || val == -1) && zeroIsBlank)
            {
                ret = "";
            }

            return ret;
        }

        public static long GetLongValue(string val)
        {
            long ret = 0;

            if (!IsNullOrEmpty(val))
            {
                if (IsNumeric(val))
                {
                    try
                    {
                        if (val.IndexOf(".") > 0)
                        {
                            int index = val.IndexOf(".");
                            val = val.Substring(0, index);
                        }

                        ret = Convert.ToInt64(ToString(val));
                    }
                    catch //(Exception ex)
                    {

                    }
                }
            }

            return ret;
        }

        public static int GetIntValue(string val)
        {
            int ret = 0;

            if (!IsNullOrEmpty(val))
            {
                if (IsNumeric(val))
                {
                    try
                    {
                        if (val.IndexOf(".") > 0)
                        {
                            int index = val.IndexOf(".");
                            val = val.Substring(0, index);
                        }

                        ret = Convert.ToInt32(ToString(val));
                    }
                    catch //(Exception ex)
                    {

                    }
                }
            }

            return ret;
        }

        public static string GetDoubleDisplay(double val, bool zeroIsBlank)
        {
            string ret = "";

            if (val != -1)
            {
                ret = val.ToString("#####.00");
            }

            if (val == 0)
                ret = "";

            return ret;
        }

        public static string GetDoubleDisplay(double val)
        {
            string ret = "";

            if (val != -1)
            {
                ret = val.ToString("#####.00");
            }

            return ret;
        }

        public static string GetIntDisplay(int val, bool zeroIsBlank)
        {
            string ret = "";

            if (val != -1)
            {
                ret = val.ToString();
            }

            if ((val == 0 || val == -1) && zeroIsBlank)
            {
                ret = "";
            }

            return ret;
        }

        public static string GetYesNo(bool val)
        {
            string ret = "No";

            if (val)
            {
                ret = "Yes";
            }

            return ret;
        }

        public static double ToDouble(string input)
        {
            double val = 0.00;

            try
            {
                val = Convert.ToDouble(ToString(input));
            }
            catch //(Exception ex)
            {
            }

            return val;
        }

        public static ArrayList GetArrayListOfInt(int[] input)
        {
            ArrayList list = new ArrayList();

            if (input != null)
            {
                foreach (int i in input)
                {
                    list.Add(i);
                }
            }

            return list;
        }

        /// <summary>
        /// Get the sequence of indexes to sort array of objects. e.g. input: 5,20,8,15 Output: 1,3,4,2
        /// </summary>
        /// <param name="input">array of numbers to be sorted</param>
        /// <param name="asc">omit or true to sort asc; false to sort desc</param>
        /// <returns></returns>
        public static int[] GetSortOrder(int[] input, bool asc)
        {
            int minNum = 0;
            int multLen = input.Length * 10;

            for (int i = 0; i < input.Length; i++)
                if (input[i] < minNum)
                    minNum = input[i];

            int[] sorted = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
                sorted[i] = (input[i] - minNum) * multLen + i;

            ArrayList ar = GetArrayListOfInt(sorted);
            ar.Sort();

            if (!asc) ar.Reverse();

            int[] output = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
                Math.DivRem((int)ar[i], multLen, out output[i]);

            return output;
        }

        /// <summary>
        /// Get the sequence of indexes to sort array of objects. e.g. input: 5,20,8,15 Output: 1,3,4,2
        /// </summary>
        /// <param name="input">array of numbers to be sorted</param>
        /// <returns></returns>
        public static int[] GetSortOrder(int[] input)
        {
            return GetSortOrder(input, true);
        }

        /// <summary>
        /// Same as SQL function ISNULL(checkVal, substVal): returns checkVal if it's not null, substVal if checkVal is null.
        /// </summary>
        /// <param name="inputObj">Object to check if it is null</param>
        /// <param name="outputObj">Object to return if inputObj is null</param>
        /// <returns></returns>
        public static object IsNull(object inputObj, object outputObj)
        {
            if (inputObj == null)
                return outputObj;
            else
                return inputObj;
        }

        public static bool IsNullOrEmpty(string inputValue)
        {
            bool isNullEmpty = false;

            if (inputValue == null)
            {
                isNullEmpty = true;
            }
            else
            {
                if (inputValue.Trim().Length == 0)
                {
                    isNullEmpty = true;
                }
            }

            return isNullEmpty;
        }

        public static string ToString(string input)
        {
            string val = "";

            if (input != null)
            {
                val = input.Trim();
            }

            return val;

        }

        public static bool IsNumeric(string val)
        {
            bool isNumeric = true;
            string valToCheck = "";

            if (IsNullOrEmpty(val))
            {
                isNumeric = false;
            }
            else
            {
                valToCheck = val.Trim();

                for (int i = 0; i < valToCheck.Length; i++)
                {
                    char c = valToCheck[i];
                    if (c == '.')
                    {
                        continue;
                    }

                    if (c == '$')
                    {
                        if (i != 0)
                        {
                            isNumeric = false;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }


                    if (!Char.IsNumber(c))
                    {
                        isNumeric = false;
                        break;
                    }
                }
            }

            return isNumeric;
        }


        public static bool IsMoney(string input, bool hasDollarSign)
        {
            bool isValid = true;

            if (IsNullOrEmpty(ToString(input)))
            {
                isValid = false;
            }
            else
            {
                //if has dollar sign 
                if (hasDollarSign)
                {
                    //dollar sign should be first char
                    if (input[0] != '$')
                    {
                        isValid = false;
                    }
                    else
                    {
                        //ensure last portion is numeric
                        string numericPortion = input.Substring(1);
                        if (!IsNumeric(input))
                        {
                            isValid = false;
                        }
                    }
                }
                else
                {
                    //does not have a dollar sign - ensure field is numeric
                    if (!IsNumeric(input))
                    {
                        isValid = false;
                    }
                }
            }

            return isValid;
        }

        public static string GetNumberSqlInClause(ArrayList inputList)
        {
            StringBuilder sb = new StringBuilder();

            if (inputList != null && inputList.Count > 0)
            {
                sb.Append("IN(");

                string temp = "";
                foreach (int item in inputList)
                {
                    temp += item.ToString() + ",";
                }

                //remove the last ,
                temp = temp.Substring(0, temp.Length - 1);
                sb.Append(temp);
                sb.Append(")");
            }

            return sb.ToString();
        }

        public static string GetDateDisplay(DateTime val, bool includeTime)
        {
            string output = "";

            if (val != null)
            {
                if (DateTime.MinValue != val)
                {
                    if (includeTime)
                    {
                        output = val.ToString("MM/dd/yyyy hh:mm tt");
                    }
                    else
                    {
                        output = val.ToString("MM/dd/yyyy");

                    }
                }
            }


            return output;
        }

        public static string StripChars(char charToStrip, string input)
        {
            string output = input;

            if (!IsNullOrEmpty(input))
            {
                output = input.Replace(charToStrip.ToString(), "");
            }

            return output;
        }



        public static string Format2DecLiteral(string input)
        {
            string output = "";

            if (input != null)
            {
                //get rid of any spaces on either side
                input = ToString(input);

                if (input.Length == 2)
                {
                    output = "." + input;
                }
                else
                {
                    if (input.Length > 2)
                    {
                        string decPart = input.Substring(input.Length - 2);
                        string forePart = input.Substring(0, input.Length - 2);
                        output = forePart + "." + decPart;
                    }
                }
            }

            return output;
        }
        public static string Format2Dec(string input)
        {
            string output = "";

            if (input != null)
            {
                //get rid of any spaces on either side
                input = ToString(input);
                output = input;

                try
                {
                    double dt = Convert.ToDouble(input);
                    output = dt.ToString("#####.00");
                }
                catch
                {

                }
            }

            return output;
        }

        public static string GetExportValue(string input)
        {
            string output = "";

            output = ToString(input);

            //wrap in quotes
            if (output.Contains(","))
            {
                //check to see if the input already is enclosed in quotes
                if (input.StartsWith('"'.ToString()) && input.EndsWith('"'.ToString()))
                {
                    //contains a comma and already enclosed in quotes
                    output = ToString(input);
                }
                else
                {
                    //enclose in quotes
                    output = '"'.ToString() + output + '"'.ToString();
                }
            }

            return output;
        }

        public static string Trim(string toTrim, int maxChars)
        {
            toTrim = ToString(toTrim);

            if (toTrim.Length > maxChars)
            {
                toTrim = toTrim.Substring(0, maxChars);
            }

            return toTrim;
        }

        /// <summary>
        /// Concatenates int array into comma-delimited string, with no spaces after comma
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string GetCommaDelimString(int[] items)
        {
            return GetCommaDelimString(items, false);
        }

        /// <summary>
        /// Concatenates int array into comma-delimited string, with or without spaces after comma
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpaces"></param>
        /// <returns></returns>
        public static string GetCommaDelimString(int[] items, bool withSpaces)
        {
            string ret = "";

            if (items != null)
            {
                if (items.Length > 0)
                {
                    foreach (int item in items)
                    {
                        ret += item.ToString() + ",";
                        if (withSpaces)
                            ret += " ";
                    }

                    ret = ret.Trim().Substring(0, ret.Trim().Length - 1);
                }
            }

            return ret;
        }

        /// <summary>
        /// Concatenates string array into comma-delimited string, with no spaces after comma
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string GetCommaDelimString(string[] items)
        {
            return GetCommaDelimString(items, false);
        }

        /// <summary>
        /// Concatenates string array into comma-delimited string, with or without spaces after comma
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpaces"></param>
        /// <returns></returns>
        public static string GetCommaDelimString(string[] items, bool withSpaces)
        {
            string ret = "";

            if (items != null)
            {
                if (items.Length > 0)
                {
                    foreach (string item in items)
                    {
                        ret += item + ",";
                        if (withSpaces)
                            ret += " ";
                    }

                    ret = ret.Trim().Substring(0, ret.Trim().Length - 1);
                }
            }

            return ret;
        }

        public static string RemoveLeadingChars(string input, string charToRemove)
        {
            string output = input;
            input = ToString(input);

            //remove leading zeros from po number            
            if (input.StartsWith(charToRemove))
            {
                string newValue = "";

                bool done = false;
                int iter = 1;

                while (!done && iter <= 10)
                {
                    if (output.StartsWith(charToRemove))
                    {
                        output = output.Substring(1);
                    }
                    else
                    {
                        done = true;
                    }
                }

                output = newValue;
            }


            return output;
        }

        public static string GetDateDisplay(string input)
        {
            string val = "";

            try
            {
                if (!IsNullOrEmpty(input))
                {
                    if (input.Length == 8)
                    {
                        int yr = Convert.ToInt32(input.Substring(0, 4));
                        int mnth = Convert.ToInt32(input.Substring(4, 2));
                        int day = Convert.ToInt32(input.Substring(6, 2));
                        DateTime dt = new DateTime(yr, mnth, day);
                        val = dt.ToString("MM/dd/yyyy");
                    }
                }

            }
            catch { }

            return val;
        }


        public static string GetSqlCommaDelimString(string[] items)
        {
            string ret = "";

            if (items != null)
            {
                if (items.Length > 0)
                {
                    foreach (string item in items)
                    {
                        ret += "'" + ToString(item) + "',";
                    }

                    ret = ret.Trim().Substring(0, ret.Trim().Length - 1);
                }
            }

            return ret;
        }

        public static string GenerateRandomNumber(int numLength)
        {
            Random rand = new Random();
            int val = 1111;

            int minValue = 1111;
            int maxValue = 9999;

            if (numLength == 3)
            {
                minValue = 111;
                maxValue = 999;
            }

            if (numLength == 5)
            {
                minValue = 11111;
                maxValue = 99999;
            }

            if (numLength == 6)
            {
                minValue = 111111;
                maxValue = 999999;
            }

            val = rand.Next(minValue, maxValue);

            return val.ToString();

        }

        public static decimal ToDecimal(string input)
        {
            decimal output = -1;

            if (!IsNullOrEmpty(input))
            {
                try
                {
                    output = Convert.ToDecimal(input);
                }
                catch { }
            }

            return output;
        }

        public static int GetListIntValue(string val)
        {
            int ret = -1;

            if (!IsNullOrEmpty(val))
            {
                if (IsNumeric(val))
                {
                    try
                    {
                        if (val.IndexOf(".") > 0)
                        {
                            int index = val.IndexOf(".");
                            val = val.Substring(0, index);
                        }

                        ret = Convert.ToInt32(ToString(val));
                    }
                    catch //(Exception ex)
                    {

                    }
                }
            }

            return ret;
        }

        public static DateTime GetDate(string input, string format)
        {
            DateTime val = DateTime.MinValue;

            try
            {
                val = Convert.ToDateTime(input);
            }
            catch
            {
                val = DateTime.MinValue;
            }

            return val;
        }

        public static string FormatSSN(string val)
        {
            string outPut = "";

            if (val != null)
            {
                if (val.Length == 9)
                {
                    outPut = val.Substring(0, 3) + "-" + val.Substring(3, 2) + "-" + val.Substring(5);
                }
            }


            return outPut;
        }

        public static DateTime GetLastDateOfMonth(int year, int month)
        {
            //create a seed date
            DateTime dtDate = new DateTime(year, month, 5, 0, 0, 0);

            // set return value to the last day of the month
            // for any date passed in to the method

            // create a datetime variable set to the passed in date
            DateTime dtTo = dtDate;

            // overshoot the date by a month
            dtTo = dtTo.AddMonths(1);

            // remove all of the days in the next month
            // to get bumped down to the last day of the
            // previous month
            dtTo = dtTo.AddDays(-(dtTo.Day));

            // return the last day of the month
            return dtTo;
        }

        public static string GetQuotedExportValue(string input)
        {
            string output = '"' + "" + '"'; ;

            if (!IsNullOrEmpty(input))
            {
                output = '"' + input.Trim() + '"';
            }

            return output;
        }

        public static int[] GetIntArray(string ids)
        {
            List<int> list = new List<int>();

            if (!IsNullOrEmpty(ids))
            {
                string[] values = ids.Split(new char[] { ',' });

                if (values != null && values.Length > 0)
                {
                    foreach (string s in values)
                    {
                        list.Add(GetIntValue(s));
                    }
                }
            }

            return list.ToArray();
        }

        public static string GetCurrencyTTV(double amt)
        {
            StringBuilder ret = new StringBuilder();

            if (amt == 0)
            {
                ret.Append("0 dollars and 0 cents");
            }
            else
            {
                string[] values = amt.ToString("##00.00").Split(new char[] { '.' });

                if (values.Length == 2)
                {
                    ret.Append(values[0] + " dollars and " + values[1] + " cents");
                }
            }

            return ret.ToString();
        }

        public static string SpaceChars(string str)
        {
            StringBuilder sb = new StringBuilder();

            if (IsNullOrEmpty(str))
                return "";

            for (int i = 0; i < str.Length; i++)
            {
                sb.Append(Convert.ToString(str[i]));
                sb.Append(" ");
            }


            return sb.ToString();
        }
    }

}
