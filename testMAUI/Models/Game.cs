using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using testMAUI.Views;

namespace testMAUI.Models
{
    public partial class Game : ObservableObject, IGameFunctions, IEquatable<Game>
    {
        public delegate void DelegateActions();
        private DelegateActions a;
        public enum genre { strategy, andenture, racing, horror, simulator,action }
        private readonly genre type;
        private List<string> actions;
        private readonly CartStore _cartStore;
        private readonly InventoryStore _inventoryStore;

        [ObservableProperty]
        private DateTime lastPlayed;
        [ObservableProperty]
        private bool isRunning;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private float price;
        [ObservableProperty]
        private string description;
        [ObservableProperty]
        private string gameIcon;
        [ObservableProperty]
        private int minutes_played;
        // public string Name { get { return name; } set { name = value; } }
        //public DateTime LastPlayed { get => lastPlayed; set => lastPlayed = value; }
        public short Type { get { return (short)type; } }
        public genre Type2 { get { return type; } }
        //public bool IsRunning { get => isRunning; set => isRunning = value; }
        //public float Price { get=>price; set=>value; } 
        //public float Description { get=>description; set=>value; }
        public Game() : this(0, "randon_name",10.00f,"random_description")
        {
        }
        public Game(short t)
        {
            lastPlayed = DateTime.MinValue;
            isRunning = false;
            Minutes_played = 0;
            this.name = "y";
            switch (t)
            {
                case 0: a = Build_base; a += Attack_enemy_base; a += Lose_base; break;
                case 3: a = Jumpscare; break;
                default: a = Load_Menu; break;
            }
            type = (genre)t;
            GameIcon = this.GetImageName();
        }
        public Game(short t, string name,float price, string description,CartStore cartStore,InventoryStore inventoryStore)
        {
            lastPlayed = DateTime.MinValue;
            isRunning = false;
            this.name = name;
            this.price = price;
            this.description = description;
            Minutes_played = 0;
            switch (t)
            {
                case 0: a = Build_base; a += Attack_enemy_base; a += Lose_base; break;
                case 3: a = Jumpscare; break;
                default: a = Load_Menu; break;
            }
            type = (genre)t;
            _cartStore = cartStore;
            _inventoryStore = inventoryStore;
            GameIcon = this.GetImageName();
        }
        public Game(short t, string name, float price, string description)
        {
            lastPlayed = DateTime.MinValue;
            isRunning = false;
            this.name = name;
            this.price = price;
            this.description = description;
            Minutes_played = 0;
            switch (t)
            {
                case 0: a = Build_base; a += Attack_enemy_base; a += Lose_base; break;
                case 3: a = Jumpscare; break;
                default: a = Load_Menu; break;
            }
            type = (genre)t;
            GameIcon= this.GetImageName();
        }
        [RelayCommand]
        private void AddToCart()
        {
            _cartStore.AddToCart(new Game(Type,Name, Price,Description,_cartStore,_inventoryStore));
        }

        public void AddToInventory()
        {
            _inventoryStore.AddToInventory(new Game(Type, Name, Price, Description, _cartStore, _inventoryStore));
        }
        [RelayCommand]
        private void StartStop()
        {
            if (!isRunning)
            {
                Play();
            }
            else
            {
                Close();
            }
        }
        [RelayCommand]
        private Task Details() => Shell.Current.GoToAsync($"{nameof(DetailsView)}",new Dictionary<string, object> { ["Game"]=this });

        public void Play()
        {
            IsRunning = true;
            Reset_Timer(out lastPlayed);
        }

        public void Close()
        {
            Minutes_played = (lastPlayed - DateTime.Now).Minutes;
            Reset_Timer(out lastPlayed);
            IsRunning = false;
        }

        public void Reset_Timer(out DateTime lastPlayed) { lastPlayed = DateTime.Now; }
        public string Export_actions()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string action in actions)
            {
                sb.Append(action + ";");
            }
            string serelialized = sb.ToString();
            return serelialized.Remove(serelialized.LastIndexOf(";"));
        }

        public int Get_Playtime() { return Minutes_played; }
        public void Do_actions() { a.Invoke(); }
        private void Build_base() => actions.Add("base build");
        private void Load_Menu() => actions.Add("menu loaded");
        private void Attack_enemy_base() => actions.Add("base destroyed");
        private void Lose_base() => actions.Add("our base destroyed");
        private void Jumpscare() => actions.Add("our base destroyed");
        public bool Equals(Game other)
        {
            if (other == null) return false;
            else if (other.name == this.Name) return true;
            return false;
        }
        public override bool Equals(Object other)
        {
            if (other == null) return false;
            Game g = other as Game;
            return Equals(g);
        }
        public string Convert_Short_to_Type(short t)
        {
            return ((genre)t).ToString();
        }

        private string GetImageName()
        {
            return Name.Replace(" ", "")+".jpg";
        }
    }
}
