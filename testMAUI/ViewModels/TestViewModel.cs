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

public partial class TestViewModel : ObservableObject,IRecipient<TestItemsRemovedMessage>, IRecipient<TestItemRemovedMessage>
{
    [ObservableProperty]
    private Microsoft.Maui.Graphics.Color saveButtonColor;
    [ObservableProperty]
	private string testingText;
    [ObservableProperty]
    private string testingNumber;
    [ObservableProperty]
    private string testingResult;
    [ObservableProperty]
    private string selectedColor;
    private readonly ItemStore _itemStore;
    private ObservableCollection<TestItem> _items;
	public ObservableCollection<TestItem> Items => _items;

    private ObservableCollection<string> _selectableColors;
    public ObservableCollection<string> SelectableColors => _selectableColors;


    public TestViewModel(ItemStore itemStore)
	{
        _itemStore = itemStore;
		saveButtonColor = Colors.Gray;
        _items = new ObservableCollection<TestItem>();
        _selectableColors = new ObservableCollection<string>();
        _selectableColors.Add("Red");
        _selectableColors.Add("Green");
        _selectableColors.Add("Black");
        selectedColor = "Green";
		_items.Add(new TestItem(11,"fff",Colors.Red,_itemStore));
        _items.Add(new TestItem(22,"dd",Colors.Blue,_itemStore));
        _itemStore.AddTestItem(new TestItem(11, "fff", Colors.Red, _itemStore));
        _itemStore.AddTestItem(new TestItem(22, "dd", Colors.Blue, _itemStore));
        WeakReferenceMessenger.Default.Register<TestItemRemovedMessage>(this);
        WeakReferenceMessenger.Default.Register<TestItemsRemovedMessage>(this);
    }
	[RelayCommand]
	public void SaveTest()
	{
		if(TestingNumber != null && TestingText != null)
		{
			int number;
            Microsoft.Maui.Graphics.Color color;
            try
            {
                number = Convert.ToInt32(TestingNumber);
            }
			catch
			{
                TestingNumber = null;
                saveButtonColor = Colors.Red;
                TestingResult = "Wrong number";
                return;
			}

            switch (selectedColor)
            {
                case "Red": color= Colors.Red; break;
                case "Green": color= Colors.Green; break;
                case "Black": color= Colors.Black; break;
                default: color= Colors.White; break;                    
            }
            TestItem t = new TestItem(number, TestingText, color,_itemStore);
            _itemStore.AddTestItem(t);
            _items.Add(t);
			TestingResult = "Saved";
			saveButtonColor = Colors.Green;
            selectedColor = "Green";
        }
		else
		{
            saveButtonColor = Colors.Red;
            TestingResult = "Cannot save empty item";
        }
		TestingText = null;
		TestingNumber = null;
    }

    [RelayCommand]
    private Task Edit() => Shell.Current.GoToAsync($"{nameof(TestView2)}");

    [RelayCommand]
    private Task TestDetails() => Shell.Current.GoToAsync($"{nameof(TestDetails)}");

    [RelayCommand]
    private void RemoveAllTest()
    {
        _items.Clear();
        _itemStore.RemoveAll();
    }

    public void Receive(TestItemRemovedMessage message)
    {
        for(int i=0;i<_items.Count;i++)
        {
            if (message.i.Text.Equals(_items[i].Text) && message.i.Color.Equals(_items[i].Color)&& message.i.Number.Equals(_items[i].Number)) {
                _items.RemoveAt(i);
            }
        }
    }
    public void Receive(TestItemsRemovedMessage message)
    {
        _items.Clear();
    }
}