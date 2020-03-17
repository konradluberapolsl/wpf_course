using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.IO;
using Microsoft.Win32;

namespace Lab1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    //
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> people = new ObservableCollection<Person>(); // Spis osób
        private bool isDataDirty = false; // Odpowiada za sprawdzenie czy dane zostały zapisane  

        public MainWindow()
        {
            InitializeComponent();
            loadPeople();
           
        }

        public bool isNotEmpty(texBoxWithErrorProvider tb) // Sprawdza czy textbox nie jest  pusty
        {
            if (tb.Text.Trim() == "")
            {
                tb.SetError(tb.Text);
                return false;
            }
            return true;
        }

        public bool personAlredyExistys(Person p) //Sprawdza czy osoba już istnieje w spisie
        {
            if (people.Contains(p))
                return true;
            return false;
        }

        private void loadPeople() //ładuje spis z pliku
        {
            string[] lines = File.ReadAllLines(@"baza.txt");
            double tmp1 = 0, tmp2 = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] tmp = lines[i].Split(' ');
                for (int j = 0; j < tmp.Length; j++)
                {
                    if (j == 2)
                        tmp1 = Double.Parse(tmp[2]);
                    else if (j == 3)
                    {
                        tmp2 = Double.Parse(tmp[3]);
                        people.Add(new Person(tmp[0], tmp[1], tmp1, tmp2));
                    }
                }
            }
            listBox.ItemsSource = people;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (isNotEmpty(textBoxName) & isNotEmpty(textBoxSurname))
            {
               Person tmp = new Person(textBoxName.Text, textBoxSurname.Text, sliderWeight.Value, sliderAge.Value);
                if (!personAlredyExistys(tmp))
                {
                    people.Add(tmp);
                    isDataDirty = true;
                } 
                else
                {
                    MessageBoxResult result = MessageBox.Show("Taka osoba już znajduje się na liście.\nChcesz ją dodać mimo to?","Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            people.Add(tmp);
                            isDataDirty = true;
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
                tmp = null;
                //listBox.ItemsSource = people;
                textBoxName.Text="";
                textBoxSurname.Text = "";
                sliderAge.Value = 0;
                sliderWeight.Value = 0;

            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (isDataDirty == true)
            {
                //SaveFileDialog saveFileDialog = new SaveFileDialog();
                //saveFileDialog.Filter = "Text file (*.txt)|*.txt";
                //if (saveFileDialog.ShowDialog() == true)
                //{
                //    File.WriteAllText(saveFileDialog.FileName, String.Empty);
                //    foreach (var item in people)
                //    {
                //        File.AppendAllText(saveFileDialog.FileName, item.ToString() + "\n");
                //    }
                //}
                //File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);

                MessageBoxResult result = MessageBox.Show("Chcesz zapisać zmiany przed zamknięciem?", "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        File.WriteAllText("baza.txt", String.Empty);
                        foreach (var item in people)
                        {
                            File.AppendAllText("baza.txt", item.ToString() + "\n");
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

        }
    }


}
