using Microsoft.AspNetCore.Mvc;
using SampleCore.Core.IServices;
using SampleCore.Core.Model;
using System.Diagnostics;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeAPIController : Controller
        {

            private readonly IEmployeeServices _employeeServices;
            public EmployeeAPIController(IEmployeeServices services)
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
                _employeeServices.CreatePersonEntry(data);

                return RedirectToAction("Read");

            }

        #endregion

            #region Read data from table
            [HttpGet]
        public IActionResult Read()
            {
           
            var value = _employeeServices.Reads();
                return Ok(value);

            }
        #endregion

            #region Edit data
            [HttpGet]
        public IActionResult Edit(int id)
            {
                var value = _employeeServices.EditForm(id);
                return Ok(value);
            }
        //[HttpPost]
        //public IActionResult Edit(int id, Employee model)
        //{
        //    _employeeServices.Update(id, model);
        //    return RedirectToAction("Read");
        //}

        #endregion

        #region Delete 
        [HttpGet]
        public IActionResult Delete(int employee_id)
            {
             _employeeServices.Delete(employee_id);
            return View();
            }


            #endregion


        }
    }
