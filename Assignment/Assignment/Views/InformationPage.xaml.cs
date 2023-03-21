using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment
{
    public partial class InformationPage : Window
    {
        public InformationPage(string color,string id)
        {
            InitializeComponent();
            VM vm = new VM(color);
            vm.LoadInfoPage(id);
            DataContext = vm;
        }

    }
}
