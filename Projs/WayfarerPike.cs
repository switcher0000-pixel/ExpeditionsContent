using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Projs
{
    class WayfarerPike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pike");
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spear);
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.scale = 1.1f;
        }

        public const float extend = 0.9f;
        public const float retract = 1.1f;
        public override void AI()
        {
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 4f;
                Projectile.netUpdate = true;
            }
            if (Main.player[Projectile.owner].itemAnimation < Main.player[Projectile.owner].itemAnimationMax / 3)
            {
                Projectile.ai[0] -= retract;
            }
            else
            {
                Projectile.ai[0] += extend;
            }
        }

    }
}
