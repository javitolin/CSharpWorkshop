using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleUnitTests.Tests
{
    [TestClass()]
    public class FibonacciResponseTests
    {
        [TestMethod()]
        public void GetFibonnaci_FirstNumber_ReturnsZero()
        {
            // Arrange
            FibonacciResponse fib = new FibonacciResponse();

            // Act
            var answer = fib.GetFibonacci(0);

            // Assert
            Assert.AreEqual(0, answer);
        }        
        
        [TestMethod()]
        public void GetFibonnaci_SecondNumber_ReturnsOne()
        {
            // Arrange
            FibonacciResponse fib = new FibonacciResponse();

            // Act
            var answer = fib.GetFibonacci(1);

            // Assert
            Assert.AreEqual(1, answer);
        }        

        [TestMethod()]
        public void GetFibonnaci_FourthNumber_ReturnsTwo()
        {
            // Arrange
            FibonacciResponse fib = new FibonacciResponse();

            // Act
            var answer = fib.GetFibonacci(3);

            // Assert
            Assert.AreEqual(2, answer);
        }

        [TestMethod()]
        public void GetFibonnaci_NegativeIndex_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            FibonacciResponse fib = new FibonacciResponse();

            // Act
            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => fib.GetFibonacci(-1));
        }
    }
}