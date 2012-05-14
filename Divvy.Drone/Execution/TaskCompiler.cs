using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Divvy.Platform;
using Microsoft.CSharp;

namespace Divvy.Drone.Execution
{
    static class TaskCompiler
    {
        public static BaseTask Compile(string source)
        {
            Dictionary<string, string> providerOptions = new Dictionary<string, string> { { "CompilerVersion", "v4.0" } };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);
            CompilerParameters compilerParameters = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false,
                IncludeDebugInformation = false,
                TreatWarningsAsErrors = false
            };
            compilerParameters.ReferencedAssemblies.Add("Divvy.Platform.dll");
            CompilerResults compilerResults1 = provider.CompileAssemblyFromSource(compilerParameters, source);

            if (compilerResults1.Errors.Count != 0)
            {
                throw new Exception(compilerResults1.Errors[0].ErrorText);
            }

            BaseTask task1 = compilerResults1.CompiledAssembly.CreateInstance("Divvy.Platform.Task") as BaseTask;

            return task1;
        }

    }
}
