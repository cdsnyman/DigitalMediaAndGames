using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootShapesUp
{
    class Enemy : Entity
    {
        public static Random rand = new Random();

        private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();
        private int timeUntilStart = 60;
        public bool IsActive { get { return timeUntilStart <= 0; } }
        public int HealthPoints = 1;
        public bool BossFlag = false;

        public static Enemy instance;

        public Enemy(Texture2D image, Vector2 position)
        {
            this.image = image;
            instance = this;
            Position = position;
            Radius = image.Width / 2f;
            color = Color.Transparent;
        }

        public static Enemy CreateGovernmentSeeker(Vector2 position)
        {
            var enemy = new Enemy(GameRoot.GovernmentSeeker, position);
            enemy.AddBehaviour(enemy.FollowPlayer(1f));
            enemy.HealthPoints = 10;

            return enemy;
        }

        internal static Entity CreateGovernmentBoss(Vector2 position)
        {
            var enemy = new Enemy(GameRoot.PirateBoss, position);
            enemy.AddBehaviour(enemy.FollowPlayer(1f));
            enemy.HealthPoints = 200;
            enemy.BossFlag = true;

            return enemy;
        }

        public static Enemy CreatePirateSeeker(Vector2 position)
        {
            var enemy = new Enemy(GameRoot.PirateSeeker, position);
            enemy.AddBehaviour(enemy.FollowPlayer(1.4f));
            enemy.HealthPoints = 6;

            return enemy;
        }

        public override void Update()
        {
            if (timeUntilStart <= 0)
                ApplyBehaviours();
            else
            {
                timeUntilStart--;
                color = Color.White * (1 - timeUntilStart / 60f);
            }

            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, GameRoot.ScreenSize - Size / 2);

            Velocity *= 0.8f;
        }

        

        private void AddBehaviour(IEnumerable<int> behaviour)
        {
            behaviours.Add(behaviour.GetEnumerator());
        }

        private void ApplyBehaviours()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                if (!behaviours[i].MoveNext())
                    behaviours.RemoveAt(i--);
            }
        }

        public void HandleCollision(Enemy other)
        {
            var d = Position - other.Position;
            Velocity += 10 * d / (d.LengthSquared() + 1);
        }

        public void WasShot()
        {
            --HealthPoints;
            if (HealthPoints == 0)
            {
                IsExpired = true;
                GameRoot.Explosion.Play(0.5f, rand.NextFloat(-0.2f, 0.2f), 0);
                GameSessionStats.NumberOfKills++;
                if (BossFlag == true)// Check used to see if a boss was killed/ end the level
                {
                    GameSessionStats.BossKilled = true;
                }
            }

        }

        public void WasCrashedInto()
        {
            if (BossFlag == false)
            {
                IsExpired = true;
                GameRoot.Explosion.Play(0.5f, rand.NextFloat(-0.2f, 0.2f), 0);
                --GameSessionStats.TotalEnemiesSpawnedInLevel; //Ensure player death does not count all despawned enemies as a spawn without a kill
            }

        }

        #region Behaviours
        IEnumerable<int> FollowPlayer(float acceleration = 1f)
        {
            while (true)
            {
                if (!PlayerShip.Instance.IsDead)
                    Velocity += (PlayerShip.Instance.Position - Position) * (acceleration / (PlayerShip.Instance.Position - Position).Length());

                if (Velocity != Vector2.Zero)
                    Orientation = Velocity.ToAngle();

                yield return 0;
            }
        }
        #endregion
    }
}
