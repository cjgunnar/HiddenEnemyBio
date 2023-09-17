using HarmonyLib;
using RimWorld;

namespace HiddenEnemyBio
{
    [HarmonyPatch(typeof(Thought), "get_" + nameof(Thought.VisibleInNeedsTab))]
    class Patch_Thought_VisibleInNeedsTab
    {
        [HarmonyPostfix]
        public static void Postfix(Thought __instance, ref bool __result)
        {
            __result = HiddenBioUtil.ShouldRevealThought(__instance);
        }
    }
}
