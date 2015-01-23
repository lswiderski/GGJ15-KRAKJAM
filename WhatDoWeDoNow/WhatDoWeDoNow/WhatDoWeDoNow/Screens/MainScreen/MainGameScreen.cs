using System;
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
        private Player player;
        public MainGameScreen(GraphicsDevice device, ContentManager _content)
            : base(device, _content, "MainGame")
        {

        }

        public override bool Init()
        {
            var r =base.Init();
            background = content.Load<Texture2D>("room");
            camera.Pos = new Vector2(1366/2,786/2);
            player = new Player(content);
            return r;
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void Draw(GameTime gameTime)
        {
            device.Clear(Color.Brown);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                     BlendState.AlphaBlend,
                     null,
                     null,
                     null,
                     null,
                     camera.get_transformation(device));
            
           // player.Draw(gameTime, spriteBatch, new Vector2(20, 20), SpriteEffects.None);
            spriteBatch.Draw(background, new Rectangle(0,0,1048,786), new Rectangle(0,0,1366,786), Color.White);
            player.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            // Check if n is pressed and go to screen2
            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                SCREEN_MANAGER.goto_screen("screen1");
            }
            base.Update(gameTime);
        }
    }
}
