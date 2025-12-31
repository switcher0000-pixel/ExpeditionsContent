using Terraria.GameContent;
﻿using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExpeditionsContent.Items.QuestItems
{
    public class Photo : ModItem
    {
        public const int brokenPrefix = 40;
        public const int ignorantPrefix = 30;
        public string npcName = "";
        public string npcMod = "";
        private Texture2D npcTexture = null;
        public Texture2D NpcTexture
        {
            get
            {
                if (npcTexture == null)
                {
                    // Don't try setting up if the photo is not normal
                    if (Item.prefix == 0)
                    {
                        try
                        {
                            if (Main.netMode != 2)
                            {
                                npcTexture = TextureAssets.Npc[Item.stack].Value;
                            }
                        }
                        catch
                        {
                            // NPC at this id doesn't exist
                            npcTexture = TextureAssets.MagicPixel.Value;
                        }
                    }
                    // Set photo info
                    SetNameAndMod();
                }

                // Give a magic pixel until Mod photo can be resolved
                if (Item.prefix == ignorantPrefix) return TextureAssets.MagicPixel.Value;

                return npcTexture;
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Photo Film");
            // Tooltip.SetDefault("Used in conjuction with a camera\n" + "<right> to clear the image");
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.rare = 0;
            Item.value = Item.buyPrice(0, 0, 0, 0);

            npcName = "";
            npcMod = "";
            npcTexture = null;
        }
        public override void SaveData(TagCompound tag)
        {
            tag["npcName"] = npcName;
            tag["npcMod"] = npcMod;
        }
        public override void LoadData(TagCompound tag)
        {
            npcName = tag.GetString("npcName");
            npcMod = tag.GetString("npcMod");
            npcTexture = null;

            // Set the photo to the item stack of this NPC
            // In case the Mod order shuffled around or something
            Mod loadMod = ModLoader.GetMod(npcMod);
            if (loadMod != null)
            {
                // Mod found!
                Item.prefix = ignorantPrefix; 
                //ignorant lmao, just so the other method can assign it
            }
            else
            {
                if (npcMod.Equals("VANILLA"))
                {
                    // Vanilla NPC so yeah
                    NPC npc = GenerateNPC();
                    Item.stack = npc.type;
                }
                // NPC is unloaded
                else
                {
                    Item.prefix = brokenPrefix;
                }
            }
        }

        /// <summary>
        /// Generate a default npc object
        /// </summary>
        /// <returns></returns>
        private NPC GenerateNPC()
        {
            try
            {
                NPC n = new NPC();
                n.SetDefaults(Item.stack);
                if (n.ModNPC != null) n.ModNPC.SetDefaults();
                if (n.townNPC)
                {
                    // Get the actual town NPC if they exist already
                    int whoAmI = NPC.FindFirstNPC(n.type);
                    if (whoAmI >= 0)
                    {
                        return Main.npc[whoAmI];
                    }
                }

                return n;
            }
            catch (Exception e)
            {
                // Main.NewText("Gen " + Item.stack + ": " + e.ToString());
            }
            return null;
        }
        /// <summary>
        /// Generate a
        /// </summary>
        public void SetNameAndMod()
        {
            if(Item.prefix == ignorantPrefix)
            {
                // This is a 1 stack item that got loaded via a Mod
                // So we have to find where it is and re-set it.
                Item.prefix = 0;
                Mod loadMod = ModLoader.GetMod(npcMod);
                Item.stack = ModContent.Find<ModNPC>(npcMod, npcName).Type;
            }

            // Get NPC from the stack
            NPC npc = GenerateNPC();

            // Early stop, if this NPC doesn't exist or photo is broken
            if (npc == null || Item.prefix == brokenPrefix)
            {
                Item.SetNameOverride("Photo"); //With 'Damaged' prefix
                Item.stack = 1;
                Item.prefix = brokenPrefix;
                return;
            }

            // Save the name of the NPC
            npcName = npc.TypeName;
            npcMod = "VANILLA";
            if (npc.ModNPC != null) // Non-vanilla
            {
                npcName = npc.ModNPC.GetType().Name; // Use mods
                npcMod = npc.ModNPC.Mod.Name;
            }

            // Set photo info
            if (npc.townNPC)
            { Item.SetNameOverride("Photo of " + npc.GivenName + ", no."); }
            else
            {
                if (npc.type == 1)
                {
                    // Slimes are just slimes
                    Item.SetNameOverride( "Photo of " + Lang.GetNPCName(NPCID.SlimeRibbonWhite) + ", no. (1)");
                }
                else
                {
                    Item.SetNameOverride("Photo of " + npc.TypeName + ", no.");
                    // If a more specialised name exists, use that instead.
                    if(npc.GivenName.Length >= npc.TypeName.Length)
                    {
                        Item.SetNameOverride("Photo of " + npc.GivenName + ", no.");
                    }
                }
            }

            // Set stack
            Item.maxStack = Item.stack;

            // Set photo rarity
            Item.rare = 0;
            if (npc.defense >= 16) Item.rare = 4;
            if (npc.boss || npc.townNPC)
            {
                Item.rare++;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if(npcName != "")
            {
                if (Item.prefix == brokenPrefix)
                {
                    tooltips.Clear();
                    if (npcMod != "")
                    {
                        tooltips.Add(new TooltipLine(Mod, "PhotoImageUnloaded", "The image is clouded beyond recognition"));
                        tooltips.Add(new TooltipLine(Mod, "PhotoImageMod", "Mod: " + npcMod));
                    }
                    else
                    {
                        tooltips.Add(new TooltipLine(Mod, "PhotoImageMissing", "The image is damaged beyond repair"));
                    }
                }
                else
                {
                    tooltips.RemoveAt(0);
                }
            }
        }

        #region Draw Photo
        // Going to use double size, since scale is half sized to fit NPC
        public const int viewPortWidth = 16 * 2;
        public const int viewPortHeight = 24 * 2;
        private Rectangle CalculateSourceRectangle()
        {
            // Make rectangle of first frame and get the centre point
            int frames = Main.npcFrameCount[Item.stack];
            Rectangle rect = new Rectangle(
                0, 0, NpcTexture.Width,
                NpcTexture.Height / frames);
            Point center = new Point(rect.Width / 2, rect.Height / 2);

            // Offset the frame by the width/height
            int width = Math.Min(viewPortWidth, rect.Width);
            int height = Math.Min(viewPortHeight, rect.Height);
            rect.Location = new Point(
                 center.X - width / 2,
                center.Y - height / 2);

            //Set size of source rectangle
            rect.Width = width;
            rect.Height = height;
            return rect;
        }

        // Photo Clearing, actually to prevent stack fiddling
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            Item.SetDefaults();
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<PhotoBlank>());
        }

        // Draw
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (NpcTexture == null) return;

            Rectangle rect = CalculateSourceRectangle();

            spriteBatch.Draw(
                NpcTexture,
                position + new Vector2(6 * scale, 2 * scale),
                rect,
                drawColor,
                0f,
                origin,
                scale / 2,
                SpriteEffects.None, 0);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (NpcTexture == null) return;

            Rectangle rect = CalculateSourceRectangle();

            spriteBatch.Draw(
                NpcTexture,
                Item.Center - Main.screenPosition,
                rect,
                lightColor,
                rotation,
                Item.Size / 2 + new Vector2(2, 6),
                scale / 2,
                SpriteEffects.None, 0);
        }
        #endregion
    }
}