/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;


namespace Soapcock.Cards
{
    class ErraticFirepower : CustomCard
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
            gun.objectsToSpawn = gun.objectsToSpawn.Append(new Gun.ObjectToSpawn()
            {
                AddToProjectile = (proj) =>
                {
                    if (UnityEngine.Random.value < 0.1f) // 10% chance
                    {
                        proj.damage *= 2f;
                    }
                }
            }).ToArray();
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
            if (debugMode) { UnityEngine.Debug.Log($"[{MeaninglessExistance.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}."); }
            gun.ammo = 3;
            gun.projectileSpeed = 1;
        }

        private void OnShoot(Gun gun)
        {
            float randomMultiplier = UnityEngine.Random.Range(0.5f, 2.5f);
            gun.projectileSpeed *= randomMultiplier;
        }

        protected override string GetTitle()
        {
            return "Erratic Firepower";
        }
        protected override string GetDescription()
        {
            return "What could possibly go wrong?\\n\\n<color=red>Every shot</color> has <color=red>RANDOM</color> projectile speed.\\n<color=red>Every reload</color> gives a <color=red>RANDOM</color> amount of ammo.\"";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Projectile Speed",
                    amount = "???",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "???",
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
*/