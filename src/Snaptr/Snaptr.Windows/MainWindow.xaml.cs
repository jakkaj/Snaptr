using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snaptr.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Snapper _snapper;

        public MainWindow()
        {
            InitializeComponent();
            _snapper = new Snapper(this);
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _snapper.DoSnapz();            
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _snapper.Stop();
            var vp = new VideoProd();
            vp.Produce();
        }
    }
}
