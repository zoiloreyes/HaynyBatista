using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HaynyBatista.Extensions
{
    public static class StringExtensions
    {
            public static string StripHtml(this string inputString)
            {
                return Regex.Replace
                  (inputString, "<.*?>", string.Empty);
            }
        
    }
}