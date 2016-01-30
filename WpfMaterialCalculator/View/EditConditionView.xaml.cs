using GalaSoft.MvvmLight.Messaging;
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
using WpfMaterialCalculator.Model;

namespace WpfMaterialCalculator.View
{
    /// <summary>
    /// AddMaterial.xaml 的交互逻辑
    /// </summary>
    public partial class EditConditionView : Window
    {
        private ListCollectionView lcv;
        public EditConditionView()
        {
            InitializeComponent();
            //获取View
            lcv = CollectionViewSource.GetDefaultView(lstMaterials.ItemsSource) as ListCollectionView;


            Messenger.Default.Register<object>(this, "EditConditionClose", obj =>
            {
                this.Close();
            });

            this.Unloaded += (s, e) =>
            {
                Messenger.Default.Unregister(this);
            };
        }

        private void txtFilterContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            lcv.Filter = m =>
            {
                MaterialItem material = m as MaterialItem;
                return material.MaterialName.ToLower().Contains(txtFilterContent.Text.ToLower());
            };
        }
    }
}
