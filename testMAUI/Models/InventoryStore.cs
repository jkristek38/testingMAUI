using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testMAUI.Models
{
    public class InventoryStore
    {
        private readonly List<Game> _items;
        //private readonly IMessenger _messenger;
        public InventoryStore(/*IMessenger messenger*/)
        {
            // _messenger = messenger;
            _items = new List<Game>();
            WeakReferenceMessenger.Default.Register<InventoryStore, InventoryItemsRequestMessage>(this, (r, m) => { m.Reply(r._items); });
            //messenger.Register<CartStore, CartItemsRequestMessage>(this, (r, m) => { m.Reply(r._items); });
        }

        public void AddToInventory(Game item)
        {
            _items.Add(item);
            WeakReferenceMessenger.Default.Send<InventoryItemAddedMessage>(new InventoryItemAddedMessage(item));
            //_messenger.Send(new CartItemAddedMessage(item));
        }
    }
}
