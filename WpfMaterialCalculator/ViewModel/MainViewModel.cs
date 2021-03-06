using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using WpfMaterialCalculator.Model;
using System;
using GalaSoft.MvvmLight.Messaging;
using WpfMaterialCalculator.Service;
using System.Linq;
using System.Collections.Generic;
using System.Windows;

namespace WpfMaterialCalculator.ViewModel
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
        public MainViewModel(IMainDataService ds, IDialogService dialogDS)
        {

            mainDataService = ds;
            dialogService = dialogDS;
            //每次启动后，加载上次的计算表
            //mainDataService.ClearCondition();
            GroupWeight = 1000;
            TotalWeight = 1000;
            TotalElementWeight = 1000;
            IsTotalWeight = true;

            Results = new ObservableCollection<CalculationResultItem>();
            ElementWeightResults = new ObservableCollection<ElementResultItem>();

            LoadConditions();


            AddConditionCommand = new RelayCommand(AddConditionAction);
            EditConditionCommand = new RelayCommand<CalculationConditionItem>(EditConditionAction);
            DeleteConditionCommand = new RelayCommand<CalculationConditionItem>(DeleteConditionAction);
            ClearConditionsCommand = new RelayCommand(ClearConditionsAction,CanClearConditonFunc);

            CalculateWeightCommand = new RelayCommand(CalculationWeightAction,CanCalculateWeightFunc);
            CalculateElementWeightCommand = new RelayCommand(CalculateElementWeightAction, CanCalculateElementWeightFunc);
            ClearWeightCommand = new RelayCommand(ClearWeightAction,CanClearWeightFunc);

            MaterialLibraryCommand = new RelayCommand(MaterialLibraryAction);
            LoadCommand = new RelayCommand(LoadAction);
            SaveCommand = new RelayCommand(SaveAction,CanSaveFunc);


            Messenger.Default.Register<NotificationMessage<object>>(this, ReloadConditionsAction);

            Messenger.Default.Register<ProjectItem>(this, "SaveConditions",SaveConditionsAction);
            Messenger.Default.Register<ProjectItem>(this, "LoadConditions", LoadConditionsAction);
        }

        private void CalculateElementWeightAction()
        {
            mainDataService.CalcualteElementWeight(Conditions, TotalElementWeight, ElementWeightResults);
        }

        private bool CanCalculateElementWeightFunc()
        {
            return ElementWeightResults.Count > 0;
        }

        private void LoadConditionsAction(ProjectItem item)
        {
            if (item!=null)
            {
                var data = mainDataService.GetCalculationsByProjectId(item.ProjectId);
                //将数据插入到TempCondition里
                mainDataService.AddConditions(data);
                LoadConditions();
            }
        }

        private void SaveConditionsAction(ProjectItem item)
        {
            if (item!=null&&Conditions.Count>0)
            {
                mainDataService.AddProject(item);
                mainDataService.AddConditionsByProjectId(Conditions, item.ProjectId);
            }
        }

        private bool CanSaveFunc()
        {
            return Results.Count > 0;
        }

        private bool CanClearWeightFunc()
        {
            return Results.Count > 0;
        }

        private bool CanCalculateWeightFunc()
        {
            return Results.Count > 0;
        }

        private bool CanClearConditonFunc()
        {
            return Results.Count > 0;
        }

        private void ClearWeightAction()
        {
            mainDataService.ClearResultWeigtht(Results);
            TotalWeight2 = 0;
        }

        private void CalculationWeightAction()
        {
            //判定计算方式
            if (IsTotalWeight)
            {
                //Results[0].Weight= 100;
                mainDataService.CalculateWithTotalWeight(Results, TotalWeight);
            }
            else
            {
                if (KnownWeightGroupItem != null)
                {
                    double totalMixtureWeight = 0;
                    mainDataService.CalcualteWithOneGroupWeight(KnownWeightGroupItem, GroupWeight, Results, out totalMixtureWeight);
                    TotalWeight2 = totalMixtureWeight;
                }
            }
        }

        private void ClearConditionsAction()
        {
            string warningMsg = Application.Current.TryFindResource("dialogDeleteConditions").ToString();
            if (dialogService.ShowDialog(warningMsg, "Delete"))
            {
                //清除临时数据库
                mainDataService.ClearConditions();
                //清除集合
                Conditions.Clear();
                Results.Clear();
                ElementWeightResults.Clear();
            }
        }

        private void ReloadConditionsAction(NotificationMessage<object> obj)
        {
            if (obj.Notification == "ReloadConditions")
            {
                LoadConditions();
            }
        }

        private void LoadConditionsBase(IList<CalculationConditionItem> data)
        {
            Conditions = new ObservableCollection<CalculationConditionItem>(data);
            if (Conditions.Count == 0)
            {
                return;
            }
            mainDataService.CalculateWt(Conditions, Results);
            mainDataService.CalcualteElementWeight(Conditions, TotalElementWeight, ElementWeightResults);
            KnownWeightGroupItem = Results[0];
        }

        private void LoadConditions()
        {
            LoadConditionsBase(mainDataService.GetAllConditions());
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
            CalculationConditionItem item = new CalculationConditionItem() { GroupName = "1",MaterialName="default",MoleWeight=100, At = 10 };
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
            string warningMsg = Application.Current.TryFindResource("dialogDeleteCondition").ToString();
            if (dialogService.ShowDialog(warningMsg, "Delete"))
            {
                mainDataService.DeleteCondition(item);
                LoadConditions();
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
        private ObservableCollection<CalculationResultItem> results;
        public ObservableCollection<CalculationResultItem> Results
        {
            get
            {
                return results;
            }
            set
            {
                Set(ref results, value);
            }
        }

        private double totalElementWeight;
        public double TotalElementWeight
        {
            get
            {
                return totalElementWeight;
            }
            set
            {
                Set(ref totalElementWeight, value);
            }
        }
        //元素重量计算列表
        private ObservableCollection<ElementResultItem> elementWeightResults;
        public ObservableCollection<ElementResultItem> ElementWeightResults
        {
            get
            {
                return elementWeightResults;
            }
            set
            {
                Set(ref elementWeightResults, value);
            }
        }
        /// <summary>
        /// 计算组项
        /// </summary>
        private CalculationResultItem knowWeightGroupItem;
        public CalculationResultItem KnownWeightGroupItem
        {
            get { return knowWeightGroupItem; }
            set
            {
                Set(ref knowWeightGroupItem, value);
            }
        }

        /// <summary>
        /// 知道计算组重量，计算其他的时候的临时组重量
        /// </summary>
        private double groupWeight;
        public double GroupWeight
        {
            get { return groupWeight; }
            set
            {
                Set(ref groupWeight, value);
            }
        }
        private double totalWeight2;
        public double TotalWeight2
        {
            get { return totalWeight2; }
            set
            {
                Set(ref totalWeight2, value);
            }
        }
        /// <summary>
        /// 计算总重量
        /// </summary>
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
        public RelayCommand CalculateElementWeightCommand { get; private set; }

        public RelayCommand ClearWeightCommand { get; private set; }
        public RelayCommand MaterialLibraryCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }


        #endregion

    }
}