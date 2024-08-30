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
    public class WomanSprite{
        //The texture for the idle woman
        private Texture2D idleTexture;

        //Animation timer for the woman
        private double animationTimer;

        //The current frame of the woman
        private short animationFrame = 1;

        /// <summary>
        /// The position of the WomanSprite
        /// </summary>
        public Vector2 Position = new Vector2(140, 300);

        /// <summary>
        /// Loads the content for the ManSprite
        /// </summary>
        /// <param name="content">The content manager to load with</param>
        public void LoadContent(ContentManager content){
            idleTexture = content.Load<Texture2D>("WomanIdle");
        }

        /// <summary>
        /// The draw method for the ManSprite
        /// </summary>
        /// <param name="gameTime">The current gameTime of the game</param>
        /// <param name="spriteBatch">The spritebatch to utilize when drawing</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if(animationTimer >= 0.3){
                animationFrame++;
                if(animationFrame > 5) animationFrame = 1;
                animationTimer -= 0.3;
            }

            var source = new Rectangle(animationFrame * 128, 0, 128, 128);

            spriteBatch.Draw(idleTexture, Position, source, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }


    }
}