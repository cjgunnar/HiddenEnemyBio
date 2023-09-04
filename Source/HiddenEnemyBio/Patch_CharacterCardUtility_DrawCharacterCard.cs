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
    [HarmonyPatch(typeof(ITab_Pawn_Character), "FillTab")]
    public class Patch_ITab_Pawn_Character_FillTab
    {
        static bool Prefix()
        {
            Pawn PawnToShowInfoAbout = ReversePatch_ITab_Pawn_Character_UpdateSize.get_PawnToShowInfoAbout();

            bool visible = HiddenBioUtil.ShouldBioVisible(PawnToShowInfoAbout);

            if (visible)
            {
                return true;
            }
            else
            {
                ReversePatch_ITab_Pawn_Character_UpdateSize.UpdateSize();
                Vector2 vector = CharacterCardUtility.PawnCardSize(PawnToShowInfoAbout);
                CharacterCardUtility.DrawCharacterCard(new Rect(17f, 17f, vector.x, vector.y), PawnToShowInfoAbout);
                return false;
            }
        }
    }

    [HarmonyPatch]
    public static class ReversePatch_ITab_Pawn_Character_UpdateSize
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(ITab_Pawn_Character), "get_PawnToShowInfoAbout")]
        public static Pawn get_PawnToShowInfoAbout()
        {
            throw new NotImplementedException("stub");
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(ITab_Pawn_Character), "UpdateSize")]
        public static void UpdateSize()
        {
            throw new NotImplementedException("stub");
        }
    }
}
