using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCricketSeasonScheduler
{
   public class CommonFunction
    {
        public static string GetTime(long date)
        {

            var i = DateTimeOffset.FromUnixTimeSeconds(date);
            string dt = (i).ToString("yyyy-MM-dd HH:mm");

            return dt;
        }

		    public static string ToTitle(string text)
        {
            string output = string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                output = text.ToString().Trim().ToLower().Replace("\"", @"");

                output = output.ToString().Trim().Replace("´", " ");
                output = output.ToString().Trim().Replace("®", " ");
                output = output.ToString().Trim().Replace("–", "");
                output = output.ToString().Trim().Replace("*", "");
                output = output.ToString().Trim().Replace("’", "'");
                output = output.ToString().Trim().Replace("!", "-");
                output = output.ToString().Trim().Replace("‘", "'");
                output = output.ToString().Trim().Replace("“", "");
                output = output.ToString().Trim().Replace("”", "");
                output = output.ToString().Trim().Replace("‘", "'");
                output = output.ToString().Trim().Replace("¼", "1/4");
                output = output.ToString().Trim().Replace("€", "");
                output = output.ToString().Trim().Replace("¥", "");
                output = output.ToString().Trim().Replace("…", "...");
                output = output.ToString().Trim().Replace("'", @"");
                output = output.ToString().Trim().Replace(".", " ");
                output = output.ToString().Trim().Replace(":", " ");
                output = output.ToString().Trim().Replace(";", " ");
                output = output.ToString().Trim().Replace("?", " ");
                output = output.ToString().Trim().Replace("? ", " ");
                output = output.ToString().Trim().Replace(",", " ");
                output = output.ToString().Trim().Replace("|", " ");
                output = output.ToString().Trim().Replace("£", " ");
                output = output.ToString().Trim().Replace("$", " ");
                output = output.ToString().Trim().Replace("&", " ");
                output = output.ToString().Trim().Replace("<", " ");
                output = output.ToString().Trim().Replace(">", " ");
                output = output.ToString().Trim().Replace(", ", " ");
                output = output.ToString().Trim().Replace("/", " ");
                output = output.ToString().Trim().Replace("%", " ");
                output = output.ToString().Trim().Replace("#", " ");
                output = output.ToString().Trim().Replace(" ", "-");
                output = output.ToString().Trim().Replace("--", "-");
                output = output.ToString().Trim().Replace("+", "-");
                output = output.ToString().Trim().Replace("[", "-");
                output = output.ToString().Trim().Replace("]", "-");
                //SW
                output = output.ToString().Trim().Replace("ä", "a");
                output = output.ToString().Trim().Replace("ö", "o");
                output = output.ToString().Trim().Replace("å", "a");
                output = output.ToString().Trim().Replace("ɕ", "-");
                output = output.ToString().Trim().Replace("ɧ", "-");
                output = output.ToString().Trim().Replace("ʃ", "-");
                output = output.ToString().Trim().Replace("é", "e");
                output = output.ToString().Trim().Replace("ẽ", "e");
                output = output.ToString().Trim().Replace("ü", "u");
                output = output.ToString().Trim().Replace("æ", "");
                output = output.ToString().Trim().Replace("š", "s");
                output = output.ToString().Trim().Replace("ž", "z");
                
                //de
                output = output.ToString().Trim().Replace("ß", "s");
                output = output.ToString().Trim().Replace("ü", "u");
                //fr
                output = output.ToString().Trim().Replace("ç", "c");
                output = output.ToString().Trim().Replace("â", "a");
                output = output.ToString().Trim().Replace("ê", "e");
                output = output.ToString().Trim().Replace("î", "i");
                output = output.ToString().Trim().Replace("ô", "o");
                output = output.ToString().Trim().Replace("û", "u");
                //added 12 sept
                output = output.ToString().Trim().Replace("ø", "o");
                output = output.ToString().Trim().Replace("å", "a");
                

                output = output.ToString().Trim().Replace("»", "-");
                output = output.ToString().Trim().Replace("@", "");
                output = output.ToString().Trim().Replace("--", "-");
                output = HTMLEncodeSpecialChars(output);
            }
            return output.ToLower();
        }

        public static string HTMLEncodeSpecialChars(string text)
        {
            //string t = System.Web.HttpUtility.UrlEncode(Encoding.GetEncoding("iso-8859-8").GetBytes(text));
            return text;
        }
    }
}
