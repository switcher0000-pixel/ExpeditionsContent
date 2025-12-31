using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Moonstone
{
    public class LootBagMoonstone : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Moongem Box");
            // Tooltip.SetDefault("Right click to open");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.maxStack = 30;
            Item.rare = 2;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<Moonstone>(), Main.rand.Next(8, 13));
        }
    }
}
