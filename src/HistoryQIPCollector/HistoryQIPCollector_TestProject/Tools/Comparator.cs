using System;
using System.Collections;
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
            // сравнение на ссылочное равенство
            if (a_x == a_y) return true;
            if (a_x == null || a_y == null) return false;

            // сравнение по типам
            var _type = a_x.GetType();
            if (_type != a_y.GetType()) return false;

            // сравнение на общее число элементов
            if (a_x.Length != a_y.Length) return false;

            // сравнение нижней границы и
            // длины по каждому из измерений
            for (var _i = 0; _i < a_x.Rank; _i++)
            {
                if (a_x.GetLowerBound(_i) != a_y.GetLowerBound(_i) ||
                    a_x.GetLength(_i) != a_y.GetLength(_i))
                    return false;
            }

            // перебор и сравнение элементов
            var _yEnum = a_y.GetEnumerator();
            foreach (var _e in a_x)
            {
                _yEnum.MoveNext();
                if (_e != null && _e.GetType().IsArray && _yEnum.Current != null && _yEnum.Current.GetType().IsArray)
                {
                    if (!Array(_e as Array, _yEnum.Current as Array)) return false;
                }
                else
                {
                    if (!Equals(_e, _yEnum.Current)) return false;
                }
            }
            return true;
        }
    }
}
