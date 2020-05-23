using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace MiniTC.Model
{
    class PanelModel : PanelInterface
    {
        public string WorkingPath { get; set; }
        public string[] AvaibleDrives => Directory.GetLogicalDrives();
        public List<string> WorkingDirectoryItems { get; set; }

        public bool getItems(string path)
        {
            WorkingDirectoryItems = new List<string>();
            try
            {
                List<string> folders = Directory.GetDirectories(path).ToList();
                List<string> files = Directory.GetFiles(path).ToList();

                if (!AvaibleDrives.Contains(Path.GetFullPath(path)))
                {
                    WorkingDirectoryItems.Add("\uD83E\uDC45");
                }

                WorkingDirectoryItems.AddRange(folders.Select(x => "\uD83D\uDDBF" + Path.GetFileName(x)));
                WorkingDirectoryItems.AddRange(files.Select(x => Path.GetFileName(x)));
                WorkingPath = Path.GetFullPath(path);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool goDirectory(string directory)
        {
            if (directory == null)
            {
                return true;
            }
            if (directory.Contains("\uD83D\uDDBF"))
            {
                string path = WorkingPath + @"\" + directory.Replace("\uD83D\uDDBF","");
                return getItems(path);
            }
            if (directory.Contains("\uD83E\uDC45"))
            {
                string path = WorkingPath + @"\" + directory.Replace("\uD83E\uDC45", "" + "..");
                return getItems(path);
            }
            return true;
        }
    }
}
