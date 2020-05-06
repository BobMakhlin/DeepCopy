using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DeepCopy.Helpers
{
    class MyDirectory
    {
        public event EventHandler CopyCreated;
        
        public void DeepCopy(string source, string dest)
        {
            Directory.CreateDirectory(dest);

            var dir = new DirectoryInfo(source);

            Parallel.ForEach(dir.GetFiles("*.*", SearchOption.TopDirectoryOnly), file =>
            {
                try
                {
                    file.CopyTo($"{dest}\\{file.Name}");
                }
                catch (Exception) { }
            });

            Parallel.ForEach(dir.GetDirectories("*.*", SearchOption.TopDirectoryOnly), 
                directory => DeepCopy(directory.FullName, $"{dest}\\{directory.Name}"));
        }
        public async Task DeepCopyAsync(string source, string dest)
        {
            await Task.Run(() => DeepCopy(source, dest));

            CopyCreated?.Invoke(this, EventArgs.Empty);
        }
    }
}
