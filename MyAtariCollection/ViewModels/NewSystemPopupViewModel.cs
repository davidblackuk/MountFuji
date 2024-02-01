using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using MountFuji.Models;
using MountFuji.Services;
using TinyMvvm;

namespace MountFuji.ViewModels
{
    public interface IConfirmableViewModel
    {
        bool Confirmed { get; set; }  
    }
    
    public partial class NewSystemViewModelViewModel: TinyViewModel, IConfirmableViewModel
    {
        private readonly IMachineTemplateService templatesService;
        private readonly IPopupNavigation popupNavigation;

        public NewSystemViewModelViewModel(IMachineTemplateService templatesService, IPopupNavigation popupNavigation)
        {
            this.templatesService = templatesService;
            this.popupNavigation = popupNavigation;

            Templates = templatesService.All();
        }



        public void SelectFirstTemplate()
        {
            SelectedTemplate = Enumerable.First<AtariConfigurationTemplate>(Templates);
        }
        

        public bool Confirmed { get; set; } 

        public AtariConfiguration GetConfiguration()
        {
            var res = templatesService.CreateConfigurationFromTemplate(SelectedTemplate.DisplayName);
            res.DisplayName = Name;
            res.Description = Description ?? String.Empty;
            return res;
        }


        [ObservableProperty()]
        private IEnumerable<AtariConfigurationTemplate> templates;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(OkCommand))]
        private AtariConfigurationTemplate selectedTemplate;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(OkCommand))]
        private string name;

        [ObservableProperty]
        private string description;


        [RelayCommand]
        private void SelectionChanged()
        {
            Name = SelectedTemplate.DisplayName;
        }
        
        
        [RelayCommand]
        private async Task Cancel()
        {
            await popupNavigation.PopAsync();
        }

        [RelayCommand(CanExecute = nameof(HasValidData))]
        private async Task Ok()
        {
            Confirmed = true;
            await popupNavigation.PopAsync();
        }


        private bool HasValidData()
        {
            return SelectedTemplate != null && !string.IsNullOrWhiteSpace(Name);
        }
    }
}

