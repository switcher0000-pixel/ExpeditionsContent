using Terraria;
using Terraria.ModLoader;
using Expeditions;

namespace ExpeditionsContent.Conditions
{
    /// <summary>
    /// Custom shop conditions based on expedition quest completion
    /// </summary>
    public static class ExpeditionConditions
    {
        /// <summary>
        /// Condition that checks if a specific expedition quest has been completed
        /// </summary>
        public static Condition QuestCompleted<T>() where T : ModExpedition
        {
            return new Condition(
                "Mods.ExpeditionsContent.Conditions.QuestCompleted",
                () => {
                    var expedition = API.FindExpedition<T>(ModContent.GetInstance<ExpeditionC>());
                    return expedition != null && expedition.completed;
                }
            );
        }

        /// <summary>
        /// Condition that checks if the world is in hardmode
        /// </summary>
        public static Condition HardMode = new Condition(
            "Mods.Terraria.Conditions.HardMode",
            () => Main.hardMode
        );

        /// <summary>
        /// Condition that checks if the first three bosses have been defeated
        /// (Eye of Cthulhu, Eater of Worlds/Brain of Cthulhu, and Skeletron)
        /// </summary>
        public static Condition ThreeBossesDefeated = new Condition(
            "Mods.ExpeditionsContent.Conditions.ThreeBosses",
            () => NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3
        );

        /// <summary>
        /// Condition that checks if the world is a Corruption world (not Crimson)
        /// </summary>
        public static Condition CorruptionWorld = new Condition(
            "Mods.ExpeditionsContent.Conditions.CorruptionWorld",
            () => !WorldGen.crimson
        );

        /// <summary>
        /// Condition that checks if the world is a Crimson world (not Corruption)
        /// </summary>
        public static Condition CrimsonWorld = new Condition(
            "Mods.ExpeditionsContent.Conditions.CrimsonWorld",
            () => WorldGen.crimson
        );

        /// <summary>
        /// Creates a condition that checks if ANY of two quests have been completed
        /// </summary>
        public static Condition AnyQuestCompleted<T1, T2>()
            where T1 : ModExpedition
            where T2 : ModExpedition
        {
            return new Condition(
                "Mods.ExpeditionsContent.Conditions.AnyQuestCompleted",
                () => {
                    var quest1 = QuestCompleted<T1>();
                    var quest2 = QuestCompleted<T2>();
                    return quest1.IsMet() || quest2.IsMet();
                }
            );
        }

        /// <summary>
        /// Creates a condition that checks if a quest is completed AND three bosses are defeated
        /// </summary>
        public static Condition QuestAndThreeBosses<T>() where T : ModExpedition
        {
            return new Condition(
                "Mods.ExpeditionsContent.Conditions.QuestAndThreeBosses",
                () => QuestCompleted<T>().IsMet() && ThreeBossesDefeated.IsMet()
            );
        }

        /// <summary>
        /// Creates a condition that checks if a quest is completed AND world is corruption
        /// </summary>
        public static Condition QuestAndCorruption<T>() where T : ModExpedition
        {
            return new Condition(
                "Mods.ExpeditionsContent.Conditions.QuestAndCorruption",
                () => QuestCompleted<T>().IsMet() && CorruptionWorld.IsMet()
            );
        }

        /// <summary>
        /// Creates a condition that checks if a quest is completed AND world is crimson
        /// </summary>
        public static Condition QuestAndCrimson<T>() where T : ModExpedition
        {
            return new Condition(
                "Mods.ExpeditionsContent.Conditions.QuestAndCrimson",
                () => QuestCompleted<T>().IsMet() && CrimsonWorld.IsMet()
            );
        }

        /// <summary>
        /// Creates a condition that checks if ANY of two quests are completed AND three bosses defeated
        /// </summary>
        public static Condition AnyQuestAndThreeBosses<T1, T2>()
            where T1 : ModExpedition
            where T2 : ModExpedition
        {
            return new Condition(
                "Mods.ExpeditionsContent.Conditions.AnyQuestAndThreeBosses",
                () => {
                    var quest1 = QuestCompleted<T1>();
                    var quest2 = QuestCompleted<T2>();
                    return (quest1.IsMet() || quest2.IsMet()) && ThreeBossesDefeated.IsMet();
                }
            );
        }
    }
}
