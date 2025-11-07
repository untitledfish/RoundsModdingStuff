using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;


namespace Soapcock.Cards
{
    class ShotsOrShots : CustomCard
    {
        private bool debugMode = false;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            if (debugMode) { UnityEngine.Debug.Log($"[{MeaninglessExistance.ModInitials}][Card] {GetTitle()} has been setup."); }
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
            if (debugMode) { UnityEngine.Debug.Log($"[{MeaninglessExistance.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}."); }

            if (UnityEngine.Random.value < 0.50f)
            {
                gun.numberOfProjectiles = UnityEngine.Random.Range(1, 11); // 1-10 bullets
                UnityEngine.Debug.Log($"Bullet Ct. is now {gun.numberOfProjectiles}");
            }
            else
            {
                gun.ammo = UnityEngine.Random.Range(1, 101); // 1-100 bullets
                UnityEngine.Debug.Log($"Ammo is now {gun.ammo}");
            }

            gun.projectileSpeed *= UnityEngine.Random.Range(0.1f, 100.0f);
            UnityEngine.Debug.Log($"Bullet Speed is now {gun.projectileSpeed}");


        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
            if (debugMode) { UnityEngine.Debug.Log($"[{MeaninglessExistance.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}."); }
            gun.numberOfProjectiles = 1;
            gun.ammo = 3;
            gun.projectileSpeed = 1;

        }


        protected override string GetTitle()
        {
            return "Shots or Shots";
        }
        protected override string GetDescription()
        {
            return "Get a random amount of Projectiles or Ammo, then get a random amount of bullet speed";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Projectiles",
                    amount = "Ambiguous",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "Ambiguous",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet Speed",
                    amount = "Ambiguous",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
        public override string GetModName()
        {
            return "MeaninglessExistance";
        }
    }
}