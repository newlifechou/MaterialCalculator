using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NewMaterialCalculator.Models;
using System.Collections.ObjectModel;

namespace NewMaterialCalculator.ViewModel
{
    public class MaterialNeedVM : ViewModelBase
    {
        public MaterialNeedVM()
        {
            CurrentMaterialNeedModel = new MaterialNeedModel()
            {
                ID = Guid.NewGuid(),
                Density = 5.75,
                Diameter = 230,
                Thickness = 6,
                Quantity = 2,
                Weight = 0,
                WeightLoss = 0
            };
            TotalWeight = 0;
            MaterialNeedModels = new ObservableCollection<MaterialNeedModel>();

            Add = new RelayCommand(ActionAdd);
            Delete = new RelayCommand<MaterialNeedModel>(ActionDelete);
        }

        private void ActionDelete(MaterialNeedModel model)
        {
            MaterialNeedModels.Remove(model);
            CalcualteTotalWeight();
        }

        private void ActionAdd()
        {
            var current = CurrentMaterialNeedModel;
            var model = new MaterialNeedModel();
            model.ID = current.ID;
            model.Density = current.Density;
            model.Diameter = current.Diameter;
            model.Thickness = current.Thickness;
            model.Quantity = current.Quantity;
            model.WeightLoss = current.WeightLoss;
            model.Weight = Math.PI * current.Diameter * current.Diameter * current.Thickness / 4 / 1000 * current.Density * current.Quantity + current.WeightLoss;

            MaterialNeedModels.Add(model);
            CalcualteTotalWeight();
        }

        private void CalcualteTotalWeight()
        {
            double result = 0;
            foreach (var item in MaterialNeedModels)
            {
                result += item.Weight;
            }
            TotalWeight = result;
        }


        private MaterialNeedModel currentMaterialNeedModel;

        public MaterialNeedModel CurrentMaterialNeedModel
        {
            get { return currentMaterialNeedModel; }
            set { currentMaterialNeedModel = value; RaisePropertyChanged(nameof(CurrentMaterialNeedModel)); }
        }

        private double totalWeight;

        public double TotalWeight
        {
            get { return totalWeight; }
            set { totalWeight = value; RaisePropertyChanged(nameof(TotalWeight)); }
        }


        public RelayCommand Add { get; set; }

        public RelayCommand<MaterialNeedModel> Delete { get; set; }

        public ObservableCollection<MaterialNeedModel> MaterialNeedModels { get; set; }

    }
}
