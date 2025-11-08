using System;
using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using Soapcock.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;


namespace Soapcock
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
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
            //Declare Harmony Patches
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
        void Start()
        {
            instance = this;
            CustomCard.BuildCard<Fih>();
            CustomCard.BuildCard<ShotsOrShots>();
        }
    }
}
