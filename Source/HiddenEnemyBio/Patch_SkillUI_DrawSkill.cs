using HarmonyLib;
using RimWorld;
using static RimWorld.SkillUI;
using UnityEngine;
using Verse;

namespace HiddenEnemyBio
{
    [HarmonyPatch(typeof(SkillUI), nameof(DrawSkill), new System.Type[4] {typeof(SkillRecord), typeof(Rect), typeof(SkillDrawMode), typeof(string)})]
    internal class Patch_SkillUI_DrawSkill
    {
        [HarmonyPrefix]
        public static bool Prefix(SkillRecord skill, Rect holdingRect, SkillDrawMode mode, string tooltipPrefix = "")
        {
            Pawn p = skill.Pawn;
            if (p == null) return true;
            if (HiddenBioUtil.ShouldDefaultBioVisible(p)) return true;
            else if (HiddenBioUtil.ShouldRevealSkills(p)) return true;
            else if (HiddenBioUtil.ShouldRevealPassionSkills(p) && mode != SkillDrawMode.Menu)
            {
                if (skill.passion > 0) return true;
                DrawSkillEmpty(skill, holdingRect, 60f);
                return false;
            }

            return true;
        }

        static void DrawSkillEmpty(SkillRecord skill, Rect holdingRect, float ___levelLabelWidth)
        {
            Widgets.BeginGroup(holdingRect);
            Text.Anchor = TextAnchor.MiddleLeft;
            Rect rect = new Rect(6f, 0f, ___levelLabelWidth + 6f, holdingRect.height);
            Widgets.Label(rect, skill.def.skillLabel.CapitalizeFirst());
            GenUI.ResetLabelAlign();
            Widgets.EndGroup();
        }
    }
}
