using System;
using System.Reflection;

using Kitchen;
using KitchenMods;

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Kitchen.Modules;

namespace PlateUp_ForceConsent
{
    public class ForceStartPracticeMode : GenericSystemBase, IModSystem
    {
        EntityQuery PopupQuery;
        protected override void Initialise() {
            base.Initialise();
            //RequireSingletonForUpdate<SKitchenMarker>();
            RequireSingletonForUpdate<SIsNightTime>();    
            Helper.LogInfo("ForcePractice Initialise()");

            this.PopupQuery = base.GetEntityQuery(new ComponentType[]
            {
                typeof(StartPracticePopup.CRequest)
            });
        }

        protected override void OnUpdate()
        {
            bool hasMatches = !this.PopupQuery.IsEmpty;

            if (!hasMatches)
            {
                return;
            }    
            
            Helper.LogInfo("FORCE: StartPracticeMode");

            ConsentElement[] consentElements = GameObject.FindObjectsOfType<ConsentElement>();                     

            if (consentElements.Length < 2)
            {
                //Debug.Log("consentElements.Length < 2");
                //Helper.LogInfo("consentElements.Length < 2");

                return;
            }

            //int counter  = 0;

            //foreach(var consentElement in consentElements)
            //{
            //    //consentElement.ClearConsents();
            //    //Helper.LogInfo("foreach"+counter+" "+consentElement.Mode);
            //    //counter++;
            //}

            ConsentElement elem = consentElements[1];
            //elem.SetAllConsents(true);
         
            ConsentElement elemTwo = consentElements[2];
            elemTwo.Mode = ConsentElement.ConsentMode.AnyRequired;
            //elemTwo.ClearConsents();
            elemTwo.SetAllConsents(true);            
        }
    }

    public class ForceLeavePracticeMode : LeavePracticeMode, IModSystem
    {
        protected override void Initialise()
        {
            base.Initialise();
            Helper.LogInfo("ForceLeavePractice Initialise()");
        }

        protected override void OnUpdate()
        {
            if (!base.Has<SPracticeMode>())
            {
                return;
            }

            EndPracticeView view = GameObject.FindObjectOfType<EndPracticeView>();

            if (!view)
            {
                return;
            }

            int[] consents = GetConsentArray(view);

            if (consents.Length > 0)
            {           
                Helper.LogInfo("FORCE: LeavePracticeMode");

                var leave = GetOrDefault<SLeavePracticeView>();
                leave.Ready = true;
                Set(leave);
            }
        }

        private int[] GetConsentArray(EndPracticeView view)
        {
            Type type = typeof(EndPracticeView);
            FieldInfo field = type.GetField("Consents", BindingFlags.NonPublic | BindingFlags.Instance);

            HashSet<int> consents = (HashSet<int>)field.GetValue(view);

            int[] consentsArrray = consents.ToArray();
            return consentsArrray;
        }
    }
}
