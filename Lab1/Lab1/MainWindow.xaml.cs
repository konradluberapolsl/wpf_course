using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    //
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> people = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();
           
        }

        public bool isNotEmpty(texBoxWithErrorProvider tb)
        {
            if (tb.Text.Trim() == "")
            {
                tb.SetError(tb.Text);
                return false;
            }
            return true;
        }

        public bool personAlredyExistys(Person p)
        {
            if (people.Contains(p))
                return true;
            return false;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (isNotEmpty(textBoxName) & isNotEmpty(textBoxSurname))
            {
               Person tmp = new Person(textBoxName.Text, textBoxSurname.Text, sliderWeight.Value, sliderAge.Value);
                if (!personAlredyExistys(tmp))
                    people.Add(tmp);
                else
                {
                    MessageBoxResult result = MessageBox.Show("Taka osoba już znajduje się na liście.\nChcesz ją dodać mimo to?","Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            people.Add(tmp);
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
                tmp = null;
                listBox.ItemsSource = people;
                textBoxName.Text="";
                textBoxSurname.Text = "";
                sliderAge.Value = 0;
                sliderWeight.Value = 0;
            }
        }
    }

    public class Person
    {
        private string name = "";
        public string Name { get { return name; } set { name = value; }}
        private string surname = "";
        public string Surname { get { return surname; } set { surname = value; } }
        private double weight = 0;
        public double Weight { get { return weight; } set { weight = value; } }
        private double age = 0;
        public double Age { get { return age; } set { age = value; } }
        public Person( string name, string surname, double weight, double age)
        {
            this.age = age;
            this.name = name;
            this.surname = surname;
            this.weight = weight;
        }
        public override string ToString()
        {
            return "Imie: " + name + " Nazwisko: " + surname + " Waga: " + weight.ToString("0") + " Wiek: " + age.ToString("0");
        }
        public bool Equals(Person other)
        {
            if (other == null)
                return false;
            if ((this.name == other.Name) && (this.surname==other.Surname) && (this.age==other.Age) && (this.weight==other.Weight))
            {
                return true; 
            }
            return false;
        }
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Person personObj = obj as Person;
            if (personObj == null)
                return false;
            else
                return Equals(personObj);
        }
    }
}
