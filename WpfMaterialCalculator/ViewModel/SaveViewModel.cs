using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using WpfMaterialCalculator.Model;
using GalaSoft.MvvmLight.Messaging;

namespace WpfMaterialCalculator.ViewModel
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
            Messenger.Default.Send<ProjectItem>(CurrentProjectItem,"SaveConditions");
            //发送关闭本窗口的消息
            Messenger.Default.Send<object>(null, "SaveClose");
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