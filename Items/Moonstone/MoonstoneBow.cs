using Terraria.GameContent;
using ReLogic.Content;
﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ExpeditionsContent.Items.Moonstone
{
    public class MoonstoneBow : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yutu Moonshot");
            // Tooltip.SetDefault("Wooden arrows turn into yutu arrows\n" + "'Defying gravitational expectations'");
            if (Main.netMode != 2)
            {
                Asset<Texture2D>[] glowMasks = new Asset<Texture2D>[TextureAssets.GlowMask.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i];
                }
                glowMasks[glowMasks.Length - 1] = ModContent.Request<Texture2D>("ExpeditionsContent/Glow/" + this.GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask = glowMasks;
            }
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodenBow);
            Item.width = 30;
            Item.height = 30;

            Item.damage = 26;
            Item.knockBack += 2f;
            Item.shootSpeed = 8.5f;
            Item.useAnimation = 20;
            Item.useTime = 20;

            Item.glowMask = customGlowMask; // See Autoload
            Item.rare = 3;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Type);
            recipe.AddIngredient(ModContent.ItemType<Moonstone>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6f, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = ModContent.ProjectileType<Projs.MoonstoneArrow>();
            Projectile.NewProjectile(source, position, velocity, type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}
