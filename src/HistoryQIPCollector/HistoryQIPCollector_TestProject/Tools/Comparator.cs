using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HistoryQIPCollector_TestProject.Tools
{
    public static class Comparator
    {
        public static bool File(string a_x, string a_y)
        {
            var _xBytes = System.IO.File.ReadAllBytes(a_x);
            var _yBytes = System.IO.File.ReadAllBytes(a_y);

            return Array(_xBytes, _yBytes);
        }

        public static bool Array(Array a_x, Array a_y)
        {
            // TODO:
            if (a_x.Length != a_y.Length)
                return false;
            for (var _i = 0; _i < a_x.Length; _i++)
            {
                if (!a_x.GetValue(_i).Equals(a_y.GetValue(_i)))
                    return false;
            }
            return true;
        }
    }
}
