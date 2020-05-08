﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace players_mvvm.View
{
    public partial class TextBoxForLetters : UserControl
    {
        public TextBoxForLetters()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent TextChangedEvent =
        EventManager.RegisterRoutedEvent("TabItemSelected",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(TextBoxForLetters));

        public event RoutedEventHandler NumberChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        private void RaiseTextChanged()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(TextBoxForLetters.TextChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextBoxForLetters),
                new FrameworkPropertyMetadata(null)
            );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(char.IsLetter)) e.Handled = true;

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseTextChanged();
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
    }
}
