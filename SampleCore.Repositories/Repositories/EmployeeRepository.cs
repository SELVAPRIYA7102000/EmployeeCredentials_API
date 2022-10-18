using SampleCore.Core.IRepositories;
using SampleCore.Core.Model;
using SampleCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCore.Repositories.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeDetailsContext entity;
        public EmployeeRepository(EmployeeDetailsContext empDetailsContext)
        {
            entity = empDetailsContext;
        }
        #region Create
        public void CreatePersonEntry(Core.Model.Employee employee)
        {
            if (employee != null)
            {
                if (employee.empid == 0)
                {

                   SampleCore.Entity.Emp_Details emp1 = new SampleCore.Entity.Emp_Details();
                    emp1.Employee_ID = employee.empid;
                    emp1.First_Name = employee.firstName;
                    emp1.Last_Name = employee.lastName;
                    emp1.Department = employee.department;
                    emp1.Gender = employee.gender;
                    emp1.Phone = employee.phone;
                    emp1.Email = employee.email;
                    emp1.Address = employee.address;
                    emp1.Age = employee.age;
                    emp1.Qualification = employee.qualification;
                    emp1.Password = employee.password;
                    emp1.Retype_Password = employee.retypePassword;
                    emp1.Is_Deleted = employee.Is_Deleted;
                    emp1.Created_Time_Stamp = DateTime.Now;
                    emp1.Updated_Time_Stamp = DateTime.Now;
                    entity.Add(emp1);
                    entity.SaveChanges();
                }

                else
                {
                    //  Employee pr = new Employee();
                    var data = entity.Emp_Details.Where(x => x.Employee_ID == employee.empid).SingleOrDefault();
                    if (data != null)
                    {
                        //   pr.empid = data.Employee_ID;
                         data.First_Name=employee.firstName;
                         data.Last_Name=employee.lastName;
                         data.Gender=employee.gender;
                         data.Age=employee.age;
                         data.Department=employee.department;
                         data.Qualification=employee.qualification;
                         data.Phone=employee.phone;
                         data.Address=employee.address;
                         data.Email=employee.email;
                         data.Password=employee.password;
                         data.Retype_Password=employee.password;
                         data.Is_Deleted = data.Is_Deleted;
                         data.Created_Time_Stamp = DateTime.Now;
                         data.Updated_Time_Stamp = DateTime.Now;
                         entity.SaveChanges();

                    }
                }
            }
        }
        
        #endregion


        #region Delete
        public void Delete(int employee_id)
        {
            var data = entity.Emp_Details.Where(x => x.Employee_ID == employee_id).SingleOrDefault();
            data.Is_Deleted = true;
            entity.SaveChanges();
        }

        #endregion


        #region Edit
        public Employee EditForm(int id)
        {
            Employee pr = new Employee();
            var data = entity.Emp_Details.Where(x => x.Employee_ID == id).SingleOrDefault();
            pr.empid = data.Employee_ID;
            pr.firstName = data.First_Name;
            pr.lastName = data.Last_Name;
            pr.gender = data.Gender;
            pr.age = data.Age;
            pr.department = data.Department;
            pr.qualification = data.Qualification;
            pr.phone = data.Phone;
            pr.address = data.Address;
            pr.email = data.Email;
            pr.password = data.Password;
            pr.retypePassword = data.Retype_Password;
            
            return pr;
        }
        //public void Update(int id, Employee model)
        //{
        //    var data = entity.Emp_Details.Where(x => x.Employee_ID == id).SingleOrDefault();
        //    if (data != null)
        //    {
        //        data.Employee_ID = model.empid;
        //        data.First_Name = model.firstName;
        //        data.Last_Name = model.lastName;
        //        data.Department = model.department;
        //        data.Qualification = model.qualification;
        //        data.Age = model.age;
        //        data.Gender = model.gender;
        //        data.Phone = model.phone;
        //        data.Address = model.address;
        //        data.Email = model.email;
        //        data.Password = model.password;
        //        data.Retype_Password = model.retypePassword;
        //        data.Created_Time_Stamp = DateTime.Now;
        //        data.Updated_Time_Stamp = DateTime.Now;
        //        entity.SaveChanges();
        //    }

        //}

        #endregion

        #region Read
        public List<Employee> Reads()
        {
            List<Employee> model = new List<Employee>();
            var data = entity.Emp_Details.Where(x => x.Is_Deleted == false).ToList();
            foreach (var item in data)
            {
                Employee pers = new Employee();
                pers.empid = item.Employee_ID;
                pers.firstName = item.First_Name;
                pers.lastName = item.Last_Name;
                pers.department = item.Department;
                pers.qualification = item.Qualification;
                pers.age = item.Age;
                pers.gender = item.Gender;
                pers.phone = item.Phone;
                pers.email = item.Email;
                pers.address = item.Address;
                pers.password = item.Password;
                pers.retypePassword = item.Retype_Password;
                pers.Updated_Time_Stamp = item.Updated_Time_Stamp;
                pers.Created_Time_Stamp = item.Created_Time_Stamp;
                model.Add(pers);
            }
            return model;
        }
        #endregion

    }

}
