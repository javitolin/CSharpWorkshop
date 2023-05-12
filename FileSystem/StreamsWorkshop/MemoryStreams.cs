namespace FileSystem.StreamsWorkshop
{
    public class MemoryStreams
    {
        public void WriteToMemory(string toWrite)
        {
            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);

            streamWriter.WriteLine(toWrite);

            // Console.WriteLine(Encoding.ASCII.GetString(stream.ToArray()));
        }

        public void CopyMemories(MemoryStream sourceMemory, MemoryStream destinationMemory)
        {
            byte[] buffer = new byte[1024];

            while (sourceMemory.Position < destinationMemory.Length)
            {
                int bytesRead = sourceMemory.Read(buffer, 0, buffer.Length);
                destinationMemory.Write(buffer, 0, bytesRead);
            }
        }
    }
}
