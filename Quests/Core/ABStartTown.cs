using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ABStartTown : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Starting A Town";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Attract someone to the town (the Guide does not count)";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.CopperHammer, 5);
        }
        public override string Description(bool complete)
        {
            return "For people to move into our town, they will need a home. A room needs walls, a door, chair, table, and a light source. You can craft all of these at a workbench. Once you have a suitable house, wait some time for the NPC to move in. \n\nTips:\n 1. Toggle 'Smart Cursor' (Ctrl on keyboard, right stick on controller) to auto-place walls\n2. Click the House icon near Equipment, and use the ? to check housing suitability ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until first boss is beaten, or is done already
            if (!expedition.completed && NPC.downedBoss1) return false;

            return API.FindExpedition<AAWelcomeQuest>(Mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Check if an NPC has a house every second, NOT INCLUDING GUIDE
            if (!cond1 && Main.time % 60 == 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].active || Main.npc[i].type == NPCID.OldMan) continue;
                    if (Main.npc[i].type == NPCID.Guide) continue;
                    if (Main.npc[i].townNPC && !Main.npc[i].homeless) cond1 = true;
                }
            }
            return cond1;
        }
    }
}
