using System;
using System.Reflection;

using Kitchen;
using KitchenMods;

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Collections;

using Kitchen.Modules;

namespace PlateUp_ForceConsent
{
    public class ForceLoad : GenericSystemBase, IModSystem
    {
        int LocalID = 0;  

        protected override void Initialise()
        {
            base.Initialise();
            //Debug.Log("ForceLoad");
            Helper.LogInfo("ForceLoad Initialise()");
            RequireSingletonForUpdate<SFranchiseMarker>();             
        }

        protected override void OnUpdate()
        {
            if(this.LocalID == 0)
            {
                this.LocalID = Helper.GetLocalPlayerID();
            }

            ConsentElement[] consentElements = GameObject.FindObjectsOfType<ConsentElement>();

            if(consentElements.Length < 2 )
            {
                //Debug.Log("consentElements.Length < 2");
                //Helper.LogInfo("consentElements.Length < 2");

                return;
            }

            ConsentElement elem = consentElements[1];

            if (elem.GetConsent(this.LocalID))
            {
                elem.SetAllConsents(true);
            }
        }
    }
}
