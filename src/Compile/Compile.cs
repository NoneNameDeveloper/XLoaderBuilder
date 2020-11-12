using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace XLoaderBuilder
{
    public class Compiler
    {
        public Compiler(string sourceCode, string savePath, string icoPath)
        {
            // include your references here
            string[] referencedAssemblies = new string[] { "System.dll", "System.Windows.Forms.dll", "System.Management.dll" };

            // .net framework dependency version
            Dictionary<string, string> providerOptions = new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } };

            string compilerOptions;
            if (icoPath == "")
            {
                compilerOptions = string.Format("/target:winexe /platform:anycpu /optimize+");
            }
            else
            {
                compilerOptions = string.Format("/target:winexe /win32icon:\"{0}\" /platform:anycpu /optimize+", icoPath);
            }


            using (CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider(providerOptions))
            {
                CompilerParameters compilerParameters = new CompilerParameters(referencedAssemblies)
                {
                    GenerateExecutable = true,
                    GenerateInMemory = false,
                    OutputAssembly = savePath, // output path
                    CompilerOptions = compilerOptions,
                    TreatWarningsAsErrors = false,
                    IncludeDebugInformation = false,
                };

                CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, sourceCode); // source.cs
                if (compilerResults.Errors.Count > 0)
                {
                    foreach (CompilerError compilerError in compilerResults.Errors)
                    {
                        MessageBox.Show(string.Format("{0}\nLine: {1} - Column: {2}\nFile: {3}", compilerError.ErrorText,
                            compilerError.Line, compilerError.Column, compilerError.FileName));
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("File succesfully created!", "Done!");
                }
            }
        }
    }
}