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

        private int editIndex;
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMainDataService ds,IDialogService dialogDS)
        {
            //Condition编辑标志位，-1表示Add,其他值表示index
            editIndex = -1;

            mainDataService = ds;
            dialogService = dialogDS;

            Conditions = new ObservableCollection<CalculationConditionItem>();
            Results = new ObservableCollection<CalculationResultItem>();
            AlreadyKnownList = new ObservableCollection<string>();
            CalWeight = 1000;



            AddConditionCommand = new RelayCommand(AddConditionAction);
            EditConditionCommand = new RelayCommand<CalculationConditionItem>(EditConditionAction);
            DeleteConditionCommand = new RelayCommand<CalculationConditionItem>(DeleteConditionAction);

            MaterialLibraryCommand = new RelayCommand(MaterialLibraryAction);
            LoadCommand = new RelayCommand(LoadAction);
            SaveCommand = new RelayCommand(SaveAction);


            Messenger.Default.Register<NotificationMessage<object>>(this, MissonAction);
        }

        private void  SetAlreadyKnownList()
        {
            AlreadyKnownList.Clear();
            AlreadyKnownList.Add("Total Weight");
            foreach (var item in Results.Select(i => i.GroupName).ToList())
            {
                AlreadyKnownList.Add(item);
            }
            AlreadyKnownItem = "Total Weight";
        }

        private void MissonAction(NotificationMessage<object> obj)
        {
            if (obj.Notification=="ConditionEditFinished")
            {
                CalculationConditionItem tmp = obj.Content as CalculationConditionItem;
                //考虑将Condition表存放到数据库当中，这样便于修改
                //在现有的Conditions里查找是否存在来判断是Add还是Edit
                if (editIndex>=0)
                {
                    Conditions[editIndex] = tmp;
                }
                else
                {
                    Conditions.Add(tmp);
                }

                CalculateWt();

                editIndex = -1;
            }
        }

        private void CalculateWt()
        {
            //得到数据后，这里进行计算
            mainDataService.CalculateWt(Conditions, Results);
            //填充重量计算选项
            SetAlreadyKnownList();
            //排序，要么对后台数据进行排序，要么使用前排的View进行排序
            RaisePropertyChanged("Conditions");
            RaisePropertyChanged("Results");
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
            CalculationConditionItem item = new CalculationConditionItem() {GroupName="1" };
            NotificationMessage<object> msg = new NotificationMessage<object>("Add", "EditMaterial", item, "OpenWindow");
            Messenger.Default.Send<NotificationMessage<object>>(msg);
        }
        private void EditConditionAction(CalculationConditionItem obj)
        {
            editIndex = Conditions.IndexOf(obj);
            CalculationConditionItem item = obj;
            NotificationMessage<object> msg = new NotificationMessage<object>("Edit", "EditMaterial", item, "OpenWindow");
            Messenger.Default.Send<NotificationMessage<object>>(msg);
        }

        private void DeleteConditionAction(CalculationConditionItem item)
        {
            if (dialogService.ShowDialog("Are you sure to delete it?", "Delete"))
            {
                Conditions.Remove(item);
                CalculateWt();
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
        private ObservableCollection<string> alreadyKnownList;
        public ObservableCollection<string> AlreadyKnownList
        {
            get { return alreadyKnownList; }
            set
            {
                Set(ref alreadyKnownList, value);
            }
        }

        private string alreadyKnownItem;
        public string AlreadyKnownItem
        {
            get { return alreadyKnownItem; }
            set
            {
                Set(ref alreadyKnownItem, value);
            }
        }

        private double calWeight;
        public double CalWeight
        {
            get { return calWeight; }
            set
            {
                Set(ref calWeight, value);
            }
        }


        #endregion

        #region 公共命令区域
        public RelayCommand AddConditionCommand { get; private set; }
        public RelayCommand<CalculationConditionItem> EditConditionCommand { get; set; }
        public RelayCommand<CalculationConditionItem> DeleteConditionCommand { get; set; }

        public RelayCommand CalculateWtCommand { get; private set; }
        public RelayCommand ClearWeightCommand { get; private set; }
        public RelayCommand MaterialLibraryCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }


        #endregion

    }
}