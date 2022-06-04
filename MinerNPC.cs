using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
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
            DisplayName.SetDefault("Miner");
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

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (NPC.downedBoss1 && HelpfulNPCs.config.MinerCanSpawn)
            {
                return true;
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
            switch (Main.rand.Next(3))
            {
                case 0:
                    return "I love being down in the mines.";
                case 1:
                    return "I have no use for these shiny gems and ore, want to buy them off me?";
                default:
                    return "What do you mean I should mine at Y level 11?";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Bars & More";
            button2 = "Gems";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            Bars = firstButton;
            shop = true;

           
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (Bars)
            {
                shop.item[nextSlot].SetDefaults(ItemID.StoneBlock);
                shop.item[nextSlot].shopCustomPrice = 1;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.DirtBlock);
                shop.item[nextSlot].shopCustomPrice = 1;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.MudBlock);
                shop.item[nextSlot].shopCustomPrice = 1;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Granite);
                shop.item[nextSlot].shopCustomPrice = 1;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Marble);
                shop.item[nextSlot].shopCustomPrice = 1;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.CopperBar);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.TinBar);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.IronBar);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.LeadBar);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.SilverBar);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.TungstenBar);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.GoldBar);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.PlatinumBar);
                nextSlot++;

                if (NPC.downedBoss1)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.DemoniteBar);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.CrimtaneBar);
                    nextSlot++;
                }

                if (NPC.downedBoss2)
                {


                    shop.item[nextSlot].SetDefaults(ItemID.MeteoriteBar);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.Obsidian);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 5);
                    nextSlot++;
                }

                if (NPC.downedBoss3)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.HellstoneBar);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.LifeCrystal);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 5);
                    nextSlot++;
                }

                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.CrystalShard);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.CobaltBar);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.PalladiumBar);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.MythrilBar);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.OrichalcumBar);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.AdamantiteBar);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.TitaniumBar);
                    nextSlot++;
                }

                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.ChlorophyteBar);
                    nextSlot++;

                }

                if (NPC.downedPlantBoss)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.LifeFruit);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 10);
                    nextSlot++;
                }

                if (NPC.downedGolemBoss)
                {
                    
                }
            }

            else
            {
                shop.item[nextSlot].SetDefaults(ItemID.Amethyst);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Topaz);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Sapphire);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Emerald);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Ruby);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Diamond);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Amber);
                nextSlot++;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 10;
            knockback = 3f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 20;
            randExtraCooldown = 20;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)//Allows you to customize how this town NPC's weapon is drawn when this NPC is swinging it (this NPC must have an attack type of 3). Item is the Texture2D instance of the item to be drawn (use Main.itemTexture[id of item]), itemSize is the width and height of the item's hitbox
        {
            scale = 2f;
            item = (Texture2D)Terraria.GameContent.TextureAssets.Item[ItemID.GoldPickaxe]; //this defines the item that this npc will use
            itemSize = 32;

            /*if (NPC.spriteDirection == 1)
            {
                offset.X = -13;
            } 

            if (NPC.spriteDirection == -1)
            {
                offset.X = 13;
            }*/
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight) //  Allows you to determine the width and height of the item this town NPC swings when it attacks, which controls the range of this NPC's swung weapon.
        {
            itemWidth = 32;
            itemHeight = 32;
        }
    }
}