using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using testMAUI.Models;

namespace testMAUI.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        private IConnectivity connection;
        private readonly CartStore cartStore;
        private readonly InventoryStore inventoryStore;
        [ObservableProperty]
        private bool loadingIsBusy;
        private readonly ObservableCollection<Game> _games;
        public ObservableCollection<Game> Games => _games;
        public GameViewModel(CartStore cartStore, InventoryStore inventoryStore, IConnectivity connection)
        {
            /* _games = new ObservableCollection<Game>()
            {
                 new Game(5,"World of Tanks Blitz ",0.0f,cartStore,inventoryStore),
                 new Game(5,"HROT",16.79f,cartStore,inventoryStore),
                 new Game(1,"Polda 3",3.29f,cartStore, inventoryStore),
                 new Game(4,"Euro Truck Simulator 2",19.99f,cartStore, inventoryStore),
                 new Game(4,"The Sims™ 4",0.0f,cartStore, inventoryStore),
                 new Game(3,"Outlast",19.5f,cartStore, inventoryStore),
                 new Game(2,"FlatOut 2",9.99f,cartStore, inventoryStore),
                 new Game(0,"Steel Division 2",39.99f,cartStore, inventoryStore),
                 new Game(0,"Hearts of Iron IV",39.99f,cartStore, inventoryStore),
                 new Game(0,"Age of Empires III: Definitive Edition",19.99f,cartStore, inventoryStore)
             };*/
            this.connection = connection;
            this.cartStore = cartStore;
            this.inventoryStore = inventoryStore;
            LoadingIsBusy = true;
            _games = new ObservableCollection<Game>();
            LoadDatabase();
            
        }

        private async Task LoadDatabase()
        {
            if (this.connection.NetworkAccess != NetworkAccess.Internet)
            {
                LoadingIsBusy = false;
                await Shell.Current.DisplayAlert("Internet Issue", "You need Internet access to buy games!", "OK");
            }
            else
            {
                var stream = await FileSystem.OpenAppPackageFileAsync("databaseofgames.json");
                StreamReader reader = new StreamReader(stream);
                //StreamReader reader = new StreamReader("C:\\Users\\jkris\\source\\repos\\testMAUI\\testMAUI\\Resources\\Raw\\databaseofgames.json");
                var json = await reader.ReadToEndAsync();
                foreach (Game item in JsonSerializer.Deserialize<List<Game>>(json))
                {
                    _games.Add(new Game(item.Type, item.Name, item.Price, item.Description, cartStore, inventoryStore));
                }
                LoadingIsBusy = false;
            }
        }
        [RelayCommand]
        public void RefreshDatabase()
        {
            LoadingIsBusy = true;
            _games.Clear();
            LoadDatabase();
        }
    }
}
