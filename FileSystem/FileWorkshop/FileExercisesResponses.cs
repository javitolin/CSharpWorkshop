
namespace FileSystem.FileWorkshop
{
    public class FileExercisesResponses
    {
        public bool CheckFileExists(string path)
        {
            return File.Exists(path);
        }

        public void WriteToFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public bool CreateFile(string path)
        {
            try
            {
                using var fileStream = File.Create(path);

                return fileStream != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<string>? GetFileContent(string path)
        {
            if (!File.Exists(path))
            {
                yield break;
            }

            foreach (string line in File.ReadLines(path))
            {
                yield return line;
            }
        }
    }
}