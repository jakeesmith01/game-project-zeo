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
    public class ZombieSprite{

        //The texture for the walking zombie
        private Texture2D walkTexture;

        //The texture for the dying zombie
        private Texture2D dieTexture;

        //Animation timer for the zombie
        private double animationTimer;

        //The current frame of the zombie
        private short animationFrame = 1;

        //The current frame of the die animation
        private short dieFrame = 1;

        //Determines how long it has been since the last frame of the dying animation was shown (to slow it down)
        private double delayTimer;

        //Roughly how long it takes to get through the die animation based off an animationTimer of 0.3 seconds and 8 frames of the animation.
        private const double dieTime = 1.5;

        //The current time of the die animation
        private double dieTimer;

        /// <summary>
        /// Determines if the zombie is dead or not
        /// </summary>
        public bool IsDead = false;

        /// <summary>
        /// The position of the Zombie sprite
        /// </summary>
        public Vector2 Position = new Vector2(800, 330);

        /// <summary>
        /// Loads the content of the ZombieSprite
        /// </summary>
        /// <param name="content">The content manager to load with</param>
        public void LoadContent(ContentManager content){
            walkTexture = content.Load<Texture2D>("Zombie/ZombieWalk");
            dieTexture = content.Load<Texture2D>("Zombie/ZombieDie");
        }

        /// <summary>
        /// Updates the ZombieSprite by moving them to the left, unless they're dead
        /// </summary>
        /// <param name="gameTime">The current game time of the game</param>
        public void Update(GameTime gameTime){
            if(IsDead) { return; } //if already dead, dont bother updating

            //otherwise continue moving the zombie to the left towards the man
            Position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Draws the ZombieSprite, either as the walking texture or the dying texture depending on the state of the game
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        /// <param name="spriteBatch">The sprite batch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if(animationTimer >= 0.3){
                animationFrame++;
                if(animationFrame > 7) animationFrame = 1;

                animationTimer -= 0.3;
            }

            var source = new Rectangle(animationFrame * 96, 0, 96, 96);

            //if dead, draw the dying texture
            if(IsDead) {
                dieTimer += gameTime.ElapsedGameTime.TotalSeconds;

                if(dieTimer >= 0.2){ //Stretches the animation out so it doesnt happen quite so fast
                    dieFrame++;
                    dieTimer -= 0.2;
                }

                var src = new Rectangle(dieFrame * 96, 0, 96, 96);

                //if the die animation is done, delay and reset
                if(dieFrame < 4){
                    spriteBatch.Draw(dieTexture, Position, src, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                }else{
                    delayTimer += gameTime.ElapsedGameTime.TotalSeconds;

                    //Delays the zombie from disappearing for a second so the player can see the death animation
                    if(delayTimer >= 1.0){ 
                        Reset();
                    } else {
                        spriteBatch.Draw(dieTexture, Position, new Rectangle(4 * 96, 0, 96, 96), Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                    }
                }
            }
            else { //else draw the walking texture
                spriteBatch.Draw(walkTexture, Position, source, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            }
        }

        /// <summary>
        /// 'Kills' the zombie to trigger the death animation
        /// </summary>
        public void KillZombie(){
            IsDead = true;
            dieTimer = 0;
        }

        /// <summary>
        /// 'Respawns' the zombie by resetting it to default values
        /// </summary>
        public void Reset(){
            IsDead = false;
            dieTimer = 0;
            dieFrame = 0;
            animationFrame = 0;
            animationTimer = 0;
            delayTimer = 0;
            Position = new Vector2(800, 330);
        }
    }
}