using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPrismDemo.Business
{
    public class Person:INotifyPropertyChanged, IDataErrorInfo
    {
        #region Properties
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        private DateTime? _lastUpdated;
        public DateTime? LastUpdated
        {
            get { return _lastUpdated; }
            set
            {
                _lastUpdated = value;
                OnPropertyChanged("LastUpdated");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Error
        {
            get { return null; }
        }
        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch(columnName)
                {
                    case "FirstName":
                        if(string.IsNullOrWhiteSpace(_firstName))
                        {
                            error = "FirstName req";
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrWhiteSpace(_lastName))
                        {
                            error = "LastName req";
                        }
                        break;
                    case "Age":
                        if (_age < 18 || _age > 85)
                        {
                            error = "Age out of the range";
                        }
                        break;
                }

                return error;
            }
        }
    }
}
