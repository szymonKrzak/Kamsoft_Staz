using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.SimpleDataBase
{
    public class DataBase
    {
        private ObservableCollection<Employee> _getDB;
        public ObservableCollection<Employee> GetDB
        {
            get
            {
                return new ObservableCollection<Employee>
                {
                    new Employee
                    {
                        EmployeeId = 1,
                        FirstName = "A",
                        LastName = "AA",
                        DeptNo = 1,
                        Salary = 123 
                    },
                    new Employee
                    {
                        EmployeeId = 2,
                        FirstName = "B",
                        LastName = "BB",
                        DeptNo = 2,
                        Salary = 234
                    },
                    new Employee
                    {
                        EmployeeId = 3,
                        FirstName = "C",
                        LastName = "CC",
                        DeptNo = 3,
                        Salary = 345
                    },
                    new Employee
                    {
                        EmployeeId = 4,
                        FirstName = "D",
                        LastName = "DD",
                        DeptNo = 4,
                        Salary = 456
                    }};
            }
            set { _getDB = value; }
        }
    }
}
