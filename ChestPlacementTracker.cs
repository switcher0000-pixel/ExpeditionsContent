using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent
{
    public class ChestPlacementTracker : GlobalTile
    {
        public override void PlaceInWorld(int i, int j, int type, Item item)
        {
            if (type != TileID.Containers && type != TileID.Containers2) return;

            Point topLeft = WorldExplorer.GetChestTopLeft(i, j);
            WorldExplorer.MarkChestPlaced(topLeft);
        }
    }
}
