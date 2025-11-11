using AIActions.ExternalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIActions.Python
{
    internal class DependencyInstall
    {
        public Action<string>? OnOutput { get; set; }

        public async Task InstallList(string[] packages,CancellationToken token=default)
        {
            if(packages.Length == 0) {
                return;
            }
            OnOutput?.Invoke("Installing dependencies...\n");
            ProcessExec executor = new ProcessExec();
            foreach (string package in packages) {
                token.ThrowIfCancellationRequested();
                await InstallPackage(package,executor, token);
            }
            OnOutput?.Invoke("Finished installing dependencies.\n");
        }

        public async Task<bool> InstallPackage(string package,ProcessExec executor,CancellationToken token=default)
        {
            try
            {
                OnOutput?.Invoke("Installing " + package+"\n");
                int code = await executor.StartAsync(Paths.PythonExecutable,"-m pip install --target \""+Paths.PipPackagesFolder+"\" " + package,token);
                if (code != 0) {
                    // If install failed try to install package without the specific version
                    int index = package.IndexOf("==");
                    if (index != -1)
                    {
                        string package_noversion = package.Substring(0, index);
                        return await InstallPackage(package_noversion, executor, token);
                    }
                    OnOutput?.Invoke("Package: "+package+" failed to install.\n");
                    return false;
                }
                OnOutput?.Invoke("Installed: "+package+"\n");
                return true;
            }
            catch(OperationCanceledException e)
            {
                OnOutput?.Invoke("Operation canceled.\n");
                throw;
            }
            catch (Exception e) {
                OnOutput?.Invoke(e.ToString());
                return false;
            }
        }
    }
}
