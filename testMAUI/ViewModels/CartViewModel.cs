using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testMAUI.Models;
using testMAUI.Views;

namespace testMAUI.ViewModels
{
    [QueryProperty(nameof(IsLoggedIn), nameof(IsLoggedIn))]
    public partial class CartViewModel : ObservableValidator, IRecipient<CartItemAddedMessage>,IRecipient<CartItemsRequestMessage>
    {
        private readonly ObservableCollection<Game> _games;
        public ObservableCollection<Game> Games => _games;

        [ObservableProperty]
        [IsTrue]
        private bool _hasAgreedToTermsAndConditions;
        [ObservableProperty]
        private string _hasAgreedToTermsAndConditionsText = "Agree to terms and conditions";
        [ObservableProperty]
        [IsTrue]
        private bool isLoggedIn;
        [ObservableProperty]
        private string isLoggedInText = "Please log in";
        [ObservableProperty]
        private bool loadingIsBusy;

        public CartViewModel(/*IMessenger messenger*/)
        {
            LoadingIsBusy = true;
            _games = new ObservableCollection<Game>();
            //messenger.RegisterAll(this);
           // messenger.Send<CartItemsRequestMessage>();
            WeakReferenceMessenger.Default.Register<CartItemAddedMessage>(this/*, (r, m) => { AddCartItem(m.Game); }*/);

            WeakReferenceMessenger.Default.Register<CartItemsRequestMessage>(this);
            WeakReferenceMessenger.Default.Send<CartItemsRequestMessage>();
            LoadingIsBusy = false;
        }

        public void Receive(CartItemAddedMessage message)
        {
            AddCartItem(message.Game);
        }
        public void Receive(CartItemsRequestMessage message)
        {
            if (!message.HasReceivedResponse) return;
            _games.Clear();
            foreach (Game item in message.Response)
            {
                AddCartItem(item);
            }
        }
        private void AddCartItem(Game g)
        {
            _games.Add(g);
        }
        [RelayCommand]
        private Task LogIn() => Shell.Current.GoToAsync($"{nameof(LoginView)}");

        [RelayCommand]
        public async Task Checkout()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                HasAgreedToTermsAndConditionsText += "!";
                IsLoggedInText += "!";
                return;
            }
            else
            {
                HasAgreedToTermsAndConditionsText=HasAgreedToTermsAndConditionsText.Replace("!", "");
                IsLoggedInText = IsLoggedInText.Replace("!", "");
                if (_games.Count == 1)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Game bought!", "Ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "All games bought!", "Ok");
                }
                foreach(Game item in _games) { item.AddToInventory(); }
            }
        }

    }
    public sealed class IsTrueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return bool.TryParse(value.ToString(), out bool result) && result
                ? ValidationResult.Success
                : new ValidationResult("Must be true");
        }
    }
}
