using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace HiddenEnemyBio
{
    internal class Settings : ModSettings
    {
        public static float defaultBioResistance = 10;
        public static float revealSkillsResistance = 10;
        public static float revealTraitsResisitance = 15;
        public static float revealBackstoryResistance = 18;
        public static float revealPassionSkillsResistance = 18;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref defaultBioResistance, "defaultBioResistance", 10);
            Scribe_Values.Look(ref revealSkillsResistance, "revealSkillsResistance", 10);
            Scribe_Values.Look(ref revealTraitsResisitance, "revealTraitsResistance", 15);
            Scribe_Values.Look(ref revealBackstoryResistance, "revealBackstoryResistance", 18);
            Scribe_Values.Look(ref revealPassionSkillsResistance, "revealPassionSkillsResistance", 18);
            base.ExposeData();
        }
    }
}
