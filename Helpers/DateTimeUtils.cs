using System;
using System.Collections.Generic;

namespace Evoflare.API.Helpers
{
    public class DateTimeUtils
    {
        public class StringDateValue
        {
            public string Text { get; set; }
            public DateTime Value { get; set; }
        }

        private static readonly Dictionary<int, string> Quarter = new Dictionary<int, string>
        {
            { 1, "I" },
            { 2, "II" },
            { 3, "III" },
            { 4, "IV" },
        };

        /// <summary>
        /// Returns a string formated like "III 2019".
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToQuarterYearString(DateTime date)
        {
            var quarter = (int)Math.Ceiling((date.Month + 1) / 3.0);
            return $"{Quarter[quarter]} {date.Year}";
        }
        private static readonly Dictionary<int, int> QuarterToMonth = new Dictionary<int, int>
        {
            {1, 1},
            {2, 4},
            {3, 7},
            {4, 10},
        };
        public static DateTime GetQuarterStartDate(DateTime date)
        {
            var quarter = (int)Math.Ceiling((date.Month + 1) / 3.0);
            return new DateTime(date.Year, QuarterToMonth[quarter], 1);
        }
    }
}
