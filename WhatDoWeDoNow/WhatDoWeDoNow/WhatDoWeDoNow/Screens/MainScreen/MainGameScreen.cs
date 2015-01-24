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
        private Dead dead;
        private List<Door> doors;
        private KeyboardState newState;
        private KeyboardState oldState;

        private Texture2D blackoverlay;
        private float overlaylevel;
        public MainGameScreen(GraphicsDevice device, ContentManager _content)
            : base(device, _content, "MainGame")
        {

        }

        public override bool Init()
        {
            var r =base.Init();
            background = content.Load<Texture2D>("room");
            blackoverlay = content.Load<Texture2D>("blackoverlay");
            camera.Pos = new Vector2(1366/2,786/2);
            player = new Player(content);
            dead = new Dead(content);
            player.Position = new Vector2(500,400);
            doors = new List<Door>();
            doors.Add(new Door(new Rectangle(250,400,30,100),"Room1"));
            doors.Add(new Door(new Rectangle(450, 150, 100, 30), "Room2"));
            doors.Add(new Door(new Rectangle(800, 400, 30, 100), "Room3"));
            doors.Add(new Door(new Rectangle(450, 600, 100, 30), "Room4"));
            oldState = Keyboard.GetState();
            overlaylevel = 0f;
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

            foreach (var door in doors)
            {
                door.Draw(spriteBatch);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                foreach (var door in doors)
                {
                    door.IsOpen = true;
                }
                
            }
            player.Draw(gameTime, spriteBatch);
            dead.Draw(spriteBatch);
            spriteBatch.Draw(blackoverlay,new Rectangle(0,0,1048,786),blackoverlay.Bounds,new Color(0,0,0,overlaylevel) );
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();
            player.Update(gameTime);
            if (newState.IsKeyDown(Keys.Space) && !oldState.IsKeyDown(Keys.Space))
            {
                foreach (var door in doors)
                {
                    if (player.BoundingBox.Intersects(door.BoundingBox))
                    {
                        door.Go();
                    }
                }
            }
            var dr = doors.ElementAt(0);
            var dr2 = doors.ElementAt(1);
            var dr3 = doors.ElementAt(2);
            if (player.Position.Y> dr.BoundingBox.Y &&
                player.Position.Y< dr.BoundingBox.Y + dr.BoundingBox.Height &&
                player.Position.X< dr.BoundingBox.X + dr.BoundingBox.Width )
            {
                overlaylevel = 1- (player.Position.X- dr.BoundingBox.X)/30;
            }
            else if (player.Position.Y> dr3.BoundingBox.Y &&
                player.Position.Y< dr3.BoundingBox.Y + dr3.BoundingBox.Height &&
                player.Position.X+player.BoundingBox.Width> dr3.BoundingBox.X)
            {
                overlaylevel = (player.Position.X + player.BoundingBox.Width - dr3.BoundingBox.X) / 30;
            }
            else if (player.Position.X> dr2.BoundingBox.X &&
                player.Position.X< dr2.BoundingBox.X + dr2.BoundingBox.Width &&
                player.Position.Y< dr2.BoundingBox.Y + dr2.BoundingBox.Height)
            {
                overlaylevel = 1 - (player.Position.Y - dr2.BoundingBox.Y) / 30;
            }
            else
            {
                overlaylevel = 0;
            }
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
