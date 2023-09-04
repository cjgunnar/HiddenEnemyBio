using RimWorld;
using Verse;
using HarmonyLib;

namespace HiddenEnemyBio
{
    [StaticConstructorOnStartup]
    public class Mod
    {
        static Mod()
        {
            new Harmony("cjgunnar.HiddenEnemyBio").PatchAll();
        }
    }
}
