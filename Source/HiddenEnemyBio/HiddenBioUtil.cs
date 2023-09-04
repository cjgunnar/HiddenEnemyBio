using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace HiddenEnemyBio
{
    internal class HiddenBioUtil
    {
        public const float defaultBioResistance = 10;
        public const float revealSkillsResistance = 10;
        public const float revealTraitsResisitance = 15;
        public const float revealBackstoryResistance = 18;
        public const float revealPassionSkillsResistance = 18;

        public static bool ShouldDefaultBioVisible(Pawn pawn)
        {
            if (pawn == null || pawn?.story == null) return false;

            if (PawnUtility.EverBeenColonistOrTameAnimal(pawn)) return true;

            // unwaveringly loyal give all information because... what else to do?
            if (pawn.IsPrisoner && !pawn.guest.Recruitable) return true;

            // prisoners with low resistance use default bio
            if (pawn.IsPrisoner && pawn.guest.resistance <= 5) return true;

            // enemies have hidden information
            if (pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile) return false;
            return true;
        }

        public static bool ShouldRevealBackstory(Pawn pawn)
        {
            if (pawn.IsPrisoner && pawn.guest.resistance > revealBackstoryResistance) return false;
            else if (!pawn.IsPrisoner && pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile) return false;
            return true;
        }

        public static bool ShouldRevealTrait(Pawn pawn, Trait trait)
        {
            if(trait.Suppressed || trait.sourceGene != null) return true;
            else if (pawn.IsPrisoner && pawn.guest.resistance > revealTraitsResisitance) return false;
            else if (!pawn.IsPrisoner && pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile) return false;
            return true;
        }

        public static bool ShouldRevealIncapable(Pawn pawn)
        {
            return ShouldRevealBackstory(pawn);
        }

        public static bool ShouldRevealPassionSkills(Pawn pawn)
        {
            if (pawn.IsPrisoner && pawn.guest.resistance > revealPassionSkillsResistance) return false;
            else if (!pawn.IsPrisoner && pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile) return false;
            return true;
        }

        public static bool ShouldRevealSkills(Pawn pawn)
        {
            if (pawn.IsPrisoner && pawn.guest.resistance > revealSkillsResistance) return false;
            else if (!pawn.IsPrisoner && pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile) return false;
            return true;
        }
    }
}
