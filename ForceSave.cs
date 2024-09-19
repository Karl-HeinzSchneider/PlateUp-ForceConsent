using System;
using System.Reflection;

using Kitchen;
using KitchenData;
using KitchenMods;

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Collections;

using Kitchen.Modules;

namespace PlateUp_ForceConsent
{
    public class ForceSave : GenericSystemBase, IModSystem
    {
        int LocalID = 0;
        EntityQuery PopupQuery;
        //EntityQuery SaveRequests;
   
        protected override void Initialise()
        {
            base.Initialise();
            Debug.Log("ForceSave");
            RequireSingletonForUpdate<SIsNightTime>();

            //this.PopupQuery = base.GetEntityQuery(new ComponentType[]
            //{
            //      typeof(QuitToLobbyPopup)
            //});
            //this.SaveRequests = this.GetEntityQuery((ComponentType)typeof(CRequestSave));
        }

        protected override void OnUpdate()
        {
            //Helper.LogInfo("OnUpdate");              
          

            //return;
            //if (this.LocalID == 0)
            //{
            //    this.LocalID = Helper.GetLocalPlayerID();
            //}

            //bool hasMatches = !this.SaveRequests.IsEmpty;
            //Helper.LogInfo("hasMatches"+hasMatches);

            //if (!hasMatches) {
            //    return; 
            // }

            //ConsentElement[] consentElements = GameObject.FindObjectsOfType<ConsentElement>();

            //if (consentElements.Length < 2)
            //{
            //    //Debug.Log("consentElements.Length < 2");
            //    Helper.LogInfo("consentElements.Length < 2");

            //    return;
            //}

            //ConsentElement elem = consentElements[1];

            //if (elem.GetConsent(this.LocalID))
            //{
            //    //elem.SetAllConsents(true);
            //    Helper.LogInfo("setAllConsents");
            //}
        }
    }
}
