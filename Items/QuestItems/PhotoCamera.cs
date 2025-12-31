using Terraria.Audio;
﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    public class PhotoCamera : ModItem
    {
        public const int frameWidth = 180;
        public const int frameHeight = 120;
        public const float maxFreeCapture = 350; // Max capture distance not relying on light
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("PhotoTron");
            // Tooltip.SetDefault("Takes photos of creatures\n" + "'Say cheese!'");
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.useAmmo = ModContent.ItemType<PhotoBlank>();
            Item.UseSound = SoundID.Camera;

            Item.useStyle = 4;
            Item.useAnimation = 40;
            Item.useTime = 40;

            Item.rare = 2;
            Item.value = Item.buyPrice(0, 3, 0, 0);
        }

        // Flashing effect
        public override void HoldItem(Player player)
        {
            if (player.itemAnimation > 0)
            {
                float brightness = (float)player.itemAnimation / Item.useAnimation;
                Lighting.AddLight(player.Top,
                    brightness * 1f,
                    brightness * 0.9f,
                    brightness * 0.8f);
            }
        }

        // This works because UI layer;
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            PhotoCamera.DrawCameraFrame(spriteBatch, Item, frameWidth, frameHeight);
        }

        public override bool? UseItem(Player player)
        {
            Lighting.AddLight(player.Top,
                1f,
                0.9f,
                0.8f);
            return PhotoCamera.TakePhoto(player, Item, frameWidth, frameHeight, maxFreeCapture);
        }

        #region Static Methods

        public static Rectangle GetCameraFrame(int width, int height)
        {
            return new Rectangle(
                Main.mouseX + (int)Main.screenPosition.X - frameWidth / 2,
                Main.mouseY + (int)Main.screenPosition.Y - frameHeight / 2,
                frameWidth,
                frameHeight);
        }

        public static void DrawCameraFrame(SpriteBatch spriteBatch, Item item, int width, int height)
        {
            DrawCameraFrame(spriteBatch, item, GetCameraFrame(width, height));
        }
        public static void DrawCameraFrame(SpriteBatch spriteBatch, Item item, Rectangle r)
        {
            if (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem] == item)
            {
                // Draw camera frame
                spriteBatch.Draw(
                ExpeditionC.CameraFrameTexture,
                r.Location.ToVector2() - Main.screenPosition,
                Color.White
                );
            }
        }

        public static bool TakePhoto(Player player, Item item, int width, int height, float range)
        {
            return TakePhoto(player, item, GetCameraFrame(width, height), range);
        }
        public static bool TakePhoto(Player player, Item item, Rectangle cameraFrame, float range)
        {
            if (player.whoAmI != Main.myPlayer) return true;

            bool canTakePicture = false;

            NPC npc = Main.npc[0];
            float distance = 1000f;
            foreach (NPC n in Main.npc)
            {
                if (!n.active) continue;
                if (cameraFrame.Intersects(n.Hitbox))
                {
                    // Can't take pictures if too dark
                    Point centre = n.Center.ToTileCoordinates();
                    Color lightColor = Lighting.GetColor(centre.X, centre.Y);
                    int darkness = 255 - (lightColor.R + lightColor.G + lightColor.B) / 3;
                    if (darkness > 240)
                    {
                        // too dark, if player can see below that range
                        if (n.Distance(player.Center) > range ||
                            !Collision.CanHit(npc.position, npc.width, npc.height,
                            player.position, player.width, player.height))
                        {
                            continue;
                        }
                    }

                    // Get the closest
                    float dist = n.Distance(cameraFrame.Center.ToVector2());
                    if (dist < distance)
                    {
                        distance = dist;
                        npc = n;
                        canTakePicture = true;
                    }
                }
            }

            if (!canTakePicture) return true;

            // Check for camera roll
            foreach (Item i in player.inventory)
            {
                if (i.ammo == item.useAmmo)
                {
                    i.stack--;
                    canTakePicture = true;
                    break;
                }
            }
            if (!canTakePicture) return true;

            // Spawn the item
            int number = Item.NewItem(player.GetSource_ItemUse(item), (int)player.position.X, (int)player.position.Y, player.width, player.height, ExpeditionC.ItemIDPhoto, npc.type, false, -1, false, false);

            // Send the item
            if (Main.netMode == 1)
            {
                NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
            }
            return true;
        }

        #endregion
    }
}
