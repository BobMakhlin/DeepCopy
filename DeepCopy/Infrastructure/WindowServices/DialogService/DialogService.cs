using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace _01_DeepCopy.Infrastructure.WindowServices.DialogService
{
    class DialogService : IDialogService
    {
        public string Path { get; set; }

        public bool OpenDirectoryDialog()
        {
            var dialog = new WinForms.FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == WinForms.DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                Path = dialog.SelectedPath;
                return true;
            }
        }

        public DialogResult MessageBoxOk(string content, string caption = "")
        {
            var result = MessageBox.Show(content, caption, MessageBoxButton.OK);

            if(result == MessageBoxResult.OK)
            {
                return DialogResult.OK;
            }
            return DialogResult.Cancel;
        }
    }
}
