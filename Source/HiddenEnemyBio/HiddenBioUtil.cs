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
        public static bool ShouldDefaultBioVisible(Pawn pawn)
        {
            if (pawn == null || pawn?.story == null) return true;

            if (PawnUtility.EverBeenColonistOrTameAnimal(pawn)) return true;

            if (pawn.guest.IsSlave) return true;

            // unwaveringly loyal never gives information
            if (pawn.IsPrisoner && !pawn.guest.Recruitable) return false;

            // prisoners below threshold
            if (pawn.IsPrisoner && pawn.guest.resistance <= Settings.useVanillaBioResistance) return true;

            // high resistance prisoners
            if (pawn.IsPrisoner) return false;

            // enemies have hidden information
            if (pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile) return false;
            return true;
        }

        public static bool ShouldRevealBackstory(Pawn pawn)
        {
            if (ShouldDefaultBioVisible(pawn)) return true;

            if (pawn.IsPrisoner && !pawn.guest.Recruitable) return false;
            if (pawn.IsPrisoner && pawn.guest.resistance > Settings.revealBackstoryResistance) return false;
            else if (!pawn.IsPrisoner && pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile) return false;
            return true;
        }

        public static bool ShouldRevealTrait(Pawn pawn, Trait trait)
        {
            if (ShouldDefaultBioVisible(pawn)) return true;

            if (trait.Suppressed || trait.sourceGene != null) return true;
            if (pawn.IsPrisoner && !pawn.guest.Recruitable) return false;
            else if (pawn.IsPrisoner && pawn.guest.resistance > Settings.revealTraitsResisitance) return false;
            else if (!pawn.IsPrisoner && pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile) return false;
            return true;
        }

        public static bool ShouldRevealThought(Thought thought)
        {
            Pawn pawn = thought.pawn;
            if (ShouldDefaultBioVisible(thought.pawn)) return true;

            // unwavering don't tell
            if (pawn.IsPrisoner && !pawn.guest.Recruitable) return false;
            
            // if traits are revealed, show thoughts
            if (pawn.IsPrisoner && pawn.guest.resistance <= Settings.revealTraitsResisitance) return true;

            // show thoughts not caused by traits
            if (thought.def.requiredTraits.NullOrEmpty()) return true;

            // show thoughts caused by genetic traits
            if(thought.def.requiredTraits.Any((traitDef) => pawn.genes.GenesListForReading?.Any((Gene gene) => gene.def.forcedTraits.Any((GeneticTraitData gtd) => gtd?.def == traitDef)) ?? false))
                return true;

            // hide other thoughts
            return false;
        }

        public static bool ShouldRevealIncapable(Pawn pawn)
        {
            return ShouldRevealBackstory(pawn);
        }

        public static bool ShouldRevealPassionSkills(Pawn pawn)
        {
            if (ShouldDefaultBioVisible(pawn)) return true;

            if (pawn.IsPrisoner && !pawn.guest.Recruitable) return false;
            if (pawn.IsPrisoner && pawn.guest.resistance > Settings.revealPassionSkillsResistance) return false;
            else if (!pawn.IsPrisoner && pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile) return false;
            return true;
        }

        public static bool ShouldRevealSkills(Pawn pawn)
        {
            if (ShouldDefaultBioVisible(pawn)) return true;

            if (pawn.IsPrisoner && !pawn.guest.Recruitable) return false;
            if (pawn.IsPrisoner && pawn.guest.resistance > Settings.revealSkillsResistance) return false;
            else if (!pawn.IsPrisoner && pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile) return false;
            return true;
        }
    }
}
