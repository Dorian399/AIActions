using AIActions.Python;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AIActions.AI
{
    internal class PromptContextFolder
    {
        public string PythonVersion { get; private set; }
        public string OS { get; private set; }
        public string FilesPath { get; private set; }
        public string FilesAmount { get; private set; }
        public string FilesExtensions {  get; private set; }
        public string FilesList { get; private set; }
        public string Prompt {  get; private set; }

        public PromptContextFolder(string filesPath, string prompt, string pythonVersion)
        {
            Prompt = prompt ?? " ";
            FilesPath = filesPath ?? "Path missing.";
            OS = RuntimeInformation.OSDescription ?? "Unknown OS";

            PythonVersion = pythonVersion;

            string[] files = Directory.GetFiles(FilesPath);
            string[] directories = Directory.GetDirectories(FilesPath);

            int filesAmount = files.Count();
            int directoriesAmount = directories.Count();
            string filesAndDirectoriesAmount = filesAmount.ToString() + " files, " + directoriesAmount.ToString() + " directories";

            FilesAmount = filesAndDirectoriesAmount ?? "0 files, 0 directories";

            List<string> filesList = new List<string>();
            HashSet<string> extensions = new HashSet<string>();

            int maxFiles = 20;
            foreach(string file in files)
            {
                string fileName = Path.GetFileName(file);
                if(maxFiles > 0)
                {
                    filesList.Add(fileName);
                    maxFiles--;
                }

                string ext = Path.GetExtension(file);
                if (!extensions.Contains(ext))
                {
                    extensions.Add(ext);
                }

            }

            FilesList = String.Join("; ", filesList);
            FilesExtensions = String.Join("; ", extensions);

        }

    }
}
