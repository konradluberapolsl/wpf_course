using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Logika interakcji dla klasy texBoxWithErrorProvider.xaml
    /// </summary>


    public partial class texBoxWithErrorProvider : UserControl
    {
        #region wlasciwosci
        public Brush TextBoxBorderBrush { get { return border.BorderBrush; }  set { border.BorderBrush = value; }  }
        public string Text { get { return textBox.Text; } set { textBox.Text = value; } }
        #endregion

        public texBoxWithErrorProvider()
        {
            InitializeComponent();
        }

        public void SetError(string errorText)
        {
            if (errorText == "")
            {
                border.BorderThickness = new Thickness(1);
                textBoxtToolTip.Visibility = Visibility.Visible;
                border.BorderBrush = Brushes.Red;
            }
               
            else
                border.BorderThickness = new Thickness(0);
        }
    }
}
