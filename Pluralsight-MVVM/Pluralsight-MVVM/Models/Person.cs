using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluralsight_MVVM.Models
{
    public class Person: INotifyPropertyChanged, IDataErrorInfo
    {
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

        private DateTime _updatedDate;

        public DateTime UpdatedDate
        {
            get { return _updatedDate; }
            set
            {
                _updatedDate = value;
                OnPropertyChanged("UpdatedDate");
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
                string error = string.Empty;

                if(string.IsNullOrEmpty(_firstName))
                {
                    error = "First Name is required";
                }

                return error;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
