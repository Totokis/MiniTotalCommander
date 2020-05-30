using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model.Klasy
{
    public interface IListOfPaths
    {
        void SetListOfPaths(List<IPath> list_of_paths);
        List<IPath> ReturnListOfPaths(IPath actual_path, IListOfDrives list_of_drives);
    }

    class ListOfPaths : IListOfPaths
    {
        public List<IPath> ReturnListOfPaths(IPath actual_path, IListOfDrives list_of_drives)
        {
            string actualPath = actual_path.ReturnPath();
            string[] ListOfDrives = list_of_drives.ReturnListOfDrives();
            List<IPath> listOfPaths = new List<IPath>();
            Path previousPath = new Path("default");
            bool rootDirecotry = false;


            foreach (var item in ListOfDrives)
            {
                if (actualPath == item)
                {
                    previousPath = new Path(actualPath);
                    rootDirecotry = true;
                }

            }
            if (!rootDirecotry)
                previousPath = new Path(Directory.GetParent(actualPath).ToString(), false, "..");

            listOfPaths.Add(previousPath);
            //TODO zrobić obsługę wyjątków
            foreach (var item in Directory.GetDirectories(actualPath))
            {
                Path newPath = new Path(item, true, "<D>");
                listOfPaths.Add(newPath);

            }

            foreach (var item in Directory.GetFiles(actualPath))
            {
                Path newPath = new Path(item);
                listOfPaths.Add(newPath);
            }

            return listOfPaths;
        }

        public void SetListOfPaths(List<IPath> list_of_paths)
        {
            throw new NotImplementedException();
        }
    }

}
