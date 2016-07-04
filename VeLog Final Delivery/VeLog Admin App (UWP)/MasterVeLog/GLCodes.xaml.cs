using Newtonsoft.Json;
using Syncfusion.Data;
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

    public sealed partial class GLCodes : Page
    {
        private RESTHandler objRest;

        // lists to display in SyncFusion Data Grids

        ObservableCollection<VeLogDivisions> lstDivision = new ObservableCollection<VeLogDivisions>();        
        ObservableCollection<VeLogCampus> lstCampus = new ObservableCollection<VeLogCampus>();       
        ObservableCollection<VeLogCourses> lstCourse = new ObservableCollection<VeLogCourses>();

        // Data grid from https://www.syncfusion.com/        

        SfDataGrid dataGridDivision = new SfDataGrid();
        SfDataGrid dataGridCampus = new SfDataGrid();
        SfDataGrid dataGridCourse = new SfDataGrid();
        
        public GLCodes()
        {
            this.InitializeComponent();

            // allowing the data grids to be filtered, sorted and adding the data grid items to the grid for display

            LoadDivisionData();
            dataGridDivision.AllowFiltering = true;
            dataGridDivision.AllowSorting = true;
            division.Children.Add(dataGridDivision);

            LoadCampusData();
            dataGridCampus.AllowFiltering = true;
            dataGridCampus.AllowSorting = true;
            campus.Children.Add(dataGridCampus);

            LoadCourseData();
            dataGridCourse.AllowFiltering = true;
            dataGridCourse.AllowSorting = true;
            course.Children.Add(dataGridCourse);

            this.dataGridDivision.FilterChanged += dataGridDivision_FilterChanged;
            this.dataGridCampus.FilterChanged += dataGridCampus_FilterChanged;
            this.dataGridCourse.FilterChanged += dataGridCourse_FilterChanged;

        }

        // ****************************************************************************************************
        // using the API to get a response and adding the list items to the data grid

        private async void LoadDivisionData()        
        {
            try
            {
                objRest = new RESTHandler();
                string div = await objRest.GetRequestDivision();

                lstDivision = JsonConvert.DeserializeObject<ObservableCollection<VeLogDivisions>>(div);
                dataGridDivision.ItemsSource = lstDivision; 
            }
            catch (Exception e)
            {
                var messageDialog = new MessageDialog("Error: " + e.Message);
                await messageDialog.ShowAsync();
            }
        }

        private async void LoadCampusData()      
        {
            try
            {
                objRest = new RESTHandler();
                string Campus = await objRest.GetRequestCampus();

                lstCampus = JsonConvert.DeserializeObject<ObservableCollection<VeLogCampus>>(Campus);
                dataGridCampus.ItemsSource = lstCampus;                         
            }
            catch (Exception e)
            {
                var messageDialog = new MessageDialog("Error: " + e.Message);
                await messageDialog.ShowAsync();
            }
        }

        private async void LoadCourseData()    
        {
            try
            {
                objRest = new RESTHandler();
                string Course = await objRest.GetRequestCourse();

                lstCourse = JsonConvert.DeserializeObject<ObservableCollection<VeLogCourses>>(Course);
                dataGridCourse.ItemsSource = lstCourse;                           
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

            this.dataGridDivision.ClearFilters();
            this.dataGridCampus.ClearFilters();
            this.dataGridCourse.ClearFilters();
        }

        // **************************************************************
        // clearing the text boxes if a record selected before filtering

        private void dataGridDivision_FilterChanged(object sender, GridFilterEventArgs e)
        {            
            ClearDivisionTextFields();
        }

        private void dataGridCampus_FilterChanged(object sender, GridFilterEventArgs e)
        {
            ClearCampusTextFields();
        }

        private void dataGridCourse_FilterChanged(object sender, GridFilterEventArgs e)
        {
            ClearCourseTextFields();
        }

        // **************************************************************

        private void btnVehicleLog_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        // ****************************************************************************************************
        // displaying a seleced data grid item in text boxes

        private void division_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (dataGridDivision.SelectedIndex > -1)
            {
                VeLogDivisions record = (VeLogDivisions)dataGridDivision.SelectedItem; //casting the object

                txtDivisionId.Text = record.Id.ToString();
                txtDivision.Text = record.Division;
            }                
        }

        private void campus_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (dataGridCampus.SelectedIndex > -1)
            {
                VeLogCampus record = (VeLogCampus)dataGridCampus.SelectedItem; //casting the object

                txtCampusId.Text = record.Id.ToString();
                txtCampus.Text = record.Campus;            
            }            
        }

        private void course_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (dataGridCourse.SelectedIndex > -1)
            {
                VeLogCourses recordCourse = (VeLogCourses)dataGridCourse.SelectedItem; //casting the object

                txtCourseId.Text = recordCourse.Id.ToString();
                txtCourse.Text = recordCourse.Courses;
                txtCourseDiv.Text = recordCourse.Division;            
            }            
        }

        // ****************************************************************************************************
        // Division CRUD

        private async void btnAddDivision_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridDivision.SelectedIndex == -1)
            {
                if (txtDivision.Text != "")
                {
                    if (txtDivision.Text.Length <= 100)
                    {
                        VeLogDivisions velogDiv = new VeLogDivisions();
                        velogDiv.Division = txtDivision.Text;

                        objRest = new RESTHandler();
                        bool status = await objRest.AddRequestDivision(velogDiv);

                        if (status == true)
                        {
                            LoadDivisionData();
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("An error occurred, Division not added.");
                            await messageDialog.ShowAsync();
                        }

                        ClearDivisionTextFields();
                    }
                    else
                    {
                        var messageDialog = new MessageDialog("Check Division length.");
                        await messageDialog.ShowAsync();
                    }                    
                }
                else
                {
                    var messageDialog = new MessageDialog("Enter Division details.");
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                var messageDialog = new MessageDialog("This record already exists.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnDeleteDivision_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridDivision.SelectedIndex > -1)
            {
                objRest = new RESTHandler();
                bool status = await objRest.DeleteRequestDivision(txtDivisionId.Text);

                if (status == true)
                {
                    LoadDivisionData();
                }
                else
                {
                    var messageDialog = new MessageDialog("An error occurred, Division not deleted.");
                    await messageDialog.ShowAsync();
                }

                ClearDivisionTextFields();
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnUpdateDivision_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridDivision.SelectedIndex > -1)
            {
                if (txtDivision.Text != "")
                {
                    if (txtDivision.Text.Length <= 100)
                    {
                        VeLogDivisions velogDiv = new VeLogDivisions();
                        velogDiv.Id = Convert.ToInt32(txtDivisionId.Text);
                        velogDiv.Division = txtDivision.Text;

                        objRest = new RESTHandler();
                        bool status = await objRest.UpdateRequestDivision(txtDivisionId.Text, velogDiv);

                        if (status == true)
                        {
                            LoadDivisionData();
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("An error occurred, Division not updated.");
                            await messageDialog.ShowAsync();
                        }

                        ClearDivisionTextFields();                    
                    }
                    else
                    {
                        var messageDialog = new MessageDialog("Check Division length.");
                        await messageDialog.ShowAsync();
                    }
                }
                else
                {
                    var messageDialog = new MessageDialog("Enter Division details.");
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnDeselectDivision_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridDivision.SelectedIndex > -1)
            {
                dataGridDivision.SelectedIndex = -1;
                ClearDivisionTextFields();
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        // ****************************************************************************************************
        // Campus CRUD

        private async void btnAddCampus_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCampus.SelectedIndex == -1)
            {
                if (txtCampus.Text != "")
                {
                    if (txtCampus.Text.Length <= 100)
                    {
                        VeLogCampus velogCamp = new VeLogCampus();
                        velogCamp.Campus = txtCampus.Text;

                        objRest = new RESTHandler();
                        bool status = await objRest.AddRequestCampus(velogCamp);

                        if (status == true)
                        {
                            LoadCampusData();
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("An error occurred, Campus not added.");
                            await messageDialog.ShowAsync();
                        }

                        ClearCampusTextFields();
                    }
                    else
                    {
                        var messageDialog = new MessageDialog("Check Campus length.");
                        await messageDialog.ShowAsync();
                    }
                }
                else
                {
                    var messageDialog = new MessageDialog("Enter Campus details.");
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                var messageDialog = new MessageDialog("This record already exists.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnDeleteCampus_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCampus.SelectedIndex > -1)
            {                
                objRest = new RESTHandler();
                bool status = await objRest.DeleteRequestCampus(txtCampusId.Text);

                if (status == true)
                {
                    LoadCampusData();
                }
                else
                {
                    var messageDialog = new MessageDialog("An error occurred, Campus not deleted.");
                    await messageDialog.ShowAsync();
                }

                ClearCampusTextFields();
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnUpdateCampus_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCampus.SelectedIndex > -1)
            {
                if (txtCampus.Text != "")
                {
                    if (txtCampus.Text.Length <= 100)
                    {
                        VeLogCampus velogCamp = new VeLogCampus();
                        velogCamp.Id = Convert.ToInt32(txtCampusId.Text);
                        velogCamp.Campus = txtCampus.Text;

                        objRest = new RESTHandler();
                        bool status = await objRest.UpdateRequestCampus(txtCampusId.Text, velogCamp);

                        if (status == true)
                        {
                            LoadCampusData();
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("An error occurred, Campus not updated.");
                            await messageDialog.ShowAsync();
                        }

                        ClearCampusTextFields();
                    }
                    else
                    {
                        var messageDialog = new MessageDialog("Check Campus length.");
                        await messageDialog.ShowAsync();
                    }
                }
                else
                {
                    var messageDialog = new MessageDialog("Enter Campus details.");
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnDeselectCampus_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCampus.SelectedIndex > -1)
            {
                dataGridCampus.SelectedIndex = -1;
                ClearCampusTextFields();
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        // ****************************************************************************************************
        // Course CRUD

        private async void btnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCourse.SelectedIndex == -1)
            {
                if (txtCourse.Text != "" && txtCourseDiv.Text != "")
                {
                    if (txtCourse.Text.Length <= 100 && txtCourseDiv.Text.Length <= 100)
                    {
                        VeLogCourses velogCourse = new VeLogCourses();
                        velogCourse.Courses = txtCourse.Text;
                        velogCourse.Division = txtCourseDiv.Text;

                        objRest = new RESTHandler();
                        bool status = await objRest.AddRequestCourse(velogCourse);

                        if (status == true)
                        {
                            LoadCourseData();
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("An error occurred, Course not added.");
                            await messageDialog.ShowAsync();
                        }

                        ClearCourseTextFields();
                    }
                    else
                    {
                        var messageDialog = new MessageDialog("Check length of Course details.");
                        await messageDialog.ShowAsync();
                    }
                }
                else
                {
                    var messageDialog = new MessageDialog("Enter Course details.");
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                var messageDialog = new MessageDialog("This record already exists.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnDeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCourse.SelectedIndex > -1)
            {
                objRest = new RESTHandler();
                bool status = await objRest.DeleteRequestCourse(txtCourseId.Text);

                if (status == true)
                {
                    LoadCourseData();
                }
                else
                {
                    var messageDialog = new MessageDialog("An error occurred, Course not deleted.");
                    await messageDialog.ShowAsync();
                }

                ClearCourseTextFields();
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnUpdateCourse_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCourse.SelectedIndex > -1)
            {
                if (txtCourse.Text != "" && txtCourseDiv.Text != "")
                {
                    if (txtCourse.Text.Length <= 100 && txtCourseDiv.Text.Length <= 100)
                    {
                        VeLogCourses velogCourse = new VeLogCourses();
                        velogCourse.Id = Convert.ToInt32(txtCourseId.Text);
                        velogCourse.Courses = txtCourse.Text;
                        velogCourse.Division = txtCourseDiv.Text;

                        objRest = new RESTHandler();
                        bool status = await objRest.UpdateRequestCourse(txtCourseId.Text, velogCourse);

                        if (status == true)
                        {
                            LoadCourseData();
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("An error occurred, Course not updated.");
                            await messageDialog.ShowAsync();
                        }

                        ClearCourseTextFields();
                    }
                    else
                    {
                        var messageDialog = new MessageDialog("Check length of Course Details.");
                        await messageDialog.ShowAsync();
                    }
                }
                else
                {
                    var messageDialog = new MessageDialog("Enter Course details.");
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        private async void btnDeselectCourse_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCourse.SelectedIndex > -1)
            {
                dataGridCourse.SelectedIndex = -1;
                ClearCourseTextFields();
            }
            else
            {
                var messageDialog = new MessageDialog("No record selected.");
                await messageDialog.ShowAsync();
            }
        }

        // ****************************************************************************************************
        // Clear all Text Fields

        private void ClearDivisionTextFields()
        {
            dataGridDivision.SelectedIndex = -1;
            txtDivisionId.Text = "";
            txtDivision.Text = "";
        }

        private void ClearCampusTextFields()
        {
            dataGridCampus.SelectedIndex = -1;
            txtCampusId.Text = "";
            txtCampus.Text = "";
        }

        private void ClearCourseTextFields()
        {
            dataGridCourse.SelectedIndex = -1;
            txtCourseId.Text = "";
            txtCourse.Text = "";
            txtCourseDiv.Text = "";
        }
    }
}
