using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PracticeAlpha_WPF_Edition
{
    public partial class App : Application
    {
        public static Window[] GetOpenWindows()
        {
            return Current.Windows.OfType<Window>().ToArray();
        }
    }
}
