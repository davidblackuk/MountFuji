using MyAtariCollection.Models;

namespace MyAtariCollection.Services;

public class MachineTemplateService : IMachineTemplateService
{
    private List<AtariConfigurationTemplate> allTemplates = new()
    {
        new ()
        {
            DisplayName = "ST",
            System = AtariSystem.ST
        },
        new ()
        {
            DisplayName = "Mega ST",
            System = AtariSystem.MegaST
        },
        new ()
        {
            DisplayName = "STE",
            System = AtariSystem.STE
        },
        new ()
        {
            DisplayName = "Mega STE",
            System = AtariSystem.MegaSTE
        },
        new ()
        {
            DisplayName = "TT",
            System = AtariSystem.TT
        },
        new ()
        {
            DisplayName = "Falcon",
            System = AtariSystem.Falcon
        },
    };

    public IEnumerable<AtariConfigurationTemplate> All() => allTemplates;

    public AtariConfiguration CreateConfigurationFromTemplate(string name)
    {
        var template =
            allTemplates.FirstOrDefault(t => t.DisplayName.ToLowerInvariant() == name.ToLowerInvariant());
        if (template == null)
        {
            throw new ArgumentException($"System '{name}' is not recognised");
        }

        AtariConfiguration config = new()
        {
            // lets use 
            DisplayName = template.DisplayName,
            System = template.System
        };

        return config;
    }
    
}