using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelpfulNPCs.Items
{
    public class FishCrate : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Crate");
            Tooltip.SetDefault("Right click for current quest fish");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.rare = ItemRarityID.White;
            Item.maxStack = 999;
            Item.value = 0;
        }

        public override bool CanRightClick()
        {
            return true;
        }


        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Main.anglerQuestItemNetIDs[Main.anglerQuest]), Main.anglerQuestItemNetIDs[Main.anglerQuest]);
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Terraria/Images/Item_" + Main.anglerQuestItemNetIDs[Main.anglerQuest]).Value;
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y - 10, (int)(texture.Width), (int)(texture.Height)), drawColor);

            return true;
        }
    }

}