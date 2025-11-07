using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using LetsGoGambling.Monobehaviours; // Make sure to create this namespace/folder

// This is the custom MonoBehaviour that will handle the logic per-player
// You should place this in its own file inside a "Monobehaviours" folder
// (e.g., "LetsGoGambling/Monobehaviours/GamblingCardEffect.cs")
namespace LetsGoGambling.Monobehaviours
{
    public class GamblingCardEffect : MonoBehaviour
    {
        private Player player;
        private Gun gun;
        private GunAmmo gunAmmo;
        private int originalMaxAmmo;
        private bool debugMode = false; // Set to true for console logs

        public void Awake()
        {
            // Get all the components we need
            this.player = GetComponent<Player>();
            this.gun = player.data.weapon.gun;
            this.gunAmmo = gun.ammo;

            // Store the original max ammo so we can reset it later
            // and use it as a base for our random calculations
            this.originalMaxAmmo = gunAmmo.maxAmmo;

            // Add listeners to the gun's actions
            gun.ShootPojectileAction += OnShoot;
            gun.reloadGunAction += OnReload;

            if (debugMode) { UnityEngine.Debug.Log($"[{LetsGoGambling.Cards.ErraticFirepower.ModInitials}][MonoBehaviour] GamblingCardEffect added to player {player.playerID}. Original max ammo: {originalMaxAmmo}"); }
        }

        private void OnShoot(Gun g)
        {
            // Set a random projectile speed multiplier
            // Range is 0.5f (50%) to 2.5f (250%)
            float randomMultiplier = UnityEngine.Random.Range(0.5f, 2.5f);
            gun.projectileSpeedMultiplier = randomMultiplier;

            if (debugMode) { UnityEngine.Debug.Log($"[{LetsGoGambling.Cards.ErraticFirepower.ModInitials}][MonoBehaviour] OnShoot: ProjSpeedMult set to {randomMultiplier}"); }
        }

        private void OnReload(Gun g)
        {
            // Set a random max ammo
            // Range is half original to double original
            // (Add +1 to Random.Range for int to make the upper bound inclusive)
            int newMaxAmmo = UnityEngine.Random.Range(originalMaxAmmo / 2, originalMaxAmmo * 2 + 1);

            // Ensure the player always gets at least 1 ammo
            if (newMaxAmmo <= 0)
            {
                newMaxAmmo = 1;
            }

            gunAmmo.maxAmmo = newMaxAmmo;

            if (debugMode) { UnityEngine.Debug.Log($"[{LetsGoGambling.Cards.ErraticFirepower.ModInitials}][MonoBehaviour] OnReload: New MaxAmmo set to {newMaxAmmo}"); }
        }

        public void OnDestroy()
        {
            // This is called when the component is destroyed (e.g., when the card is removed)
            // We must remove our listeners and reset the stats to normal
            gun.ShootPojectileAction -= OnShoot;
            gun.reloadGunAction -= OnReload;

            // Reset stats
            gun.projectileSpeedMultiplier = 1f;
            gunAmmo.maxAmmo = originalMaxAmmo;

            if (debugMode) { UnityEngine.Debug.Log($"[{LetsGoGambling.Cards.ErraticFirepower.ModInitials}][MonoBehaviour] GamblingCardEffect removed from player {player.playerID}. Stats reset."); }
        }
    }
}

// This is the card itself
// (e.g., "LetsGoGambling/Cards/ErraticFirepower.cs")
namespace LetsGoGambling.Cards
{
    class ErraticFirepower : CustomCard
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
            // Add our custom MonoBehaviour to the player
            // This component will handle all the logic
            player.gameObject.AddComponent<LetsGoGambling.Monobehaviours.GamblingCardEffect>();

            if (debugMode) { UnityEngine.Debug.Log($"[{ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}."); }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Find and destroy our custom MonoBehaviour to clean up
            var effect = player.gameObject.GetComponent<LetsGoGambling.Monobehaviours.GamblingCardEffect>();
            if (effect != null)
            {
                Destroy(effect);
            }

            if (debugMode) { UnityEngine.Debug.Log($"[{ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}."); }
        }


        protected override string GetTitle()
        {
            return "Erratic Firepower";
        }
        protected override string GetDescription()
        {
            return "What could possibly go wrong?\n\n<color=red>Every shot</color> has <color=red>RANDOM</color> projectile speed.\n<color=red>Every reload</color> gives a <color=red>RANDOM</color> amount of ammo.";
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
        S
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false, // Or true? It's a gamble!
                    stat = "Projectile Speed",
                    amount = "???",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false, // Or true?
                    stat = "Ammo",
                    amount = "???",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed; // Seems fitting
        }
        public override string GetModName()
        {
            return ModInitials; // Or "LetsGoGambling"
        }
    }
}