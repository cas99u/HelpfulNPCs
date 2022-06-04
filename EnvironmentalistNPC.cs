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
    public class EnvironmentalistNPC : ModNPC
    {
        public static bool Plants = false;

        public override string Texture
        {
            get
            {
                return "HelpfulNPCs/EnvironmentalistNPC";
            }
        }



        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Environmentalist");
            Main.npcFrameCount[NPC.type] = 23;
            NPCID.Sets.AttackFrameCount[NPC.type] = 2;
            NPCID.Sets.DangerDetectRange[NPC.type] = 300;
            NPCID.Sets.AttackType[NPC.type] = 2;
            NPCID.Sets.AttackTime[NPC.type] = 30;
            NPCID.Sets.AttackAverageChance[NPC.type] = 10;
            NPCID.Sets.HatOffsetY[NPC.type] = 2;
            NPCID.Sets.MagicAuraColor[NPC.type] = Color.ForestGreen;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = -1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction                  
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Dislike) 
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love) 
                .SetNPCAffection(NPCID.WitchDoctor, AffectionLevel.Like) 
                .SetNPCAffection(NPCID.BestiaryGirl, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike) 
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate) 
            ;

        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("The Environmentalist loves nature. He sells plants and animals for conservation. You wouldn't do anything bad with them, right?"),
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
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Merchant;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (NPC.downedQueenBee && HelpfulNPCs.config.EnvironmentalistCanSpawn)
            {
                return true;
            }
            return false;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
                { "Gregor", 
                  "Mendal",
                  "Theophrastus",
                  "Hooke"
            };
        }

        public override string GetChat()
        {
            switch (Main.rand.Next(3))
            {
                case 0:
                    return "Hey! Don't step on my plants!";
                case 1:
                    return "There's no such thing as magic beanstalks.";
                case 2:
                    return "My research on Terrarian critters is almost complete.";
                default:
                    return "Growing plants takes hardwork and dedication.";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Plants";
            button2 = "Critters";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            Plants = firstButton;
            shop = true;


        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (Plants)
            {

                shop.item[nextSlot].SetDefaults(ItemID.ClayPot);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Coral);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Seashell);
                shop.item[nextSlot].shopCustomPrice = 100;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Starfish);
                shop.item[nextSlot].shopCustomPrice = 100;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Mushroom);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.GrassSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.GlowingMushroom);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.MushroomGrassSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.VileMushroom);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.CorruptSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.ViciousMushroom);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.CrimsonSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Blinkroot);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.BlinkrootSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Daybloom);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.DaybloomSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Deathweed);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.DeathweedSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Fireblossom);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.FireblossomSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Moonglow);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.MoonglowSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Shiverthorn);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.ShiverthornSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Waterleaf);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.WaterleafSeeds);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Pumpkin);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.PumpkinSeed);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.JungleSpores);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.JungleGrassSeeds);
                nextSlot++;

                for (int i = 4349; i <= 4354; i++)
                {
                    shop.item[nextSlot].SetDefaults(i);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper: 5);
                    nextSlot++;
                }

                shop.item[nextSlot].SetDefaults(ItemID.KryptonMoss);
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper: 10);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.XenonMoss);
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper: 10);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.ArgonMoss);
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper: 10);
                nextSlot++;

                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.HallowedSeeds);
                    nextSlot++;
                }
            }
            else
            {
                shop.item[nextSlot].SetDefaults(ItemID.Bird);
                nextSlot++;

                

                shop.item[nextSlot].SetDefaults(ItemID.BlueJay);
                nextSlot++;

                

                shop.item[nextSlot].SetDefaults(ItemID.Bunny);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Cardinal);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Duck);
                nextSlot++;

                

                shop.item[nextSlot].SetDefaults(ItemID.Frog);
                nextSlot++;

                

                shop.item[nextSlot].SetDefaults(ItemID.Goldfish);
                nextSlot++;

                

                shop.item[nextSlot].SetDefaults(ItemID.MallardDuck);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.FairyCritterBlue);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.FairyCritterGreen);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.FairyCritterPink);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Grebe);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Mouse);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Owl);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Penguin);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Pupfish);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Rat);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Seagull); // eren
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Seahorse);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Squirrel);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.SquirrelRed);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Turtle);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.TurtleJungle);
                nextSlot++;

                if (NPC.downedPlantBoss)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.EmpressButterfly);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 25);
                    nextSlot++;
                }

            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 3f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 20;
            randExtraCooldown = 20;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.Leaf;
            attackDelay = 20;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 5;
            gravityCorrection = 0;
            randomOffset = .2f;
        }
    }
}