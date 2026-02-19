using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace WinSW.Tests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ElevatedFactAttribute : FactAttribute
    {
        public ElevatedFactAttribute([CallerFilePath] string sourceFilePath = null, [CallerLineNumber] int sourceLineNumber = -1)
#if NET
            :base(sourceFilePath, sourceLineNumber)
#endif
        {
            if (!Program.IsProcessElevated())
            {
                this.Skip = "Access is denied";
            }
        }
    }
}
