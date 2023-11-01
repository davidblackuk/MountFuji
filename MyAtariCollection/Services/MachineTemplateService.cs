using MyAtariCollection.Models;

namespace MyAtariCollection.Services;

public class MachineTemplateService : IMachineTemplateService
{
    private List<AtariConfigurationTemplate> allTemplates = new()
    {
        new ()
        {
            DisplayName = "ST",
            SystemType = AtariSystemType.ST,
            StVideoTiming = VideoTiming.Three,
        },
        new ()
        {
            DisplayName = "Mega ST",
            SystemType = AtariSystemType.MegaST,
            StVideoTiming = VideoTiming.Three,
        },
        new ()
        {
            DisplayName = "STE",
            SystemType = AtariSystemType.STE,
            StVideoTiming = VideoTiming.Three,
        },
        new ()
        {
            DisplayName = "Mega STE",
            SystemType = AtariSystemType.MegaSTE,
            StVideoTiming = VideoTiming.Three,
        },
        new ()
        {
            DisplayName = "TT",
            SystemType = AtariSystemType.TT,
            StVideoTiming = VideoTiming.Three,
        },
        new ()
        {
            DisplayName = "Falcon",
            SystemType = AtariSystemType.Falcon,
            StVideoTiming = VideoTiming.Three,
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
            SystemType = template.SystemType,
            StVideoTiming = template.StVideoTiming
        };

        return config;
    }
    
}