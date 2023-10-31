using MyAtariCollection.Models;

namespace MyAtariCollection.Services;

public interface ISystemsService
{
    IEnumerable<AtariConfiguration> All();
  
}