using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootShapesUp
{
    static class EnemySpawner
    {
        static Random rand = new Random();
        //Default spawn values
        static float governmentSeekerSpawnChance = 60; 
        static float pirateSeekerSpawnChance = 60;

        public static void Update()
        {
            if (!PlayerShip.Instance.IsDead && EntityManager.Count < 200)
            {
                switch (GameSessionStats.CurrentLevel)
                {
                    case 1: //Level 1
                        GameSessionStats.NumberOfKillsRequired = 20;

                        if (ProgressToNextLevel())
                            break;

                        if (LevelSpawnCapReached())
                            break;
                        
                        if (rand.Next((int)governmentSeekerSpawnChance) == 0)
                        {
                            EntityManager.Add(Enemy.CreateGovernmentSeeker(GetSpawnPosition()));
                            ++GameSessionStats.TotalEnemiesSpawnedInLevel;
                        }
                            
                        break;

                    case 2://Level 2
                        GameSessionStats.NumberOfKillsRequired = 30;

                        if (ProgressToNextLevel())
                            break;

                        if (LevelSpawnCapReached())
                            break;

                        if (rand.Next((int)governmentSeekerSpawnChance * 2) == 0)
                        {
                            EntityManager.Add(Enemy.CreateGovernmentSeeker(GetSpawnPosition()));
                            ++GameSessionStats.TotalEnemiesSpawnedInLevel;
                        }
                            
                        if (rand.Next((int)pirateSeekerSpawnChance * 2) == 0)
                        {
                            EntityManager.Add(Enemy.CreatePirateSeeker(GetSpawnPosition()));
                            ++GameSessionStats.TotalEnemiesSpawnedInLevel;
                        }
                            

                        break;

                    case 3://Level 3

                        if (GameSessionStats.BossStatus == false) //Ensure only one boss is spawned
                        {
                            EntityManager.Add(Enemy.CreateGovernmentBoss(GetSpawnPosition()));
                            GameSessionStats.BossStatus = true;
                        }

                        if(GameSessionStats.BossKilled == true)
                        {
                            GameRoot.ResetGameSession();
                            GameSessionStats.CurrentLevel = 1;
                        }

                        if (rand.Next((int)governmentSeekerSpawnChance * 2) == 0)
                        {
                            EntityManager.Add(Enemy.CreateGovernmentSeeker(GetSpawnPosition()));
                        }
                            
                        if (rand.Next((int)pirateSeekerSpawnChance * 2) == 0)
                        {
                            EntityManager.Add(Enemy.CreatePirateSeeker(GetSpawnPosition()));
                        }
                            
                        
                        break;
                }


            }

        }

        private static bool ProgressToNextLevel()
        {
            //Progress to next level if required kill count has been reached
            if (GameSessionStats.NumberOfKills == GameSessionStats.NumberOfKillsRequired)
            {
                ++GameSessionStats.CurrentLevel;
                GameSessionStats.TotalEnemiesSpawnedInLevel = 0;
                return true;
            }
            return false;
            
        }

        private static bool LevelSpawnCapReached()
        {
            //Don't spawn new enemies if there are already enough to kill for next level
            if (GameSessionStats.TotalEnemiesSpawnedInLevel == GameSessionStats.NumberOfKillsRequired)
            {
                return true;
            }
            return false;
        }

        private static Vector2 GetSpawnPosition()
        {
            Vector2 pos;
            do
            {
                pos = new Vector2(rand.Next((int)GameRoot.ScreenSize.X), rand.Next((int)GameRoot.ScreenSize.Y));
            }
            while (Vector2.DistanceSquared(pos, PlayerShip.Instance.Position) < 250 * 250);

            return pos;
        }

        public static void Reset()
        {
            governmentSeekerSpawnChance = 60;
            pirateSeekerSpawnChance = 60;
        }
    }
}
