﻿using System;
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
        public Door(Rectangle rec, string destroom)
        {
            BoundingBox = rec;
            nextRoom = destroom;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawDebug(spriteBatch);
        }

        public void Go()
        {
            SCREEN_MANAGER.goto_screen(nextRoom);
        }
        void DrawDebug(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(BoundingBox,Color.Red);
        }

    }
}
