using RimWorld;
using Verse;
using HarmonyLib;

namespace HiddenEnemyBio
{
    [StaticConstructorOnStartup]
    public class Core
    {
        public static HiddenEnemyBio hiddenEnemyBio;
        static Core()
        {
            Log.Message("Hello World, it's cjgunnar");
            var harmony = new Harmony("cjgunnar.HiddenEnemyBio");
            harmony.PatchAll();
        }
    }
}
