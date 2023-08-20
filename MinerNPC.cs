using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace HelpfulNPCs
{
    [AutoloadHead]
    public class MinerNPC : ModNPC
    {
        public static bool Bars = false;

        public override string Texture
        {
            get
            {
                return "HelpfulNPCs/MinerNPC";
            }
        }


        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Miner");
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.AttackFrameCount[NPC.type] = 5;
            NPCID.Sets.DangerDetectRange[NPC.type] = 150;
            NPCID.Sets.AttackType[NPC.type] = 3;
            NPCID.Sets.AttackTime[NPC.type] = 25;
            NPCID.Sets.AttackAverageChance[NPC.type] = 10;
            NPCID.Sets.HatOffsetY[NPC.type] = -1;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = -1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction                  
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Like)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.DD2Bartender, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Love)
                .SetNPCAffection(NPCID.GoblinTinkerer, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Dislike)
            ;

        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("The Miner spends most of his time deep underground. For some reason, he actually enjoys mining."),
            });
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;

        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            for (int k = 0; k < Main.maxPlayers; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }


                if (player.inventory.Any(item => item.type == ModContent.ItemType<Items.GlimmeringStone>()))
                {
                    return true;
                }
            }

            return false;

        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
                { "Gold",
                  "John",
                  "Edward",
                  "Thomas"
            };
        }

        public override string GetChat()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.MinerNPC.StandardDialogue1");
                case 1:
                    return Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.MinerNPC.StandardDialogue2");
                case 2:
                    return Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.MinerNPC.StandardDialogue3");
                default:
                    return Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.MinerNPC.StandardDialogue4");
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.MinerNPC.ChatOption1");
            button2 = Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.MinerNPC.ChatOption2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                shop = "Valuables";
            }
            else
            {
                shop = "Blocks";
            }

        }

        public override void AddShops()
        {
            var barShop = new NPCShop(Type, "Valuables")
                .Add(ItemID.Amethyst)
                .Add(ItemID.Topaz)
                .Add(ItemID.Sapphire)
                .Add(ItemID.Emerald)
                .Add(ItemID.Ruby)
                .Add(ItemID.Diamond)
                .Add(ItemID.Amber)
                .Add(ItemID.CopperBar)
                .Add(ItemID.TinBar)
                .Add(ItemID.IronBar)
                .Add(ItemID.LeadBar)
                .Add(ItemID.SilverBar)
                .Add(ItemID.TungstenBar)
                .Add(ItemID.GoldBar)
                .Add(ItemID.PlatinumBar)
                .Add(ItemID.DemoniteBar, Condition.DownedEyeOfCthulhu)
                .Add(ItemID.CrimtaneBar, Condition.DownedEyeOfCthulhu)
                .Add(ItemID.MeteoriteBar, Condition.DownedEowOrBoc)
                .Add(new Item(ItemID.Obsidian) { shopCustomPrice = Item.buyPrice(silver: 3) }, Condition.DownedEowOrBoc)
                .Add(ItemID.Hellstone, Condition.DownedSkeletron)
                .Add(new Item(ItemID.LifeCrystal) { shopCustomPrice = Item.buyPrice(gold: 5) }, Condition.DownedSkeletron)
                .Add(ItemID.CrystalShard, Condition.Hardmode)
                .Add(ItemID.CobaltBar, Condition.Hardmode)
                .Add(ItemID.PalladiumBar, Condition.Hardmode)
                .Add(ItemID.MythrilBar, Condition.Hardmode)
                .Add(ItemID.OrichalcumBar, Condition.Hardmode)
                .Add(ItemID.AdamantiteBar, Condition.Hardmode)
                .Add(ItemID.TitaniumBar, Condition.Hardmode)
                .Add(ItemID.ChlorophyteBar, Condition.DownedMechBossAll)
                .Add(new Item(ItemID.LifeFruit) { shopCustomPrice = Item.buyPrice(gold: 10) }, Condition.DownedPlantera);
            barShop.Register();

            var gemShop = new NPCShop(Type, "Blocks")
                .Add(new Item(ItemID.Wood) { shopCustomPrice = Item.buyPrice(copper: 5) })
                .Add(new Item(ItemID.DirtBlock) { shopCustomPrice = Item.buyPrice(copper: 1) })
                .Add(new Item(ItemID.StoneBlock) { shopCustomPrice = Item.buyPrice(copper: 2) })
                .Add(new Item(ItemID.ClayBlock) { shopCustomPrice = Item.buyPrice(copper: 2) })
                .Add(new Item(ItemID.SiltBlock) { shopCustomPrice = Item.buyPrice(silver: 1) })
                .Add(new Item(ItemID.SandBlock) { shopCustomPrice = Item.buyPrice(copper: 1) })
                .Add(new Item(ItemID.Sandstone) { shopCustomPrice = Item.buyPrice(copper: 2) })
                .Add(new Item(ItemID.HardenedSand) { shopCustomPrice = Item.buyPrice(copper: 2) })
                .Add(new Item(ItemID.SnowBlock) { shopCustomPrice = Item.buyPrice(copper: 1) })
                .Add(new Item(ItemID.IceBlock) { shopCustomPrice = Item.buyPrice(copper: 2) })
                .Add(new Item(ItemID.SlushBlock) { shopCustomPrice = Item.buyPrice(silver: 1) })
                .Add(new Item(ItemID.MudBlock) { shopCustomPrice = Item.buyPrice(copper: 1) })
                .Add(new Item(ItemID.Granite) { shopCustomPrice = Item.buyPrice(copper: 2) })
                .Add(new Item(ItemID.Marble) { shopCustomPrice = Item.buyPrice(copper: 2) })
                .Add(new Item(ItemID.AshBlock) { shopCustomPrice = Item.buyPrice(copper: 3) }, Condition.DownedSkeletron);


            gemShop.Register();
        }

        //public override void ModifyActiveShop(string shopName, Item[] items)
        //{
        //    if (Bars)
        //    {
        //        shop.item[nextSlot].SetDefaults(ItemID.StoneBlock);
        //        shop.item[nextSlot].shopCustomPrice = 1;
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.DirtBlock);
        //        shop.item[nextSlot].shopCustomPrice = 1;
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.MudBlock);
        //        shop.item[nextSlot].shopCustomPrice = 1;
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Granite);
        //        shop.item[nextSlot].shopCustomPrice = 1;
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Marble);
        //        shop.item[nextSlot].shopCustomPrice = 1;
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.CopperBar);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.TinBar);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.IronBar);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.LeadBar);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.SilverBar);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.TungstenBar);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.GoldBar);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.PlatinumBar);
        //        nextSlot++;

        //        if (NPC.downedBoss1)
        //        {
        //            shop.item[nextSlot].SetDefaults(ItemID.DemoniteBar);
        //            nextSlot++;

        //            shop.item[nextSlot].SetDefaults(ItemID.CrimtaneBar);
        //            nextSlot++;
        //        }

        //        if (NPC.downedBoss2)
        //        {


        //            shop.item[nextSlot].SetDefaults(ItemID.MeteoriteBar);
        //            nextSlot++;

        //            shop.item[nextSlot].SetDefaults(ItemID.Obsidian);
        //            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 5);
        //            nextSlot++;
        //        }

        //        if (NPC.downedBoss3)
        //        {
        //            shop.item[nextSlot].SetDefaults(ItemID.HellstoneBar);
        //            nextSlot++;
        //            shop.item[nextSlot].SetDefaults(ItemID.LifeCrystal);
        //            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 5);
        //            nextSlot++;
        //        }

        //        if (Main.hardMode)
        //        {
        //            shop.item[nextSlot].SetDefaults(ItemID.CrystalShard);
        //            nextSlot++;

        //            shop.item[nextSlot].SetDefaults(ItemID.CobaltBar);
        //            nextSlot++;

        //            shop.item[nextSlot].SetDefaults(ItemID.PalladiumBar);
        //            nextSlot++;

        //            shop.item[nextSlot].SetDefaults(ItemID.MythrilBar);
        //            nextSlot++;

        //            shop.item[nextSlot].SetDefaults(ItemID.OrichalcumBar);
        //            nextSlot++;

        //            shop.item[nextSlot].SetDefaults(ItemID.AdamantiteBar);
        //            nextSlot++;

        //            shop.item[nextSlot].SetDefaults(ItemID.TitaniumBar);
        //            nextSlot++;
        //        }

        //        if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
        //        {
        //            shop.item[nextSlot].SetDefaults(ItemID.ChlorophyteBar);
        //            nextSlot++;

        //        }

        //        if (NPC.downedPlantBoss)
        //        {
        //            shop.item[nextSlot].SetDefaults(ItemID.LifeFruit);
        //            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 10);
        //            nextSlot++;
        //        }
        //    }

        //    else
        //    {
        //        shop.item[nextSlot].SetDefaults(ItemID.Amethyst);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Topaz);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Sapphire);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Emerald);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Ruby);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Diamond);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Amber);
        //        nextSlot++;
        //    }
        //}

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 10;
            knockback = 3f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 50;
            randExtraCooldown = 30;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)//Allows you to customize how this town NPC's weapon is drawn when this NPC is swinging it (this NPC must have an attack type of 3). Item is the Texture2D instance of the item to be drawn (use Main.itemTexture[id of item]), itemSize is the width and height of the item's hitbox
        {
            //scale = 2f;
            item = ModContent.Request<Texture2D>("Terraria/Images/Item_" + ItemID.Minecart).Value; //this defines the item that this npc will use

            itemSize = 30;

            if (NPC.spriteDirection == 1)
            {
                offset.X = -7;
            }

            if (NPC.spriteDirection == -1)
            {
                offset.X = 7;
            }
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight) //  Allows you to determine the width and height of the item this town NPC swings when it attacks, which controls the range of this NPC's swung weapon.
        {
            itemWidth = 32;
            itemHeight = 32;
        }

        public override bool CanGoToStatue(bool toKingStatue) => true;
    }
}