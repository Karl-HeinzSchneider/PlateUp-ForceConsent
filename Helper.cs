using Kitchen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlateUp_ForceConsent
{
    public static class Helper
    {
        private static string ModPrefix = "[ForceConsent] ";

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

        public static void LogInfo(string message)
        {
            Debug.Log(ModPrefix + message);
        }
        public static void LogWarning(string message)
        {
            Debug.LogWarning(ModPrefix + message);
        }
        public static void LogError(string message)
        {
            Debug.LogError(ModPrefix + message);
        }
    }
}
