namespace Divvy.Core
{
    public interface IHasPanel<T> where T : DivvyPanel
    {
        T Panel { get; }
    }
}