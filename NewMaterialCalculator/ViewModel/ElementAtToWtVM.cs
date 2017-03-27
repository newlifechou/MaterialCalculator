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
            isNew = true;

            Weight = 1000;
            Elements = new ObservableCollection<DcBDElement>();
            InputElements = new ObservableCollection<ElementModel>();
            StandardGroups = new ObservableCollection<ElementGroupModel>();
            StandardWtElements = new ObservableCollection<ElementModel>();

            using (var service = new ElementServiceClient())
            {
                var models = service.GetElements();
                Elements.Clear();
                models.ToList().ForEach(i => Elements.Add(i));
                var item = Elements.FirstOrDefault();
                CurrentInputElement = new Models.ElementModel()
                {
                    GroupNumber = 1,
                    Name = item.Name,
                    MolWeight = item.MolWeight,
                    At = 10,
                    Wt = 0,
                    Weight = 0
                };

            }

            Select = new RelayCommand<BasicService.DcBDElement>(ActionSelect);
            Save = new RelayCommand(ActionSave);
            Delete = new RelayCommand<ElementModel>(ActionDelete);
            Edit = new RelayCommand<Models.ElementModel>(ActionEdit);
        }

        private void ActionEdit(ElementModel model)
        {
            CurrentInputElement = model;
            isNew = false;
        }

        private bool isNew;
        private void ActionDelete(ElementModel model)
        {
            InputElements.Remove(model);
        }

        private void ActionSave()
        {
            if (isNew)
            {
                InputElements.Add(CurrentInputElement);
            }
            else
            {

            }
            isNew = true;
        }

        private void ActionSelect(DcBDElement obj)
        {
            var model = new ElementModel();
            model.Name = obj.Name;
            model.MolWeight = obj.MolWeight;
            model.At = 10;
            model.Wt = 0;
            model.Weight = 0;
            model.GroupNumber = 1;
            CurrentInputElement = model;
        }

        public ObservableCollection<DcBDElement> Elements { get; set; }
        public ObservableCollection<ElementModel> InputElements { get; set; }
        public ObservableCollection<ElementModel> StandardWtElements { get; set; }
        public ObservableCollection<ElementGroupModel> StandardGroups { get; set; }


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



        public RelayCommand<DcBDElement> Select { get; set; }
        public RelayCommand Save { get; set; }
        public RelayCommand<ElementModel> Edit{ get; set; }
        public RelayCommand<ElementModel> Delete { get; set; }
    }
}
