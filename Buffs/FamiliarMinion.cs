using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Buffs
{
    class FamiliarMinion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            PlayerExplorer modPlayer = player.GetModPlayer<PlayerExplorer>();
            int minionCount = 0;
            minionCount += player.ownedProjectileCounts[ModContent.ProjectileType<Projs.Familiars.MinionFox>()];
            minionCount += player.ownedProjectileCounts[ModContent.ProjectileType<Projs.Familiars.MinionChicken>()];
            minionCount += player.ownedProjectileCounts[ModContent.ProjectileType<Projs.Familiars.MinionCat>()];
            if (minionCount > 0)
            {
                modPlayer.familiarMinion = true;
            }
            if (!modPlayer.familiarMinion)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}
