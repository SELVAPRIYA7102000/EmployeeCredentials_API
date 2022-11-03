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
                    emp1.Emp_LocationID = employee.Emp_LocationID;
                    emp1.Date_Of_Birth = DateTime.Parse(employee.Dob);
                    entity.Add(emp1);
                    entity.SaveChanges();
                    
                }

                else
                {
                    var data = entity.Emp_Details.Where(x => x.Employee_ID == employee.empid).SingleOrDefault();

                   
                    if (data != null)
                    {

                        var value = data.Emp_LocationID;
                        var val1 = entity.Emp_Location.Where(x => x.Emp_LocationID == value).SingleOrDefault();
                        employee.location = val1.Location;

                        
                        data.First_Name = employee.firstName;
                        data.Last_Name = employee.lastName;
                        data.Gender = employee.gender;
                        data.Age = employee.age;
                        data.Department = employee.department;
                        data.Qualification = employee.qualification;
                        data.Phone = employee.phone;
                        data.Address = employee.address;
                        data.Email = employee.email;
                        data.Password = employee.password;
                        data.Retype_Password = employee.password;
                        data.Date_Of_Birth = DateTime.Parse(employee.Dob);
                        data.Is_Deleted = employee.Is_Deleted;
                        data.Created_Time_Stamp = DateTime.Now;
                        data.Updated_Time_Stamp = DateTime.Now;
                        data.Emp_LocationID = employee.Emp_LocationID;
                        
                      
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
            Dropdown();

            var value = data.Emp_LocationID;
            var val1 = entity.Emp_Location.Where(x => x.Emp_LocationID == value).SingleOrDefault();

            pr.Dob = data.Date_Of_Birth.ToString("yyyy-MM-dd");
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
            pr.location = val1.Location;
            pr.Emp_LocationID = val1.Emp_LocationID;
            return pr;
        }
       

        #endregion

        #region Read
        public List<Employee> Reads()
        {

            var data = (from item in entity.Emp_Details.Where(x => x.Is_Deleted == false)
                        join location in entity.Emp_Location on item.Emp_LocationID equals location.Emp_LocationID into empsal
                        from location in empsal.DefaultIfEmpty()

                        select new Employee
                        {

                            empid = item.Employee_ID,
                            firstName = item.First_Name,
                            lastName = item.Last_Name,
                            department = item.Department,
                            qualification = item.Qualification,
                            age = item.Age,
                            gender = item.Gender,
                            phone = item.Phone,
                            email = item.Email,
                            location = location.Location,
                            address = item.Address,
                            password = item.Password,

                            Dob = item.Date_Of_Birth.ToString("yyyy-MM-dd"),
                            retypePassword = item.Retype_Password,

                            Updated_Time_Stamp = item.Updated_Time_Stamp,
                            Created_Time_Stamp = item.Created_Time_Stamp

                        }).ToList();
            return data;
        }
        //List<Employee> model = new List<Employee>();
        //    var data = entity.Emp_Details.Where(x => x.Is_Deleted == false).ToList();

        //    foreach (var item in data)
        //    {
        //        Employee pers = new Employee();
        //        pers.empid = item.Employee_ID;
        //        pers.firstName = item.First_Name;
        //        pers.lastName = item.Last_Name;
        //        pers.department = item.Department;
        //        pers.qualification = item.Qualification;
        //        pers.age = item.Age;
        //        pers.gender = item.Gender;
        //        pers.phone = item.Phone;
        //        pers.email = item.Email;
        //        pers.address = item.Address;
        //        var value = item.Emp_LocationID;
        //        var datas = entity.Emp_Location.Where(x => x.Emp_LocationID == value).SingleOrDefault();
        //        pers.location = datas.Location;
        //        pers.Dob = item.Date_Of_Birth.ToString("yyyy-MM-dd");
        //        pers.password = item.Password;
        //        pers.retypePassword = item.Retype_Password;
        //        pers.Updated_Time_Stamp = item.Updated_Time_Stamp;
        //        pers.Created_Time_Stamp = item.Created_Time_Stamp;
        //        model.Add(pers);
        //    }

        //    return model;
        //}


        public List<Employee> Dropdown()
        {
            List<Employee> model = new List<Employee>();
            var data = entity.Emp_Location.Where(x => x.Is_Deleted == false).ToList();
            if (data != null)
            {
                foreach (var item in data)
                {
                    Employee pers = new Employee();
                    pers.Emp_LocationID = item.Emp_LocationID;
                    pers.location = item.Location;

                    model.Add(pers);
                }
            }
            return model;

        }


       


        #endregion

        #region search
        public List<Employee> OnGet(string searchterm)
        {
            if (searchterm != null)
            {

                List<Employee> emp = new List<Employee>();
                var data = entity.Emp_Details.Where((x) => x.First_Name.Contains(searchterm)).ToList();
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
                    emp.Add(pers);
                }
                return emp;
            }
            else
            {
                return Reads();
            }
        }
        #endregion
        public Employee Detail(int employee_id)
        {


            Employee pr = new Employee();
            var data = entity.Emp_Details.Where(x => x.Employee_ID == employee_id).SingleOrDefault();
           
            var value = data.Emp_LocationID;
            var val1 = entity.Emp_Location.Where(x => x.Emp_LocationID == value).SingleOrDefault();
 
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
            pr.location = val1.Location;
            return pr;

        }
    }
}




