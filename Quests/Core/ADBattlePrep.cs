using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ADBattlePrep : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Battle Preparation";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 1; // Important flag - highlighted quest
            expedition.ctgImportant = true;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Use 3 different buff potions simultaneously";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.IronskinPotion, 3);
            AddRewardItem(ItemID.RegenerationPotion, 3);
            AddRewardItem(ItemID.SwiftnessPotion, 3);
        }
        public override string Description(bool complete)
        {
            return "Before facing dangerous enemies, experienced adventurers prepare themselves with buff potions. Unlike healing potions which you drink when hurt, buff potions give you temporary advantages like increased defense, faster movement, or health regeneration. The key is that multiple buffs stack - you can use several different potions at once! Try using 3 different buff potions at the same time to see how powerful preparation can be.";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until Eye of Cthulhu is beaten
            if (!expedition.completed && NPC.downedBoss1) return false;

            // Requires completing The Alchemist's Apprentice
            return API.FindExpedition<ACAlchemist>(Mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                // Count active buffs (excluding debuffs)
                int buffCount = 0;
                for (int i = 0; i < Player.MaxBuffs; i++)
                {
                    if (player.buffType[i] > 0 && !Main.debuff[player.buffType[i]])
                    {
                        buffCount++;
                    }
                }
                
                cond1 = buffCount >= 3;
            }
            return cond1;
        }
    }
}
