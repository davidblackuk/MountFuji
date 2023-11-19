using System;
namespace MyAtariCollection.ViewModels
{
    public partial class NewSystemPopupViewModel: TinyViewModel
    {
        private readonly IMachineTemplateService templatesService;
        private readonly IPopupNavigation popupNavigation;

        public NewSystemPopupViewModel(IMachineTemplateService templatesService, IPopupNavigation popupNavigation)
        {
            this.templatesService = templatesService;
            this.popupNavigation = popupNavigation;

            Templates = templatesService.All();
        }



        public void Foo()
        {
            SelectedTemplate = Templates.First();
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
        private async void Cancel()
        {
            await popupNavigation.PopAsync();
        }

        [RelayCommand(CanExecute = nameof(HasValidData))]
        private async void Ok()
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

