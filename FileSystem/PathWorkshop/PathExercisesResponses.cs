namespace FileSystem.PathWorkshop
{
    public class PathExercisesResponses
    {
        public string CombinePaths(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        public string GetFilename(string path, bool withExtension = true)
        {
            if (withExtension)
            {
                return Path.GetFileName(path);
            }

            return Path.GetFileNameWithoutExtension(path);
        }

        public bool IsRootedPath(string path)
        {
            return Path.IsPathRooted(path);
        }

        public string? GetParentFolder(string path)
        {
            return Path.GetDirectoryName(path);
        }
    }
}
