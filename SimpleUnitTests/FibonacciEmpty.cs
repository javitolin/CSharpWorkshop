namespace SimpleUnitTests
{
    public class FibonacciEmpty
    {
        public bool IsFibonacciNumber(int number)
        {
            int fib1 = 0, fib2 = 1, fibonacci = 0;

            while (fibonacci < number)
            {
                fibonacci = fib1 + fib2;
                fib1 = fib2;
                fib2 = fibonacci;
            }

            return (fibonacci == number);
        }

        public int GetFibonacci(int index)
        {
            if (index == 0)
                return 0;

            if (index == 1)
                return 1;

            int fib1 = 0, fib2 = 1, fibonacci = 0;

            for (int i = 2; i <= index; i++)
            {
                fibonacci = fib1 + fib2;
                fib1 = fib2;
                fib2 = fibonacci;
            }

            return fibonacci;
        }
    }
}
