using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.ViewModel
{
    using Model;
    using BaseClass;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

   
    class VMMiniTC: ViewModelBase
    {
        private MiniTC PanelITC = new Model.MiniTC("C:\\Users\\pawel\\Desktop\\Abba");
        public VMMiniTC() { Window_Loaded(); }
        ~VMMiniTC() { }

        private string _currentPathLeft;
        public string CurrentPathLeft
        {
            get { return _currentPathLeft;  }
            set { _currentPathLeft = value; onPropertyChanged(nameof(CurrentPathLeft)); }
        }

        private string _currentPathRight;
        public string CurrentPathRight
        {
            get { return _currentPathRight; }
            set { _currentPathRight = value; onPropertyChanged(nameof(CurrentPathRight)); }
        }

        private string _leftDiscSelection;
        public string LeftDiscSelection
        {
            get { return _leftDiscSelection; }
            set { _leftDiscSelection = value; }
        }
        private string _rightDiscSelection;
        public string RightDiscSelection
        {
            get {return _rightDiscSelection; }
            set { _rightDiscSelection = value; }
        }

        private Path _selectedPathLeft;

        public Path SelectedPathLeft
        {
            get { return _selectedPathLeft; }
            set
            {
                _selectedPathLeft = value; onPropertyChanged(nameof(SelectedPathLeft));
            }

        }

        private Path _selectedPath;

        public Path SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                _selectedPath = value; onPropertyChanged(nameof(SelectedPath));
            }

        }

        private Path _selectedPathRight;

        public Path SelectedPathRight
        {
            get { return _selectedPathRight; }
            set { _selectedPathRight = value; onPropertyChanged(nameof(SelectedPathRight)); }

        }

        private ObservableCollection<IPath> _leftListOfPaths = new ObservableCollection<IPath>();
        private ObservableCollection<IPath> _rightListOfPaths = new ObservableCollection<IPath>();

        public ObservableCollection<IPath> LeftListOfPaths
        {
            get { return _leftListOfPaths; }
            set { _leftListOfPaths = value; }
        }

        public ObservableCollection<IPath> RightListOfPaths
        {
            get { return _rightListOfPaths; }
            set { _rightListOfPaths = value; }
        }

        private string[] _leftListOfDrives;
        private string[] _rightListOfDrives;

        public string [] LeftListOfDrives
        {
            get {_leftListOfDrives = PanelITC.LeftPanel.ReturnListOfDrives(); return _leftListOfDrives; }
            set { _leftListOfDrives = value; onPropertyChanged(nameof(LeftListOfDrives)); }
        }
        public string[] RightListOfDrives
        {
            get { return _rightListOfDrives; }
            set { _rightListOfDrives = value; onPropertyChanged(nameof(RightListOfDrives)); }
        }

        private ObservableCollection<IPath> CastToObservable(List<IPath> lista, ObservableCollection<IPath> observable)
        {
           
            observable.Clear();
            foreach (var item in lista)
            {
                observable.Add(item);
            }
            return observable;
        }
        private void UpdateLeft(object sender)
        {

            if (SelectedPathLeft != null)
            {
                if(SelectedPathLeft.ReturnRepresentation()!=null)
                {
                    PanelITC.LeftPanel.SetCurrentPath(SelectedPathLeft.ReturnPath());
                    LeftListOfPaths = CastToObservable(PanelITC.LeftPanel.ReturnListOfPaths(),LeftListOfPaths);
                    CurrentPathLeft = PanelITC.LeftPanel.ReturnCurrentPath();

                }
                else
                {
                    PanelITC.LeftPanel.SetCurrentPath(SelectedPathLeft.ReturnPath());
                    CurrentPathLeft = PanelITC.LeftPanel.ReturnCurrentPath();
                    SelectedPathRight = null;
                }

            }
            //Console.WriteLine(LeftListOfPaths[1].ReturnPath());
        }

        private void UpdateRight(object sender)
        {

            if (SelectedPathRight != null)
            {
                if (SelectedPathRight.ReturnRepresentation() != null)
                {

                    PanelITC.RightPanel.SetCurrentPath(SelectedPathRight.ReturnPath());
                    RightListOfPaths = CastToObservable(PanelITC.RightPanel.ReturnListOfPaths(), RightListOfPaths);
                    CurrentPathRight = PanelITC.RightPanel.ReturnCurrentPath();

                }
                else
                {
                    PanelITC.RightPanel.SetCurrentPath(SelectedPathRight.ReturnPath());
                    CurrentPathRight = PanelITC.RightPanel.ReturnCurrentPath();
                    SelectedPathLeft = null;
                }

            }
            //Console.WriteLine(LeftListOfPaths[1].ReturnPath());
        }

        private void copyFiles(object sender)
        {
            if (SelectedPathLeft == null && SelectedPathRight != null)
            {
                string source = SelectedPathRight.ReturnPath();
                string target = PanelITC.LeftPanel.ReturnCurrentPath();
                PanelITC.CopyButton.copyFile(source,target);
                LeftListOfPaths = CastToObservable(PanelITC.LeftPanel.ReturnListOfPaths(), LeftListOfPaths);

            }
            else if (SelectedPathLeft != null && SelectedPathRight == null)
            {
                string source = SelectedPathLeft.ReturnPath();
                string target = PanelITC.LeftPanel.ReturnCurrentPath();
                PanelITC.CopyButton.copyFile(source, target);
                RightListOfPaths = CastToObservable(PanelITC.RightPanel.ReturnListOfPaths(), RightListOfPaths);
            }
            else if(SelectedPathLeft == null && SelectedPathRight == null)
            {
                //TODO wyświetl se okno dialogowe co powie NIE MASZ NIC ZAZNACZONEGO
            }
        }

        private void leftDiscLoad(object obj)
        {
            if (LeftDiscSelection != null)
            {
                PanelITC.LeftPanel.SetCurrentPath(LeftDiscSelection) ;
                LeftListOfPaths = CastToObservable(PanelITC.LeftPanel.ReturnListOfPaths(), LeftListOfPaths);
                CurrentPathLeft = PanelITC.LeftPanel.ReturnCurrentPath();

            }
        }
        private void rightDiscLoad(object obj)
        {
            if (RightDiscSelection != null)
            {
                PanelITC.RightPanel.SetCurrentPath(RightDiscSelection);
                RightListOfPaths = CastToObservable(PanelITC.RightPanel.ReturnListOfPaths(), RightListOfPaths);
                CurrentPathRight = PanelITC.RightPanel.ReturnCurrentPath();
            }
        }

        private void refreshLeft(object obj)
        {
            LeftListOfDrives= PanelITC.LeftPanel.ReturnListOfDrives();
        }

        private void refreshRight(object obj)
        {
            RightListOfDrives = PanelITC.RightPanel.ReturnListOfDrives();
        }



        private ICommand _leftLoad = null;  
        public ICommand LeftLoad
        {
            get
            {
                if (_leftLoad == null)
                {
                    _leftLoad = new RelayCommand(
                       UpdateLeft,
                        arg => true
                    );
                }
                return _leftLoad;
            }
        }

        private ICommand _rightLoad = null;
        public ICommand RightLoad
        {
            get
            {
                if (_rightLoad == null)
                {
                    _rightLoad = new RelayCommand(
                       UpdateRight,
                        arg => true
                    );
                }
                return _rightLoad;
            }
        }

        private ICommand _copyFiles = null;

        public ICommand CopyFiles
        {
            get
            {
                if (_copyFiles == null)
                {
                    _copyFiles = new RelayCommand(
                       copyFiles,
                        arg => true
                    );
                }
                return _copyFiles;
            }
        }

        private ICommand _leftDiscLoad = null;

        public ICommand LeftDiscLoad
        {
            get
            {
                if (_leftDiscLoad == null)
                {
                    _leftDiscLoad = new RelayCommand(
                       leftDiscLoad,
                        arg => true
                    );
                }
                return _leftDiscLoad;
            }
        }

        private ICommand _rightDiscLoad = null;

        public ICommand RightDiscLoad
        {
            get
            {
                if (_rightDiscLoad == null)
                {
                    _rightDiscLoad = new RelayCommand(
                       rightDiscLoad,
                        arg => true
                    );
                }
                return _rightDiscLoad;
            }
        }

        private ICommand _refreshLeftWhenDroppedDown;

        public ICommand RefreshLeftWhenDroppedDown
        {
            get
            {
                if (_refreshLeftWhenDroppedDown == null)
                {
                    _refreshLeftWhenDroppedDown = new RelayCommand(
                       refreshLeft,
                        arg => true
                    );
                }
                return _refreshLeftWhenDroppedDown;
            }
        }

        private ICommand _refreshRightWhenDroppedDown;

        public ICommand RefreshRightWhenDroppedDown
        {
            get
            {
                if (_refreshRightWhenDroppedDown == null)
                {
                    _refreshRightWhenDroppedDown = new RelayCommand(
                       refreshRight,
                        arg => true
                    );
                }
                return _refreshRightWhenDroppedDown;
            }
        }


        private void Window_Loaded()
        {
            LeftListOfPaths =CastToObservable(PanelITC.LeftPanel.ReturnListOfPaths(),LeftListOfPaths);
            RightListOfPaths = CastToObservable(PanelITC.RightPanel.ReturnListOfPaths(),RightListOfPaths);
            LeftListOfDrives = PanelITC.LeftPanel.ReturnListOfDrives();
            RightListOfDrives = PanelITC.RightPanel.ReturnListOfDrives();
            CurrentPathLeft = PanelITC.LeftPanel.ReturnCurrentPath();
            CurrentPathRight = PanelITC.RightPanel.ReturnCurrentPath();


        }
    }
}
