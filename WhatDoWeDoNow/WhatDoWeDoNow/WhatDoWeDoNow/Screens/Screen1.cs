using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WhatDoWeDoNow.ScreenManager;

namespace WhatDoWeDoNow.Screens
{
    class Screen1 : Screen
    {
        private Texture2D testTexture2D;
        private Animation testAnimation;
        private AnimationPlayer player;
        public Screen1(GraphicsDevice device, ContentManager _content)
            : base(device,_content, "screen1")
        {
        }

        public override bool Init()
        {
            testTexture2D = content.Load<Texture2D>("testpng");
            testAnimation = new Animation(content.Load<Texture2D>("testanimation"),0.2f,true,30);
            player.PlayAnimation(testAnimation);

            return base.Init();
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void Draw(GameTime gameTime)
        {
            device.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                     BlendState.AlphaBlend,
                     null,
                     null,
                     null,
                     null,
                     camera.get_transformation(device));
            player.Draw(gameTime,spriteBatch,new Vector2(20,20),SpriteEffects.None );
            spriteBatch.Draw(testTexture2D,Vector2.Zero,Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camera.Move(new Vector2(-5,0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camera.Move(new Vector2(5,0));
            }
            
            // Check if m is pressed and go to screen2
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                SCREEN_MANAGER.goto_screen("screen2");
            }
            base.Update(gameTime);
        }
    }
}
