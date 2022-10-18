using EmployeeCredentials.Models;
using Microsoft.AspNetCore.Mvc;
using SampleCore.Core.IServices;
using SampleCore.Core.Model;
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
            return View();

        }

        [HttpPost]
        public IActionResult Index(Employee data)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7150/EmployeeAPI/Index");
                var PostTask = client.PostAsJsonAsync<Employee>(client.BaseAddress, data);
                PostTask.Wait();
                var checkresult = PostTask.Result;

                _employeeServices.CreatePersonEntry(data);

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
                client.BaseAddress = new Uri("https://localhost:7150/api/EmployeeAPI/Read");
                var GetTask = client.GetAsync(client.BaseAddress);
                GetTask.Wait();
                var result = GetTask.Result;
                var value = _employeeServices.Reads();
                return View(value);


            } }
        #endregion

        #region Edit data
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var value = _employeeServices.EditForm(id);
            return View("Index",value);
        }
        //[HttpPost]
        //public IActionResult Edit(int id, Employee model)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:7150/EmployeeAPI/Edit");
        //        var PostTask = client.PostAsJsonAsync<Employee>(client.BaseAddress,model);
        //        PostTask.Wait();
        //        var checkresult = PostTask.Result;
        //         _employeeServices.Update(id,model);

        //        return RedirectToAction("Read");
        //    }
        //}

        #endregion

        #region Delete 
        [HttpGet]
        public IActionResult Delete(int employee_id)
        {
            using (HttpClient client = new HttpClient())
            {
                
                client.BaseAddress = new Uri("https://localhost:7150/api/EmployeeAPI/Delete");
               
                _employeeServices.Delete(employee_id);
                return RedirectToAction("Read");
            }
        }


        #endregion


    }
}
