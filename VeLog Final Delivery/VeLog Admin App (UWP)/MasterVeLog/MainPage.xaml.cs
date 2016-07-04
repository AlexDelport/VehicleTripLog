using Newtonsoft.Json;
using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MasterVeLog
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page //, INotifyPropertyChanged
    {        
        private RESTHandler objRest;

        // list to display in SyncFusion Data Grid

        ObservableCollection<VeLog> LstLog = new ObservableCollection<VeLog>(); // list to display in SyncFusion Data Grid 

        // Data grid from https://www.syncfusion.com/ 

        SfDataGrid dataGrid = new SfDataGrid();
        

        public MainPage()
        {
            this.InitializeComponent();

            // allowing the data grid to be filtered, sorted and adding the data grid items to the grid for display

            LoadLogData();
            dataGrid.AllowFiltering = true;
            dataGrid.AllowSorting = true;
            root.Children.Add(dataGrid);

            this.dataGrid.FilterChanged += dataGrid_FilterChanged;
        }

        // ****************************************************************************************************
        // using the API to get a response and adding the list items to the data grid

        private async void LoadLogData()       
        {
            try
            {
                objRest = new RESTHandler();
                string log = await objRest.GetRequestVehicleLog();

                LstLog = JsonConvert.DeserializeObject<ObservableCollection<VeLog>>(log);
                dataGrid.ItemsSource = LstLog;

                // sorting data grid, display most recent entry first  

                this.dataGrid.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = "CreationDate", SortDirection = ListSortDirection.Descending });             
            }
            catch (Exception e)
            {
                var messageDialog = new MessageDialog("Error: " + e.Message);
                await messageDialog.ShowAsync();
            }
        }

        // ****************************************************************************************************
        // exporting all or filtered data grid records to an Excel spreadsheet in the Users Downloads folder
        // the Downloads folder does not have a -- CreationCollisionOption.ReplaceExisting -- therefore it will give an error
        // if the file already exists, we have overcome this by giving each file a unique name
        // the UWP app automatically creates a folder with the app name within the destination path

        private async void btnExportToXLS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var options = new ExcelExportingOptions();

                string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + "h" + DateTime.Now.Minute.ToString() + "m" + DateTime.Now.Second.ToString() + "s";

                options.ExcelVersion = ExcelVersion.Excel2010; // <----- this can be changed for Vision to -----> options.ExcelVersion = ExcelVersion.Excel2016;

                var excelEngine = dataGrid.ExportToExcel(dataGrid.View, options);

                var workBook = excelEngine.Excel.Workbooks[0];

                StorageFile storageFile = await DownloadsFolder.CreateFileAsync("Velog_" + date + ".xlsx");

                if (storageFile != null)
                {
                    await workBook.SaveAsAsync(storageFile);
                    txtFolderName.Text = "Downloads/VeLog   <--- Copy & Paste in File Explorer";
                    txtFileName.Text =  "Velog_" + date + ".xlsx    <--- File Name";
                    var messageDialog = new MessageDialog("File successfully exported.");
                    await messageDialog.ShowAsync();
                }
                    
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog("File NOT exported!" + "\n" + "Error: " + ex.Message);
                await messageDialog.ShowAsync();
            }            
        }

        // ****************************************************************************************************

        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            // clearing multiple filters at the same time instead of individually on the grid

            dataGrid.SelectedIndex = -1;
            this.dataGrid.ClearFilters();
        }

        private void dataGrid_FilterChanged(object sender, GridFilterEventArgs e)
        {
            dataGrid.SelectedIndex = -1;
        }

        private void btnGLCodesAdmin_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GLCodes));
        }

        private void btnCarsAdmin_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Cars));
        }
    }
}
