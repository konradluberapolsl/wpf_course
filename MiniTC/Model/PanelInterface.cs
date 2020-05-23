using System.Collections.Generic;

namespace MiniTC.Model
{
    interface PanelInterface
    {
        string WorkingPath { get; set; }
        string[] AvaibleDrives { get; }

        List<string> WorkingDirectoryItems { get; set; }

        bool getItems(string path);
        bool goDirectory(string directory);
    }
}
