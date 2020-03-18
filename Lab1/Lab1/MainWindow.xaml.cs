using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Text;


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
            listBox.ItemsSource = people;
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
           
        }

        private void clearValues()
        {
            textBoxName.Text = "";
            textBoxSurname.Text = "";
            sliderAge.Value = 0;
            sliderWeight.Value = 0;
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
                    MessageBoxResult result = MessageBox.Show("Taka osoba już znajduje się na liście.\nChcesz ją dodać mimo to?", "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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
                clearValues();

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

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                textBoxName.Text = people[listBox.SelectedIndex].Name;
                textBoxSurname.Text = people[listBox.SelectedIndex].Surname;
                sliderAge.Value = people[listBox.SelectedIndex].Age;
                sliderWeight.Value = people[listBox.SelectedIndex].Weight;

                //System.Windows.Controls.Button newBtn = new Button();
                //newBtn.Content = "A New Button";     
                //newBtn.Click += new RoutedEventHandler(newBtn_Click);
                //stackPanel.Children.Add(newBtn);
                buttonDelete.Visibility = Visibility.Visible;
                buttonChange.Visibility = Visibility.Visible;
            }
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            if (isNotEmpty(textBoxName) & isNotEmpty(textBoxSurname))
            {
                Person tmp = new Person(textBoxName.Text, textBoxSurname.Text, sliderWeight.Value, sliderAge.Value);
                if (!personAlredyExistys(tmp))
                {
                    people[listBox.SelectedIndex].Name = textBoxName.Text;
                    people[listBox.SelectedIndex].Surname = textBoxSurname.Text;
                    people[listBox.SelectedIndex].Age = sliderAge.Value;
                    people[listBox.SelectedIndex].Weight = sliderWeight.Value;
                    listBox.Items.Refresh();
                    clearValues();
                    buttonChange.Visibility = Visibility.Hidden;
                    isDataDirty = true;
                }
                else if (textBoxName.Text == people[listBox.SelectedIndex].Name & textBoxSurname.Text == people[listBox.SelectedIndex].Surname & sliderAge.Value == people[listBox.SelectedIndex].Age & sliderWeight.Value == people[listBox.SelectedIndex].Weight)
                {
                    MessageBox.Show("Nic nie zmieniłeś", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Taka osoba już znajduje się na liście.\nChcesz ją dodać mimo to?", "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            people.Add(tmp);
                            isDataDirty = true;
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                    clearValues();
                    buttonChange.Visibility = Visibility.Hidden;
                }
                tmp = null;



            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e) //BRAK ZABEZPIECZENIA PRZED ZMIANA ZAZNACZONEGO ELEMENTU TODO -KIEDYŚ :D  
        {
           
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć?" , "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    people.RemoveAt(listBox.SelectedIndex);
                    //listBox.Items.Refresh();
                    clearValues();
                    isDataDirty = true;
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }


}
