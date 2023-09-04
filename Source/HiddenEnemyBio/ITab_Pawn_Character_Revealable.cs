using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using UnityEngine.UIElements;
using Verse;
using HarmonyLib;
using Verse.Sound;

namespace HiddenEnemyBio
{
    [StaticConstructorOnStartup]
    public class ITab_Pawn_Character_Revealable : ITab
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

        public ITab_Pawn_Character_Revealable()
        {
            labelKey = "TabCharacter";
            tutorTag = "Character";
        }

        protected override void UpdateSize()
        {
            base.UpdateSize();
            size = CharacterCardUtility.PawnCardSize(PawnToShowInfoAbout) + new Vector2(17f, 17f) * 2f;
        }

        protected override void FillTab()
        {
            if(HiddenBioUtil.ShouldBioVisible(PawnToShowInfoAbout))
            {
                UpdateSize();
                Vector2 vector = CharacterCardUtility.PawnCardSize(PawnToShowInfoAbout);
                CharacterCardUtility.DrawCharacterCard(new Rect(17f, 17f, vector.x, vector.y), PawnToShowInfoAbout);
            }
            else
            {
                UpdateSize();
                Vector2 vector = CharacterCardUtility.PawnCardSize(PawnToShowInfoAbout);
                CharacterIntelUtility.DrawCharacterCard(new Rect(17f, 17f, vector.x, vector.y), PawnToShowInfoAbout);
            }
            
        }
    }

}
