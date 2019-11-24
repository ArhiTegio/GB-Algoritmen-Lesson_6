using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GB_Algoritmen_Lesson_6
{
    static class ExGetHashToString
    {
        public static string GetHash(this string txt) => txt.Select(x => (int)x).Aggregate((x, y) => x + y).ToString();
    }
}
