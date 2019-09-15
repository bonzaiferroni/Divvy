namespace Bonwerk.Divvy.Core
{
    public interface IHasPanel<T> where T : Element
    {
        T Panel { get; }
    }
}