using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using WpfMaterialCalcualator.Model;
using System;
using GalaSoft.MvvmLight.Messaging;
using WpfMaterialCalcualator.Service;
using System.Linq;
using System.Collections.Generic;

namespace WpfMaterialCalcualator.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        #region 私有成员区域
        private readonly IMainDataService mainDataService;
        private readonly IDialogService dialogService;
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMainDataService ds,IDialogService dialogDS)
        {

            mainDataService = ds;
            dialogService = dialogDS;
            //每次启动后，加载上次的计算表
            //mainDataService.ClearCondition();
            GroupWeight = 1000;
            TotalWeight = 1000;
            IsTotalWeight = true;

            KnowWeightGroupList = new ObservableCollection<string>();
            Results = new ObservableCollection<CalculationResultItem>();

            ReloadConditions();


            AddConditionCommand = new RelayCommand(AddConditionAction);
            EditConditionCommand = new RelayCommand<CalculationConditionItem>(EditConditionAction);
            DeleteConditionCommand = new RelayCommand<CalculationConditionItem>(DeleteConditionAction);
            ClearConditionsCommand = new RelayCommand(ClearConditionsAction);
            CalculateWeightCommand = new RelayCommand(CalculationWeightAction);

            MaterialLibraryCommand = new RelayCommand(MaterialLibraryAction);
            LoadCommand = new RelayCommand(LoadAction);
            SaveCommand = new RelayCommand(SaveAction);


            Messenger.Default.Register<NotificationMessage<object>>(this, ReloadConditionsAction);
        }

        private void CalculationWeightAction()
        {
            //判定计算方式
            if (IsTotalWeight)
            {
                Results[0].Wt = 100;
                //mainDataService.CalculateWithTotalWeight(Results, TotalWeight);
            }
            else
            {
                if (string.IsNullOrEmpty(KnownWeightGroupName))
                {

                }

                double totalMixtureWeight = 0;
                mainDataService.CalcualteWithOneGroupWeight(KnownWeightGroupName, GroupWeight, Results, out totalMixtureWeight);

            }
        }

        private void ClearConditionsAction()
        {
            if (dialogService.ShowDialog("Delete All Conditions?","Delete"))
            {
                mainDataService.ClearCondition();
                ReloadConditions();
            }
        }

        private void  SetKnowWeightGroupList()
        {
            KnowWeightGroupList.Clear();
            foreach (var item in Results.Select(i => i.GroupName).ToList())
            {
                KnowWeightGroupList.Add(item);
            }
            if (KnowWeightGroupList.Count>0)
            {
                KnownWeightGroupName = KnowWeightGroupList[0];                
            }
        }

        private void ReloadConditionsAction(NotificationMessage<object> obj)
        {
            if (obj.Notification== "ReloadConditions")
            {
                ReloadConditions();
            }
        }

        private void ReloadConditions()
        {
            Conditions = new ObservableCollection<CalculationConditionItem>(mainDataService.GetAllConditions());
            mainDataService.CalculateWt(Conditions, Results);
            SetKnowWeightGroupList();
            //ItemsSource的数据源只要引用不同，就会自动更新界面，所以不用下面的
            //RaisePropertyChanged(() =>Conditions);
            //RaisePropertyChanged(() => Results);
        }

        private void SaveAction()
        {
            NotificationMessage<object> msg = new NotificationMessage<object>(this, "Save", null, "OpenWindow");
            Messenger.Default.Send<NotificationMessage<object>>(msg);
        }

        private void LoadAction()
        {
            NotificationMessage<object> msg = new NotificationMessage<object>(this, "Load", null, "OpenWindow");
            Messenger.Default.Send<NotificationMessage<object>>(msg);
        }

        private void MaterialLibraryAction()
        {
            NotificationMessage<object> msg = new NotificationMessage<object>(this, "MaterialLibrary", null, "OpenWindow");
            Messenger.Default.Send<NotificationMessage<object>>(msg);
        }

        private void AddConditionAction()
        {
            CalculationConditionItem item = new CalculationConditionItem() {GroupName="1",At=10};
            NotificationMessage<object> msg = new NotificationMessage<object>(this, "EditCondition", item, "OpenWindow");
            Messenger.Default.Send<NotificationMessage<object>>(msg);
        }
        private void EditConditionAction(CalculationConditionItem item)
        {
            CalculationConditionItem tmpItem = new CalculationConditionItem();
            tmpItem.Id = item.Id;
            tmpItem.GroupName = item.GroupName;
            tmpItem.MaterialName = item.MaterialName;
            tmpItem.MoleWeight = item.MoleWeight;
            tmpItem.At = item.At;
            NotificationMessage<object> msg = new NotificationMessage<object>(this, "EditCondition", tmpItem, "OpenWindow");
            Messenger.Default.Send<NotificationMessage<object>>(msg);
        }

        private void DeleteConditionAction(CalculationConditionItem item)
        {
            if (dialogService.ShowDialog("Are you sure to delete it?", "Delete"))
            {
                mainDataService.DeleteCondition(item);
                ReloadConditions();
            }
        }


        #region 公共属性区域
        //条件列表
        private ObservableCollection<CalculationConditionItem> conditions;
        public ObservableCollection<CalculationConditionItem> Conditions
        {
            get
            {
                return conditions;
            }
            set
            {
                Set(ref conditions, value);
            }
        }
        //结果列表
        public ObservableCollection<CalculationResultItem> Results {
            get; set;
        }
        //private ObservableCollection<CalculationResultItem> results;
        //public ObservableCollection<CalculationResultItem> Results
        //{
        //    get
        //    {
        //        return results;
        //    }
        //    set
        //    {
        //        Set(ref results, value);
        //    }
        //}
        //重量计算-计算组组名
        private string calculationGroupName;
        public string CalculationGroupName
        {
            get { return calculationGroupName; }
            set
            {
                Set(ref calculationGroupName, value);
            }
        }
        //重量计算-计算组重量
        private double calculationWeight;
        public double CalculationWeight
        {
            get { return calculationWeight; }
            set
            {
                Set(ref calculationWeight, value);
            }
        }

        //AlreadyKnown
        private ObservableCollection<string> knowWeightGroupList;
        public ObservableCollection<string> KnowWeightGroupList
        {
            get { return knowWeightGroupList; }
            set
            {
                Set(ref knowWeightGroupList, value);
            }
        }

        private string knownWeightGroupName;
        public string KnownWeightGroupName
        {
            get { return knownWeightGroupName; }
            set
            {
                Set(ref knownWeightGroupName, value);
            }
        }

        private double groupWeight;
        public double GroupWeight
        {
            get { return groupWeight; }
            set
            {
                Set(ref groupWeight, value);
            }
        }

        private double totalWeight;
        public double TotalWeight
        {
            get { return totalWeight; }
            set
            {
                Set(ref totalWeight, value);
            }
        }
        private bool isTotalWeight;
        public bool IsTotalWeight
        {
            get { return isTotalWeight; }
            set
            {
                Set(ref isTotalWeight, value);
            }
        }


        #endregion

        #region 公共命令区域
        public RelayCommand AddConditionCommand { get; private set; }
        public RelayCommand<CalculationConditionItem> EditConditionCommand { get; set; }
        public RelayCommand<CalculationConditionItem> DeleteConditionCommand { get; set; }
        public RelayCommand ClearConditionsCommand { get; private set; }


        public RelayCommand CalculateWeightCommand { get; private set; }
        public RelayCommand ClearWeightCommand { get; private set; }
        public RelayCommand MaterialLibraryCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }


        #endregion

    }
}