// Mount Fuji - A front end for the Hatari Emulator
//    Copyright (C) 2024  David Black
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Runtime.CompilerServices;

namespace MountFuji.ViewModels.MainViewModelCommands;

public class MainViewModelCommandsBase
{
    private readonly IServiceProvider serviceProvider;
    private Lazy<MainViewModel> lazyMainViewModel;

    /// <summary>
    /// Get the MainViewModel for the command. This is resolved lazily and once only.
    /// </summary>
    protected MainViewModel ViewModel => lazyMainViewModel.Value;

    /// <summary>
    /// Base class for commands that require access to the main view model
    /// </summary>
    /// <param name="serviceProvider"></param>
    public MainViewModelCommandsBase(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        lazyMainViewModel = new Lazy<MainViewModel>(InitializeMainViewModel);
    }
    
    /// <summary>
    /// Retrieves the singleton MainViewModel instance
    /// </summary>
    /// <returns></returns>
    private MainViewModel InitializeMainViewModel()
    {
        return serviceProvider.GetService<MainViewModel>();
    }
}