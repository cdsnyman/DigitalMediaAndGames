using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShootShapesUp
{
    struct GameSessionStats
    {

        public static int NumberOfKills = 0;
        public static int NumberOfKillsRequired;
        public static int NumberOfLives = 3;
        public static Stopwatch StopWatch = new Stopwatch();

        public static int CurrentLevel = 1;
        public static int TotalEnemiesSpawnedInLevel = 0;
        public static bool BossStatus = false;
        public static bool BossKilled = false;
    }
}
