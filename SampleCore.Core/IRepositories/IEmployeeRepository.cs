using SampleCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCore.Core.IRepositories
{
    public interface IEmployeeRepository
    {
        public void CreatePersonEntry(Employee employee);
        public void Delete(int employee_id);
        public Employee EditForm(int id);
       // public void Update(int id, Employee model);
         public  List<Employee> Reads();
    }

}
