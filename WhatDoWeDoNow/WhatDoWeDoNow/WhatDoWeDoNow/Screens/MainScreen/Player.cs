using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WhatDoWeDoNow.Screens.MainScreen
{
     public class Player : IGameComponent
     {
         private enum PLAYER_STATE
         {
             run,
             stop
         }

         private PLAYER_STATE playerState;
        // private Texture2D walkingAnimationTexture;
         private Texture2D stopTexture;
         private Texture2D Ludek1;
         private Texture2D Ludek2;
         //private AnimationPlayer player;
         //private Animation walkingAnimation;
         private Vector2 position;
         private Vector2 origin;
         public Rectangle BoundingBox { get { return new Rectangle((int)position.X + (stopTexture.Width / 2 - 30),(int)position.Y+ (stopTexture.Height - 40), 60, 20); } }
         public Vector2 Position
         {
             get
             {
                 return position;
             }
             set { position = value; }
         }

         public Player(ContentManager _content)
         {
             //walkingAnimationTexture = _content.Load<Texture2D>("testanimation");
             //walkingAnimation = new Animation(walkingAnimationTexture,0.2f,true,30);
             //player.PlayAnimation(walkingAnimation);
             stopTexture = _content.Load<Texture2D>("Ludek");
             Ludek1 = _content.Load<Texture2D>("Ludek1");
             Ludek2 = _content.Load<Texture2D>("Ludek2");
             position = new Vector2(300,300);
             origin = new Vector2(stopTexture.Width/2, stopTexture.Height);
         }
        public void Initialize()
        {
            throw new NotImplementedException();
        }

         public void Move(Vector2 v)
         {
             position += v;
         }
         int licznik = 0;
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
           //if (playerState == PLAYER_STATE.walkingLeft)
           //{
           //    //player.Draw(gameTime,spriteBatch,position,SpriteEffects.None );
           //}
           //else if (playerState == PLAYER_STATE.walkingRight)
           //{
           //    //player.Draw(gameTime, spriteBatch, position, SpriteEffects.FlipVertically);
           //}
           // else 
            if (playerState == PLAYER_STATE.stop)
            {
                spriteBatch.Draw(stopTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            else
            {
                licznik++;
                if (licznik < 12) spriteBatch.Draw(Ludek1, position, null, Color.White, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0f);
                else if (licznik < 24) spriteBatch.Draw(Ludek2, position, null, Color.White, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0f);
                if (licznik == 23)
                {
                    licznik = 0;
                }
            }
            DrawDebug(spriteBatch);
            
        }

         public void DrawDebug(SpriteBatch spriteBatch)
         {
             spriteBatch.DrawRectangle(BoundingBox,Color.Green);
         }

         public void Update(GameTime gameTime)
         {
             if (Keyboard.GetState().IsKeyDown(Keys.Left))
             {  
                 playerState = PLAYER_STATE.run;
                 if(position.X+origin.X>Game1.MinXPosition)
                 Move(new Vector2(-5,0));
             }
             else if (Keyboard.GetState().IsKeyDown(Keys.Right))
             {
                 playerState = PLAYER_STATE.run;
                 if (position.X + origin.X < Game1.MaxXPosition)
                 Move(new Vector2(5, 0));
             }
             if (Keyboard.GetState().IsKeyDown(Keys.Up))
             {
                 playerState = PLAYER_STATE.run;
                 if (position.Y + origin.Y > Game1.MinYPosition)
                 Move(new Vector2(0,-5));
             }
             else if (Keyboard.GetState().IsKeyDown(Keys.Down))
             {
                 playerState = PLAYER_STATE.run;
                 if (position.Y + origin.Y < Game1.MaxYPosition)
                 Move(new Vector2(0,5));
             }
             else if (!Keyboard.GetState().IsKeyDown(Keys.Left) && !Keyboard.GetState().IsKeyDown(Keys.Right))
             {
                 playerState = PLAYER_STATE.stop;
             }
         }

    }
}
