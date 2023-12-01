using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using RP7XMC_HFT_2022232.Models;

namespace RP7XMC_HFT_2022232.WpfClient
{
    public class ServiceWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<Service> Services { get; set; }

        private Service selectedService;

        public Service SelectedService
        {
            get { return selectedService; }
            set
            {
                if (value != null)
                {
                    selectedService = new Service()
                    {
                        ServiceName = value.ServiceName,
                        ServiceId = value.ServiceId
                    };
                    OnPropertyChanged();
                    (DeleteServiceCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateServiceCommand { get; set; }

        public ICommand DeleteServiceCommand { get; set; }

        public ICommand UpdateServiceCommand { get; set; }
        public ICommand BrandCommand { get; set; }
        public ICommand CarCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public ServiceWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Services = new RestCollection<Service>("http://localhost:2810/", "service");
                BrandCommand = new RelayCommand(() =>
                {
                    new BrandWindow().Show();
                });
                CarCommand = new RelayCommand(() =>
                {
                    new MainWindow().Show();
                });
                CreateServiceCommand = new RelayCommand(() =>
                {
                    Services.Add(new Service()
                    {
                        ServiceName = SelectedService.ServiceName
                    });
                });

                UpdateServiceCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Services.Update(SelectedService);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteServiceCommand = new RelayCommand(() =>
                {
                    Services.Delete(SelectedService.ServiceId);
                },
                () =>
                {
                    return SelectedService != null;
                });
                SelectedService = new Service();
            }

        }
    }
}
