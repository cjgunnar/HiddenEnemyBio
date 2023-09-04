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
    }
}
