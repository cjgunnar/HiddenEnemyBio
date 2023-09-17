using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace HiddenEnemyBio
{
    internal class Patch_GetBackstory
    {
        static MethodInfo Pawn_StoryTracker_GetBackstory = AccessTools.Method(typeof(Pawn_StoryTracker), nameof(Pawn_StoryTracker.GetBackstory));

        public static IEnumerable<CodeInstruction> DrawCharacterCardMethod_Patch(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Callvirt && (MethodInfo)codes[i].operand == Pawn_StoryTracker_GetBackstory)
                {
                    // Log.Message(codes[i].ToString());
                    codes[i].operand = typeof(Patch_GetBackstory).GetMethod("GetBackstory");
                    
                    // Log.Message(codes[i - 2].ToString());
                    codes[i - 2].opcode = OpCodes.Nop;
                    codes[i - 2].operand = null;
                    break;
                }
            }
            return codes;
        }

        public static BackstoryDef GetBackstory(Pawn pawn, BackstorySlot slot)
        {
            if (!HiddenBioUtil.ShouldRevealBackstory(pawn))
                if (pawn.story.GetBackstory(slot) != null)
                {
                    BackstoryDef unknownBackstory = DefDatabase<BackstoryDef>.GetNamed("HEB.UnknownBackstory");
                    return unknownBackstory;
                }
                    

            return pawn.story.GetBackstory(slot);
        }

    }
}
