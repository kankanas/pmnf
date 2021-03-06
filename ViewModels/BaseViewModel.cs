using VidsNet.Classes;
using System.Linq;
using VidsNet.DataModels;

namespace VidsNet.ViewModels
{
    public abstract class BaseViewModel {
        public virtual bool LoggedIn { get{ return true; } }
        public int UserId {get;}
        public abstract string ActiveMenuItem {get;}
        public string CurrentUrl {get;}
        public string SessionHash {get;}

        public string PageTitle
        {
            get
            {
                var item = MenuItems.Items.Where(x => x.Name == ActiveMenuItem).FirstOrDefault();
                if(item is MenuItem) {
                    return item.Name;
                }
                return "";
            }
        }

        public MenuItems MenuItems {get;}

        public BaseViewModel(UserData userData) {
            
            if(userData != null) {
                CurrentUrl = userData.CurrentUrl;
                UserId = userData.Id;
                SessionHash = userData.SessionHash;
                MenuItems = new MenuItems(userData.GetSystemMessageCount());
            }
            else {
                CurrentUrl = "/";
                UserId = 0;
                MenuItems = new MenuItems(0);
            }
        }
    }
}