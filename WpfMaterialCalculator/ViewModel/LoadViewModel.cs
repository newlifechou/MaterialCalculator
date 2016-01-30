using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using WpfMaterialCalculator.Model;
using WpfMaterialCalculator.Service;
using GalaSoft.MvvmLight.CommandWpf;
using System;

namespace WpfMaterialCalculator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LoadViewModel : ViewModelBase
    {
        private readonly IMainDataService mainDS;
        private readonly IDialogService dialogDS;
        /// <summary>
        /// Initializes a new instance of the LoadViewModel class.
        /// </summary>
        public LoadViewModel(IMainDataService mainds,IDialogService dialogds)
        {
            mainDS = mainds;
            dialogDS = dialogds;

            LoadCommand = new RelayCommand<ProjectItem>(LoadAction);
            DeleteCommand = new RelayCommand<ProjectItem>(DeleteAction);

            Messenger.Default.Register<NotificationMessage<object>>(this, InitialAction);
        }

        private void LoadAction(ProjectItem obj)
        {
            Messenger.Default.Send<ProjectItem>(obj, "LoadConditions");
            Messenger.Default.Send<object>(null, "LoadClose");
        }

        private void DeleteAction(ProjectItem item)
        {
            if (dialogDS.ShowDialog("Really want to Delete this Project?","Delete"))
            {
                mainDS.DeleteProject(item);
                mainDS.DeleteConditionsByProjectId(item.ProjectId);
                LoadProjects();
            }
        }

        private void InitialAction(NotificationMessage<object> obj)
        {
            if (obj.Target.ToString()=="Load"&&obj.Notification=="OpenWindow")
            {
                LoadProjects();
            }
        }

        private void LoadProjects()
        {
            Projects = new ObservableCollection<ProjectItem>(mainDS.GetAllProjects());
        }
        private ObservableCollection<ProjectItem> projects;
        public ObservableCollection<ProjectItem> Projects
        {
            get { return projects; }
            set
            {
                if (projects == value)
                    return;
                projects = value;
                RaisePropertyChanged(() => Projects);
            }
        }

        public RelayCommand<ProjectItem> LoadCommand { get; private set; }
        public RelayCommand<ProjectItem> DeleteCommand { get; private set; }
    }
}