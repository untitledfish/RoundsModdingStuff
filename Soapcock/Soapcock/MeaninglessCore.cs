using System;
using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using Soapcock.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;


namespace Soapcock
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class MeaninglessExistance : BaseUnityPlugin
    {
        private const string ModId = "com.bobblebutt.rounds.MeaninglessExistance";
        private const string ModName = "MeaninglessExistance";
        public const string Version = "0.1.0";
        public const string ModInitials = "DIH";
        public static MeaninglessExistance instance { get; private set; }


        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
        void Start()
        {
            instance = this;
            CustomCard.BuildCard<MyCardName>();
        }
    }
}
