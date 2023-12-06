using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RP7XMC_HFT_2022232.Models;
using Microsoft.VisualBasic;

namespace RP7XMC_HFT_2022232.WpfClient
{
    public class BrandWindowViewModel : ObservableRecipient
    {
        public string T_Box { get; set; }
        public string T_Box2 { get; set; }
        public string T_Box3 { get; set; }
        public string T_Box4 { get; set; }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<Brand> Brands { get; set; }

        public List<string> HighestCost { get; set; }
        public List<string> LowestCost { get; set; }
        public List<string> AverageCostForAllBrands { get; set; }
        public List<string> AlphabeticOrder { get; set; }

        public RestService service;

        public ICommand MaintenanceCostUnder { get; set; }


        private Brand selectedBrand;

        public Brand SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                if (value != null)
                {
                    selectedBrand = new Brand()
                    {
                        BrandName = value.BrandName,
                        BrandId = value.BrandId
                    };
                    OnPropertyChanged();
                    (DeleteBrandCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateBrandCommand { get; set; }

        public ICommand DeleteBrandCommand { get; set; }

        public ICommand UpdateBrandCommand { get; set; }

        public ICommand CarCommand { get; set; }

        public ICommand ServiceCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public BrandWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Brands = new RestCollection<Brand>("http://localhost:2810/", "brand", "hub");
                service = new RestService("http://localhost:2810/", "brand");
                CarCommand = new RelayCommand(() =>
                {
                    new MainWindow().Show();
                });
                ServiceCommand = new RelayCommand(() =>
                {
                    new ServiceWindow().Show();
                });

                HighestCost = new RestService("http://localhost:2810/").Get<string>("Values/HighestCost");
                foreach (var item in HighestCost)
                {
                    T_Box = item.ToString();
                }

                LowestCost = new RestService("http://localhost:2810/").Get<string>("Values/LowestCost");
                foreach (var item in LowestCost)
                {
                    T_Box2 = item.ToString();
                }

                AverageCostForAllBrands = new RestService("http://localhost:2810/").Get<string>("Values/AverageCostForAllBrands");
                foreach (var item in AverageCostForAllBrands)
                {
                    T_Box3 = item.ToString();
                }

                AlphabeticOrder = new RestService("http://localhost:2810/").Get<string>("Values/AlphabeticOrder");
                foreach (var item in AlphabeticOrder)
                {
                    T_Box4 = item.ToString();
                }

                MaintenanceCostUnder = new RelayCommand(() =>
                {
                    int data = int.Parse(Interaction.InputBox("MaintenanceCostUnder"));
                    double gaben = service.Get<double>(data, "Values/MCUnder");
                    MessageBox.Show($"MaintenanceCostUnder {data} there are {gaben} cars.");
                });


                CreateBrandCommand = new RelayCommand(() =>
                {
                    Brands.Add(new Brand()
                    {
                        BrandName = SelectedBrand.BrandName
                    });
                });

                UpdateBrandCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Brands.Update(SelectedBrand);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteBrandCommand = new RelayCommand(() =>
                {
                    Brands.Delete(SelectedBrand.BrandId);
                },
                () =>
                {
                    return SelectedBrand != null;
                });
                SelectedBrand = new Brand();
            }

        }
    }
}
