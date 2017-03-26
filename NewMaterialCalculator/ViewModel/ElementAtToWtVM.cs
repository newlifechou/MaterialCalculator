using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NewMaterialCalculator.Models;
using NewMaterialCalculator.BasicService;
using System.Collections.ObjectModel;

namespace NewMaterialCalculator.ViewModel
{
    public class ElementAtToWtVM : ViewModelBase
    {
        public ElementAtToWtVM()
        {

        }

        public ObservableCollection<DcBDElement> Elements { get; set; }

        public ObservableCollection<ElementModel> InputElements { get; set; }
        public ObservableCollection<ElementModel> StandardAtElements { get; set; }
        public ObservableCollection<ElementModel> StandardWtElements { get; set; }

        private double weight;

        public double Weight
        {
            get { return weight; }
            set { weight = value; RaisePropertyChanged(nameof(Weight)); }
        }

        private ElementModel currentInputElement;

        public ElementModel CurrentInputElement
        {
            get { return currentInputElement; }
            set { currentInputElement = value; RaisePropertyChanged(nameof(CurrentInputElement)); }
        }



        public RelayCommand<DcBDElement> Add { get; set; }
        public RelayCommand Save { get; set; }
        public RelayCommand<ElementModel> Delete { get; set; }
    }
}
