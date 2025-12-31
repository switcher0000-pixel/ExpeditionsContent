using Terraria.Audio;
﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ExpeditionsContent.Items.Wayfarer
{
    public class WayfarerSubArm : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wayfarer's Subber");
            // Tooltip.SetDefault("50% chance not to consume ammo\n" + "'Spray and pray'");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Minishark);
            Item.width = 42;
            Item.height = 24;

            Item.UseSound = SoundID.Item42;
            Item.damage = 4;
            Item.knockBack = 0.5f;
            Item.useAnimation = 5;
            Item.useTime = 5;
            Item.shootSpeed += 2f;

            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            velocity.X += Main.rand.NextFloatDirection() * 2f;
            velocity.Y += Main.rand.NextFloatDirection() * 2f;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override void HoldItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                player.itemRotation += Main.rand.NextFloatDirection() * 0.13f;
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 3);
        }

        /// <summary>
        /// 50% chance to not consume ammo - matches the old "50% chance not to consume ammo" tooltip
        /// </summary>
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextBool(2); // 50% chance to consume (50% chance to save)
        }
    }
}