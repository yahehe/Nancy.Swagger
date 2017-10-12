using System;
using System.Text;

namespace Swagger.ObjectModel
{
    public static class Utilities
    {        
        public static string ToCamelCase(string val)
        {
            if (String.IsNullOrWhiteSpace(val))
            {
                return val;
            }

            var sb = new StringBuilder();
            var nextToUpper = true;
            foreach (var c in val.Trim())
            {
                if (Char.IsLetter(c))
                {
                    if (sb.Length == 0)
                    {
                        sb.Append(Char.ToLower(c));
                    }
                    else
                    {
                        sb.Append(nextToUpper ? Char.ToUpper(c) : c);
                    }
                    nextToUpper = false;
                }
                else
                {
                    if (Char.IsDigit(c))
                    {
                        if (sb.Length == 0)
                        {
                            sb.Append("_");
                        }
                        sb.Append(nextToUpper ? Char.ToUpper(c) : c);
                    }
                    nextToUpper = true;
                }
            }

            return sb.ToString();
        }
    }
}
