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
    internal class ForceStartPracticeMode : GenericSystemBase, IModSystem
    {
        EntityQuery PopupQuery;
        protected override void Initialise() {
            base.Initialise();
            //RequireSingletonForUpdate<SKitchenMarker>();
            RequireSingletonForUpdate<SIsNightTime>();     

            this.PopupQuery = base.GetEntityQuery(new ComponentType[]
            {
                typeof(StartPracticePopup.CRequest)
            });
        }

        protected override void OnUpdate()
        {
            bool hasMatches = !this.PopupQuery.IsEmpty;

            if (hasMatches)
            {
                Debug.Log("FORCE: StartPracticeMode");

                ConsentElement[] consentElement = GameObject.FindObjectsOfType<ConsentElement>();

                if (consentElement.Length > 1)
                {
                    consentElement[1].SetAllConsents(true);
                }                             
            }
        }
    }

    public class ForceLeavePracticeMode : LeavePracticeMode, IModSystem
    {
        protected override void Initialise()
        {
            base.Initialise();               
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
                Debug.Log("FORCE: LeavePracticeMode");

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
