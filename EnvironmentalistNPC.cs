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
    public class EnvironmentalistNPC : ModNPC
    {

        public override string Texture
        {
            get
            {
                return "HelpfulNPCs/EnvironmentalistNPC";
            }
        }



        public override void SetStaticDefaults()
        {
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
				new FlavorTextBestiaryInfoElement("Mods.HelpfulNPCs.Bestiary.EnvironmentalistNPC"),
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

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            for (int k = 0; k < Main.maxPlayers; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                
                if (player.inventory.Any(item => item.type == ModContent.ItemType<Items.DazzlingFlower>()))
                {
                    return true;
                }
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
            switch (Main.rand.Next(4))
            {
                case 0:
                    return Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.EnvironmentalistNPC.StandardDialogue1");
                case 1:
                    return Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.EnvironmentalistNPC.StandardDialogue2");
                case 2:
                    return Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.EnvironmentalistNPC.StandardDialogue3");
                default:
                    return Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.EnvironmentalistNPC.StandardDialogue4");
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.EnvironmentalistNPC.ChatOption1");
            button2 = Language.GetTextValue("Mods.HelpfulNPCs.Dialogue.EnvironmentalistNPC.ChatOption2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                shop = "Plants";
            } else
            {
                shop = "Critters";
            }


        }
        
        public override void AddShops()
        {
            var plantShop = new NPCShop(Type, "Plants")
                
                
                .Add(ItemID.Mushroom)
                .Add(ItemID.GrassSeeds)
                .Add(ItemID.Pumpkin)
                .Add(ItemID.PumpkinSeed)
                .Add(ItemID.GlowingMushroom)
                .Add(ItemID.MushroomGrassSeeds)
                .Add(ItemID.VileMushroom)
                .Add(ItemID.CorruptSeeds)
                .Add(ItemID.ViciousMushroom)
                .Add(ItemID.CrimsonSeeds)
                .Add(ItemID.Blinkroot)
                .Add(ItemID.BlinkrootSeeds)
                .Add(ItemID.Daybloom)
                .Add(ItemID.DaybloomSeeds)
                .Add(ItemID.Shiverthorn)
                .Add(ItemID.ShiverthornSeeds)
                .Add(ItemID.Waterleaf)
                .Add(ItemID.WaterleafSeeds)
                .Add(ItemID.Deathweed, Condition.DownedEowOrBoc)
                .Add(ItemID.DeathweedSeeds, Condition.DownedEowOrBoc)
                .Add(ItemID.Moonglow, Condition.DownedQueenBee)
                .Add(ItemID.MoonglowSeeds, Condition.DownedQueenBee)
                .Add(ItemID.Fireblossom, Condition.DownedSkeletron)
                .Add(ItemID.FireblossomSeeds, Condition.DownedSkeletron)
                .Add(ItemID.JungleSpores, Condition.DownedQueenBee)
                .Add(ItemID.JungleGrassSeeds);
            for (int i = 4349; i <= 4354; i++)
            {
                plantShop.Add(new Item(i) { shopCustomPrice = Item.buyPrice(copper: 5) });
            }
            plantShop.Add(new Item(ItemID.KryptonMoss) { shopCustomPrice = Item.buyPrice(copper: 10) })
                .Add(new Item(ItemID.XenonMoss) { shopCustomPrice = Item.buyPrice(copper: 10) })
                .Add(new Item(ItemID.ArgonMoss) { shopCustomPrice = Item.buyPrice(copper: 10) })
                .Add(ItemID.HallowedSeeds, Condition.Hardmode);

            plantShop.Register();

            var critterShop = new NPCShop(Type, "Critters")
                .Add(ItemID.Bird)
                .Add(ItemID.BlueJay)
                .Add(ItemID.Bunny)
                .Add(ItemID.Cardinal)
                .Add(ItemID.YellowCockatiel)
                .Add(ItemID.GrayCockatiel)
                .Add(ItemID.Duck)
                .Add(ItemID.Frog)
                .Add(ItemID.Goldfish)
                .Add(ItemID.MallardDuck)
                .Add(ItemID.Shimmerfly)
                .Add(ItemID.FairyCritterBlue)
                .Add(ItemID.FairyCritterGreen)
                .Add(ItemID.FairyCritterPink)
                .Add(ItemID.Grebe)
                .Add(ItemID.ScarletMacaw)
                .Add(ItemID.BlueMacaw)
                .Add(ItemID.Mouse)
                .Add(ItemID.Owl)
                .Add(ItemID.Penguin)
                .Add(ItemID.Pupfish)
                .Add(ItemID.Rat)
                .Add(ItemID.Seagull)
                .Add(ItemID.Seahorse)
                .Add(ItemID.Squirrel)
                .Add(ItemID.SquirrelRed)
                .Add(ItemID.Toucan)
                .Add(ItemID.Turtle)
                .Add(ItemID.TurtleJungle);
            critterShop.Register();
        }

        //public override void ModifyActiveShop(string shopName, Item[] items)
        //{
        //    if (Plants)
        //    {

        //        shop.item[nextSlot].SetDefaults(ItemID.ClayPot);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Cobweb);
        //        shop.item[nextSlot].shopCustomPrice = 10;
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Coral);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Seashell);
        //        shop.item[nextSlot].shopCustomPrice = 100;
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Starfish);
        //        shop.item[nextSlot].shopCustomPrice = 100;
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Mushroom);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.GrassSeeds);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.GlowingMushroom);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.MushroomGrassSeeds);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.VileMushroom);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.CorruptSeeds);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.ViciousMushroom);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.CrimsonSeeds);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Blinkroot);
        //        nextSlot++;

        //        //shop.item[nextSlot].SetDefaults(ItemID.BlinkrootSeeds);
        //        //nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Daybloom);
        //        nextSlot++;

        //        //shop.item[nextSlot].SetDefaults(ItemID.DaybloomSeeds);
        //        //nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Deathweed);
        //        nextSlot++;

        //        //shop.item[nextSlot].SetDefaults(ItemID.DeathweedSeeds);
        //        //nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Fireblossom);
        //        nextSlot++;

        //        //shop.item[nextSlot].SetDefaults(ItemID.FireblossomSeeds);
        //        //nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Moonglow);
        //        nextSlot++;

        //        //shop.item[nextSlot].SetDefaults(ItemID.MoonglowSeeds);
        //        //nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Shiverthorn);
        //        nextSlot++;

        //        //shop.item[nextSlot].SetDefaults(ItemID.ShiverthornSeeds);
        //        //nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Waterleaf);
        //        nextSlot++;

        //        //shop.item[nextSlot].SetDefaults(ItemID.WaterleafSeeds);
        //        //nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Pumpkin);
        //        nextSlot++;

        //        //shop.item[nextSlot].SetDefaults(ItemID.PumpkinSeed);
        //        //nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.JungleSpores);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.JungleGrassSeeds);
        //        nextSlot++;

        //        for (int i = 4349; i <= 4354; i++)
        //        {
        //            shop.item[nextSlot].SetDefaults(i);
        //            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper: 5);
        //            nextSlot++;
        //        }

        //        shop.item[nextSlot].SetDefaults(ItemID.KryptonMoss);
        //        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper: 10);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.XenonMoss);
        //        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper: 10);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.ArgonMoss);
        //        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper: 10);
        //        nextSlot++;

        //        if (Main.hardMode)
        //        {
        //            shop.item[nextSlot].SetDefaults(ItemID.HallowedSeeds);
        //            nextSlot++;
        //        }
        //    }
        //    else
        //    {
        //        shop.item[nextSlot].SetDefaults(ItemID.Bird);
        //        nextSlot++;



        //        shop.item[nextSlot].SetDefaults(ItemID.BlueJay);
        //        nextSlot++;



        //        shop.item[nextSlot].SetDefaults(ItemID.Bunny);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Cardinal);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Duck);
        //        nextSlot++;



        //        shop.item[nextSlot].SetDefaults(ItemID.Frog);
        //        nextSlot++;



        //        shop.item[nextSlot].SetDefaults(ItemID.Goldfish);
        //        nextSlot++;



        //        shop.item[nextSlot].SetDefaults(ItemID.MallardDuck);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.FairyCritterBlue);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.FairyCritterGreen);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.FairyCritterPink);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Grebe);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Mouse);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Owl);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Penguin);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Pupfish);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Rat);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Seagull); // eren
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Seahorse);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Squirrel);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.SquirrelRed);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.Turtle);
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ItemID.TurtleJungle);
        //        nextSlot++;

        //        if (NPC.downedPlantBoss)
        //        {
        //            shop.item[nextSlot].SetDefaults(ItemID.EmpressButterfly);
        //            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 25);
        //            nextSlot++;
        //        }

        //    }
        //}

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

        public override bool CanGoToStatue(bool toKingStatue) => true;
    }
}