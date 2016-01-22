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

        #region ˽�г�Ա����
        private readonly IMainDataService mainDataService;
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMainDataService ds)
        {
            this.mainDataService = ds;

            Conditions = new ObservableCollection<CalculationConditionItem>();
            Results = new ObservableCollection<CalculationResultItem>();
            AlreadyKnownList = new ObservableCollection<string>();
            CalWeight = 1000;



            AddConditionCommand = new RelayCommand(EditMaterialAction);
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
                Conditions.Add(tmp);
                //�õ����ݺ�������м���
                mainDataService.CalculateWt(Conditions, Results);

                //�����������ѡ��
                SetAlreadyKnownList();

                //����Ҫô�Ժ�̨���ݽ�������Ҫôʹ��ǰ�ŵ�View��������


                RaisePropertyChanged("Conditions");
                RaisePropertyChanged("Results");

            }
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

        private void EditMaterialAction()
        {
            CalculationConditionItem item = new CalculationConditionItem() {GroupName="1" };
            NotificationMessage<object> msg = new NotificationMessage<object>(this, "EditMaterial", item, "OpenWindow");
            Messenger.Default.Send<NotificationMessage<object>>(msg);
        }



        #region ������������
        //�����б�
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
        //����б�
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
        //��������-����������
        private string calculationGroupName;
        public string CalculationGroupName
        {
            get { return calculationGroupName; }
            set
            {
                Set(ref calculationGroupName, value);
            }
        }
        //��������-����������
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

        #region ������������
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