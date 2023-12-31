﻿using RimWorld;
using Verse;
using HarmonyLib;
using UnityEngine;
using System;
using System.Reflection;

namespace HiddenEnemyBio
{
    [StaticConstructorOnStartup]
    public class HiddenEnemyBioMod : Mod
    {
        public HiddenEnemyBioMod(ModContentPack content) : base(content)
        {
            Harmony harmony = new Harmony("cjgunnar.HiddenEnemyBio");
            harmony.PatchAll();

            var assemblyClass = AccessTools.TypeByName("CharacterCardUtility").GetNestedType("<>c__DisplayClass42_0", BindingFlags.NonPublic);
            var assemblyMethod = assemblyClass.GetMethod("<DoLeftSection>b__3", BindingFlags.NonPublic | BindingFlags.Instance);
            harmony.Patch(assemblyMethod, transpiler: new HarmonyMethod(typeof(Patch_GetBackstory).GetMethod("DrawCharacterCardMethod_Patch")));
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            // listingStandard.CheckboxLabeled("exampleBoolExplanation", ref settings.exampleBool, "exampleBoolToolTip");

            listingStandard.Label("HEB.SettingsVanillaBio".Translate() + ": " + Math.Round(Settings.useVanillaBioResistance, 2));
            Settings.useVanillaBioResistance = listingStandard.Slider(Settings.useVanillaBioResistance, 0f, 40f);
            
            listingStandard.Label("HEB.SettingsShowSkills".Translate() + ": " + Math.Round(Settings.revealSkillsResistance, 2));
            Settings.revealSkillsResistance = listingStandard.Slider(Settings.revealSkillsResistance, 0f, 40f);
            
            listingStandard.Label("HEB.SettingsShowTraits".Translate() + ": " + Math.Round(Settings.revealTraitsResisitance, 2));
            Settings.revealTraitsResisitance = listingStandard.Slider(Settings.revealTraitsResisitance, 0f, 40f);
            
            listingStandard.Label("HEB.SettingsShowBackstory".Translate() + ": " + Math.Round(Settings.revealBackstoryResistance, 2));
            Settings.revealBackstoryResistance = listingStandard.Slider(Settings.revealBackstoryResistance, 0f, 40f);
            
            listingStandard.Label("HEB.SettingsShowPassions".Translate() + ": " + Math.Round(Settings.revealPassionSkillsResistance, 2));
            Settings.revealPassionSkillsResistance = listingStandard.Slider(Settings.revealPassionSkillsResistance, 0f, 40f);
            
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "HEB.HiddenEnemyBio".Translate();
        }

    }
}
