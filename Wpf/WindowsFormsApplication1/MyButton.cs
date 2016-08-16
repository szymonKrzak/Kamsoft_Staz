using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ButtonCustomDependencyProperty
{
    class MyButton : DependencyObject
    {
        public static readonly DependencyProperty CounterProperty = DependencyProperty.Register(name: "Counter", propertyType: typeof(int), ownerType: typeof(MyButton), typeMetadata: new FrameworkPropertyMetadata(0));

        public int Counter
        {
            get { return (int)GetValue(MyButton.CounterProperty); }
            set { SetValue(MyButton.CounterProperty, value); }
        }
    }
}
