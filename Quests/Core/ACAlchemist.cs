using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ACAlchemist : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "The Alchemist's Apprentice";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Craft any potion";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.Bottle, 3);
            AddRewardItem(ItemID.LesserHealingPotion, 5);
        }
        public override string Description(bool complete)
        {
            return "Potions are essential for survival in Terraria. To brew potions, you need to place a Bottle on a flat surface like a table or platform - this creates an alchemy station. Bottles can be crafted from glass at a furnace. Once you have an alchemy station, you can craft healing potions and buff potions from various materials found in the world. Try crafting your first potion!";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Hide if already in hardmode
            if (!expedition.completed && Main.hardMode) return false;

            return API.FindExpedition<ACFirstDefense>(Mod).completed;
        }
        
        public override void OnCraftItem(Item item, Recipe recipe, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!expedition.condition1Met)
            {
                // Check if this is any potion (healing or buff)
                if (item.consumable && item.buffType > 0)
                {
                    // It's a buff potion
                    expedition.condition1Met = true;
                }
                else if (item.healLife > 0 || item.healMana > 0)
                {
                    // It's a healing/mana potion
                    expedition.condition1Met = true;
                }
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}
