using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf
{
    /// <summary>
    /// Właściwości zależne - DependencyProperty str 68
    /// </summary>
    public class MyButton : DependencyObject
    {
        #region Teoria
        //A - nazwa klasy
        //public static readonly DependencyProperty FirstDependencyProperty = DependencyProperty.Register(name: "FirstDependency", propertyType: typeof(Boolean), ownerType: typeof(A), typeMetadata: new PropertyMetadata(false));

        //public bool FirstDependency
        //{
        //    get { return (bool)GetValue(A.FirstDependencyProperty); }
        //    set { SetValue(A.FirstDependencyProperty, value); }
        //}
        #endregion
        #region Przykład
        public static readonly DependencyProperty CounterProperty = DependencyProperty.Register(name: "Counter", propertyType: typeof(int), ownerType: typeof(MainWindow), typeMetadata: new FrameworkPropertyMetadata(0));

        public int Counter
        {
            get { return (int)GetValue(MyButton.CounterProperty); }
            set { SetValue(MyButton.CounterProperty, value); }
        }
        #endregion

    }

    

}
