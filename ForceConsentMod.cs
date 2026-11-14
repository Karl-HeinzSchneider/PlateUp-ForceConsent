using System;
using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using KitchenData;
using KitchenMods;
using Platforms;
using UnityEngine;

namespace PlateUp_ForceConsent
{
    public sealed class ForceConsentMod : IModInitializer
    {
        public void PostActivate(Mod mod)
        {
            var harmony = new Harmony("plateup.forceconsent");
            try
            {
                harmony.PatchAll(typeof(ForceConsentMod).Assembly);
                Debug.Log("[ForceConsent] Installed vote patches");
            }
            catch (Exception exception)
            {
                harmony.UnpatchSelf();
                Debug.LogError($"[ForceConsent] Could not install vote patches: {exception}");
            }
        }

        public void PreInject() { }

        public void PostInject() { }

        internal static void AllowOneVote(ConsentElement consent)
        {
            consent.Mode = ConsentElement.ConsentMode.AnyRequired;
            consent.PartialFillSpeed = consent.FillSpeed;
        }
    }

    [HarmonyPatch(typeof(EndPracticeView), "UpdateData")]
    internal static class LeavePracticeVote
    {
        private static void Postfix(ConsentElement ___Consent)
        {
            if (Session.NetworkedPlayState.IsOwnGame() && ___Consent != null)
            {
                ForceConsentMod.AllowOneVote(___Consent);
            }
        }
    }

    [HarmonyPatch(typeof(GenericChoiceView), "HandleUpdate")]
    internal static class PopupVote
    {
        private static void Postfix(GenericChoiceView.ViewData __0, ConsentElement ___Consent,
            ref GenericChoiceDecision ___Choice)
        {
            if (!Session.NetworkedPlayState.IsOwnGame() || ___Consent == null ||
                ___Choice != GenericChoiceDecision.None)
            {
                return;
            }

            switch (__0.TextSet)
            {
                case PopupType.EnterPracticeMode:
                    ForceConsentMod.AllowOneVote(___Consent);
                    break;
                case PopupType.LoadPreviousSave:
                    foreach (var input in __0.Inputs)
                    {
                        if (Players.Main.Get(input.PlayerID).IsLocalUser && ___Consent.GetConsent(input.PlayerID))
                        {
                            ___Choice = GenericChoiceDecision.Accept;
                            break;
                        }
                    }
                    break;
            }
        }
    }
}
