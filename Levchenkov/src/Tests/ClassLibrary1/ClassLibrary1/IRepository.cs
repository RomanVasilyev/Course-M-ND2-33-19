namespace ClassLibrary1
{
    public interface IRepository<T>
    {
        T Get(int id);
    }
}