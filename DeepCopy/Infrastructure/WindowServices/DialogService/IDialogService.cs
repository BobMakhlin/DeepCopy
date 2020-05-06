using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DeepCopy.Infrastructure.WindowServices.DialogService
{
    interface IDialogService
    {
        string Path { get; set; }
        bool OpenDirectoryDialog();

        DialogResult MessageBoxOk(string content, string caption = "");
    }
}
