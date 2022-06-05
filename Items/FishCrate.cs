using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelpfulNPCs.Items
{
    public class FishCrate : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Crate");
            Tooltip.SetDefault("Right click to receive the current quest fish");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
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

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Terraria/Images/Item_" + Main.anglerQuestItemNetIDs[Main.anglerQuest]).Value;
            Vector2 position = Item.position - Main.screenPosition + new Vector2(Item.width / 3, -Item.height / 3);
            spriteBatch.Draw(texture, position, lightColor);
            return true;
        }
    }

}