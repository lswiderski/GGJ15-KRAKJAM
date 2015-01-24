﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WhatDoWeDoNow.ScreenManager;

namespace WhatDoWeDoNow.Screens.MainScreen
{
    class MainGameScreen : Screen
    {
        private Texture2D background;
        
        private KeyboardState newState;
        private KeyboardState oldState;
        private bool inited = false;
        public MainGameScreen(GraphicsDevice device, ContentManager _content)
            : base(device, _content, "MainGame")
        {

        }

        public override bool Init()
        {
            var r = base.Init();
            background = content.Load<Texture2D>("room");
            blackoverlay = content.Load<Texture2D>("blackoverlay");
            camera.Pos = new Vector2(1366 / 2, 786 / 2);
            player = new Player(content);
            
            player.Position = new Vector2(420, 210);
            foreach (var door in doors)
            {
                door.IsOpen = true;
            }
            inited = false;

            oldState = Keyboard.GetState();
            overlaylevel = 0f;
            if (Done)
            {
                foreach (var door in doors)
                {
                    door.IsOpen = true;
                }
            }
            return r;
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void Draw(GameTime gameTime)
        {
            device.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                     BlendState.AlphaBlend,
                     null,
                     null,
                     null,
                     null,
                     camera.get_transformation(device));

            // player.Draw(gameTime, spriteBatch, new Vector2(20, 20), SpriteEffects.None);
            spriteBatch.Draw(background, new Rectangle(0,0,1048,786), Color.White);

            foreach (var door in doors)
            {
                door.Draw(spriteBatch);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                Done = true;
                foreach (var door in doors)
                {
                    door.IsOpen = true;
                }

            }
            player.Draw(gameTime, spriteBatch);

            spriteBatch.Draw(blackoverlay, new Rectangle(0, 0, 1048, 786), blackoverlay.Bounds, new Color(0, 0, 0, overlaylevel));
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();
            player.Update(gameTime);

           
            // Check if n is pressed and go to screen2
            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                SCREEN_MANAGER.goto_screen("screen1");
            }
            base.Update(gameTime);
            oldState = newState;
        }
    }
}