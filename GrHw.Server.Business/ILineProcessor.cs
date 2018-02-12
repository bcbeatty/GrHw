namespace GrHw.Server.Business
{
    public interface ILineProcessor<T> where T : class
    {
        T ParseLine(string line);
        char Delimiter { get; }
    }
}