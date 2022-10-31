using EmployeeCredentials.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SampleCore.Core.IServices;
using SampleCore.Core.Model;
using SampleCore.Entity;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EmployeeCredentials.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;
        public HomeController(IEmployeeServices services)
        {

            _employeeServices = services;
        }

        #region   Create table

        [HttpGet]
        public IActionResult Index()
        {

            //EmployeeDetailsContext context = new EmployeeDetailsContext();
            //List<Emp_Location> locations = context.Emp_Location.ToList();
            //ViewBag.location = new SelectList(locations, "Emp_LocationID", "Location");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7150/api/EmployeeAPI/Index");
                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Employee>>();
                    readTask.Wait();
                    ViewBag.location = readTask.Result;
                    
                }

            }

            //var GetTask = client.GetAsync(client.BaseAddress);
            //GetTask.Wait();
            //var result = GetTask.Result;
            //var value = _employeeServices.Dropdown();
            //ViewBag.location = GetTask.Result;

            // }
            return View();

            
        }


        [HttpPost]
        public IActionResult Index(Employee data)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7150/api/EmployeeAPI/Index");
                var PostTask = client.PostAsJsonAsync<Employee>(client.BaseAddress, data);
                PostTask.Wait();
                var checkresult = PostTask.Result;
                return RedirectToAction("Read");

            }
        }

        #endregion

        #region Read data from table
        [HttpGet]
        public IActionResult Read()
        {
           
            using (HttpClient client = new HttpClient())
            {
                IEnumerable<Employee> emp = null;
                client.BaseAddress = new Uri("https://localhost:7150/api/EmployeeAPI/Read");
                var GetTask = client.GetAsync(client.BaseAddress);
                GetTask.Wait();
                var result = GetTask.Result;
                if (result.IsSuccessStatusCode)
                {
         var readTask = result.Content.ReadFromJsonAsync<IList<Employee>>();
                    readTask.Wait();
                    emp = readTask.Result;
                }
                return View(emp);


            } }
        #endregion

        #region Edit data
        [HttpGet]
        public IActionResult Edit(int id)
        {

            Index();
            Employee emp = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7150/api/EmployeeAPI/Edit?id=");
            var GetTask = client.GetAsync(client.BaseAddress+id.ToString());
                GetTask.Wait();
                var result = GetTask.Result;
             
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Employee>();
                    readTask.Wait();
                    emp = readTask.Result;
                }
            }


                return View("Index",emp);
            }
        
  
        #endregion





        #region Delete 
        [HttpGet]
        public IActionResult Delete(int employee_id)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7150/api/EmployeeAPI/Delete?employee_id=");

                var deleteTask = client.DeleteAsync(client.BaseAddress + employee_id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Read");
                }
            }
                return RedirectToAction("Read");
            
        }


        #endregion

        [HttpGet]
        public IActionResult Search(string searchterm)
        {
            if (searchterm != null)
            {
                List<Employee> emp = null;
                using (HttpClient client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://localhost:7150/api/EmployeeAPI/Search?searchterm=");
                    var GetTask = client.GetAsync(client.BaseAddress + searchterm);
                    GetTask.Wait();
                    var result = GetTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask= result.Content.ReadFromJsonAsync<IList<Employee>>();
                        readTask.Wait();
                        emp=(List<Employee>)readTask.Result;
                    }
                  //  _employeeServices.OnGet(searchterm);
                    return View("Read",emp);
                }
            }
            else
            {
                return RedirectToAction("Read");
            }
        }


        [HttpGet]
        public IActionResult Detail(int employee_id)
        {
            Employee emp = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://localhost:7150/api/EmployeeAPI/Detail?employee_id=");

                var GetTask = client.GetAsync(client.BaseAddress+employee_id.ToString());
                GetTask.Wait();
                var result = GetTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Employee>();
                    readTask.Wait();
                    emp = readTask.Result;
                }
               // var value = _employeeServices.Detail(employee_id);
                return PartialView(emp);
            }
        }
    }
}
