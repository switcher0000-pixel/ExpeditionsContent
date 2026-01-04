using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ACFirstDefense : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "First Defense";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Craft a metal helmet";
            expedition.conditionDescription2 = "Craft a metal chestplate";
            expedition.conditionDescription3 = "Craft metal greaves";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.LesserHealingPotion, 3);
            AddRewardItem(ItemID.RecallPotion, 2);
        }
        public override string Description(bool complete)
        {
            return "Now that you can smelt bars, it's time to protect yourself. Metal armor provides defense, reducing damage from enemy attacks. Craft a full set of armor (helmet, chestplate, and greaves) from any metal tier. Wearing a complete set also grants a set bonus! Check your defense stat in your inventory to see your protection level.";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Hide if already in hardmode
            if (!expedition.completed && Main.hardMode) return false;

            return API.FindExpedition<ABSmeltOres>(Mod).completed;
        }
        
        public override void OnCraftItem(Item item, Recipe recipe, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            int type = item.type;
            
            // Check for helmets
            if (!expedition.condition1Met)
            {
                if (type == ItemID.CopperHelmet || type == ItemID.TinHelmet ||
                    type == ItemID.IronHelmet || type == ItemID.LeadHelmet ||
                    type == ItemID.SilverHelmet || type == ItemID.TungstenHelmet ||
                    type == ItemID.GoldHelmet || type == ItemID.PlatinumHelmet)
                {
                    expedition.condition1Met = true;
                }
            }
            
            // Check for chestplates
            if (!expedition.condition2Met)
            {
                if (type == ItemID.CopperChainmail || type == ItemID.TinChainmail ||
                    type == ItemID.IronChainmail || type == ItemID.LeadChainmail ||
                    type == ItemID.SilverChainmail || type == ItemID.TungstenChainmail ||
                    type == ItemID.GoldChainmail || type == ItemID.PlatinumChainmail)
                {
                    expedition.condition2Met = true;
                }
            }
            
            // Check for greaves
            if (!expedition.condition3Met)
            {
                if (type == ItemID.CopperGreaves || type == ItemID.TinGreaves ||
                    type == ItemID.IronGreaves || type == ItemID.LeadGreaves ||
                    type == ItemID.SilverGreaves || type == ItemID.TungstenGreaves ||
                    type == ItemID.GoldGreaves || type == ItemID.PlatinumGreaves)
                {
                    expedition.condition3Met = true;
                }
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1 && cond2 && cond3;
        }
    }
}
