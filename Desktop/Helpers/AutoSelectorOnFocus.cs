using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Helpers
{
    class AutoSelectorOnFocus
    { 
        public static void TextBox_GotFocus(object sender, RoutedEventArgs e) => (sender as TextBox).SelectAll();
        public static void PasswordBox_GotFocus(object sender, RoutedEventArgs e) => (sender as PasswordBox).SelectAll();
    }
}
