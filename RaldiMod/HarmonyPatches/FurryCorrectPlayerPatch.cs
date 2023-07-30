using HarmonyLib;

namespace RaldiMod.HarmonyPatches
{
    [HarmonyPatch(typeof(PrincipalScript))]
    [HarmonyPatch("CorrectPlayer")]
    public class FurryCorrectPlayerPatch
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            return !RaldiMod.FurryDisabled;
        }
    }
}