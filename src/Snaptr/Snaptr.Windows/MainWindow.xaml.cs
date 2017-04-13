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
        RecordArea _recordArea;
        public MainWindow()
        {
            InitializeComponent();
            
            this.Loaded += MainWindow_Loaded;
           
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _recordArea = new RecordArea();
            
            _recordArea.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _snapper = new Snapper(_recordArea);
            _recordArea.Opacity = 0;
            _recordArea.IsHitTestVisible = false;
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
            _recordArea.Opacity = 1;
            _recordArea.IsHitTestVisible = true;

        }
    }
}
