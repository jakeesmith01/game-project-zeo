using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Threading;

namespace gameproj1{
    public class CarSprite{
        //The texture for the car
        private Texture2D carTexture;

        //The animation timer for the car
        private double animationTimer;

        //The current frame of the car
        private short animationFrame = 1;

        //The position of the car
        public Vector2 Position = new Vector2(30, 182);

        /// <summary>
        /// Loads the content for the car
        /// </summary>
        /// <param name="content">The content manager to load with</param>
        public void LoadContent(ContentManager content){
            carTexture = content.Load<Texture2D>("CarIdle");
        }

        /// <summary>
        /// Draws the car sprite
        /// </summary>
        /// <param name="gameTime">The current game time</param>
        /// <param name="spriteBatch">The spritebatch to use for drawing</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if(animationTimer >= 0.3){
                animationFrame++;
                if(animationFrame > 2){
                    animationFrame = 1;
                }
                animationTimer = 0;
            }

            spriteBatch.Draw(carTexture, Position, new Rectangle(animationFrame * 256, 0, 256, 256), Color.White);
        }
    }
}