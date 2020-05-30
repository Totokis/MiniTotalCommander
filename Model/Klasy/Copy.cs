using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model.Klasy
{
    public interface ICopy
    {
        bool copyFile(string source, string target);
    }
    class Copy : ICopy
    {

        public bool copyFile(string source, string target)
        {
            
            if(target == @"C:\")
            {
                //wyświetl okienko dialogowe "Nie kopiuj nic do głównego katalogu"
                return false;
            }
            else
            {
                string name = System.IO.Path.GetFileName(source);
                target = System.IO.Path.Combine(target, name);
                System.IO.File.Copy(source, target, true);
                return true;
            }

        }
    }
}
