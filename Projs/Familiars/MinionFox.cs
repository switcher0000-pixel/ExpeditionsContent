using Terraria.GameContent;
﻿using Terraria;
using Terraria.ID;

namespace ExpeditionsContent.Projs.Familiars
{
    class MinionFox : FamiliarMinion
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Familiar Fox");
            Main.projFrames[Projectile.type] = 13;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            AIPrioritiseNearPlayer = true;
            AIPrioritiseFarEnemies = false;

            // What does the fox say? "pls don't reference null instances"
            if (Main.netMode == 2) return;

            DrawOriginOffsetY = (TextureAssets.Projectile[Projectile.type].Value.Width - Projectile.width) / 2;
            DrawOffsetX = (TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]) - Projectile.height - 4;
        }
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 22;

            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.netImportant = true;
        }
    }
}
