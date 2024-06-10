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

using System.Configuration;
using Microsoft.Extensions.Logging;
using Mopups.Pages;
using MountFuji;
using MountFuji.Services.UpdatesService;
using MountFuji.Views;
using MountFujiTests.ViewModels.MainViewModelCommands;

namespace MountFujiTests.ViewModels;

public class MainViewModelTests: CommandsTestBase
{
    private Mock<IFujiFilePickerService> fujiFilePickerMock;
    
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
    }


    
    private MainViewModel CreateSut()
    {

        return MainViewModel;
    }
}