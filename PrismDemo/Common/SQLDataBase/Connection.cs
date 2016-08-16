using CL.Lib.DB.G5DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.SQLDataBase
{
    public class Connection
    {
        public ObservableCollection<Employee> employee;
        private int _currentIndex;

        public ObservableCollection<Employee> GetData()
        {
            _currentIndex = 0;
            var g5 = CreateConnection(DbConnectionType.MainDb);
            employee = new ObservableCollection<Employee>();
            G5SelectBuilder select = new G5SelectBuilder();
            select.FirstPart = @"SELECT * FROM sysadm.employee_Szymon";
            select.BuildSelect();
            g5.Cmd(select);

            while (g5.Reader.Read())
            {
                employee.Add(new Employee()
                {
                    EmployeeId = g5.Db2Int32("employee_id"),
                    FirstName = g5.Db2Str("first_name"),
                    LastName = g5.Db2Str("last_name"),
                    Salary = g5.Db2Int32("dept_no"),
                    DeptNo = g5.Db2Int32("salary"),
                });
                _currentIndex = g5.Db2Int32("employee_id");
            }
            return employee;
        }         

        public ObservableCollection<Employee> AddNewWorker(string firstName, string lastName)
        {
            int dept = 10;
            int salary = 2000;
            var g5 = CreateConnection(DbConnectionType.MainDb);
            employee = new ObservableCollection<Employee>();
            G5SelectBuilder select = new G5SelectBuilder();
            //select.FirstPart = @"INSERT INTO sysadm.employee_Szymon VALUES (" + _currentIndex + ", '" + firstName + "', '" + lastName +"', " + dept + ", " + salary + ")";
            select.FirstPart = @"SELECT:INSERT INTO sysadm.employee_Szymon VALUES (12, 'few', 'fwe', 10, 2000)";
            select.BuildSelect();
            g5.Cmd(select);

            
            while (g5.Reader.Read())
            {
                employee.Add(new Employee()
                {
                    EmployeeId = g5.Db2Int32("employee_id"),
                    FirstName = g5.Db2Str("first_name"),
                    LastName = g5.Db2Str("last_name"),
                    Salary = g5.Db2Int32("dept_no"),
                    DeptNo = g5.Db2Int32("salary"),
                });
                _currentIndex = g5.Db2Int32("employee_id");
            }

            return employee;
        }

        protected G5Sql CreateConnection(DbConnectionType type = DbConnectionType.MainDb)
        {
            return new G5Sql("Server=PR1CUR1;User ID=sysadm;Password=adminklx1;Persist Security Info=True;Min Pool Size=4;Connection Lifetime=120", "System.Data.OracleClient");
        }
    }
}
