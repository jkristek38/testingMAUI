using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testMAUI.Models
{
    /*public class CartItemAddedMessage
    {
        private readonly Game _game;
        public Game Game
        {
            get { return _game; }
        }

        public CartItemAddedMessage(Game g)
        {
            _game = g;
        }
    }*/
    public record CartItemAddedMessage(Game g)
    {
        public Game Game => g;
    }
    public record InventoryItemAddedMessage(Game g)
    {
        public Game Game => g;
    }
    public class CartItemsRequestMessage : RequestMessage<IEnumerable<Game>> { }
    public class InventoryItemsRequestMessage : RequestMessage<IEnumerable<Game>> { }

    public class TestItemsRequestMessage : RequestMessage<IEnumerable<TestItem>> { }
    public record TestItemAddedMessage(TestItem i) { public TestItem i =i; }
    public record TestItemRemovedMessage(TestItem i) { public TestItem i = i; }
    public record TestItemsRemovedMessage();
}
