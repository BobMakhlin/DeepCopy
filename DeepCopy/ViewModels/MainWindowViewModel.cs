using _01_DeepCopy.Helpers;
using _01_DeepCopy.Infrastructure.WindowServices.DialogService;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _01_DeepCopy.ViewModels
{
    class MainWindowViewModel : NotifyPropertyChanged
    {
        #region Private Definitions
        MyDirectory directory = new MyDirectory();
        string sourceFolderPath;
        string destFolderPath;
        IDialogService dialog;
        private bool progressBarAnimationEnabled;
        private bool uiEnabled;
        #endregion

        public MainWindowViewModel(IDialogService dialog)
        {
            InitCommands();
            UiEnabled = true;
            Monitor = new ThreadPoolMonitor();
            Monitor.StartMonitor();

            this.dialog = dialog;
            directory.CopyCreated += (s, e) =>
            {
                dialog.MessageBoxOk("The copy was created successfully");
            };
        }

        // Commands.
        public ICommand SelectSourceFolderCommand { get; set; }
        public ICommand SelectDestFolderCommand { get; set; }
        public ICommand CopyFolderCommand { get; set; }

        public string SourceFolderPath
        {
            get => sourceFolderPath;
            set
            {
                sourceFolderPath = value;
                OnPropertyChanged();
            }
        }
        public string DestFolderPath
        {
            get => destFolderPath;
            set
            {
                destFolderPath = value;
                OnPropertyChanged();
            }
        }
        public bool UiEnabled
        {
            get => uiEnabled;
            set
            {
                uiEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool ProgressBarAnimationEnabled
        {
            get => progressBarAnimationEnabled;
            set
            {
                progressBarAnimationEnabled = value;
                OnPropertyChanged();
            }
        }
        public ThreadPoolMonitor Monitor { get; set; }

        void InitCommands()
        {
            SelectSourceFolderCommand = new RelayCommand(SelectSourceFolder);
            SelectDestFolderCommand = new RelayCommand(SelectDestFolder);
            CopyFolderCommand = new RelayCommand(CopyFolderAsync);
        }

        void SelectSourceFolder()
        {
            if (dialog.OpenDirectoryDialog())
            {
                SourceFolderPath = dialog.Path;
            }
        }
        void SelectDestFolder()
        {
            if (dialog.OpenDirectoryDialog())
            {
                DestFolderPath = dialog.Path;
            }
        }
        async void CopyFolderAsync()
        {
            UiEnabled = false;
            ProgressBarAnimationEnabled = true;

            await directory.DeepCopyAsync(SourceFolderPath, DestFolderPath);

            UiEnabled = true;
            ProgressBarAnimationEnabled = false;
        }
    }
}
