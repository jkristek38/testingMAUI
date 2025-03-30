using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testMAUI.Models;

namespace testMAUI.ViewModels
{
    [QueryProperty(nameof(Game), nameof(Game))]
    public partial class DetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private Game game;

        [RelayCommand]
        Task Return() => Shell.Current.GoToAsync("..");
    }
}
