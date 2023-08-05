//using System;
//using System.ComponentModel;
//using System.IO;
//using Terraria;
//using Terraria.IO;
//using Terraria.ModLoader;
//using Terraria.ModLoader.Config;

//namespace HelpfulNPCs
//{
//    public class Config : ModConfig
//    {
//        public override ConfigScope Mode => ConfigScope.ServerSide;

//        [Label("Miner Can Spawn")]
//        [DefaultValue(true)]
//        public bool MinerCanSpawn;

//        [Label("Fisherman Can Spawn")]
//        [DefaultValue(true)]
//        public bool FishermanCanSpawn;

//        [Label("Hunter Can Spawn")]
//        [DefaultValue(true)]
//        public bool HunterCanSpawn;

//        [Label("Environmentalist Can Spawn")]
//        [DefaultValue(true)]
//        public bool EnvironmentalistCanSpawn;

//        [Label("Chester Can Spawn")]
//        [DefaultValue(true)]
//        public bool ChesterCanSpawn;

//        public override void OnLoaded()
//        {
//            HelpfulNPCs.config = this;
//        }
//    }
//}
