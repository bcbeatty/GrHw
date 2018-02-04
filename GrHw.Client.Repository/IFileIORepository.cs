using System.Text;

namespace GrHw.Client.Repository
{
    public interface IFileIORepository
    {
        string[] ReadFile(string path);
    }

   
}
