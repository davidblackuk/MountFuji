

namespace MyAtariCollection.Services;

public class SystemsService : ISystemsService
{
    public IEnumerable<AtariConfiguration> All()
    {
        List<AtariConfiguration> res = new List<AtariConfiguration>();

        // temporary crap implementation
        IMachineTemplateService templateService = new MachineTemplateService();
        foreach (AtariConfigurationTemplate template in templateService.All())
        {
            res.Add(templateService.CreateConfigurationFromTemplate(template.DisplayName));
        }
        return res;
    }
}