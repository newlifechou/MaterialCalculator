using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using WpfMaterialCalcualator.Service;
using WpfMaterialCalcualator.Model;
using System.Collections.ObjectModel;
using System;

namespace WpfMaterialCalcualator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MaterialLibraryViewModel : ViewModelBase
    {
        #region 私有变量
        private readonly IMaterialLibraryDataService materialDataService;
        private readonly IDialogService dialogService;
        #endregion
        /// <summary>
        /// Initializes a new instance of the MaterialLibraryViewModel class.
        /// </summary>
        public MaterialLibraryViewModel(IMaterialLibraryDataService datads, IDialogService dialogds)
        {
            materialDataService = datads;
            dialogService = dialogds;
            Reload();
            HideEditArea();
            EditMaterialItem = new MaterialItem() { };

            AddCommand = new RelayCommand(AddAction);
            EditCommand = new RelayCommand<MaterialItem>(EditAction);
            DeleteCommand = new RelayCommand<MaterialItem>(DeleteAction);
            SaveCommand = new RelayCommand(SaveAction, CanSaveFunc);
            CancelCommand = new RelayCommand(CancelAction);

        }

        private bool CanSaveFunc()
        {
            return editMaterialItem.IsValid;
        }

        private void CancelAction()
        {
            HideEditArea();
        }

        private void DeleteAction(MaterialItem item)
        {
            if (dialogService.ShowDialog("Delete this material?", "Delete"))
            {
                materialDataService.DeleteMaterialItem(item);
                Reload();
            }
        }

        private void EditAction(MaterialItem item)
        {
            ShowEditArea();
            EditState = "Editing the Choosing Item";
            //注意以后这一要深拷贝一下
            MaterialItem newItem = new MaterialItem();
            newItem.Id = item.Id;
            newItem.MaterialName = item.MaterialName;
            newItem.MoleWeight = item.MoleWeight;
            newItem.PopRate = item.PopRate;
            EditMaterialItem = newItem;
        }

        private void AddAction()
        {
            ShowEditArea();
            EditState = "Adding New Item";
            EditMaterialItem = new MaterialItem() { MaterialName = "default", MoleWeight = 10 };
        }

        private void SaveAction()
        {
            if (EditMaterialItem.Id == Guid.Empty)
            {
                EditMaterialItem.Id = Guid.NewGuid();
                materialDataService.AddMaterialItem(EditMaterialItem);
            }
            else
            {
                bool result = materialDataService.UpdateMaterialItem(EditMaterialItem);
            }
            Reload();

            HideEditArea();
        }

        private void Reload()
        {
            Materials = new ObservableCollection<MaterialItem>(materialDataService.GetAllMaterialItems());

        }

        private void ShowEditArea()
        {
            EditAreaWidth = 450;
            MainEnableState = false;
        }

        private void HideEditArea()
        {
            EditAreaWidth = 0;
            MainEnableState = true;
        }

        #region 公开属性
        /// <summary>
        /// 显示编辑状态字符串
        /// </summary>
        private string editState;
        public string EditState
        {
            get { return editState; }
            set
            {
                Set(ref editState, value);
            }
        }
        /// <summary>
        /// 主区域可用状态，和编辑区域出现相反
        /// </summary>
        private bool mainEnableState;
        public bool MainEnableState
        {
            get { return mainEnableState; }
            set
            {
                Set(ref mainEnableState, value);
            }
        }

        /// <summary>
        /// 控制编辑区域显示宽度
        /// </summary>
        private int editAreaWidth;
        public int EditAreaWidth
        {
            get { return editAreaWidth; }
            set
            {
                Set(ref editAreaWidth, value);
            }
        }


        /// <summary>
        ///当前编辑的材料项
        /// </summary>
        private MaterialItem editMaterialItem;
        public MaterialItem EditMaterialItem
        {
            get { return editMaterialItem; }
            set
            {
                Set(ref editMaterialItem, value);
            }
        }

        /// <summary>
        /// 所有材料列表
        /// </summary>
        private ObservableCollection<MaterialItem> materials;
        public ObservableCollection<MaterialItem> Materials
        {
            get { return materials; }
            set
            {
                Set(ref materials, value);
            }
        }


        #endregion

        #region 公开命令
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand<MaterialItem> EditCommand { get; private set; }
        public RelayCommand<MaterialItem> DeleteCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        #endregion
    }
}