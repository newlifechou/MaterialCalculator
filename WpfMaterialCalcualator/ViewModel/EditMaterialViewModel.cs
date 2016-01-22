using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfMaterialCalcualator.Model;
using System;
using WpfMaterialCalcualator.Service;

namespace WpfMaterialCalcualator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class EditMaterialViewModel : ViewModelBase
    {
        #region 私有变量区域
        private readonly IMaterialLibraryDataService materialLibraryDS;
        #endregion
        /// <summary>
        /// Initializes a new instance of the EditMaterialViewModel class.
        /// </summary>
        public EditMaterialViewModel(IMaterialLibraryDataService ds)
        {
            this.materialLibraryDS = ds;

            Materials = new ObservableCollection<MaterialItem>(ds.GetAllMaterialItems());

            GroupNames = new ObservableCollection<string>(CreateGroups());
            SelectMaterialCommand = new RelayCommand<MaterialItem>(SelectMaterialAction);

        }
        /// <summary>
        /// 选择材料库项目的时候，自动填入到计算条件项目中
        /// </summary>
        /// <param name="item"></param>
        private void SelectMaterialAction(MaterialItem item)
        {
            ConditionItem.MaterialName = item.MaterialName;
            ConditionItem.MoleWeight = item.MoleWeight;
        }

        /// <summary>
        /// 生成Groups列表项
        /// </summary>
        /// <returns></returns>
        private List<string> CreateGroups()
        {
            List<string> tmp = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                tmp.Add((i + 1).ToString());
            }
            return tmp;
        }



        #region 公开属性区域
        //材料库项目列表
        private ObservableCollection<MaterialItem> materials;
        public ObservableCollection<MaterialItem> Materials
        {
            get { return materials; }
            set
            {
                Set(ref materials, value);
            }
        }

        /// <summary>
        /// 当前计算项
        /// </summary>
        private CalculationConditionItem conditionItem;
        public CalculationConditionItem ConditionItem
        {
            get { return conditionItem; }
            set
            {
                Set(ref conditionItem, value);
            }
        }
        /// <summary>
        /// Group待选项里列表
        /// </summary>
        private ObservableCollection<string> groupNames;
        public ObservableCollection<string> GroupNames
        {
            get { return groupNames; }
            set
            {
                Set(ref groupNames, value);
            }
        }

        #endregion

        #region 公开命令区域
        public RelayCommand<MaterialItem> SelectMaterialCommand { get; private set; }
        public RelayCommand AddInCommand { get; private set; }
        
        
        #endregion
    }
}