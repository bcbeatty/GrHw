using GrHw.Client.Repository;

namespace GwHw.Client.Repository.Implementation
{
    public class FileIORepository : IFileIORepository
    {
        public string[] ReadFile(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }
    }
}
