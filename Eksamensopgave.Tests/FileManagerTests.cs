using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Stregsystem.DataAccess;
using Stregsystem.Abstractions;

namespace Stregsystem.Tests
{
    public class FileManagerTests
    {
        /*
            The name of your test should consist of three parts:
            * The name of the method being tested.
            * The scenario under which it's being tested.
            * The expected behavior when the scenario is invoked.
        */
        public Mock<IStreamReader> StreamReaderMock { get; } = new Mock<IStreamReader>();
        [Theory]
        [InlineData(';')]
        [InlineData(',')]
        public void Load_CanHandleDifferentSplitChars_SplitsString(char splitChar)
        {
            // Arrange
            string text1 = "Test Text";
            string text2 = " text  ";
            LoadFromFile<Foo> fileManager = new LoadFromFile<Foo>(StreamReaderMock.Object, splitChar);
            StreamReaderMock.Setup(sr => sr.ReadLine()).Returns(text1 + splitChar + text2);
            StreamReaderMock.SetupSequence(sr => sr.EndOfStream).Returns(false).Returns(true);
            // Act
            IEnumerable<Foo> actual = fileManager.Load(arr => new Foo() { Property1 = arr[0], Property2 = arr[1]});;

            // Assert
            Assert.Single(actual);
            Foo actualObj = actual.First();
            Assert.Equal(text1, actualObj.Property1);
            Assert.Equal(text2, actualObj.Property2);
        }

        [Fact]
        public void Load_CanSkipFirstLine_RunOnce()
        {
            // Arrange
            LoadFromFile<Foo> fileManager = new LoadFromFile<Foo>(StreamReaderMock.Object, ',');
            StreamReaderMock.Setup(sr => sr.EndOfStream).Returns(true);
            // Act
            IEnumerable<Foo> actual = fileManager.Load(null);

            // Assert
            StreamReaderMock.Verify(sr => sr.ReadLine(), Times.Once);
        }
    }
}
