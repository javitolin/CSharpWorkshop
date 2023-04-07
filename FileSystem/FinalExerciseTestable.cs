using System.IO.Abstractions;
using System.Text.RegularExpressions;

namespace FileSystem
{
    public class FinalExerciseTestable
    {
        private IFileSystem _fileSystem;
        public FinalExerciseTestable(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        private void ValidateInput(string directory, string regexPattern)
        {
            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (string.IsNullOrEmpty(regexPattern))
            {
                throw new ArgumentNullException(nameof(regexPattern));
            }

            if (!_fileSystem.Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"Directory [{directory}] not found");
            }

            try
            {
                var _ = new Regex(regexPattern);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Wrong regex pattern - [{regexPattern}]", nameof(regexPattern), ex);
            }
        }

        private bool CheckFileContentMatch(string filePath, Regex regex)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !_fileSystem.File.Exists(filePath))
            {
                return false;
            }

            foreach (var line in _fileSystem.File.ReadLines(filePath))
            {
                if (regex.IsMatch(line))
                {
                    return true;
                }

            }

            return false;
        }

        public IEnumerable<string?> GetMatchesRecursive(string directory, string regexPattern)
        {
            ValidateInput(directory, regexPattern);

            Regex regex = new Regex(regexPattern);
            foreach (var file in _fileSystem.Directory.GetFiles(directory))
            {
                if (CheckFileContentMatch(file, regex))
                {
                    yield return file;
                }
            }

            foreach (var innerDirectory in _fileSystem.Directory.GetDirectories(directory))
            {
                foreach (var match in GetMatchesRecursive(innerDirectory, regexPattern))
                {
                    yield return match;
                }
            }
        }

        public IEnumerable<string> GetMatches(string directory, string regexPattern)
        {
            ValidateInput(directory, regexPattern);

            Regex regex = new Regex(regexPattern);
            Stack<string> directories = new Stack<string>();
            directories.Push(directory);

            while (directories.Any())
            {
                string currentDirectory = directories.Pop();
                foreach (var file in _fileSystem.Directory.GetFiles(currentDirectory))
                {
                    if (CheckFileContentMatch(file, regex))
                    {
                        yield return file;
                    }
                }

                foreach (var innerDirectory in _fileSystem.Directory.GetDirectories(currentDirectory))
                {
                    directories.Push(innerDirectory);
                }
            }

        }
    }
}
