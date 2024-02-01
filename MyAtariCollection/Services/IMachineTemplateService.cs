using MountFuji.Models;

namespace MountFuji.Services;

public interface IMachineTemplateService
{
    IEnumerable<AtariConfigurationTemplate> All();
    AtariConfiguration CreateConfigurationFromTemplate(string name);
    
    
}