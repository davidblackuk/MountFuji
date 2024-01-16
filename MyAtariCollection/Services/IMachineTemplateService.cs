using MyAtariCollection.Models;

namespace MyAtariCollection.Services;

public interface IMachineTemplateService
{
    IEnumerable<AtariConfigurationTemplate> All();
    AtariConfiguration CreateConfigurationFromTemplate(string name);
    
    
}