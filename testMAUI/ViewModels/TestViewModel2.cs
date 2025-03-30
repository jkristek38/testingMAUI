using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testMAUI.Models;
using testMAUI.Views;

namespace testMAUI.ViewModels;

public partial class TestViewModel2 : ObservableObject, IRecipient<TestItemAddedMessage>, IRecipient<TestItemsRequestMessage>, IRecipient<TestItemRemovedMessage>
{

    private ObservableCollection<TestItem> _items;
    public ObservableCollection<TestItem> Items => _items;
    private ItemStore _itemStore;
    public TestViewModel2(ItemStore itemStore)
    {
        _itemStore = itemStore;
        _items = new ObservableCollection<TestItem>();
        WeakReferenceMessenger.Default.Register<TestItemAddedMessage>(this);
        WeakReferenceMessenger.Default.Register<TestItemsRequestMessage>(this);
        WeakReferenceMessenger.Default.Register<TestItemRemovedMessage>(this);
        WeakReferenceMessenger.Default.Send<TestItemsRequestMessage>();
    }

    [RelayCommand]
    private Task ReturnTest() => Shell.Current.GoToAsync("..");
    [RelayCommand]
    private Task TestDetails() => Shell.Current.GoToAsync($"{nameof(TestDetails)}");

    public void Receive(TestItemAddedMessage message)
    {
        _items.Add(message.i);
    }

    public void Receive(TestItemsRequestMessage message)
    {

            _items.Clear();
            foreach(TestItem item in message.Response)
            {
                _items.Add(item);
            }
    }


    [RelayCommand]
    private void RemoveAllTest()
    {
        _items.Clear();
        _itemStore.RemoveAll();
    }

    public void Receive(TestItemRemovedMessage message)
    {
            _items.Remove(message.i);
    }
}
