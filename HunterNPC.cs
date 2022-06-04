
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using On.Terraria.GameContent.ItemDropRules;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace HelpfulNPCs
{
    [AutoloadHead]
    public class HunterNPC : ModNPC
    {
        private static int shopChoice = 0;

        public override string Texture
        {
            get
            {
                return "HelpfulNPCs/HunterNPC";
            }
        }


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hunter");
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 250;
            NPCID.Sets.AttackType[NPC.type] = 1;
            NPCID.Sets.AttackTime[NPC.type] = 17;
            NPCID.Sets.AttackAverageChance[NPC.type] = 10;
            NPCID.Sets.HatOffsetY[NPC.type] = -3;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = -1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction                  
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Like)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Love)
                .SetNPCAffection(NPCID.WitchDoctor, AffectionLevel.Like)
                .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Dislike)
            ;

        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("The Hunter strikes fear into the hearts of all monsters. Yes, his parents named him Hunter."),
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
            NPC.defense = 30;
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Merchant;

        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (NPC.downedBoss1 && HelpfulNPCs.config.HunterCanSpawn)
            {
                return true;
            }
            return false;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
                { "Hunter",
            };
        }

        public override string GetChat()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return "Quiet, I need to focus.";
                case 1:
                    return "I have slain many monsters.";
                case 2:
                    return "Yes, I am Hunter the Hunter.";
                default:
                    return "You should be glad that I'm not hunting you.";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {

            if (shopChoice == 0)
            {
                button = "Monster Drops";
            }
            else if (shopChoice == 1)
            {
                button = "Souls/Essences";
            }
            else if (shopChoice == 2)
            {
                button = "Ammunition";
            } 
            else
            {
                shopChoice = 0;
            }
            
            button2 = "Change Shop";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            

            if (firstButton)
            {
                shop = true;
            } 
            else
            {
                shopChoice++;
            }

        }
        

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (shopChoice == 0)
            {
                shop.item[nextSlot].SetDefaults(ItemID.Gel);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.PinkGel);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Lens);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.BlackLens);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.AntlionMandible);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.RottenChunk);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Vertebrae);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.WormTooth);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.Hook);
                nextSlot++;

                shop.item[nextSlot].SetDefaults(ItemID.SharkFin);
                nextSlot++;

                if (NPC.downedBoss2)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.TatteredCloth);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.ShadowScale);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.TissueSample);
                    nextSlot++;
                }

                if (NPC.downedBoss3)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Bone);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 2);
                    nextSlot++;    
                }

                if (NPC.downedQueenBee || NPC.downedBoss3)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Feather);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 2);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.Stinger);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.Vine);
                    nextSlot++;
                }

                if (Main.hardMode == true)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.CursedFlame);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.Ichor);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.DarkShard);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.LightShard);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.AncientCloth);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.PixieDust);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.UnicornHorn);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.SpiderFang);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.LivingFireBlock);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(copper: 5);
                    nextSlot++;



                    shop.item[nextSlot].SetDefaults(ItemID.FrostCore);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.AncientBattleArmorMaterial); // Forbidden Fragment
                    nextSlot++;



                }

                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {

                    shop.item[nextSlot].SetDefaults(ItemID.HallowedBar);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.RodofDiscord);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(platinum: 3);
                    nextSlot++;



                    shop.item[nextSlot].SetDefaults(ItemID.TurtleShell);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 5);
                    nextSlot++;
                }


                if (NPC.downedGolemBoss)
                {


                    shop.item[nextSlot].SetDefaults(ItemID.LunarTabletFragment);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 25);
                    nextSlot++;
                }


            }
            if (shopChoice == 1)
            {
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.SoulofLight);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.SoulofNight);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.SoulofFlight);
                    nextSlot++;
                }

                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {

                    shop.item[nextSlot].SetDefaults(ItemID.SoulofSight);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.SoulofMight);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.SoulofFright);
                    nextSlot++;
                }
                if (NPC.downedPlantBoss)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Ectoplasm);
                    nextSlot++;


                }

                if (NPC.downedMoonlord)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.FragmentSolar);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 5);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.FragmentVortex);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 5);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.FragmentNebula);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 5);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.FragmentStardust);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(gold: 5);
                    nextSlot++;
                }
            }
            if (shopChoice == 2)
            {
                shop.item[nextSlot].SetDefaults(ItemID.MusketBall);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SilverBullet);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.TungstenBullet);
                nextSlot++;
                if (NPC.downedBoss2)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.MeteorShot);
                    nextSlot++;
                }
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.PartyBullet);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CursedBullet);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.IchorBullet);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CrystalBullet);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.GoldenBullet);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ExplodingBullet);
                    nextSlot++;
                }

                if (NPC.downedMechBossAny)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.HighVelocityBullet);
                    nextSlot++;
                }
                if (NPC.downedPlantBoss)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.ChlorophyteBullet);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.NanoBullet);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.VenomBullet);
                    nextSlot++;
                }
                if (NPC.downedMoonlord)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.MoonlordBullet);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 1);
                    nextSlot++;
                }

                shop.item[nextSlot].SetDefaults(ItemID.WoodenArrow);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.FlamingArrow);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.FrostburnArrow);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BoneArrow);
                nextSlot++;
                if (NPC.downedBoss1)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.JestersArrow);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.UnholyArrow);
                    nextSlot++;
                }
                if (NPC.downedBoss3)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.HellfireArrow);
                    nextSlot++;
                }
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.CursedArrow);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.IchorArrow);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.HolyArrow);
                    nextSlot++;
                }
                if (NPC.downedPlantBoss)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.ChlorophyteArrow);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.VenomArrow);
                    nextSlot++;
                }
                if (NPC.downedMoonlord)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.MoonlordArrow);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(silver: 3);
                    nextSlot++;
                }
                
            }
            
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            if (!Main.hardMode)
            {
                damage = 15;
                knockback = 2f;
            }
            else
            {
                damage = 50;
                knockback = 10f;
            }

        }

        public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
        {
            if (!Main.hardMode)
            {
                scale = 1f;
                item = ItemID.Musket;
                closeness = 1;
            }
            else
            {
                scale = 1f;
                item = ItemID.SniperRifle;
                closeness = 1;
            }
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            if (!Main.hardMode)
            {
                cooldown = 10;
                randExtraCooldown = 20;
            }
            else
            {
                cooldown = 30;
                randExtraCooldown = 40;
            }

        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (!Main.hardMode)
            {
                projType = ProjectileID.Bullet;
                attackDelay = 3;
            }
            else
            {
                projType = ProjectileID.BulletHighVelocity;
                attackDelay = 5;
            }

        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            if (!Main.hardMode)
            {
                multiplier = 15f;
                randomOffset = 0.5f;
            }

            else
            {
                multiplier = 20f;
                randomOffset = 0f;
            }

        }
    }
}