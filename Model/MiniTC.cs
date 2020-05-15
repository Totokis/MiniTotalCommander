using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;


namespace MiniTC.Model
{
    public interface IPath
    {
        string ReturnPath();

        void SetPath(string path);

        string ReturnRepresentation();

        string ReturnFileName();
    }
    public interface ICopy
    {
        void copyFile(string source, string target);
    }
    public interface IPanel
    {
        void SetCurrentPath(string path);
        string ReturnCurrentPath();
        string[] ReturnListOfDrives();
        void SetListOfDrives(string[] list_of_drives);
        List<IPath> ReturnListOfPaths();
    }
    public interface IListOfDrives
    {
        string[] ReturnListOfDrives();
        void SetListOfDrives(string[] list_of_drives);
    }

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

    class Path : IPath
    {
        private string _fileName;
        private string _path;
        private string _representation;
        private bool _showPath;
        public string ReturnPath()
        {
            return _path;
        }

        public void SetPath(string path)
        {
            _path = path;
        }

        public override string ToString()
        {
            if (_showPath)
                return $"{_representation}: {_path}";
            else
                return $"{_representation}";
        }

        public string ReturnRepresentation()
        {
            return _representation;
        }

        public string ReturnFileName()
        {
            return _fileName;
        }

        public Path(string path, bool showPath = true, string representation = null)
        {
            //_previousPath = 
            _path = path;
            _representation = representation;
            _showPath = showPath;
        }
        public Path(string path, string fileName,  bool showPath = true, string representation = null)
        {
            //_previousPath = 
            _path = path;
            _representation = representation;
            _showPath = showPath;
            _fileName = fileName;
        }
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

        public string ReturnCurrentPath() { return ActualPath.ReturnPath(); }
        public void SetCurrentPath(string path) { ActualPath.SetPath(path); }

        public string[] ReturnListOfDrives() { return ListOfDrives.ReturnListOfDrives(); }
        public void SetListOfDrives(string[] list_of_drives) { ListOfDrives.SetListOfDrives(list_of_drives); }

        public List<IPath> ReturnListOfPaths() { return ListOfPaths.ReturnListOfPaths(ActualPath, ListOfDrives); }

    }

    class Copy : ICopy
    {
        public void copyFile(string source, string target)
        {
            string name = System.IO.Path.GetFileName(source);
            target = System.IO.Path.Combine(target, name);
            System.IO.File.Copy(source, target, true);
        }

        public Copy()
        {

        }

    }


    public class MiniTC
    {

        public IPanel RightPanel;
        public IPanel LeftPanel;
        public ICopy CopyButton;

        public MiniTC()
        {

            LeftPanel = new Panel(Directory.GetCurrentDirectory());
            RightPanel = new Panel(Directory.GetCurrentDirectory());
            CopyButton = new Copy();
        }

        public MiniTC(string path)
        {
            LeftPanel = new Panel(path);
            RightPanel = new Panel(path);
            CopyButton = new Copy();
        }

    }
}
