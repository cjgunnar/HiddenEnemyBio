using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;

namespace HiddenEnemyBio
{
    [HarmonyPatch(typeof(Trait), "get_" + nameof(Trait.LabelCap))]
    class Patch_Trait_LabelCap
    {
        [HarmonyPostfix]
        public static void Postfix(Trait __instance, ref string __result)
        {
            if (!HiddenBioUtil.ShouldRevealTrait(__instance.pawn, __instance))
            {
                __result = "HEB.UnknownTrait".Translate().CapitalizeFirst();
            }
        }
    }

    [HarmonyPatch(typeof(Trait), "get_" + nameof(Trait.Label))]
    class Patch_Trait_Label
    {
        [HarmonyPostfix]
        public static void Postfix(Trait __instance, ref string __result)
        {
            if (!HiddenBioUtil.ShouldRevealTrait(__instance.pawn, __instance))
            {
                __result = "HEB.UnknownTrait".Translate().CapitalizeFirst();
            }
        }
    }

    [HarmonyPatch(typeof(Trait), nameof(Trait.TipString))]
    class Patch_Trait_TipString
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn pawn, Trait __instance, ref string __result)
        {
            if (!HiddenBioUtil.ShouldRevealTrait(pawn, __instance))
            {
                __result = "HEB.UnknownTrait".Translate().CapitalizeFirst();
            }
        }
    }

    [HarmonyPatch(typeof(Trait), nameof(Trait.ToString))]
    class Patch_Trait_ToString
    {
        [HarmonyPostfix]
        public static void Postfix(Trait __instance, ref string __result)
        {
            if (!HiddenBioUtil.ShouldRevealTrait(__instance.pawn, __instance))
            {
                __result = "HEB.UnknownTrait".Translate().CapitalizeFirst();
            }
        }
    }
}
