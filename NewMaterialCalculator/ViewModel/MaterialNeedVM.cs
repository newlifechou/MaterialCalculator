using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NewMaterialCalculator.Models;
using System.Collections.ObjectModel;
using NewMaterialCalculator.BasicService;

namespace NewMaterialCalculator.ViewModel
{
    public class MaterialNeedVM : ViewModelBase
    {
        public MaterialNeedVM()
        {
            ClearCurrentMaterialNeedModel();
            TotalWeight = 0;
            CanClear = true;

            InitializeProperties();

            MaterialNeedModels = new ObservableCollection<MaterialNeedModel>();
            Compounds = new ObservableCollection<DcBDCompound>();
            Molds = new ObservableCollection<DcBDVHPMold>();
            

            Add = new RelayCommand(ActionAdd);
            Delete = new RelayCommand<MaterialNeedModel>(ActionDelete);
        }

        private void InitializeProperties()
        {
            try
            {
                using (var service = new VHPMoldServiceClient())
                {
                    var result = service.GetVHPMold();
                    Molds.Clear();
                    result.ToList().ForEach(i => Molds.Add(i));
                }
                using (var service = new CompoundServiceClient())
                {
                    var result = service.GetAllCompounds();
                    Compounds.Clear();
                    result.ToList().ForEach(i => Compounds.Add(i));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
            if (CanClear)
            {
                ClearCurrentMaterialNeedModel();
            }


        }

        private void ClearCurrentMaterialNeedModel()
        {
            var model = new MaterialNeedModel()
            {
                ID = Guid.NewGuid(),
                Diameter = 230,
                Thickness = 6,
                Quantity = 1,
                Weight = 0,
                WeightLoss = 0
            };
            if (CurrentMaterialNeedModel != null)
            {
                model.Density = CurrentMaterialNeedModel.Density;
            }
            else
            {
                model.Density = 5.75;
            }
            CurrentMaterialNeedModel = model;
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

        private bool canClear;

        public bool CanClear
        {
            get { return canClear; }
            set { canClear = value; RaisePropertyChanged(nameof(CanClear)); }
        }



        public RelayCommand Add { get; set; }

        public RelayCommand<MaterialNeedModel> Delete { get; set; }

        public ObservableCollection<MaterialNeedModel> MaterialNeedModels { get; set; }


        public ObservableCollection<DcBDCompound> Compounds { get; set; }
        public ObservableCollection<DcBDVHPMold> Molds { get; set; }
    }
}
