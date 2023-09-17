using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using HarmonyLib;
using static RimWorld.SkillUI;

namespace HiddenEnemyBio
{
    [HarmonyPatch(typeof(SkillUI), nameof(DrawSkillsOf))]
    internal class Patch_SkillUI_DrawSkillsOf
    {
        static bool Prefix(Pawn p, Vector2 offset, SkillDrawMode mode, Rect container, float ___levelLabelWidth, List<SkillDef> ___skillDefsInListOrderCached)
        {
            if (HiddenBioUtil.ShouldDefaultBioVisible(p)) return true;
            else if (HiddenBioUtil.ShouldRevealSkills(p)) return true;
            else if (HiddenBioUtil.ShouldRevealPassionSkills(p) && mode != SkillDrawMode.Menu) {
                bool hasPassion = ___skillDefsInListOrderCached.Any((SkillDef skillDef) => p.skills.GetSkill(skillDef).passion > 0);
                if(!hasPassion)
                {
                    DrawNoPassionLabel(p, offset, container);
                    return false;
                }

                // draw normally and hit the override in DrawSkill
                return true;
            }
            else
            { 
                DrawUnknownSkillsLabel(p, offset, container);
                return false;
            }
        }

        static void DrawNoPassionLabel(Pawn p, Vector2 offset, Rect container)
        {
            Widgets.Label(new Rect(offset.x, offset.y, 230f, container.height), "HEB.NoPassions".Translate(p.Named("PAWN")));
        }

        static void DrawUnknownSkillsLabel(Pawn p, Vector2 offset, Rect container)
        {
            Widgets.Label(new Rect(offset.x, offset.y, 230f, container.height), "HEB.UnknownSkillsPassions".Translate(p.Named("PAWN")));
        }
    }
}
