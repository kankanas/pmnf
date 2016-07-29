using System.Linq;
using VidsNet.Models;
namespace VidsNet
{
    public static class Setup {

        public static bool UsersExist() {
            using(var db = new DatabaseContext()) {
                if(db.Users.Any()) {
                    return true;
                }
                return false;
            }
        }

        public static bool SettingsExist() {
            using(var db = new DatabaseContext()) {
                if(db.Settings.Any()) {
                    return true;
                }
                return false;
            }
        }

        public static bool IsSetup() {
            return (Setup.UsersExist() && Setup.SettingsExist());
        }

    }
}