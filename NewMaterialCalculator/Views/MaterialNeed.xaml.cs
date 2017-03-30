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
using NewMaterialCalculator.BasicService;

namespace NewMaterialCalculator.Views
{
    /// <summary>
    /// MaterialNeed.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialNeed : UserControl
    {
        public MaterialNeed()
        {
            InitializeComponent();
        }

        private void lstCompounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCompounds.SelectedItem != null)
            {
                txtDensity.Text = (lstCompounds.SelectedItem as DcBDCompound).Density.ToString();
                txtDensity.Focus();
            }
        }

        private void lstMolds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstMolds.SelectedItem != null)
            {
                txtInnerDiameter.Text = (lstMolds.SelectedItem as DcBDVHPMold).InnerDiameter.ToString();
                txtInnerDiameter.Focus();
            }
        }
    }
}
