using System;
using System.IO;
using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;

namespace ExpeditionsContent
{
    public class WorldExplorer : ModSystem
    {
        public static bool savedClerk = false;
        private static HashSet<Point> placedChests = new HashSet<Point>();

        public override void OnWorldLoad()
        {
            if (Main.netMode == 2)
            {
                Console.WriteLine("Expeditions: World Initialising");
            }

            // Reset bools
            savedClerk = false;
            placedChests.Clear();
        }

        #region SaveLoad overrides

        public override void SaveWorldData(TagCompound tag)
        {
            tag["savedClerk"] = savedClerk;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            savedClerk = tag.GetBool("savedClerk");
        }

        #endregion

        public override void PostDrawFullscreenMap(ref string mouseText)
        {
            ModMapController.DrawFullscreenMap(ref mouseText);
        }

        public static void MarkChestPlaced(Point topLeft)
        {
            placedChests.Add(topLeft);
        }

        public static bool IsPlayerPlacedChest(Point topLeft)
        {
            return placedChests.Contains(topLeft);
        }

        public static Point GetChestTopLeft(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int left = i;
            int top = j;
            if (tile != null)
            {
                left = i - (tile.TileFrameX / 18) % 2;
                top = j - (tile.TileFrameY / 18) % 2;
            }
            return new Point(left, top);
        }
    }
}
