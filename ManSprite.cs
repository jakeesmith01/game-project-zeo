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
    public class ManSprite{
        //The texture for the idle man
        private Texture2D idleTexture;

        //The texture for the attacking man
        private Texture2D atkTexture;

        //Animation timer for the man
        private double animationTimer;

        //The current frame of the man
        private short animationFrame = 1;

        //Determines if the man is attacking in this frame or not
        private bool isAttacking = false;

        //Roughly how long it takes to get through the attack animation based off an animationTimer of 0.3 seconds and 5 frames of the animation.
        private const double atkAnimationTime = 1.5;

        //The current time of the attack animation
        private double atkTimer;

        /// <summary>
        /// The position of the ManSprite 
        /// </summary>
        public Vector2 Position = new Vector2(240, 300);

        /// <summary>
        /// Loads the content for the ManSprite
        /// </summary>
        /// <param name="content">The content manager to load with</param>
        public void LoadContent(ContentManager content){
            idleTexture = content.Load<Texture2D>("Man/ManIdle");
            atkTexture = content.Load<Texture2D>("Man/ManAttack");
        }

        /// <summary>
        /// Updates the ManSprite, manages the switching of animations
        /// </summary>
        /// <param name="gameTime">The current gametime</param>
        public void Update(GameTime gameTime){
            if(isAttacking) {
                atkTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if(atkTimer > atkAnimationTime){
                    isAttacking = false;
                    atkTimer = 0;
                }
            }
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
                if(animationFrame > 4) animationFrame = 1;

                animationTimer -= 0.3;
            }

            var source = new Rectangle(animationFrame * 128, 0, 128, 128);

            //if attacking, draw the attacking texture
            if(isAttacking) {
                spriteBatch.Draw(atkTexture, Position, source, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            else{
                spriteBatch.Draw(idleTexture, Position, source, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        /// Method to initiate the attacking animation of the ManSprite
        /// </summary>
        public void Attack(){
            isAttacking = true;
            atkTimer = 0;
        }

    }
}