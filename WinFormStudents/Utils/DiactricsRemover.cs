using System.Text;

namespace WinFormStudents.Utils
{
    static class DiactricsRemover
    {
        public static string RemoveDiacritics(this string s)
        {
            string asciiEquivalents = Encoding.ASCII.GetString(
                         Encoding.GetEncoding("Cyrillic").GetBytes(s)
                     );

            return asciiEquivalents;
        }
    }
}
