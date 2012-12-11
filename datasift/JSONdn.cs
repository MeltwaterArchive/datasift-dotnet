using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace datasift
{
    /// <summary>
    /// Provides access to JSON data using a dot notation.
    /// </summary>
    public class JSONdn
    {
        /// <summary>
        /// The internal object that holds the JSON data.
        /// </summary>
        private JToken m_data = null;

        /// <summary>
        /// Constructor using a JToken object as the source data.
        /// </summary>
        /// <param name="obj">The JToken object.</param>
        public JSONdn(JToken obj)
        {
            m_data = obj;
        }

        /// <summary>
        /// Constructor using a JObject object as the source data.
        /// </summary>
        /// <param name="obj">The JObject object.</param>
        public JSONdn(JObject obj)
        {
            m_data = obj.Root;
        }

        /// <summary>
        /// Constructor using a JSON string as the source data.
        /// </summary>
        /// <param name="source">The JSON string.</param>
        public JSONdn(string source)
        {
            m_data = JObject.Parse(source).Root;
        }

        /// Walk down the JSON data and return the object that represents the
        /// last element of the dot-separated key.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>A JToken object.</returns>
        public JToken resolveString_old(string key)
        {
            string[] parts = key.Split(new Char[] { '.' });
            JToken retval = m_data[parts[0]];
            for (int i = 1; i < parts.Length; i++)
            {
                if (retval[parts[i]] == null)
                {
                    throw new InvalidDataException("JSON key does not exist");
                }
                retval = retval[parts[i]];
            }
            return retval;
        }

        /// <summary>
        /// Split a string with . as a delimeter,
        /// However escaped dots are not a delimeter
        /// </summary>
        /// <param name="str">String to split</param>
        /// <returns>array of substrings</returns>
        internal static IEnumerable<string> _SplitAndUnescape(string str)
        {
            //split on dot(.), but not esscaped dots(\.)
            //Also being aware of escaped escapes(\\) before dots(.) e.g. don't split \\. but do split \\\.
            //see tests: Test_Split
            const char NotADotOrEscape = '\0'; //could be anything, but not \ or .
            var Result = new List<string>();
            var prevChar = NotADotOrEscape;
            var begining = 0;
            var len = str.Length;
            for (var index =0; index <len;index++)
            {
                var thisChar = str[index];
                if (prevChar == '\\')
                {
                    //:tricky: sometimes we lie about the prevchar, if there is \\a then when on a (a being any char) then prevChar is not \
                    if (thisChar == '.')
                    {
                        //unescape the (not splitting) dots
                        //:tricky: we are scanning left to right, and now adjust the char one behind the one we are on, and as a consequence everything to the right of us.
                        str=str.Remove(index - 1, 1);
                        index--;
                        len--;
                    }
                    prevChar = NotADotOrEscape;
                }
                else if (thisChar == '.')
                {
                    //split
                    Result.Add(str.Substring(begining, index - begining));
                    begining = index + 1;
                    //
                    prevChar = NotADotOrEscape;
                }
                else
                {
                    prevChar = thisChar;
                }
            }
            Result.Add(str.Substring(begining));
            return Result;
        }

        /// <summary>
        /// If a key that has dots in it is used in dot notation, then things will be bad,
        /// because if is an ambiguous grama,
        /// use this routine to escape the dots, it will replace a . with a \. 
        /// (a \. is ilegal in json, so is unambiguous)
        /// The other methods will split on an unescaped dot, and unescape these dots.
        /// </summary>
        /// <param name="key">input key</param>
        /// <returns>escaped key</returns>
        public static string EscapeDotsInKey(string key)
        {
            return key.Replace(".", "\\.");
        }

        /// <summary>
        /// Walk down the JSON data and return the object that represents the
        /// last element of the dot-separated key.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>A JToken object.</returns>
        public JToken resolveString(string key) 
        {
            IEnumerable<string> parts = _SplitAndUnescape(key);
           
            var cursor = parts.GetEnumerator();
            cursor.MoveNext();
            JToken retval = m_data;
            retval = retval[cursor.Current];
            while(cursor.MoveNext())
            {
                if (retval[cursor.Current] == null)
                {
                    throw new InvalidDataException("JSON key does not exist");
                }
                retval = retval[cursor.Current];
            }
            return retval;
        }

        /// <summary>
        /// Get the item specified by the key as a JToken object.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>A JToken object.</returns>
        public JToken getJVal(string key = "")
        {
            if (key.Length == 0)
            {
                return (JToken)m_data;
            }
            return resolveString(key);
        }

        /// <summary>
        /// Get the children of the item at the given key as an array of
        /// JToken objects.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>An array of JToken objects.</returns>
        public JToken[] getChildren(string key)
        {
            List<JToken> retval = new List<JToken>();
            JToken current = getJVal(key).First;
            while (current != null)
            {
                retval.Add(current);
                current = current.Next;
            }
            return retval.ToArray();
        }

        /// <summary>
        /// Get the item at the given key as a boolean.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>The value converted to a boolean.</returns>
        public bool getBoolVal(string key)
        {
            return Convert.ToBoolean(getStringVal(key));
        }

        /// <summary>
        /// Get the item at the given key as an integer.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>The value converted to an integer.</returns>
        public int getIntVal(string key)
        {
            return Convert.ToInt32(getStringVal(key));
        }

        /// <summary>
        /// Get the item at the given key as a long.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>The value converted to a long.</returns>
        public long getLongVal(string key)
        {
            return Convert.ToInt64(getStringVal(key));
        }

        /// <summary>
        /// Get the item at the given key as a double.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>The value converted to a double.</returns>
        public double getDoubleVal(string key)
        {
            return Convert.ToDouble(getStringVal(key));
        }

        /// <summary>
        /// Get the item at the given key as a DateTime object by parsing it
        /// using the given format string. See the docs for
        /// DateTime.ParseExact for details on the format string.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <param name="format">The format string.</param>
        /// <returns>The DateTime object.</returns>
        public DateTime getDateTimeVal(string key, string format)
        {
            return DateTime.ParseExact(getStringVal(key), format, null);
        }

        /// <summary>
        /// Get the item at the given key as a unix timestamp and convert it
        /// to a DateTime object.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>The DateTime object.</returns>
        public DateTime getDateTimeFromLongVal(string key)
        {
            return Utils.UnixTimestampToDateTime(getLongVal(key));
        }

        /// <summary>
        /// Get the item at the given key as a string.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>The value converted to a string.</returns>
        public string getStringVal(string key)
        {
            return resolveString(key).ToString();
        }

        /// <summary>
        /// Check for the given key.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>True if the key exists.</returns>
        public bool has(string key)
        {
            return resolveString(key) != null;
        }

        public bool hasChildren(string key = "")
        {
            if (key.Length == 0)
            {
                return m_data.First != null;
            }
            else
            {
                try
                {
                    JToken tok = getJVal(key).First;
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get a list of keys below the given key.
        /// </summary>
        /// <param name="key">The item key.</param>
        /// <returns>The list of keys.</returns>
        public string[] getKeys(string key = "")
        {
            List<string> retval = new List<string>();

            JToken jtoken = getJVal(key).First;

            while (jtoken != null)
            {
                retval.Add(((JProperty)jtoken).Name);
                jtoken = jtoken.Next;
            }

            return retval.ToArray();
        }
    }
}
