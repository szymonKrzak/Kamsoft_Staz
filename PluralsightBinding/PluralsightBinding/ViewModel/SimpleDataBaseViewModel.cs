using PluralsightBinding.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightBinding.ViewModel
{
    public class SimpleDataBaseViewModel
    {
        private ObservableCollection<Customer> _dataBase;
        public ObservableCollection<Customer> DataBase
        {
            get
            {
                return new ObservableCollection<Customer>
                {
                    new Customer
                    {
                        Id = 1,
                        FirstName = "a",
                        LastName = "aa",
                        Orders = new ObservableCollection<Order>
                        {
                            new Order { Id = 1, ItemsTotal = 11.1 },
                            new Order { Id = 11, ItemsTotal = 111.1 }
                        }
                    },
                    new Customer
                    {
                        Id = 2,
                        FirstName = "b",
                        LastName = "bb",
                        Orders = new ObservableCollection<Order>
                            {
                                new Order { Id = 2, ItemsTotal = 22.2 }
                            }
                    },
                    new Customer
                    {
                        Id = 3,
                        FirstName = "c",
                        LastName = "cc",
                        Orders = new ObservableCollection<Order>
                            {
                                new Order { Id = 3, ItemsTotal = 33.3 }
                            }
                    }
                    };
            }
            set { _dataBase = value; }
        }
    }
}
