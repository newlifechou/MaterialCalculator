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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NewMaterialCalculator.Views;


namespace NewMaterialCalculator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //SetMainArea(new MaterialNeed());
            SetMainArea(new ElementStandard());
        }

        private void SetMainArea(UserControl view)
        {
            if (view != null)
            {
                this.mainArea.Content = view;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimum_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximum_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.IsOpen = true;
        }

        private void Element_Click(object sender, RoutedEventArgs e)
        {
            SetMainArea(new ElementStandard());
        }

        private void MaterialNeed_Click(object sender,RoutedEventArgs e)
        {
            SetMainArea(new MaterialNeed());
        }
    }
}
