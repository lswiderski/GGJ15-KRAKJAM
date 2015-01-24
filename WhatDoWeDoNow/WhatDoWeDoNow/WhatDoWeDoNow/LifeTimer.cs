using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WhatDoWeDoNow
{
    class LifeTimer
    {
        private int baseLevel;
        private float currentLevel;

        public LifeTimer()
        {
            baseLevel = 60;
            currentLevel = baseLevel;
        }
        public void Update(GameTime gameTime)
        {
            currentLevel -= gameTime.ElapsedGameTime.Seconds;
            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                int x = 0;
            }
        }
    }
}
