using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RimWorld.SkillUI;
using UnityEngine;
using Verse;
using HarmonyLib;
using RimWorld.BaseGen;

namespace HiddenEnemyBio
{
    [HarmonyPatch(typeof(SkillUI), "DrawSkillsOf")]
    internal class Patch_SkillUI_DrawSkillsOf
    {
        static bool Prefix(Pawn p, Vector2 offset, SkillDrawMode mode, Rect container, float ___levelLabelWidth, List<SkillDef> ___skillDefsInListOrderCached)
        {
            if (HiddenBioUtil.ShouldDefaultBioVisible(p)) return true;
            else if (HiddenBioUtil.ShouldRevealSkills(p)) return true;
            else if (HiddenBioUtil.ShouldRevealPassionSkills(p)) {
                DrawPassionSkillsOf(p, offset, mode, container, ___levelLabelWidth, ___skillDefsInListOrderCached);
                return false;
            }
            else return true;
        }

        static void DrawPassionSkillsOf(Pawn p, Vector2 offset, SkillDrawMode mode, Rect container, float ___levelLabelWidth, List<SkillDef> ___skillDefsInListOrderCached)
        {
            Text.Font = GameFont.Small;
            if (p.DevelopmentalStage.Baby())
            {
                Color color = GUI.color;
                GUI.color = Color.gray;
                TextAnchor anchor = Text.Anchor;
                Text.Anchor = TextAnchor.MiddleCenter;
                Widgets.Label(new Rect(offset.x, offset.y, 230f, container.height), "SkillsDevelopLaterBaby".Translate());
                GUI.color = color;
                Text.Anchor = anchor;
                return;
            }
            List<SkillDef> allDefsListForReading = DefDatabase<SkillDef>.AllDefsListForReading;
            for (int i = 0; i < allDefsListForReading.Count; i++)
            {
                float x = Text.CalcSize(allDefsListForReading[i].skillLabel.CapitalizeFirst()).x;
                if (x > ___levelLabelWidth)
                {
                    ___levelLabelWidth = x;
                }
            }
            bool hadPassion = false;
            for (int j = 0; j < ___skillDefsInListOrderCached.Count; j++)
            {
                SkillDef skillDef = ___skillDefsInListOrderCached[j];
                float y = (float)j * 27f + offset.y;
                if (p.skills.GetSkill(skillDef).passion > 0)
                {
                    DrawSkill(p.skills.GetSkill(skillDef), new Vector2(offset.x, y), mode);
                    hadPassion = true;
                }
            }
            if(!hadPassion)
            {
                Widgets.Label(new Rect(offset.x, offset.y, 230f, container.height), "HEB.NoPassions".Translate(p.Named("PAWN")));
            }
        }
    }
}
