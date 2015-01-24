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
    class Room1 : Screen
    {
        private Texture2D background;
        private Player player;
        private List<Door> doors;
        private KeyboardState newState;
        private KeyboardState oldState;
        public Room1(GraphicsDevice device, ContentManager _content)
            : base(device, _content, "Room1")
        {

        }

        public override bool Init()
        {
            var r = base.Init();
            background = content.Load<Texture2D>("room1");
            camera.Pos = new Vector2(1366 / 2, 786 / 2);
            player = new Player(content);
            player.Position = new Vector2(500, 400);
            doors = new List<Door>();
            doors.Add(new Door(new Rectangle(250, 400, 30, 100), "Room1"));
            doors.Add(new Door(new Rectangle(450, 150, 100, 30), "Room2"));
            doors.Add(new Door(new Rectangle(800, 400, 30, 100), "Room3"));
            doors.Add(new Door(new Rectangle(450, 600, 100, 30), "Room4"));
            oldState = Keyboard.GetState();
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
            spriteBatch.Draw(background, new Rectangle(0, 0, 1048, 786), new Rectangle(0, 0, 1366, 786), Color.White);

            foreach (var door in doors)
            {
                door.Draw(spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);
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

            base.Update(gameTime);
            oldState = newState;
        }
    }
}
