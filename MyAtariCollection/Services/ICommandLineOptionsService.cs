namespace MyAtariCollection.Services;

public interface ICommandLineOptionsService
{
    string Generate(AtariConfiguration config);
}