using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testMAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string entryName;
        [ObservableProperty]
        private string entryPassword;
        [ObservableProperty]
        private Microsoft.Maui.Graphics.Color loginButtonBackColor;
        [ObservableProperty]
        private bool logged = true;
        [ObservableProperty]
        private string loggingButtonText = "Login";
        public LoginViewModel()
        {
            LoginButtonBackColor = Colors.Blue;
        }
        [RelayCommand]
        public void LoginUser()
        {
            if(EntryName == "User" &&  EntryPassword == "abc")
            {
                LoginButtonBackColor = Colors.Green;
                Logged = false;
                LoggingButtonText = "Already logged";
                EntryName = null;
                EntryPassword = null;
                Return();
            }
            else
            {
                LoginButtonBackColor = Colors.Red;
                EntryName = null;
                EntryPassword = null;
            }
        }
        [RelayCommand]
        Task Return() => Shell.Current.GoToAsync($"..?IsLoggedIn={!Logged}");
    }
}
