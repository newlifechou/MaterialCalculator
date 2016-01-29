using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfMaterialCalcualator.Model;
using GalaSoft.MvvmLight.Messaging;

namespace WpfMaterialCalcualator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SaveViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the SaveViewModel class.
        /// </summary>
        public SaveViewModel()
        {
            CurrentProjectItem = new ProjectItem() { ProjectId = Guid.NewGuid(), ProjectName = "default", SaveDate = DateTime.Now };
            SaveCommand = new RelayCommand(SaveAction, CanSaveFunc);
        }

        private void SaveAction()
        {
            NotificationMessage<ProjectItem> msg = new NotificationMessage<ProjectItem>(this, "MainViewModel", 
                CurrentProjectItem, "SaveConditions");
            Messenger.Default.Send<NotificationMessage<ProjectItem>>(msg);
        }

        private bool CanSaveFunc()
        {
            return CurrentProjectItem.IsValid;
        }

        private ProjectItem currentProjectItem;
        public ProjectItem CurrentProjectItem
        {
            get { return currentProjectItem; }
            set
            {
                Set(ref currentProjectItem, value);
            }
        }
        public RelayCommand SaveCommand { get; set; }
    }
}