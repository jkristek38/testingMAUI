using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testMAUI.Models
{
    public class CartStore
    {
        private readonly List<Game> _items;
        //private readonly IMessenger _messenger;
        public CartStore(/*IMessenger messenger*/)
        {
          // _messenger = messenger;
            _items = new List<Game>();
           WeakReferenceMessenger.Default.Register<CartStore,CartItemsRequestMessage>(this, (r, m) => { m.Reply(r._items); });
            //messenger.Register<CartStore, CartItemsRequestMessage>(this, (r, m) => { m.Reply(r._items); });
        }

        public void AddToCart(Game item)
        {
            _items.Add(item);
            WeakReferenceMessenger.Default.Send<CartItemAddedMessage>(new CartItemAddedMessage(item));
            //_messenger.Send(new CartItemAddedMessage(item));
        }
    }
}

