﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfMaterialCalculator.Model;
using System;
using WpfMaterialCalculator.Service;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace WpfMaterialCalculator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class EditConditionViewModel : ViewModelBase
    {
        #region 私有变量区域
        private readonly IMainDataService mainDS;
        private readonly IMaterialLibraryDataService materialLibraryDS;
        #endregion
        /// <summary>
        /// Initializes a new instance of the EditConditionViewModel class.
        /// </summary>
        public EditConditionViewModel(IMainDataService mainds, IMaterialLibraryDataService mlds)
        {
            mainDS = mainds;
            materialLibraryDS = mlds;

            SelectMaterialCommand = new RelayCommand<MaterialItem>(SelectMaterialAction);
            SaveCommand = new RelayCommand(SaveAction,CanSaveFunc);

            Messenger.Default.Register<NotificationMessage<object>>(this, InitialAction);
        }

        private bool CanSaveFunc()
        {
            //通过判断DesignMode来避免设计时候的错误显示
            if (IsInDesignMode)
            {
                return true;
            }
            else
            {
                return ConditionItem.IsValid;
            }
        }

        private void SaveAction()
        {
            if (ConditionItem.Id == Guid.Empty)
            {
                ConditionItem.Id = Guid.NewGuid();
                mainDS.AddCondition(ConditionItem);
            }
            else
            {
                mainDS.UpdateCondition(ConditionItem);
            }
            //发送消息到MainView更新Conditions
            NotificationMessage<object> msg = new NotificationMessage<object>(this, "MainView", null, "ReloadConditions");
            Messenger.Default.Send<NotificationMessage<object>>(msg);
            //发送关闭本窗口的消息
            Messenger.Default.Send<object>(null, "EditConditionClose");
        }

        private void InitialAction(NotificationMessage<object> obj)
        {
            if (obj.Target.ToString() == "EditCondition")
            {
                ConditionItem = obj.Content as CalculationConditionItem;

                Materials = new ObservableCollection<MaterialItem>(materialLibraryDS.GetAllMaterialItems());
                GroupNames = new ObservableCollection<string>(CreateGroups());
            }
        }

        /// <summary>
        /// 选择材料库项目的时候，自动填入到计算条件项目中
        /// </summary>
        /// <param name="item"></param>
        private void SelectMaterialAction(MaterialItem item)
        {
            //每次选择材料后，都给材料的PopRate数值+1
            item.PopRate++;
            materialLibraryDS.UpdateMaterialItem(item);

            ConditionItem.MaterialName = item.MaterialName;
            ConditionItem.MoleWeight = item.MoleWeight;

            //引发属性改动事件
            RaisePropertyChanged(()=>ConditionItem);
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
        public RelayCommand SaveCommand { get; private set; }
        #endregion
    }
}