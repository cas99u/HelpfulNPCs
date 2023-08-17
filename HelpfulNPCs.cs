using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelpfulNPCs
{
    class HelpfulNPCs : Mod
    {
        // internal static Config config;
        public HelpfulNPCs()
        {
            
        }


        public override void Unload()
        {
            // config = null;
        }

        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("Census", out Mod censusMod))
            {
                // https://github.com/JavidPack/Census/blob/dcf974cd38ee4701dde52ef1a48b1bdb475f0417/Census.cs#L502
                void AddNPC<T>(string condition) where T : ModNPC => censusMod.Call("TownNPCCondition", ModContent.NPCType<T>(), condition);

                AddNPC<EnvironmentalistNPC>("When Queen Bee has been defeated");
                AddNPC<FishermanNPC>("When the Angler is alive");
                AddNPC<HunterNPC>("When the Eye of Cthulhu has been defeated");
                AddNPC<MinerNPC>("When the Eye of Cthulhu has been defeated");
            }

            if (ModLoader.TryGetMod("DialogueTweak", out Mod dialogueTweakMod))
            {
                // https://github.com/Cyrillya/DialogueTweak/blob/3ca3c7f0d8bcf2aa4c6810fd6a1556a8eb4a0ae1/DialogueTweak.ModCall.cs#L7
                // If a condition is provided, a frame function must also be provided, even if that function is null.
                dialogueTweakMod.Call("ReplaceShopButtonIcon", ModContent.NPCType<EnvironmentalistNPC>(), "Terraria/Images/Item_" + ItemID.Daybloom);
                dialogueTweakMod.Call("ReplaceExtraButtonIcon", ModContent.NPCType<EnvironmentalistNPC>(), "Terraria/Images/Item_" + ItemID.Bunny);

                dialogueTweakMod.Call("ReplaceShopButtonIcon", ModContent.NPCType<FishermanNPC>(), "Terraria/Images/Item_" + ItemID.Bass);
                dialogueTweakMod.Call("ReplaceExtraButtonIcon", ModContent.NPCType<FishermanNPC>(), "Terraria/Images/Item_" + ItemID.ApprenticeBait);

                Func<Rectangle> ItemAnimationFrame(int itemId)
                {
                    Texture2D texture = TextureAssets.Item[itemId].Value;
                    return () => Main.itemAnimations[itemId]?.GetFrame(texture) ?? texture.Frame();
                }

                // Show a different icon for each shop.
                dialogueTweakMod.Call("ReplaceShopButtonIcon", ModContent.NPCType<HunterNPC>(), "Terraria/Images/Item_" + ItemID.Gel, () => HunterNPC.shopChoice == 0, null);
                dialogueTweakMod.Call("ReplaceShopButtonIcon", ModContent.NPCType<HunterNPC>(), "Terraria/Images/Item_" + ItemID.SoulofLight, () => HunterNPC.shopChoice == 1, ItemAnimationFrame(ItemID.SoulofLight));
                dialogueTweakMod.Call("ReplaceShopButtonIcon", ModContent.NPCType<HunterNPC>(), "Terraria/Images/Item_" + ItemID.EndlessQuiver, () => HunterNPC.shopChoice == 2, null);

                // Show either an Iron or Lead bar, depending on which is found naturally in the world.
                dialogueTweakMod.Call("ReplaceShopButtonIcon", ModContent.NPCType<MinerNPC>(), "Terraria/Images/Item_" + ItemID.IronBar, () => WorldGen.SavedOreTiers.Iron != TileID.Lead, null);
                dialogueTweakMod.Call("ReplaceShopButtonIcon", ModContent.NPCType<MinerNPC>(), "Terraria/Images/Item_" + ItemID.LeadBar, () => WorldGen.SavedOreTiers.Iron == TileID.Lead, null);
                dialogueTweakMod.Call("ReplaceExtraButtonIcon", ModContent.NPCType<MinerNPC>(), "Terraria/Images/Item_" + ItemID.Diamond);
            }
        }
    }
}

 
