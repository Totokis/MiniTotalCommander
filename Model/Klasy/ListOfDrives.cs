using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model.Klasy
{

    public interface IListOfDrives
    {
        string[] ReturnListOfDrives();
        void SetListOfDrives(string[] list_of_drives);
    }

    class ListOfDrives : IListOfDrives
    {
        
        private string[] _listOfDrives
        {
            get
            {
                return Directory.GetLogicalDrives();
            }

            set
            {
                _listOfDrives = value;
            }
        }

        public string[] ReturnListOfDrives() { return _listOfDrives; }


        public void SetListOfDrives(string[] list_of_drives) { _listOfDrives = list_of_drives; }

    }
}
