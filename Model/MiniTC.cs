using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using MiniTC.Model.Klasy;


namespace MiniTC.Model
{
    using Klasy;

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
