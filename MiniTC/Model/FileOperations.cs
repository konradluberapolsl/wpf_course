using System.IO;
using System.Windows;

namespace MiniTC.Model
{
    class FileOperations
    {
        public static void Copy(string fileName, string sourcePath, string destinationPath)
        {
            if (fileName == null)
                return;
            if (fileName.Contains("\uD83D\uDDBF"))
            {
                var directoryName = fileName.Replace("\uD83D\uDDBF", "");
                var source = new DirectoryInfo(sourcePath + directoryName);
                var destination = new DirectoryInfo(destinationPath + @"\" + directoryName);
                if (destinationPath.Contains(source.ToString()))
                {
                    MessageBox.Show(Resources.Res.CopyToSub);
                    return;
                }

                try
                {
                    CopyDirectory(source, destination);
                }
                catch
                {
                    MessageBox.Show(Resources.Res.ErrorCopyDir);
                }
            }
            else
            {
                var source = Path.Combine(sourcePath, fileName);
                try
                {

                    var destination = Path.Combine(destinationPath, fileName);
                    File.Copy(source, destination, true);
                }
                catch
                {
                    MessageBox.Show(Resources.Res.ErrorCopyFile);
                }
            }
        }
        private static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            Directory.CreateDirectory(destination.FullName);

            foreach (FileInfo f in source.GetFiles())
                f.CopyTo(Path.Combine(destination.FullName, f.Name), true);

            foreach (DirectoryInfo directorySourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = destination.CreateSubdirectory(directorySourceSubDir.Name);
                CopyDirectory(directorySourceSubDir, nextTargetSubDir);
            }
        }
    }
}
