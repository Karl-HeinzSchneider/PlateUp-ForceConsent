using Kitchen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateUp_ForceConsent
{
    public static class Helper
    {

        public static int GetLocalPlayerID()
        {
            int LocalID = 0;

            foreach (PlayerInfo info in Players.Main.All())
            {
                if (info.IsLocalUser)
                {
                    //this.LogObject(info);
                    LocalID = info.ID;
                    break;
                }
            }

            return LocalID;
        }
    }
}
