using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace testMAUI.Models
{
    public partial class TestItem : ObservableObject
    {
        [ObservableProperty]
        private int number;
        [ObservableProperty]
        private string text;
        [ObservableProperty]
        private Color color;
        private ItemStore _itemStore;

        public TestItem(int number, string text, Color color,ItemStore itemStore)
        {
            this.number = number;
            this.text = text;
            this.color = color;
            _itemStore = itemStore;
        }
        [RelayCommand]
        private void RemoveItem()
        {
            _itemStore.Remove(this);
        }
    }

    public class ItemStore
    {
        private List<TestItem> _items;
        public ItemStore()
        {
            _items = new List<TestItem>();
            WeakReferenceMessenger.Default.Register<ItemStore, TestItemsRequestMessage>(this, (r, m) => { m.Reply(r._items); });
        }
        public void AddTestItem(TestItem i)
        {
            _items.Add(i);
            WeakReferenceMessenger.Default.Send(new TestItemAddedMessage(i));
        }
        public void RemoveAll()
        {
            _items.Clear();
            WeakReferenceMessenger.Default.Send(new TestItemsRemovedMessage());
        }
        public void Remove(TestItem t)
        {
            _items.Remove(t);
            WeakReferenceMessenger.Default.Send(new TestItemRemovedMessage(t));
        }
    }



}
