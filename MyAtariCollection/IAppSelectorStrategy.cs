namespace MyAtariCollection;

public interface IAppSelectorStrategy
{
    Task<string> SelectApplication(string title, Action<string> complete);
}