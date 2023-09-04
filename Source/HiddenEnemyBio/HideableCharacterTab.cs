using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using UnityEngine.UIElements;
using Verse;

namespace HiddenEnemyBio
{
    public class HiddenEnemyBio : ITab
    {
        private Pawn PawnToShowInfoAbout
        {
            get
            {
                Pawn pawn = null;
                if (base.SelPawn != null)
                {
                    pawn = base.SelPawn;
                }
                else if (base.SelThing is Corpse corpse)
                {
                    pawn = corpse.InnerPawn;
                }
                if (pawn == null)
                {
                    Log.Error("Character tab found no selected pawn to display.");
                    return null;
                }
                return pawn;
            }
        }

        public override bool IsVisible => PawnToShowInfoAbout.story != null;
        

        public HiddenEnemyBio()
        {
            labelKey = "HBT.HiddenEnemyBio";
        }

        protected override void UpdateSize()
        {
            base.UpdateSize();
            size = CharacterCardUtility.PawnCardSize(PawnToShowInfoAbout) + new Vector2(17f, 17f) * 2f;
        }

        protected override void FillTab()
        {
            if (PawnToShowInfoAbout.IsFreeColonist || PawnToShowInfoAbout.IsColonist || (PawnToShowInfoAbout.IsPrisonerOfColony && PawnToShowInfoAbout.guest.resistance < 5f))
            {
                UpdateSize();
                Vector2 vector = CharacterCardUtility.PawnCardSize(PawnToShowInfoAbout);
                CharacterCardUtility.DrawCharacterCard(new Rect(17f, 17f, vector.x, vector.y), PawnToShowInfoAbout);
            }
            else
            {
                if (PawnToShowInfoAbout == null)
                    return;

                var rect = new Rect(10, 30, size.x - 20, size.y - 30);

                var listing = new Listing_Standard();

                bool oldEnabled = GUI.enabled;
                GUI.enabled = true;
                listing.Begin(rect);

                listing.Label("You don't know anything about this character.");
                listing.GapLine();

                listing.End();
                GUI.enabled = oldEnabled;
            }
        }
    }
}
