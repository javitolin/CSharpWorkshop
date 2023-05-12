using System.IO.Abstractions;

namespace FileSystem.StreamsWorkshop
{
    public class FileStreams
    {
        public IEnumerable<string> ReadFromFile(string filename)
        {
            using var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(stream);

            do
            {
                yield return streamReader.ReadLine()!;
            }
            while (!streamReader.EndOfStream);
        }

        public void CopyFile(string sourceFile, string destinationFile)
        {
            using var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
            using var destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            
            byte[] buffer = new byte[1024];

            while (sourceStream.Position < sourceStream.Length)
            {
                int bytesRead = sourceStream.Read(buffer, 0, buffer.Length);
                destinationStream.Write(buffer, 0, bytesRead);
            }

        }
    }
}
