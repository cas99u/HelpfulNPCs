using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.Utilities;

namespace HelpfulNPCs
{
    public class Chester : ModNPC
    {
       
        bool chester = false;
        bool hasItem = false;

        
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Chester");

            Main.npcFrameCount[NPC.type] = 2;

            NPCID.Sets.ActsLikeTownNPC[Type] = true;

         

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Frame = 1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.width = 52;
            NPC.height = 46;
            NPC.aiStyle = 0;
            NPC.damage = 0;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 1f;
            NPC.dontTakeDamageFromHostiles = true;
            NPC.npcSlots = 1;
            NPC.rarity = 2;

        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
           
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
                {
                    BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
                   
                    new FlavorTextBestiaryInfoElement("A mysterious man that shows up in random places. He likes to resell treasure that he finds.")
                });
        }

        public override void FindFrame(int frameHeight)
        {
            if (chester)
            {
                NPC.frame.Y = 46;

            } else
            {
                NPC.frame.Y = 0;
            }
        }

        public override bool CanChat()
        {
            return true;
        }

        public override string GetChat()
        {
            chester = true;
            NPC.GivenName = "Chester";
            NPC.rotation = 0f;
            switch (Main.rand.Next(3))
            {
                case 0:
                    return "In the Village or out in the fields, I have all the deals! You're not gonna BELIEVE what I just found in this chest. Want to see?";
                case 1:
                    return "Psst... Hey, you wouldn't BELIEVE the merchandise I've got for ya today... Why not have a look?";
                default:
                    return "Psst... Hey. I'm loaded with deals that'll blow you away! Why not have a look?";
            }

        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Yes";
        }

        

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            shop = true;

        }
        public override void AI()
        {
            if (!hasItem)
            {
                surfaceItem = getItem(surface);
                cavernItem = getItem(cavern);
                jungleItem = getItem(jungle);
                dungeonItem = getItem(dungeon);
                iceItem = getItem(ice);
                desertItem = getItem(desert);
                hellItem = getItem(hell);
                skyItem = getItem(sky);
                oceanItem = getItem(ocean);
                hasItem = true;
                NPC.GivenName = "Mysterious Chest";
            }

            NPC.ai[0]++;
            if (!chester && NPC.ai[0] == 120)
            {
                NPC.velocity.Y = -3f;
                NPC.rotation = 0f;
                NPC.ai[0] = 0;
            }
            if (!chester && NPC.ai[0] > 5 && NPC.ai[0] <= 10)
            {
                NPC.velocity.Y = 0f;
                NPC.rotation += 0.05f;
            }
            if (!chester && NPC.ai[0] > 10 && NPC.ai[0] <= 20)
            {
                NPC.velocity.Y = 0f;
                NPC.rotation -= 0.05f;
            }
            if (!chester && NPC.ai[0] > 20 && NPC.ai[0] <= 25)
            {
                NPC.velocity.Y = 0f;
                NPC.rotation += 0.05f;
            }
        }

        static int surfaceItem;
        static int cavernItem;
        static int jungleItem;
        static int dungeonItem;
        static int iceItem;
        static int desertItem;
        static int hellItem;
        static int skyItem;
        static int oceanItem;

        List<int> surface = new List<int>
        {
            ItemID.Spear,
            ItemID.Blowpipe,
            ItemID.WoodenBoomerang,
            ItemID.Aglet,
            ItemID.ClimbingClaws,
            ItemID.Umbrella,
            ItemID.CordageGuide,
            ItemID.WandofSparking,
            ItemID.Radar,
            ItemID.PortableStool,
            ItemID.LivingWoodWand,
            ItemID.LeafWand,
            ItemID.BabyBirdStaff
        };

        List<int> cavern = new List<int>()
        {
            ItemID.BandofRegeneration,
            ItemID.CloudinaBottle,
            ItemID.Extractinator,
            ItemID.FlareGun,
            ItemID.HermesBoots,
            ItemID.LavaCharm,
            ItemID.Mace,
            ItemID.MagicMirror,
            ItemID.ShoeSpikes,
            ItemID.LuckyHorseshoe,
        };

        List<int> jungle = new List<int>
        {
            ItemID.AnkletoftheWind,
            ItemID.FeralClaws,
            ItemID.StaffofRegrowth,
            ItemID.FiberglassFishingPole,
            ItemID.Boomstick,
            ItemID.HoneyDispenser,
            ItemID.FlowerBoots,
            ItemID.Seaweed
        };

        List<int> dungeon = new List<int>
        {
            ItemID.Handgun,
            ItemID.AquaScepter,
            ItemID.MagicMissile,
            ItemID.BlueMoon,
            ItemID.CobaltShield,
            ItemID.Muramasa,
            ItemID.ShadowKey,
            ItemID.Valor,
            ItemID.BoneWelder
        };

        List<int> ice = new List<int>
        {
            ItemID.IceBoomerang,
            ItemID.IceBlade,
            ItemID.IceSkates,
            ItemID.SnowballCannon,
            ItemID.BlizzardinaBottle,
            ItemID.FlurryBoots,
            ItemID.IceMirror,
            ItemID.Fish,
            ItemID.IceMachine
        };

        List<int> desert = new List<int>
        {
            ItemID.SandstorminaBottle,
            ItemID.FlyingCarpet,
            ItemID.AncientChisel,
            ItemID.SandBoots,
            ItemID.ThunderSpear,
            ItemID.ThunderStaff,
            ItemID.MagicConch,
            ItemID.EncumberingStone,
            ItemID.CatBast,
            ItemID.MysticCoilSnake,
        };

        List<int> hell = new List<int>
        {
            ItemID.DarkLance,
            ItemID.Sunfury,
            ItemID.Flamelash,
            ItemID.FlowerofFire,
            ItemID.HellwingBow,
            ItemID.TreasureMagnet,
            ItemID.OrnateShadowKey,
            ItemID.HellCake
        };

        List<int> sky = new List<int>
        {
            ItemID.Starfury,
            ItemID.ShinyRedBalloon,
            ItemID.SkyMill,
            ItemID.CreativeWings
        };

        List<int> ocean = new List<int>
        {
            ItemID.Trident,
            ItemID.Flipper,
            ItemID.SandcastleBucket,
            ItemID.BreathingReed,
            ItemID.FloatingTube,
            ItemID.SharkBait,
            ItemID.WaterWalkingBoots
        };

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (Main.LocalPlayer.ZoneOverworldHeight || Main.LocalPlayer.ZoneDirtLayerHeight)
            {
                shop.item[nextSlot].SetDefaults(surfaceItem);
                nextSlot++;
            }

            if (Main.LocalPlayer.ZoneRockLayerHeight)
            {
                shop.item[nextSlot].SetDefaults(cavernItem);
                nextSlot++;
            }

            if (Main.LocalPlayer.ZoneJungle && Main.LocalPlayer.ZoneRockLayerHeight)
            {
                shop.item[nextSlot].SetDefaults(jungleItem);
                nextSlot++;
            }

            if (Main.LocalPlayer.ZoneDungeon)
            {
                shop.item[nextSlot].SetDefaults(dungeonItem);
                nextSlot++;
            }

            if (Main.LocalPlayer.ZoneSnow && Main.LocalPlayer.ZoneRockLayerHeight)
            {
                shop.item[nextSlot].SetDefaults(iceItem);
                nextSlot++;
            }

            if (Main.LocalPlayer.ZoneDesert && Main.LocalPlayer.ZoneRockLayerHeight)
            {
                shop.item[nextSlot].SetDefaults(desertItem);
                nextSlot++;
            }

            if (Main.LocalPlayer.ZoneUnderworldHeight)
            {
                shop.item[nextSlot].SetDefaults(hellItem);
                nextSlot++;
            }

            if (Main.LocalPlayer.ZoneSkyHeight)
            {
                shop.item[nextSlot].SetDefaults(skyItem);
                nextSlot++;
            }

            if (Main.LocalPlayer.ZoneBeach)
            {
                shop.item[nextSlot].SetDefaults(oceanItem);
                nextSlot++;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (HelpfulNPCs.config.ChesterCanSpawn && !NPC.AnyNPCs(ModContent.NPCType<Chester>())) ? 0.02f : 0f;
        }

        private int getItem(List<int> items)
        {
            return items[Main.rand.Next(0, items.Count)];

        }
    }
}