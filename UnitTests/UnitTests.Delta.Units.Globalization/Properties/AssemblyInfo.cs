using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;

[assembly: AssemblyTitle("UnitTests.Delta.Units.Globalization")]
[assembly: AssemblyDescription("Unit tests for Delta.Units (Globalization)")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Delta Software")]
[assembly: AssemblyProduct("Delta.Units")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: CollectionBehavior(DisableTestParallelization = true)]

// Remark: this test assembly cannot be run by xunit.runner.console.x86.exe: it raises a StackOverflowException
// However, test are successfully executed if using the x64 version of the runner (xunit.runner.console.exe)