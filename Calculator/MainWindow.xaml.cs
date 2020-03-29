using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace Calculator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            //Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }

        private bool isNumber(string text)
        {
            double temp;
            if (double.TryParse(text, out temp))
                return true;

            return false;
        
        }

        private void B_Digit_Clicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (isNumber(Operation.Text) || Operation.Text=="")
                if (Operation.Text == "0")
                    Operation.Text = button.Content.ToString();
                else
                    Operation.Text += button.Content.ToString();
        }
        private void B_Dot_Clicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (isNumber(Operation.Text))
                if (!Operation.Text.Contains('.'))
                    Operation.Text += button.Content.ToString();
                
            
        }

        private void B_Function_Clicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (isNumber(Operation.Text))
            {
                if (Operation.Text != "0" && Equation.Text == "0")
                {
                    Equation.Text = Operation.Text + button.Content.ToString();
                    Operation.Text = 0.ToString();
                }
                else if (Operation.Text != "0" && Equation.Text != "0")
                {
                    Equation.Text += Operation.Text + button.Content.ToString();
                    Operation.Text = 0.ToString();
                }
            }
        }

        private void B_Equal_Clicked(object sender, RoutedEventArgs e)
        {
            {
                string math = (Equation.Text += Operation.Text);
                Equation.Text = "0";
                Operation.Text = calculate(math);
            }
        }

        private void B_Delete_Clicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Name == "B_C")
            {
                Equation.Text = "0";
                Operation.Text = "0";
            }
            else if (button.Name == "B_CE")
                Operation.Text = "0";
            else if (button.Name == "B_Delete")
            {
                if (Operation.Text != "0" && isNumber(Operation.Text))
                {
                    if (Operation.Text.Length == 1)
                        Operation.Text = "0";
                    else
                        Operation.Text = Operation.Text.Remove(Operation.Text.Length - 1, 1);

                }
            }
        }

        private string calculate(string math)
        {
            math = Regex.Replace
            (
                math, @"\d+(\.\d+)?", m =>
                {
                    var x = m.ToString();
                    return x.Contains(".") ? x : string.Format("{0}.0", x);
                }
            );
            try
            {
                double value = Math.Round(Convert.ToDouble(new DataTable().Compute(math, string.Empty)), 8);
                return (value < -9999999999 || value > 9999999999) ? "Out of range" : value.ToString();
            }
            catch (DivideByZeroException)
            {
                return "Cannot divide by zero";
            }
            catch
            {
                return "Error";
            }
        }


    }
}
