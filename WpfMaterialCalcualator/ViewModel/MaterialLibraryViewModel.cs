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

            AddCommand = new RelayCommand(AddAction);
            EditCommand = new RelayCommand<MaterialItem>(EditAction);
            DeleteCommand = new RelayCommand<MaterialItem>(DeleteAction);
            SaveCommand = new RelayCommand(SaveAction);

        }

        private void DeleteAction(MaterialItem item)
        {
            if (dialogService.ShowDialog("Delete this material?","Delete"))
            {
                materialDataService.DeleteMaterialItem(item);
            }
        }

        private void EditAction(MaterialItem item)
        {
            EditMaterialItem = item;
        }

        private void SaveAction()
        {
            if (EditMaterialItem.Id==Guid.Empty)
            {
                EditMaterialItem.Id = Guid.NewGuid();
                materialDataService.AddMaterialItem(EditMaterialItem);
            }
            else
            {
                materialDataService.UpdateMaterialItem(EditMaterialItem);
            }
            Reload();

            EditMaterialItem.Id = Guid.Empty;
            EditMaterialItem.MaterialName = string.Empty;
            EditMaterialItem.MoleWeight = 0;
            EditMaterialItem.PopRate = 0;
            RaisePropertyChanged("EditMaterialItem");
        }

        private void AddAction()
        {
            EditMaterialItem = new MaterialItem();
        }

        private void Reload()
        {
            Materials = new ObservableCollection<MaterialItem>(materialDataService.GetAllMaterialItems());

        }


        #region 公开属性
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
        #endregion
    }
}