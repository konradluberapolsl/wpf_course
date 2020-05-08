using players_mvvm.Model;
using players_mvvm.ViewModel.BaseClass;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace players_mvvm.ViewModel
{
    class P_Management : ViewModelBase
    {
        private string dataPath = @"database.json";

        private string name = null;
        private string surname = null;
        private uint age = 15;
        private uint weight = 50;


        private Player selectedPlayer = null;
        private BindingList<Player> players = new BindingList<Player>();

        #region Properties 

        public string Name
        {
            get => name;
            set
            {
                name = value;
                onPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                onPropertyChanged(nameof(Surname));
            }
        }

        public uint Age
        {
            get => age;
            set
            {
                age = value;
                onPropertyChanged(nameof(Age));
            }
        }

        public uint Weight
        {
            get => weight;
            set
            {
                weight = value;
                onPropertyChanged(nameof(Weight));
            }
        }

        public Player SelectedPlayer
        {
            get => selectedPlayer;
            set
            {
                selectedPlayer = value;
                onPropertyChanged(nameof(SelectedPlayer));
                if (ChangePlayer.CanExecute(null)) ChangePlayer.Execute(null);
            }
        }

        public BindingList<Player> Players
        {
            get => players;
            set
            {
                players = value;
                onPropertyChanged(nameof(Players));
            }
        }

        #endregion

        #region Commands

        private bool FieldsNotNull { get { return (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) && Age > 0 && Weight > 0); } }

        private ICommand addPlayer;
        private ICommand deletePlayer;
        private ICommand modifyPlayer;
       // private ICommand clear;
        private ICommand changePlayer;
        private ICommand loadData;
        private ICommand saveData;


        public ICommand AddPlayer
        {
            get
            {
                if (addPlayer is null)
                {
                    addPlayer = new RelayCommand(execute =>
                    {
                        var player = new Player(Name, Surname, (uint)Age, (uint)Weight);
                        if (!Players.Contains(player))
                        {
                            Players.Add(player);
                            onPropertyChanged(nameof(Players));
                        }
                    }, canExecute => FieldsNotNull);
                }
                return addPlayer;
            }
        }
        public ICommand DeletePlayer
        {
            get
            {
                if (deletePlayer == null)
                {
                    deletePlayer = new RelayCommand(execute =>
                    {
                        var player = new Player(Name, Surname, (uint)Age, (uint)Weight);
                        if (Players.Contains(player))
                        {
                            Players.Remove(player);
                            onPropertyChanged(nameof(Players));
                        }
                    }, canExecute => FieldsNotNull && SelectedPlayer != null
                    );
                }
                return deletePlayer;
            }
        }

        public ICommand ModifyPlayer
        {
            get
            {
                if (modifyPlayer is null)
                {
                    modifyPlayer = new RelayCommand(execute =>
                    {
                        var player = new Player(Name, Surname, (uint)Age, (uint)Weight);
                        if (Players.Contains(SelectedPlayer))
                        {
                            var index = players.IndexOf(selectedPlayer);
                            Players[index].Copy(player);
                            Players.ResetItem(index);
                        }
                    }, canExecute => FieldsNotNull && SelectedPlayer != null);
                }
                return modifyPlayer;
            }
        }

        //public ICommand Clear
        //{
        //    get
        //    {
        //        if (clear is null)
        //        {
        //            clear = new RelayCommand(
        //                execute =>
        //                {
        //                    Name = null;
        //                    Surname = null;
        //                    Age = 15;
        //                    Weight = 50;
        //                }, canExecute => true);
        //        }
        //        return clear;
        //    }
        //} nie uzywane



        public ICommand ChangePlayer
        {
            get
            {
                if (changePlayer is null)
                {
                    changePlayer = new RelayCommand(
                        execute =>
                        {
                            Name = SelectedPlayer.Name;
                            Surname = SelectedPlayer.Surname;
                            Age = (uint)(SelectedPlayer.Age);
                            Weight = (uint)(SelectedPlayer.Weight);
                        }, canExecute => SelectedPlayer != null);
                }
                return changePlayer;
            }
        }

        public ICommand Load
        {
            get
            {
                if (loadData is null)
                {
                    loadData = new RelayCommand(execute =>
                    {
                        try
                        {
                            var jsonPlayers = File.ReadAllText(dataPath);
                            Players = JsonConvert.DeserializeObject<BindingList<Player>>(jsonPlayers);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        onPropertyChanged(nameof(Load));
                        Players.ResetBindings();
                    }, canExecute => File.Exists(dataPath) && (new FileInfo(dataPath).Length > 0));
                }
                return loadData;
            }
        }
        public ICommand Save
        {
            get
            {
                if (saveData is null)
                {
                    saveData = new RelayCommand(execute =>
                    {
                        try
                        {
                            var jsonPlayers = JsonConvert.SerializeObject(Players);
                            File.WriteAllText(dataPath, jsonPlayers);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        onPropertyChanged(nameof(Save));
                    }, canExecute => true);
                }
                return saveData;
            }
        }


        #endregion

        public P_Management() { }

    }

}
