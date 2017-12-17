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
        static float governmentSeekerSpawnChance = 60;
        static float pirateSeekerSpawnChance = 60;

        public static void Update()
        {
            if (!PlayerShip.Instance.IsDead && EntityManager.Count < 200)
            {
                if (rand.Next((int)governmentSeekerSpawnChance) == 0)
                    EntityManager.Add(Enemy.CreateGovernmentSeeker(GetSpawnPosition()));
                if (rand.Next((int)pirateSeekerSpawnChance) == 0)
                    EntityManager.Add(Enemy.CreatePirateSeeker(GetSpawnPosition()));
            }

            // slowly increase the spawn rate as time progresses
            if (governmentSeekerSpawnChance > 20)
                governmentSeekerSpawnChance -= 0.005f;
            if (pirateSeekerSpawnChance > 20)
                pirateSeekerSpawnChance -= 0.005f;
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
            inverseSpawnChance = 60;
        }
    }
}
