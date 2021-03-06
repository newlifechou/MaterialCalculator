/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WpfMaterialCalculator"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using WpfMaterialCalculator.Common;
using WpfMaterialCalculator.Service;

namespace WpfMaterialCalculator.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IMaterialLibraryDataService,MaterialLibraryDataService>();
            SimpleIoc.Default.Register<IMainDataService, MainDataService>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EditConditionViewModel>(true);
            SimpleIoc.Default.Register<MaterialLibraryViewModel>();
            SimpleIoc.Default.Register<LoadViewModel>(true);
            SimpleIoc.Default.Register<SaveViewModel>();


            SimpleIoc.Default.Register<WindowController>(true);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public EditConditionViewModel EditCondition
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditConditionViewModel>();
            }
        }

        public MaterialLibraryViewModel MaterialLibrary
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MaterialLibraryViewModel>();
            }
        }

        public LoadViewModel Load
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoadViewModel>();
            }
        }
        public SaveViewModel Save
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SaveViewModel>();
            }
        }





        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}