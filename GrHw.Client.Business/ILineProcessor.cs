namespace GrHw.Client.Business
{
    public interface ILineProcessor<T> where T : class
    {
        T ParseLine(string[] line);
        string ToLine(T item, char delimiter);
    }
}