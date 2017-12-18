using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootShapesUp
{
    class HeadsUpDisplay : GameRoot
    {
        static string NumberOfKillsLabel = "Total Kills: ";
        static string NumberOfLivesLabel = "Lives Remaining: ";
        static string TotalTimeElapsedLabel = "Timer: ";

        static string CurrentLevelLabel = "Level: ";

        static Vector2 NumberOfKillsLocation = new Vector2(50, 30);
        static Vector2 TotalTimeElapsedLocation = new Vector2(graphics.PreferredBackBufferWidth / 2 - NumberOfLivesLabel.Length, 30);
        static Vector2 NumberOfLivesLocation = new Vector2(graphics.PreferredBackBufferWidth - 200, 30);
        static Vector2 CurrentLevelLocation = new Vector2(graphics.PreferredBackBufferWidth / 2 - CurrentLevelLabel.Length, graphics.PreferredBackBufferHeight - 30);

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, NumberOfKillsLabel + GameSessionStats.NumberOfKills, NumberOfKillsLocation, Color.White);
            spriteBatch.DrawString(Font, NumberOfLivesLabel  + GameSessionStats.NumberOfLives, NumberOfLivesLocation, Color.White);
            spriteBatch.DrawString(Font, TotalTimeElapsedLabel  + GameSessionStats.StopWatch.Elapsed.Seconds, TotalTimeElapsedLocation, Color.White);

            spriteBatch.DrawString(Font, CurrentLevelLabel + GameSessionStats.CurrentLevel, CurrentLevelLocation, Color.White);
        }

    }
}
