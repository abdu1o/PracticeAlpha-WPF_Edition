using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeAlpha_WPF_Edition.EntitiesController
{
    public class Spawn
    {
        public Tuple<double, double> LeftTop()
        {
            Tuple<double, double> coords = Tuple.Create(0d, 0d);
            return coords;
        }

        public Tuple<double, double> LeftMidl()
        {
            Tuple<double, double> coords = Tuple.Create(0d, 450d);
            return coords;
        }

        public Tuple<double, double> LeftBottom()
        {
            Tuple<double, double> coords = Tuple.Create(0d, 900d);
            return coords;
        }

        public Tuple<double, double> TopMidl()
        {
            Tuple<double, double> coords = Tuple.Create(820d, 0d);
            return coords;
        }

        public Tuple<double, double> BottomMidl()
        {
            Tuple<double, double> coords = Tuple.Create(820d, 900d);
            return coords;
        }

        public Tuple<double, double> RightTop()
        {
            Tuple<double, double> coords = Tuple.Create(1630d, 0d);
            return coords;
        }

        public Tuple<double, double> RightMidl()
        {
            Tuple<double, double> coords = Tuple.Create(1630d, 450d);
            return coords;
        }

        public Tuple<double, double> RightBottom()
        {
            Tuple<double, double> coords = Tuple.Create(1630d, 900d);
            return coords;
        }
    }
}
