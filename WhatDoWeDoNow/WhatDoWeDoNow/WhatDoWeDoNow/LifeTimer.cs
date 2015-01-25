using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WhatDoWeDoNow
{
    public class LifeTimer
    {
        private int baseLevel;
        public float currentLevel;

        public LifeTimer()
        {
            baseLevel = 100 *1000;
            currentLevel = baseLevel;
        }
        public void Update(GameTime gameTime)
        {
            currentLevel -= gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}
