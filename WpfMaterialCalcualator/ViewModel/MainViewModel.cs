using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using WpfMaterialCalcualator.Model;
using System;
using GalaSoft.MvvmLight.Messaging;

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

        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            EditMaterialCommand = new RelayCommand(EditMaterialAction);
            MaterialLibraryCommand = new RelayCommand(MaterialLibraryAction);


        }

        private void MaterialLibraryAction()
        {
            NotificationMessage<object> msg = new NotificationMessage<object>(this, "MaterialLibrary", null, "OpenWindow");
            Messenger.Default.Send<NotificationMessage<object>>(msg);
        }

        private void EditMaterialAction()
        {
            CalculationConditionItem item = new CalculationConditionItem() { };
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

        #endregion

        #region ������������
        public RelayCommand EditMaterialCommand { get; private set; }
        public RelayCommand CalculateCommand { get; private set; }
        public RelayCommand ClearWeightCommand { get; private set; }
        public RelayCommand MaterialLibraryCommand { get; private set; }
        #endregion

    }
}