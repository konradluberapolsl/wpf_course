using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MiniTC.View
{

    public partial class Panel : UserControl
    {
        public Panel()
        {
            InitializeComponent();
        }

        #region DependencyProperty

        public string[] Drives
        {
            get => (string[])GetValue(DrivesDP);
            set => SetValue(DrivesDP, value);
        }

        public static readonly DependencyProperty DrivesDP = DependencyProperty.Register(nameof(Drives), typeof(string[]), typeof(Panel), new FrameworkPropertyMetadata(null));

        public string SelectedDrive
        {
            get => (string)GetValue(SelectedDriveDP);
            set => SetValue(SelectedDriveDP, value);
        }
        public static readonly DependencyProperty SelectedDriveDP = DependencyProperty.Register(nameof(SelectedDrive), typeof(string), typeof(Panel), new FrameworkPropertyMetadata(null));

        public string WorkingPath
        {
            get => (string)GetValue(WorkingPathDP);
            set => SetValue(WorkingPathDP, value);
        }

        public static readonly DependencyProperty WorkingPathDP = DependencyProperty.Register(nameof(WorkingPath), typeof(string), typeof(Panel), new FrameworkPropertyMetadata(null));

        public string WorkingDirectory
        {
            get => (string)GetValue(WorkingDirectoryDP);
            set => SetValue(WorkingDirectoryDP, value);
        }

        public static readonly DependencyProperty WorkingDirectoryDP = DependencyProperty.Register(nameof(WorkingDirectory), typeof(List<string>), typeof(Panel), new FrameworkPropertyMetadata(null)); 

        public string SelectedFile
        {
            get => (string)GetValue(SelectedFileDP);
            set => SetValue(SelectedFileDP, value);
        }
        public static readonly DependencyProperty SelectedFileDP = DependencyProperty.Register(nameof(SelectedFile), typeof(string), typeof(Panel), new FrameworkPropertyMetadata(null));

        #endregion

        #region R Events
        public static readonly RoutedEvent ChangeDriveEvent = EventManager.RegisterRoutedEvent(nameof(ChangeDrive), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Panel));
        
        public event RoutedEventHandler ChangeDrive
        {
            add { AddHandler(ChangeDriveEvent, value); }
            remove { RemoveHandler(ChangeDriveEvent, value); }
        }

        public static readonly RoutedEvent ChangeSelectedFileEvent = EventManager.RegisterRoutedEvent(nameof(ChangeSelectedFile), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Panel));
       
        public event RoutedEventHandler ChangeSelectedFile
        {
            add { AddHandler(ChangeSelectedFileEvent, value); }
            remove { RemoveHandler(ChangeSelectedFileEvent, value); }
        }

        public static readonly RoutedEvent MouseClickEvent = EventManager.RegisterRoutedEvent(nameof(MouseClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Panel));
        public event RoutedEventHandler MouseClick
        {
            add { AddHandler(MouseClickEvent, value); }
            remove { RemoveHandler(MouseClickEvent, value); }
        }

        public static readonly RoutedEvent FocusedPanelEvent = EventManager.RegisterRoutedEvent(nameof(FocusedPanel), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Panel));
        public event RoutedEventHandler FocusedPanel
        {
            add { AddHandler(FocusedPanelEvent, value); }
            remove { RemoveHandler(FocusedPanelEvent, value); }
        }
        #endregion

        #region Events
        private void DriveSelectionChanged(object sender, SelectionChangedEventArgs e) => RaiseEvent(new RoutedEventArgs(ChangeDriveEvent));
        private void ListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) => RaiseEvent(new RoutedEventArgs(ChangeSelectedFileEvent));
        private void LBMouseClick(object sender, MouseButtonEventArgs e) => RaiseEvent(new RoutedEventArgs(MouseClickEvent));
        private void LBGotFocus(object sender, RoutedEventArgs e) => RaiseEvent(new RoutedEventArgs(FocusedPanelEvent));
        #endregion

    }
}
