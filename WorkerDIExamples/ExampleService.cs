namespace WorkerDIExamples
{
    public class ExampleService : IExampleService
    {
        public int MyNumber = 1;

        public int GetNumber()
        {
            return MyNumber;
        }

        public void RaiseNumber()
        {
            MyNumber++;
        }
    }
}
