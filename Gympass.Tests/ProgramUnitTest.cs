using NUnit.Framework;
using System.IO;

namespace Gympass.Tests
{
    [TestFixture]
    class ProgramUnitTest
    {

        [SetUp]
        public void SetUp()
        {
            File.Delete("result.txt");
        }
        [Test]
        public void ShouldCreateOutputFileWhenInputExists()
        {
            Program.Main(new string[1] { "resources/input.txt" });

            Assert.AreEqual(true, File.Exists("result.txt"));
        }

        [Test]
        public void ShouldNotCreateOutputFileWhenInputDoesNotExist()
        {
            Program.Main(new string[1] { "resources/inut.txt" });

            Assert.AreEqual(false, File.Exists("result.txt"));
        }

        [Test]
        public void ShouldNotCreateOutputFileWhenNoFileInformed()
        {
            Program.Main(new string[0]);

            Assert.AreEqual(false, File.Exists("result.txt"));
        }
    }
}
