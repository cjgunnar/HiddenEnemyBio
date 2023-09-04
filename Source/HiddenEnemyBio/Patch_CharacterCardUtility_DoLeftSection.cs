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

namespace HiddenEnemyBio
{
    [HarmonyPatch(typeof(CharacterCardUtility), "DoLeftSection")]
    public class Patch_CharacterCardUtility_DoLeftSection
    {
        static bool Prefix(Rect rect, Rect leftRect, Pawn pawn)
        {
            bool visible = HiddenBioUtil.ShouldBioVisible(pawn);

            if(visible)
            {
                return true;
            }
            else
            {
                GUI.Label(leftRect, "This character's background is unknown.");
                return false;
            }
        }
    }
}
