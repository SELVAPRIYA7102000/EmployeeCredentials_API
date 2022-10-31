

using SampleCore.Core.IRepositories;
using SampleCore.Core.IServices;
using SampleCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCore.Service.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void CreatePersonEntry(Employee employee)
        {
            //business condition
            if (employee.firstName != string.Empty && employee.firstName != string.Empty)
            {
                _employeeRepository.CreatePersonEntry(employee);
            }
        }
        public void Delete(int employee_id)
        {
            _employeeRepository.Delete(employee_id);
        }
        public Employee EditForm(int id)
        {
            return _employeeRepository.EditForm(id);

        }
        //public void Update(int id, Employee model)
        //{
        //    _employeeRepository.Update(id, model);

        //}
        public List<Employee> Reads()
        {
            return _employeeRepository.Reads();
        }

        public List<Employee> OnGet(string searchterm)
        {
           return  _employeeRepository.OnGet(searchterm);
        }

        public Employee Detail(int employee_id)
        {
            return _employeeRepository.Detail(employee_id);

        }
        public List<Employee> Dropdown()
        {
            return _employeeRepository.Dropdown();
        }
    }
}
