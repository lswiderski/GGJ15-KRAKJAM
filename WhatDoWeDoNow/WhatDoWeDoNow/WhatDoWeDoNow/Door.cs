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
        private Texture2D texture;
        public Rectangle BoundingBox;
        private string nextRoom;
        public bool IsOpen;
        private PLAYER_ENTER_FROM doorPosType;
        public Door(Rectangle rec, string destroom, PLAYER_ENTER_FROM type)
        {
            BoundingBox = rec;
            nextRoom = destroom;
            IsOpen = false;
            switch (type)
            {
                case PLAYER_ENTER_FROM.Down:
                    doorPosType = PLAYER_ENTER_FROM.Up;
                    break;
                case PLAYER_ENTER_FROM.Up:
                    doorPosType = PLAYER_ENTER_FROM.Down;
                    break;
                case PLAYER_ENTER_FROM.Left:
                    doorPosType = PLAYER_ENTER_FROM.Right;
                    break;
                case PLAYER_ENTER_FROM.Right:
                   doorPosType = PLAYER_ENTER_FROM.Left;
                    break;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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
