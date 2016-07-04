using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MasterVeLog
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Cars : Page //, INotifyPropertyChanged
    {
        private RESTHandler objRest;

        ObservableCollection<VeLogCars> LstCars = new ObservableCollection<VeLogCars>();

        // Data grid from https://www.syncfusion.com/

        SfDataGrid dataGridCars = new SfDataGrid();

        public Cars()
        {
            this.InitializeComponent();

            // allowing the data grids to be filtered, sorted and adding the data grid items to the grid for display

            LoadCarData();
            dataGridCars.AllowFiltering = true;
            dataGridCars.AllowSorting = true;
            cars.Children.Add(dataGridCars);

            this.dataGridCars.FilterChanged += dataGridCars_FilterChanged;
        }

        // ****************************************************************************************************
        // using the API to get a response and adding the list items to the data grid


        private async void LoadCarData()      
        {
            try
            {
                objRest = new RESTHandler();
                string div = await objRest.GetRequestCar();

                LstCars = JsonConvert.DeserializeObject<ObservableCollection<VeLogCars>>(div);
                dataGridCars.ItemsSource = LstCars;                                 
            }
            catch (Exception e)
            {
                var messageDialog = new MessageDialog("Error: " + e.Message);
                await messageDialog.ShowAsync();
            }
        }

        // ****************************************************************************************************

        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            // clearing multiple filters at the same time instead of individually on the grid

            this.dataGridCars.ClearFilters();
        }

        private void dataGridCars_FilterChanged(object sender, GridFilterEventArgs e)
        {
            // clearing the text boxes if a record selected before filtering
            ClearCarTextFields();
        }

        private void btnVehicleLog_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }        

        // ****************************************************************************************************
        // displaying a seleced data grid item in text boxes

        private void cars_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (dataGridCars.SelectedIndex > -1)
            {
                VeLogCars recordCar = (VeLogCars)dataGridCars.SelectedItem; //casting the object

                txtCarId.Text = recordCar.Id.ToString();
                txtCarRego.Text = recordCar.Registration;
                txtCarMake.Text = recordCar.Make;
                txtCarModel.Text = recordCar.Model;
                txtCarColour.Text = recordCar.Colour;
            }            
        }        

        // ****************************************************************************************************
        // Cars CRUD

        private async void btnAddCar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCars.SelectedIndex == -1)
            {
                if (txtCarRego.Text != "" && txtCarMake.Text != "" && txtCarModel.Text != "" && txtCarColour.Text != "")
                {
                    if (txtCarRego.Text.Length <= 100 && txtCarMake.Text.Length <= 100 && txtCarModel.Text.Length <= 100 && txtCarColour.Text.Length <= 100)
                    {
                        VeLogCars velogCar = new VeLogCars();
                        velogCar.Registration = txtCarRego.Text;
                        velogCar.Make = txtCarMake.Text;
                        velogCar.Model = txtCarModel.Text;
                        velogCar.Colour = txtCarColour.Text;

                        objRest = new RESTHandler();
                        bool status = await objRest.AddRequestCar(velogCar);

                        if (status == true)
                        {
                            LoadCarData();
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("An error occurred, Car not added.");
                            await messageDialog.ShowAsync();
                        }

                        ClearCarTextFields();
                    }
                    else
                    {
                        var messageDialog = new MessageDialog("Check length of Car details.");
                        await messageDialog.ShowAsync();
                    }
                }
                else
                {
                    var messageDialog = new MessageDialog("Enter Car details.");
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                var messageDialog = new MessageDialog("This record already exists.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCars.SelectedIndex > -1)
            {
                objRest = new RESTHandler();
                bool status = await objRest.DeleteRequestCar(txtCarId.Text);

                if (status == true)
                {
                    LoadCarData();
                }
                else
                {
                    var messageDialog = new MessageDialog("An error occurred, Car not deleted.");
                    await messageDialog.ShowAsync();
                }

                ClearCarTextFields();
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnUpdateCar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCars.SelectedIndex > -1)
            {
                if (txtCarRego.Text != "" && txtCarMake.Text != "" && txtCarModel.Text != "" && txtCarColour.Text != "")
                {
                    if (txtCarRego.Text.Length <= 100 && txtCarMake.Text.Length <= 100 && txtCarModel.Text.Length <= 100 && txtCarColour.Text.Length <= 100)
                    {
                        VeLogCars velogCar = new VeLogCars();
                        velogCar.Id = Convert.ToInt32(txtCarId.Text);
                        velogCar.Registration = txtCarRego.Text;
                        velogCar.Make = txtCarMake.Text;
                        velogCar.Model = txtCarModel.Text;
                        velogCar.Colour = txtCarColour.Text;

                        objRest = new RESTHandler();
                        bool status = await objRest.UpdateRequestCar(txtCarId.Text, velogCar);

                        if (status == true)
                        {
                            LoadCarData();
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("An error occurred, Car not updated.");
                            await messageDialog.ShowAsync();
                        }

                        ClearCarTextFields();
                    }
                    else
                    {
                        var messageDialog = new MessageDialog("Check length of Car details.");
                        await messageDialog.ShowAsync();
                    }
                }
                else
                {
                    var messageDialog = new MessageDialog("Enter Car details.");
                    await messageDialog.ShowAsync();
                }                
                }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnDeselectCar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCars.SelectedIndex > -1)
            {
                dataGridCars.SelectedIndex = -1;
                ClearCarTextFields();
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        // ****************************************************************************************************
        // Clear all Text Fields

        private void ClearCarTextFields()
        {
            dataGridCars.SelectedIndex = -1;
            txtCarId.Text = "";
            txtCarRego.Text = "";
            txtCarMake.Text = "";
            txtCarModel.Text = "";
            txtCarColour.Text = "";
        }
    }
}
