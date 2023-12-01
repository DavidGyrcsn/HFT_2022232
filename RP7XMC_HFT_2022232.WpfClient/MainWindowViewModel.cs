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
    public class MainWindowViewModel: ObservableRecipient
        {
            private string errorMessage;

            public string ErrorMessage
            {
                get { return errorMessage; }
                set { SetProperty(ref errorMessage, value); }
            }


            public RestCollection<Car> Cars { get; set; }

            private Car selectedCar;

            public Car SelectedCar
        {
                get { return selectedCar; }
                set
                {
                    if (value != null)
                    {
                        selectedCar = new Car()
                        {
                            CarName = value.CarName,
                        CarId = value.CarId
                    };
                        OnPropertyChanged();
                        (DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
                    }
                }
            }


            public ICommand CreateCarCommand { get; set; }

            public ICommand DeleteCarCommand { get; set; }

            public ICommand UpdateCarCommand { get; set; }
            public ICommand BrandCommand { get; set; }
            public ICommand ServiceCommand { get; set; }

        public static bool IsInDesignMode
            {
                get
                {
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
                }
            }


            public MainWindowViewModel()
            {
                if (!IsInDesignMode)
                {
                Cars = new RestCollection<Car>("http://localhost:2810/", "car");
                BrandCommand = new RelayCommand(() =>
                {
                    new BrandWindow().Show();
                });
                ServiceCommand = new RelayCommand(() =>
                {
                    new ServiceWindow().Show();
                });

                CreateCarCommand = new RelayCommand(() =>
                    {
                        Cars.Add(new Car()
                        {
                            CarName = SelectedCar.CarName
                        });
                    });

                UpdateCarCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            Cars.Update(SelectedCar);
                        }
                        catch (ArgumentException ex)
                        {
                            ErrorMessage = ex.Message;
                        }

                    });

                DeleteCarCommand = new RelayCommand(() =>
                    {
                        Cars.Delete(SelectedCar.CarId);
                    },
                    () =>
                    {
                        return SelectedCar != null;
                    });
                SelectedCar = new Car();
                }

            }
        }
}
