using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ACGearingUp : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Gearing Up";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Open a naturally spawned chest";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.HermesBoots, 1);
        }
        public override string Description(bool complete)
        {
            return "Valuable loot can be found in chests even on the surface, so keep an eye out while you explore. A little extra mobility goes a long way when you are gearing up for the dangers below. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until first boss is beaten, or is done already
            if (!expedition.completed && NPC.downedBoss1) return false;

            return API.FindExpedition<ABSmeltOres>(Mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                cond1 = ExpeditionsContent.PlayerExplorer.Get(player).openedNaturalChest;
            }
            return cond1;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            int rewardType = ItemID.HermesBoots;
            if (API.InInventory[ItemID.HermesBoots])
            {
                if (!API.InInventory[ItemID.ClimbingClaws])
                {
                    rewardType = ItemID.ClimbingClaws;
                }
                else if (!API.InInventory[ItemID.ShoeSpikes])
                {
                    rewardType = ItemID.ShoeSpikes;
                }
                else
                {
                    rewardType = ItemID.TigerClimbingGear;
                }
            }

            rewards.Clear();
            Item rewardItem = new Item();
            rewardItem.SetDefaults(rewardType);
            rewardItem.stack = 1;
            rewards.Add(rewardItem);
        }
    }
}
