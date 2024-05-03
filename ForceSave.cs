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
    public class ForceSave : GenericSystemBase, IModSystem
    {
        int LocalID = 0;
        EntityQuery PopupQuery;
        protected override void Initialise()
        {
            base.Initialise();
            Debug.Log("ForceSave");
            RequireSingletonForUpdate<SIsNightTime>();

            //this.PopupQuery = base.GetEntityQuery(new ComponentType[]
            //{
            //      typeof(QuitToLobbyPopup)
            //});
        }

        protected override void OnUpdate()
        {
            if (this.LocalID == 0)
            {
                this.LocalID = Helper.GetLocalPlayerID();
            }
        }
    }
}
