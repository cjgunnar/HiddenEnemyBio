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
        public static bool ShouldBioVisible(Pawn pawn)
        {
            if (pawn == null || pawn?.story == null) return false;

            if (pawn.IsColonist) return true;

            if (pawn.IsPrisonerOfColony && pawn.guest.resistance < 5f) return true;

            return false;
        }

        public static bool ShouldRevealBackstory(Pawn pawn)
        {
            if(!ShouldBioVisible(pawn)) return false;

            if (pawn.IsPrisonerOfColony)
            {
                if (pawn.guest.resistance < 18) return true;
                else return false;
            }

            return false;
        }

        public static bool ShouldRevealTraits(Pawn pawn)
        {
            if (!ShouldBioVisible(pawn)) return false;

            if (pawn.IsPrisonerOfColony)
            {
                if (pawn.guest.resistance < 15) return true;
                else return false;
            }

            return false;
        }

        public static bool ShouldRevealIncapable(Pawn pawn)
        {
            return ShouldRevealBackstory(pawn);
        }


    }
}
