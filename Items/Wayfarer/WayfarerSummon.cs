using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ExpeditionsContent.Items.Wayfarer
{
    /// <summary>
    /// Summons 3 types of familiars:
    /// Fox guards the player's space
    /// Chicken chases enemies normally
    /// Cat attacks away from the player
    /// </summary>
    public class WayfarerSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wayfarer's Bell");
            // Tooltip.SetDefault("Summons a familiar to fight for you");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.HornetStaff);
            Item.UseSound = SoundID.Item25;
            
            Item.damage = 11;
            Item.knockBack = 3f;
            Item.shoot = ModContent.ProjectileType<Projs.Familiars.MinionFox>();

            // Create buff that manages the modPlayer's minion bool
            Item.buffType = ModContent.BuffType<Buffs.FamiliarMinion>();

            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 2;

            ItemID.Sets.StaffMinionSlotsRequired[Item.type] = 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            player.AddBuff(Item.buffType, 3600, true);

            int foxes = player.ownedProjectileCounts[ModContent.ProjectileType<Projs.Familiars.MinionFox>()];
            int chickens = player.ownedProjectileCounts[ModContent.ProjectileType<Projs.Familiars.MinionChicken>()];
            int cats = player.ownedProjectileCounts[ModContent.ProjectileType<Projs.Familiars.MinionCat>()];
            if (foxes > chickens)
            {
                type = ModContent.ProjectileType<Projs.Familiars.MinionChicken>();
            }
            else if (chickens > cats)
            {
                type = ModContent.ProjectileType<Projs.Familiars.MinionCat>();
            }
            position = Main.MouseWorld - new Vector2(12, 10);
            velocity = Vector2.Zero;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2();
        }
    }
}