using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DataAccessLayer.Model;
using DataTransferLayer.DTO;


namespace BusinessAccessLayer.Repo
{
    public class BusinessLayer : IBusinessLayer
    {
        private dbClassesEntities context = new dbClassesEntities();
        

        public void Create(List<HelperModel> model)
        {
            using(DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Employee employee = new Employee();
                    EmpDetail empDetail = new EmpDetail();

                    foreach (var emp in model)
                    {
                        employee.EmployeeId = emp.EmployeeModel.EmployeeId;
                        employee.EmployeeName = emp.EmployeeModel.EmployeeName;
                        context.Employees.Add(employee);

                        empDetail.DetailId = emp.EmpDetailModel.DetailId;
                        empDetail.FatherName = emp.EmpDetailModel.FatherName;
                        empDetail.Address = emp.EmpDetailModel.Address;
                        empDetail.Contact = emp.EmpDetailModel.Contact;
                        context.EmpDetails.Add(empDetail);

                    }
                    context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }            
        }

        public void Delete(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();

            try
            {
                var data = context.Employees.Find(id);

                context.Entry(data).State = EntityState.Deleted;
                context.SaveChanges();
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
            }
        }

        public IEnumerable<HelperModel> Getemployee(int id)
        {
            IEnumerable<HelperModel> data = new List<HelperModel>();

            data = (from e in context.Employees
                    join d in context.EmpDetails on e.EmployeeId equals d.EmpId
                    where e.EmployeeId == id
                    select new HelperModel
                    {
                        EmployeeModel = new EmployeeModel
                        {
                            EmployeeId = e.EmployeeId,
                            EmployeeName = e.EmployeeName
                        },
                        EmpDetailModel = new EmpDetailModel
                        {
                            DetailId = d.DetailId,
                            FatherName = d.FatherName,
                            Address = d.Address,
                            Contact = d.Contact,
                            EmpId = d.EmpId
                        }
                    }).ToList();

            return data;
        }

        public IEnumerable<HelperModel> GetEmployees()
        {
            IEnumerable<HelperModel> data = new List<HelperModel>();

            data = (from e in context.Employees
                    join d in context.EmpDetails on e.EmployeeId equals d.EmpId
                    select new HelperModel
                    {
                        EmployeeModel = new EmployeeModel
                        {
                            EmployeeId = e.EmployeeId,
                            EmployeeName = e.EmployeeName
                        },
                        EmpDetailModel = new EmpDetailModel
                        {
                            DetailId = d.DetailId,
                            FatherName = d.FatherName,
                            Address = d.Address,
                            Contact = d.Contact,
                            EmpId = d.EmpId
                        }
                    }).ToList();

            return data;
        }

        public void Update(int id, List<HelperModel> model)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();

            try
            {
                var emp = context.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
                var dtl = context.EmpDetails.Where(x => x.EmpId == id).FirstOrDefault();

                foreach (var data in model)
                {
                    emp.EmployeeId = data.EmployeeModel.EmployeeId;
                    emp.EmployeeName = data.EmployeeModel.EmployeeName;
                    context.Entry(emp).State = EntityState.Modified;

                    dtl.FatherName = data.EmpDetailModel.FatherName;
                    dtl.Address = data.EmpDetailModel.Address;
                    dtl.Contact = data.EmpDetailModel.Contact;
                    dtl.EmpId = data.EmpDetailModel.EmpId;
                    context.Entry(dtl).State = EntityState.Modified;
                }
                 
                context.SaveChanges();
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
            }


        }
    }
}
