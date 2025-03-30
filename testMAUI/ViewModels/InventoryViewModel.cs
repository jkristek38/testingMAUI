using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testMAUI.Models;
using testMAUI.Views;

namespace testMAUI.ViewModels
{
    public partial class InventoryViewModel : ObservableObject, IRecipient<InventoryItemsRequestMessage>, IRecipient<InventoryItemAddedMessage>
    {
        [ObservableProperty]
        private bool loadingIsBusy;
        private readonly ObservableCollection<Game> _games;
        public ObservableCollection<Game> Games => _games;


        public InventoryViewModel(/*IMessenger messenger*/)
        {
            LoadingIsBusy = true;
            _games = new ObservableCollection<Game>();
            //messenger.RegisterAll(this);
            // messenger.Send<CartItemsRequestMessage>();
            WeakReferenceMessenger.Default.Register<InventoryItemAddedMessage>(this);

            WeakReferenceMessenger.Default.Register<InventoryItemsRequestMessage>(this);
            WeakReferenceMessenger.Default.Send<InventoryItemsRequestMessage>();
            LoadingIsBusy = false;
        }
        private void AddInventoryItem(Game g)
        {
            _games.Add(g);
        }

        public void Receive(InventoryItemsRequestMessage message)
        {
            if (!message.HasReceivedResponse) return;
            _games.Clear();
            foreach (Game item in message.Response)
            {
                AddInventoryItem(item);
            }
        }

        public void Receive(InventoryItemAddedMessage message)
        {
            AddInventoryItem(message.Game);
        }

    }
}
