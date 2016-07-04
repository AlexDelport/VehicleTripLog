using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MasterVeLog
{
    class RESTHandler

    // http://velogdataentry.azurewebsites.net/api/VeLogDataAPI - Vehicle Log Details
    // http://velogdataentry.azurewebsites.net/api/VelogDivisionAPI - Division Details
    // http://velogdataentry.azurewebsites.net/api/VelogCampusAPI - Campus Details
    // http://velogdataentry.azurewebsites.net/api/VelogCourseAPI - Course Details
    // http://velogdataentry.azurewebsites.net/api/VelogUsersAPI - User Details
    // http://velogdataentry.azurewebsites.net/api/VelogCarsAPI - Car Details

    // the following method was used for the request
    // http://www.c-sharpcorner.com/UploadFile/2b876a/consume-web-service-using-httpclient-to-post-and-get-json-da/

    {
        public async Task<string> GetRequestVehicleLog() // Reading Vehicle Log Details
        {
            try
            {
                Uri geturi = new Uri("http://velogdataentry.azurewebsites.net/api/VeLogDataAPI");
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception exVehicleGet)
            {
                var messageDialog = new MessageDialog("Error: " + exVehicleGet);
                await messageDialog.ShowAsync();
                return "error";
            }               
        }

        // ****************************************************************************************************
        // Division CRUD

        public async Task<string> GetRequestDivision()
        {
            try
            {
                Uri geturi = new Uri("http://velogdataentry.azurewebsites.net/api/VelogDivisionAPI");
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception exDivisionGet)
            {
                var messageDialog = new MessageDialog("Error: " + exDivisionGet);
                await messageDialog.ShowAsync();
                return "error";
            }                      
        }

        public async Task<bool> AddRequestDivision(VeLogDivisions velogDiv)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogDivisionAPI");
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(velogDiv);
                var objClient = new HttpClient();
                HttpResponseMessage responseAdd = await objClient.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                if (responseAdd.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exDivisionAdd)
            {
                var messageDialog = new MessageDialog("Error: " + exDivisionAdd);
                await messageDialog.ShowAsync();
                return false;
            }            
        }

        public async Task<bool> DeleteRequestDivision(string Id)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogDivisionAPI/" + Id);
                var objClient = new HttpClient();
                HttpResponseMessage responseDelete = await objClient.DeleteAsync(requestUri);

                if (responseDelete.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exDivisionDelete)
            {
                var messageDialog = new MessageDialog("Error: " + exDivisionDelete);
                await messageDialog.ShowAsync();
                return false;
            }
        }

        public async Task<bool> UpdateRequestDivision(string Id, VeLogDivisions velogDiv)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogDivisionAPI/" + Id);
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(velogDiv);
                var objClient = new HttpClient();
                HttpResponseMessage responseUpdate = await objClient.PutAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                if (responseUpdate.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exDivisionUpdate)
            {
                var messageDialog = new MessageDialog("Error: " + exDivisionUpdate);
                await messageDialog.ShowAsync();
                return false;
            }                       
        }

        // ****************************************************************************************************
        // Campus CRUD

        public async Task<string> GetRequestCampus()
        {
            try
            {
                Uri geturi = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCampusAPI");
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception exCampusGet)
            {
                var messageDialog = new MessageDialog("Error: " + exCampusGet);
                await messageDialog.ShowAsync();
                return "error";
            }            
        }

        public async Task<bool> AddRequestCampus(VeLogCampus velogCamp)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCampusAPI");
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(velogCamp);
                var objClient = new HttpClient();
                HttpResponseMessage responseAdd = await objClient.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                if (responseAdd.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exCampusAdd)
            {
                var messageDialog = new MessageDialog("Error: " + exCampusAdd);
                await messageDialog.ShowAsync();
                return false;
            }
        }

        public async Task<bool> DeleteRequestCampus(string Id)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCampusAPI/" + Id);
                var objClient = new HttpClient();
                HttpResponseMessage responseDelete = await objClient.DeleteAsync(requestUri);

                if (responseDelete.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exCampusDelete)
            {
                var messageDialog = new MessageDialog("Error: " + exCampusDelete);
                await messageDialog.ShowAsync();
                return false;
            }
        }

        public async Task<bool> UpdateRequestCampus(string Id, VeLogCampus velogCamp)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCampusAPI/" + Id);
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(velogCamp);
                var objClient = new HttpClient();
                HttpResponseMessage responseUpdate = await objClient.PutAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                if (responseUpdate.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exCampusUpdate)
            {
                var messageDialog = new MessageDialog("Error: " + exCampusUpdate);
                await messageDialog.ShowAsync();
                return false;
            }
        }

        // ****************************************************************************************************
        // Courses CRUD

        public async Task<string> GetRequestCourse()
        {
            try
            {
                Uri geturi = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCourseAPI");
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception exCourseGet)
            {
                var messageDialog = new MessageDialog("Error: " + exCourseGet);
                await messageDialog.ShowAsync();
                return "error";
            }            
        }

        public async Task<bool> AddRequestCourse(VeLogCourses velogCourse)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCourseAPI");
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(velogCourse);
                var objClient = new HttpClient();
                HttpResponseMessage responseAdd = await objClient.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                if (responseAdd.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exCourseAdd)
            {
                var messageDialog = new MessageDialog("Error: " + exCourseAdd);
                await messageDialog.ShowAsync();
                return false;
            }
        }

        public async Task<bool> DeleteRequestCourse(string Id)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCourseAPI/" + Id);
                var objClient = new HttpClient();
                HttpResponseMessage responseDelete = await objClient.DeleteAsync(requestUri);

                if (responseDelete.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exCourseDelete)
            {
                var messageDialog = new MessageDialog("Error: " + exCourseDelete);
                await messageDialog.ShowAsync();
                return false;
            }
        }

        public async Task<bool> UpdateRequestCourse(string Id, VeLogCourses velogCourse)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCourseAPI/" + Id);
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(velogCourse);
                var objClient = new HttpClient();
                HttpResponseMessage responseUpdate = await objClient.PutAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                if (responseUpdate.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exCourseUpdate)
            {
                var messageDialog = new MessageDialog("Error: " + exCourseUpdate);
                await messageDialog.ShowAsync();
                return false;
            }
        }

        // ****************************************************************************************************
        // Users CRUD

        // Users Service/API is NOT currently used by this App

        //public async Task<string> GetRequestUsers()
        //{
        //    try
        //    {
        //        Uri geturi = new Uri("http://velogdataentry.azurewebsites.net/api/VelogUsersAPI");
        //        HttpClient client = new HttpClient();
        //        HttpResponseMessage responseGet = await client.GetAsync(geturi);
        //        string response = await responseGet.Content.ReadAsStringAsync();
        //        return response;
        //    }
        //    catch (Exception exUsersGet)
        //    {
        //        var messageDialog = new MessageDialog("Error: " + exUsersGet);
        //        await messageDialog.ShowAsync();
        //        return "error";
        //    }
        //}

        // ****************************************************************************************************
        // Cars CRUD

        public async Task<string> GetRequestCar()
        {
            try
            {
                Uri geturi = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCarsAPI");
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception exCarGet)
            {
                var messageDialog = new MessageDialog("Error: " + exCarGet);
                await messageDialog.ShowAsync();
                return "error";
            }
        }

        public async Task<bool> AddRequestCar(VeLogCars velogCar)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCarsAPI");
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(velogCar);
                var objClient = new HttpClient();
                HttpResponseMessage responseAdd = await objClient.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                if (responseAdd.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exCarAdd)
            {
                var messageDialog = new MessageDialog("Error: " + exCarAdd);
                await messageDialog.ShowAsync();
                return false;
            }
        }

        public async Task<bool> DeleteRequestCar(string Id)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCarsAPI/" + Id);
                var objClient = new HttpClient();
                HttpResponseMessage responseDelete = await objClient.DeleteAsync(requestUri);

                if (responseDelete.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exCarDelete)
            {
                var messageDialog = new MessageDialog("Error: " + exCarDelete);
                await messageDialog.ShowAsync();
                return false;
            }
        }

        public async Task<bool> UpdateRequestCar(string Id, VeLogCars velogCar)
        {
            try
            {
                Uri requestUri = new Uri("http://velogdataentry.azurewebsites.net/api/VelogCarsAPI/" + Id);
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(velogCar);
                var objClient = new HttpClient();
                HttpResponseMessage responseUpdate = await objClient.PutAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                if (responseUpdate.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception exCarUpdate)
            {
                var messageDialog = new MessageDialog("Error: " + exCarUpdate);
                await messageDialog.ShowAsync();
                return false;
            }
        }
    }
}
