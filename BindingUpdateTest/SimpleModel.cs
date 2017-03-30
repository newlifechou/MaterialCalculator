using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;

namespace BindingUpdateTest
{
    public class SimpleModel : ViewModelBase
    {
        public SimpleModel()
        {
            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            MessageBox.Show(Name);
        }

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public RelayCommand Save
        {
            get;set;
        }

    }
}
