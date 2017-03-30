using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// ElementAtToWt.xaml 的交互逻辑
    /// </summary>
    public partial class ElementStandard : UserControl
    {
        ICollectionView view;
        public ElementStandard()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(lst.ItemsSource);
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                view.Filter = new Predicate<object>(item =>
                {
                    var child = txtSearch.Text.Trim().ToLower();
                    DcBDElement element = (DcBDElement)item;
                    return element.Name.ToLower().Contains(child);
                });
                view.Refresh();
            }
            catch (Exception ex)
            {

            }
            e.Handled = true;
        }
    }
}
