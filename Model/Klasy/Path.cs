using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model.Klasy
{
    public interface IPath
    {
        string ReturnPath();

        void SetPath(string path);

        string ReturnRepresentation();

        string ReturnFileName();
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
        public Path(string path, string fileName, bool showPath = true, string representation = null)
        {
            //_previousPath = 
            _path = path;
            _representation = representation;
            _showPath = showPath;
            _fileName = fileName;
        }
    }

}
