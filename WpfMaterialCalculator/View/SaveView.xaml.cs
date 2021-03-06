﻿using GalaSoft.MvvmLight.Messaging;
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

namespace WpfMaterialCalculator.View
{
    /// <summary>
    /// SaveView.xaml 的交互逻辑
    /// </summary>
    public partial class SaveView : Window
    {
        public SaveView()
        {
            InitializeComponent();
            Messenger.Default.Register<object>(this, "SaveClose", obj =>
            {
                this.Close();
            });

            this.Unloaded += (s, e) =>
            {
                Messenger.Default.Unregister(this);
            };
        }
    }
}
