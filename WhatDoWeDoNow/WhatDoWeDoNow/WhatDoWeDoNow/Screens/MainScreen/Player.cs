using System;
using System.Collections.Generic;
using System.Linq;
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
             walkingLeft,
             walkingRight,
             stop
         }

         private PLAYER_STATE playerState;
         private Texture2D walkingAnimationTexture;
         private Texture2D stopTexture;
         private AnimationPlayer player;
         private Animation walkingAnimation;
         private Vector2 position;

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
             walkingAnimationTexture = _content.Load<Texture2D>("testanimation");
             walkingAnimation = new Animation(walkingAnimationTexture,0.2f,true,30);
             player.PlayAnimation(walkingAnimation);
             stopTexture = _content.Load<Texture2D>("testpng");
             position = new Vector2(300,300);
         }
        public void Initialize()
        {
            throw new NotImplementedException();
        }

         public void Move(Vector2 v)
         {
             position += v;
         }
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            if (playerState == PLAYER_STATE.walkingLeft)
            {
                player.Draw(gameTime,spriteBatch,position,SpriteEffects.None );
            }
            else if (playerState == PLAYER_STATE.walkingRight)
            {
                player.Draw(gameTime, spriteBatch, position, SpriteEffects.FlipVertically);
            }
            else if(playerState == PLAYER_STATE.stop)
            {
                spriteBatch.Draw(stopTexture,position,Color.White);
            }

            switch (playerState)
            {
                    case PLAYER_STATE.walkingLeft:
                    Move(new Vector2(-5,0));
                    break;
                    case PLAYER_STATE.walkingRight:
                    Move(new Vector2(5, 0));
                    break;
                    case PLAYER_STATE.stop:
                    break;
            }
        }

         public void Update(GameTime gameTime)
         {
             if (Keyboard.GetState().IsKeyDown(Keys.Left))
             {
                 playerState = PLAYER_STATE.walkingLeft;
             }
             else if (Keyboard.GetState().IsKeyDown(Keys.Right))
             {
                 playerState = PLAYER_STATE.walkingRight;
             }
             else
             {
                 playerState = PLAYER_STATE.stop;
             }
         }

    }
}
