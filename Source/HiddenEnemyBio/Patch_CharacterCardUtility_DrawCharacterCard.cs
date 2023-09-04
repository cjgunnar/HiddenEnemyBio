using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using UnityEngine.UIElements;
using Verse;
using HarmonyLib;
using Verse.Sound;

namespace HiddenEnemyBio
{
    [HarmonyPatch(typeof(CharacterCardUtility), "DrawCharacterCard")]
    public static class Patch_CharacterCardUtility_DrawCharacterCard
    {
        static bool Prefix(Rect rect, Pawn pawn, Action randomizeCallback, Rect creationRect, bool showName)
        {
            if(HiddenBioUtil.ShouldBioVisible(pawn))
            {
                return true;
            }
            else
            {
                CharacterIntelUtility.DrawCharacterCard(rect, pawn, randomizeCallback, creationRect, showName);
                return false;
            }
        }
    }
}
