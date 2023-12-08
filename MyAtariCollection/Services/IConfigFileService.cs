namespace MyAtariCollection.Services;

public interface IConfigFileService
{
    string Generate(AtariConfiguration config);
}