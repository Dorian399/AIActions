using AIActions.Python;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AIActions.AI
{
    internal class PromptContextFile
    {
        public string PythonVersion { get; private set; } = "N/A";
        public string OS { get; private set; } = "N/A";
        public string FileName { get; private set; } = "N/A";
        public string FilePath { get; private set; } = "N/A";
        public string AbsolutePath { get; private set; } = "N/A";
        public string FileSize { get; private set; } = "N/A";
        public string FileExtension { get; private set; } = "N/A";
        public string FileFirst40Bytes { get; private set; } = "N/A";
        public string Prompt { get; private set; } = "N/A";

        public PromptContextFile(string filePath, string prompt, string pythonVersion)
        {
            if (filePath == null || !File.Exists(filePath))
                return;

            Prompt = prompt ?? " ";
            AbsolutePath = filePath ?? "Path missing";
            OS = RuntimeInformation.OSDescription ?? "Unknown OS";

            PythonVersion = pythonVersion;

            FileName = Path.GetFileName(filePath) ?? "Filename missing";

            FileInfo file = new FileInfo(filePath!);

            FileSize = file.Length.ToString() ?? "Filesize missing";

            FileExtension = file.Extension;

            using (StreamReader reader = new StreamReader(filePath!))
            {
                char[] buffer = new char[40];
                int readCount = reader.Read(buffer, 0, buffer.Length);
                FileFirst40Bytes = new string(buffer, 0, readCount) ?? "";
            }

        }

    }
}
