using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.SimpleDataBase
{
    public class Employee : BindableBase
    { 
        private int _employeeId;
        public int EmployeeId
        {
            get { return _employeeId; }
            set { SetProperty(ref _employeeId, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set{ SetProperty(ref _firstName, value);}
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set {SetProperty(ref _lastName, value); }
        }

        private int _deptNo;
        public int DeptNo
        {
            get { return _deptNo; }
            set {SetProperty(ref _deptNo, value); }
        }

        private double _salary;
        public double Salary
        {
            get { return _salary; }
            set {SetProperty(ref _salary, value); }
        }
    }
}
