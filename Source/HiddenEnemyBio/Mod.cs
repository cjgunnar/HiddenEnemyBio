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
            Log.Message("Hello World, it's cjgunnar");
            new Harmony("cjgunnar.HiddenEnemyBio").PatchAll();
        }
    }
}
