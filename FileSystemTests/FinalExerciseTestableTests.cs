using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.IO.Abstractions;
using System.Text;

namespace FileSystem.Tests
{
    [TestClass()]
    public class FinalExerciseTestableTests
    {
        Mock<IFileSystem> fileSystemMock = new Mock<IFileSystem>();

        public FinalExerciseTestable GetTestableObject()
        {
            FinalExerciseTestable finalExerciseTestable = new FinalExerciseTestable(fileSystemMock.Object);
            return finalExerciseTestable;
        }

        [TestMethod()]
        public void GetMatches_DirectoyEmpty_EmptyResponse()
        {
            // Arrange 
            FinalExerciseTestable finalExerciseTestable = GetTestableObject();
            string directoryName = "MyDirectory";
            fileSystemMock.Setup(fs => fs.Directory.GetFiles(directoryName))
                .Returns(new string[] { });

            fileSystemMock.Setup(fs => fs.Directory.GetDirectories(directoryName))
                .Returns(new string[] { });

            fileSystemMock.Setup(fs => fs.Directory.Exists(directoryName))
                .Returns(true);

            // Act
            var matches = finalExerciseTestable.GetMatches(directoryName, "[a-z]+");

            Assert.IsNotNull(matches);
            Assert.IsTrue(!matches.Any());
        }

        [TestMethod()]
        public void GetMatches_OneFileThatMatches_ResponseContainsIt()
        {
            // Arrange 
            FinalExerciseTestable finalExerciseTestable = GetTestableObject();
            string directoryName = "MyDirectory";
            string fileName = "MyFile";
            fileSystemMock.Setup(fs => fs.Directory.GetFiles(directoryName))
                .Returns(new string[] { fileName });

            fileSystemMock.Setup(fs => fs.Directory.GetDirectories(directoryName))
                .Returns(new string[] { });

            fileSystemMock.Setup(fs => fs.Directory.Exists(directoryName))
                .Returns(true);

            fileSystemMock.Setup(fs => fs.File.Exists(fileName))
                .Returns(true);

            fileSystemMock.Setup(fs => fs.File.ReadLines(fileName))
                .Returns(new[] { "hello" });

            // Act
            var matches = finalExerciseTestable.GetMatches(directoryName, "[a-z]+");

            Assert.IsNotNull(matches);
            Assert.AreEqual(1, matches.Count());
        }
    }
}