/*
   Mount Fuji - A front end for the Hatari Emulator
   Copyright (C) 2024  David Black

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

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

