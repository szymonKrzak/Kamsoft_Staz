﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lib.Startup
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        //public ImageSource Image
        //{
        //    get { return (ImageSource)GetValue(ImageProperty); }
        //    set { SetValue(ImageProperty, value); }
        //}

        //public static readonly DependencyProperty ImageProperty =
        //   DependencyProperty.Register("Image", typeof(ImageSource), typeof(LoginView), new UIPropertyMetadata(null)); 
    }
}
