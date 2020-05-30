using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model.Klasy
{
    public interface IPanel
    {
        void SetCurrentPath(string path);
        string ReturnCurrentPath();
        string[] ReturnListOfDrives();
        void SetListOfDrives(string[] list_of_drives);
        List<IPath> ReturnListOfPaths();
    }

    class Panel : IPanel
    {
        private IPath ActualPath;

        private IListOfPaths ListOfPaths;

        private IListOfDrives ListOfDrives;


        public Panel(string initial_path)
        {
            ActualPath = new Path(initial_path);
            ListOfPaths = new ListOfPaths();
            ListOfDrives = new ListOfDrives();
        }

        public string ReturnCurrentPath()
        {
            string path =  ActualPath.ReturnPath();
            string str;
            Console.WriteLine(path.Length);
          
            if (path.Length > 30)
            {

                string name = System.IO.Path.GetFileName(path);//nazwa aktualnego pliku
                string prename = Directory.GetParent(path).ToString();
                //string root = Directory.GetDirectoryRoot(path).ToString();
                prename = System.IO.Path.GetFileName(prename);
                Console.WriteLine("sciezka" + prename + name);
                //path = "";
                
                str = path.Substring(0, 2) + "\\" + prename + "\\" + name; // Dla systemów unix trzeba by tu  zrobić / zamiast \
                if(str.Length > 30)
                {
                    str = path.Substring(0, 2) + "\\" + prename + "\\" + name; // Dla systemów unix trzeba by tu  zrobić / zamiast \
                    str.Remove(10, 10);
                    str.Insert(10, "..\\...\\..");
                }
                
            }
            else
            {
                str = path;
            }
            return str;
        }
        public void SetCurrentPath(string path) { ActualPath.SetPath(path); }

        public string[] ReturnListOfDrives() { return ListOfDrives.ReturnListOfDrives(); }
        public void SetListOfDrives(string[] list_of_drives) { ListOfDrives.SetListOfDrives(list_of_drives); }

        public List<IPath> ReturnListOfPaths() { return ListOfPaths.ReturnListOfPaths(ActualPath, ListOfDrives); }

    }
}
