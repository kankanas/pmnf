using System;
using System.Collections;
using System.Collections.Generic;
using VidsNet.DataModels;
using VidsNet.Enums;

namespace VidsNet.Classes
{
    public class MenuItems : IEnumerator,IEnumerable
    {
        
        public List<MenuItem> Items {get; private set;}
        private int position = -1;

        object IEnumerator.Current
        {
            get
            {
                return Items[position];
            }
        }

        public MenuItems() {
            Items = new List<MenuItem>();
            Items.Add(new MenuItem(){ Name = "Virtual view", Url = "/", Html = "<i class=\"glyphicon glyphicon-facetime-video iconSmall\"></i>" });
            Items.Add(new MenuItem(){ Name = "Physical view", Url = "/physical", Html = "<i class=\"glyphicon glyphicon-th-list iconSmall\"></i>" });
            Items.Add(new MenuItem(){ Name = "Settings", Url = "/account/settings", Html = "<i class=\"glyphicon glyphicon-wrench iconSmall\"></i>" });
            Items.Add(new MenuItem(){ Name = "System Messages", Url = "/systemmmessages", 
                Html = "<i class=\"glyphicon glyphicon-envelope iconSmall\"></i></button>" });
            Items.Add(new MenuItem(){ Name = "Logout", Url = "/account/logout", Html = "<i class=\"glyphicon glyphicon-log-out iconSmall\"></i>" });
        }

        bool IEnumerator.MoveNext()
        {
            position++;
            return (position < Items.Count);
        }

        void IEnumerator.Reset()
        {
            position = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }
    } 
}