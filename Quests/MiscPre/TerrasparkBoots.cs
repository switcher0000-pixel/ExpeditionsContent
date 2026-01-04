using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class TerrasparkBoots : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "The Ultimate Footwear";
            SetNPCHead(NPCID.GoblinTinkerer);
            expedition.difficulty = 3;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Obtain Frostspark Boots";
            expedition.conditionDescription2 = "Obtain Lava Waders";
            expedition.conditionDescription3 = "Craft Terraspark Boots";
        }

        public override void AddItemsOnLoad()
        {
            // Reward for completing this epic journey
            AddRewardMoney(Item.buyPrice(0, 1, 0, 0)); // 1 gold
            AddRewardItem(ItemID.SwiftnessPotion, 5);
        }

        public override string Description(bool complete)
        {
            if (complete)
            {
                return "Excellent work! Those Terraspark Boots will serve you well through your adventures. With speed on ice, lava immunity, water walking, and flight time - you're unstoppable!";
            }

            return @"Looking to craft the ultimate pre-Hardmode boots? The Terraspark Boots combine mobility, speed, and elemental protection like no other accessory.

To craft them, you'll need to combine two powerful accessories at my Tinkerer's Workshop:

1. FROSTSPARK BOOTS - Combine these components:
   • Lightning Boots (Spectre Boots + Aglet + Anklet of the Wind)
     - Spectre Boots = Running Boots* + Rocket Boots
     - Aglet (surface chests)
     - Anklet of the Wind (Underground Jungle chests)
   • Ice Skates (Ice Chests in Underground Snow biome)

2. LAVA WADERS - Combine these components:
   • Obsidian Water Walking Boots (Water Walking Boots + Obsidian Skull)
     - Water Walking Boots (Water Chests/Ocean Crates)
     - Obsidian Skull (craft from 20 Obsidian)
   • Lava Charm (lava layer chests/Obsidian Crates)
   • Obsidian Rose (dropped by Fire Imps in Underworld)

*Running Boots = Hermes/Flurry/Sailfish/Dunerider Boots

This will be quite the journey, but the reward is worth it!";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Requires Goblin Tinkerer to be available and player has explored underground
            if (NPC.FindFirstNPC(NPCID.GoblinTinkerer) == -1) return false;

            // Player should have decent progression (at least 200 HP)
            if (player.statLifeMax < 200) return false;

            return true;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Condition 1: Has Frostspark Boots (in inventory or equipped)
            if (!cond1)
            {
                cond1 = API.InInventory[ItemID.FrostsparkBoots] || HasEquipped(player, ItemID.FrostsparkBoots);
            }

            // Condition 2: Has Lava Waders (in inventory or equipped)
            if (!cond2)
            {
                cond2 = API.InInventory[ItemID.LavaWaders] || HasEquipped(player, ItemID.LavaWaders);
            }

            // Condition 3: Has crafted Terraspark Boots
            if (!cond3 && cond1 && cond2)
            {
                cond3 = API.InInventory[ItemID.TerrasparkBoots] || HasEquipped(player, ItemID.TerrasparkBoots);
            }

            return cond1 && cond2 && cond3;
        }

        private bool HasEquipped(Player player, int itemType)
        {
            for (int i = 0; i < player.armor.Length; i++)
            {
                if (player.armor[i].type == itemType)
                    return true;
            }
            return false;
        }
    }
}
