using System;
using System.Collections.Generic;
using System.Text;

namespace HermesLogic
{
    public static class StringExtension
    {
        public static bool IsPasswordOk(this string text)
        {
            return !String.IsNullOrEmpty(text) && text.Length >= 8;
        }
    }
}
