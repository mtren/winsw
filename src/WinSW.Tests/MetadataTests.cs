#if NETFRAMEWORK
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using WinSW.Tests.Util;
using Xunit;

namespace WinSW.Tests
{
    public sealed class MetadataTests
    {
        [Fact]
        public void Extern()
        {
            var version = new Version(4, 0, 0, 0);

            using var peReader = new PEReader(File.OpenRead(Layout.NET461Exe));
            var metadataReader = peReader.GetMetadataReader();
            foreach (var handle in metadataReader.AssemblyReferences)
            {
                var assembly = metadataReader.GetAssemblyReference(handle);
                string assemblyName = metadataReader.GetString(assembly.Name);
                if (assemblyName != "System.IO.Compression" && assemblyName != "System.Runtime.InteropServices.RuntimeInformation")
                {
                    Assert.Equal(version, assembly.Version);
                }
            }
        }
    }
}
#endif
