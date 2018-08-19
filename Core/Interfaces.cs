namespace Divvy.Core
{
    public interface IHasPanel<T> where T : Element
    {
        T Panel { get; }
    }
}