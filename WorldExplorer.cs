using System;
using System.IO;

using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExpeditionsContent
{
    public class WorldExplorer : ModSystem
    {
        public static bool savedClerk = false;

        public override void OnWorldLoad()
        {
            if (Main.netMode == 2)
            {
                Console.WriteLine("Expeditions: World Initialising");
            }

            // Reset bools
            savedClerk = false;
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
    }
}