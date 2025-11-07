using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using LetsGoGambling.Monobehaviours; // Make sure this namespace matches your project

// This is the custom MonoBehaviour that will handle the logic per-player
// You should place this in its own file inside a "Monobehaviours" folder
// (e.g., "LetsGoGambling/Monobehaviours/JumpySawsEffect.cs")
namespace LetsGoGambling.Monobehaviours
{
    public class JumpySawsEffect : MonoBehaviour
    {
        private Player player;
        private CharacterData characterData;
        private SpawnSaw spawnSaw; // The component from the vanilla "Saw" card
        private float chanceToSpawn = 0.33f; // 33% chance
        private bool debugMode = false; // Set to true for console logs

        public void Awake()
        {
            // Get all the components we need
            this.player = GetComponent<Player>();
            this.characterData = player.data;
            this.spawnSaw = GetComponent<SpawnSaw>(); // Get the saw spawner

            // Add listener to the player's jump action
            characterData.jumpAction += OnJump;

            if (debugMode) { UnityEngine.Debug.Log($"[{LetsGoGambling.Cards.RiskyLeap.ModInitials}][MonoBehaviour] JumpySawsEffect added to player {player.playerID}."); }
        }

        private void OnJump()
        {
            // Roll the dice
            if (UnityEngine.Random.value < chanceToSpawn)
            {
                // Check if the SpawnSaw component is present (it should be)
                if (this.spawnSaw != null)
                {
                    // Success! Spawn a saw.
                    this.spawnSaw.Spawn();
                    if (debugMode) { UnityEngine.Debug.Log($"[{LetsGoGambling.Cards.RiskyLeap.ModInitials}][MonoBehaviour] OnJump: SUCCESS. Spawning saw."); }
                }
            }
            else
            {
                if (debugMode) { UnityEngine.Debug.Log($"[{LetsGoGambling.Cards.RiskyLeap.ModInitials}][MonoBehaviour] OnJump: FAILED. No saw."); }
            }
        }

        public void OnDestroy()
        {
            // This is called when the component is destroyed (e.g., when the card is removed)
            // We must remove our listener
            characterData.jumpAction -= OnJump;

            if (debugMode) { UnityEngine.Debug.Log($"[{LetsGoGambling.Cards.RiskyLeap.ModInitials}][MonoBehaviour] JumpySawsEffect removed from player {player.playerID}."); }
        }
    }
}

// This is the card itself
// (e.g., "LetsGoGambling/Cards/RiskyLeap.cs")
namespace LetsGoGambling.Cards
{
    class RiskyLeap : CustomCard
    {
        // Using a static string for the Mod Initials
        public static string ModInitials = "LGG";
        private bool debugMode = false; // Set to true for console logs

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            if (debugMode) { UnityEngine.Debug.Log($"[{ModInitials}][Card] {GetTitle()} has been setup."); }
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Add the vanilla SpawnSaw component. This component knows how to create
            // and manage orbiting saws. It MUST be added before our custom effect.
            var sawSpawner = player.gameObject.AddComponent<SpawnSaw>();
            // You might need to manually set the saw prefab here if it doesn't
            // load automatically, but often it does.

            // Add our custom MonoBehaviour to the player
            // This component will listen for jumps
            player.gameObject.AddComponent<LetsGoGambling.Monobehaviours.JumpySawsEffect>();

            if (debugMode) { UnityEngine.Debug.Log($"[{ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}."); }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Find and destroy our custom MonoBehaviour
            var jumpEffect = player.gameObject.GetComponent<LetsGoGambling.Monobehaviours.JumpySawsEffect>();
            if (jumpEffect != null)
            {
                Destroy(jumpEffect);
            }

            // Find and destroy the SpawnSaw component
            // This will also clean up all saws spawned by it
            var sawSpawner = player.gameObject.GetComponent<SpawnSaw>();
            if (sawSpawner != null)
            {
                Destroy(sawSpawner);
            }

            if (debugMode) { UnityEngine.Debug.Log($"[{ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}."); }
        }


        protected override string GetTitle()
        {
            return "Risky Leap";
        }
        protected override string GetDescription()
        {
            return "Feeling lucky, punk?\n\n<color=red>Jumping</color> has a <color=red>RANDOM</color> chance to spawn an <color=red>ORBITING SAW</color>.";
        }
        protected override GameObject GetCardArt()
        {
            return null; // You can add your custom art here
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common; // Feel free to change this
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Saws on Jump",
                    amount = "Maybe?",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed; // Sticking with the theme
        }
        public override string GetModName()
        {
            return ModInitials; // Or "LetsGoGambling"
        }
    }
}