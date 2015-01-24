using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WhatDoWeDoNow.ScreenManager;

namespace WhatDoWeDoNow
{
    public class Door
    {
        private Texture2D openTexture;
        private Texture2D closeTexture;
        public Rectangle BoundingBox;
        private string nextRoom;
        public bool IsOpen;
        private PLAYER_ENTER_FROM doorPosType;
        private Vector2 origin;
        private float rotate;
        public Door(Rectangle rec, string destroom, PLAYER_ENTER_FROM type,Texture2D close, Texture2D open)
        {
            BoundingBox = rec;
            nextRoom = destroom;
            IsOpen = false;
            openTexture = open;
            closeTexture = close;
            switch (type)
            {
                case PLAYER_ENTER_FROM.Down:
                    rotate = MathHelper.ToRadians(180);
                    doorPosType = PLAYER_ENTER_FROM.Up;
                    break;
                case PLAYER_ENTER_FROM.Up:
                    rotate = MathHelper.ToRadians(0);
                    doorPosType = PLAYER_ENTER_FROM.Down;
                    break;
                case PLAYER_ENTER_FROM.Left:
                    rotate = MathHelper.ToRadians(270);
                    doorPosType = PLAYER_ENTER_FROM.Right;
                    break;
                case PLAYER_ENTER_FROM.Right:
                    rotate = MathHelper.ToRadians(90);
                   doorPosType = PLAYER_ENTER_FROM.Left;
                    break;
            }
            
            origin = new Vector2(closeTexture.Width/2, closeTexture.Height/2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsOpen)
                spriteBatch.Draw(openTexture, BoundingBox, null, Color.White, rotate, origin, SpriteEffects.None, 0);
            else
            {
                spriteBatch.Draw(closeTexture, BoundingBox, null, Color.White, rotate, origin, SpriteEffects.None, 0);
            }
            DrawDebug(spriteBatch);
        }

        public void Go()
        {
            if (IsOpen)
            {
                Game1.PlayerEnterFrom = doorPosType;
                SCREEN_MANAGER.goto_screen(nextRoom); 
            }
               
        }
        void DrawDebug(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(BoundingBox,Color.Red);
        }

    }
}
