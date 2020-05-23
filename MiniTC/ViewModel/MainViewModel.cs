using MiniTC.Model;
using MiniTC.ViewModel.BaseClass;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MiniTC.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private PanelInterface focusedSide;

        private PanelInterface left;
        private string selectedFileLeft;

        private PanelInterface right;
        private string selectedFileRight;

        public string[] Drives { get => left.AvaibleDrives;  }

        #region Left
        
        public string SelectedDriveLeft
        {
            set
            {
                if (value != null)
                {
                    left.getItems(value);
                    onPropertyChanged(nameof(SelectedDriveLeft), nameof(WorkingDirectoryLeft), nameof(WorkingPathLeft));
                }
            }
        }
        public string SelectedFileLeft { set => selectedFileLeft = value; }
        public List<string> WorkingDirectoryLeft => left.WorkingDirectoryItems;
        public string WorkingPathLeft => left.WorkingPath;

        //Commands

        private ICommand focusLeft = null;
        public ICommand FocusLeft
        {
            get
            {
                if (focusedSide == null)
                {
                    focusLeft = new RelayCommand(x => focusedSide = left, x => true);
                }
                return focusLeft;
            }
        }

        private ICommand goToDirectoryLeft = null;
        public ICommand GoToDirectoryLeft
        {
            get
            {
                if (goToDirectoryLeft == null)
                {
                    goToDirectoryLeft = new RelayCommand(
                                                            x =>
                                                            {
                                                                if (WorkingDirectoryLeft == null)
                                                                {
                                                                    return;
                                                                }
                                                                if (!left.goDirectory(selectedFileLeft))
                                                                {
                                                                    MessageBox.Show(Resources.Res.CanNotOpen);
                                                                }
                                                                else
                                                                {
                                                                    onPropertyChanged(nameof(WorkingDirectoryLeft), nameof(WorkingPathLeft));
                                                                }
                                                            },
                                                            x => true
                                                        );
                }
                return goToDirectoryLeft;
            }
        }


        #endregion

        #region Right
        public string SelectedDriveRight
        {
            set
            {
                if (value != null)
                {
                    right.getItems(value);
                    onPropertyChanged(nameof(SelectedDriveRight), nameof(WorkingDirectoryRight), nameof(WorkingPathRight));
                }
            }
        }
        public string SelectedFileRight { set => selectedFileRight = value; }
        public List<string> WorkingDirectoryRight => right.WorkingDirectoryItems;
        public string WorkingPathRight => right.WorkingPath;

        //Commands

        private ICommand focusRight = null;
        public ICommand FocusRight
        {
            get
            {
                if (focusedSide == null)
                {
                    focusRight = new RelayCommand(x => focusedSide = right, x => true);
                }
                return focusRight;
            }
        }

        private ICommand goToDirectoryRight = null;
        public ICommand GoToDirectoryRight
        {
            get
            {
                if (goToDirectoryRight == null)
                {
                    goToDirectoryRight = new RelayCommand(
                                                            x =>
                                                            {
                                                                if (WorkingDirectoryRight == null)
                                                                {
                                                                    return;
                                                                }
                                                                if (!right.goDirectory(selectedFileRight))
                                                                {
                                                                    MessageBox.Show(Resources.Res.CanNotOpen);
                                                                }
                                                                else
                                                                {
                                                                    onPropertyChanged(nameof(WorkingDirectoryRight), nameof(WorkingPathRight));
                                                                }
                                                            },
                                                            x => true
                                                        );
                }
                return goToDirectoryRight;
            }
        }
        #endregion

        #region Copy
        private ICommand copy = null;
        public ICommand Copy
        {
            get
            {
                if (copy == null)
                    copy = new RelayCommand(x => CopySomething(), x => true);
                return copy;
            }
        }

        private void CopySomething()
        {
            if (focusedSide == left)
            {
                FileOperations.Copy(selectedFileLeft, left.WorkingPath, right.WorkingPath);
                right.getItems(right.WorkingPath);
                onPropertyChanged(nameof(WorkingDirectoryRight));
            }
            if (focusedSide == right)
            {
                FileOperations.Copy(selectedFileRight, right.WorkingPath, left.WorkingPath);
                left.getItems(left.WorkingPath);
                onPropertyChanged(nameof(WorkingDirectoryLeft));
            }
        }
        #endregion


        public MainViewModel()
        {
            focusedSide = null;
            left = new PanelModel();
            right = new PanelModel();
        }
    }
}
