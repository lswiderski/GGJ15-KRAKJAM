using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WhatDoWeDoNow.Screens.MainScreen;

namespace WhatDoWeDoNow.ScreenManager
{
    public class Screen
    {
        protected GraphicsDevice device = null;
        protected ContentManager content = null;
        protected SpriteBatch spriteBatch;
        protected Camera2d camera;
        protected bool Done;

        protected Player player;
        private Dead dead;

        protected List<Door> doors;

        protected Texture2D blackoverlay;
        protected float overlaylevel;

        /// <summary>
        /// Screen Constructor
        /// </summary>
        /// <param name="name">Must be unique since when you use ScreenManager is per name</param>
        public Screen(GraphicsDevice _device, ContentManager _content, string name)
        {

            Name = name;
            this.device = _device;
            content = _content;
            Done = false;

        }

        ~Screen()
        {
        }

        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Virtual Function that's called when entering a Screen
        /// override it and add your own initialization code
        /// </summary>
        /// <returns></returns>
        public virtual bool Init()
        {
            spriteBatch = new SpriteBatch(device);
            camera = new Camera2d();
            doors = new List<Door>();
            dead = new Dead(content);
            doors.Add(new Door(new Rectangle(120, 300, 30, 200), "Room1", PLAYER_ENTER_FROM.Left,content));
            doors.Add(new Door(new Rectangle(390, 100, 270, 30), "Room2", PLAYER_ENTER_FROM.Up,content));
            doors.Add(new Door(new Rectangle(890, 300, 30, 200), "Room3", PLAYER_ENTER_FROM.Right,content));
            doors.Add(new Door(new Rectangle(390, 640, 270, 30), "Room4", PLAYER_ENTER_FROM.Down,content));
            player = new Player(content);
            switch (Game1.PlayerEnterFrom)
            {
                case PLAYER_ENTER_FROM.Down:
                    player.Position = new Vector2(420, 445);
                    doors[3].IsOpen = true;
                    break;
                case PLAYER_ENTER_FROM.Up:
                    player.Position = new Vector2(420, -105);
                    doors[1].IsOpen = true;
                    break;
                case PLAYER_ENTER_FROM.Left:
                    player.Position = new Vector2(55, 215);
                    doors[0].IsOpen = true;
                    break;
                case PLAYER_ENTER_FROM.Right:
                    player.Position = new Vector2(786, 190);
                    doors[2].IsOpen = true;
                    break;

            }
            return true;
        }

        /// <summary>
        /// Virtual Function that's called when exiting a Screen
        /// override it and add your own shutdown code
        /// </summary>
        /// <returns></returns>
        public virtual void Shutdown()
        {
        }

        /// <summary>
        /// Override it to have access to elapsed time
        /// </summary>
        /// <param name="elapsed">GameTime</param>
        public virtual void Update(GameTime gameTime)
        {
            var dr = doors.ElementAt(0);
            var dr2 = doors.ElementAt(1);
            var dr3 = doors.ElementAt(2);
            var dr4 = doors.ElementAt(3);

            if (player.BoundingBox.Y > dr.BoundingBox.Y &&
                player.BoundingBox.Y < dr.BoundingBox.Y + dr.BoundingBox.Height &&
                player.BoundingBox.X < dr.BoundingBox.X + dr.BoundingBox.Width)
            {
                if (dr.IsOpen)
                {
                    overlaylevel = 1 - (float)(player.BoundingBox.X - dr.BoundingBox.X) / 30;
                }

            }
            else if (player.BoundingBox.Y > dr3.BoundingBox.Y &&
                player.BoundingBox.Y < dr3.BoundingBox.Y + dr3.BoundingBox.Height &&
                player.BoundingBox.X + player.BoundingBox.Width > dr3.BoundingBox.X)
            {
                if (dr3.IsOpen)
                {
                    var x = (float)(player.BoundingBox.X + player.BoundingBox.Width - dr3.BoundingBox.X) / 30;
                    overlaylevel = x;
                }
            }
            else if (player.BoundingBox.X > dr2.BoundingBox.X &&
                player.BoundingBox.X < dr2.BoundingBox.X + dr2.BoundingBox.Width &&
                player.BoundingBox.Y < dr2.BoundingBox.Y + dr2.BoundingBox.Height)
            {
                if (dr2.IsOpen)
                {
                    overlaylevel = 1 - (float)(player.BoundingBox.Y - dr2.BoundingBox.Y) / 30;
                }
            }
            else if (player.BoundingBox.X > dr4.BoundingBox.X &&
                 player.BoundingBox.X < dr4.BoundingBox.X + dr4.BoundingBox.Width &&
                 player.BoundingBox.Y > dr4.BoundingBox.Y)
            {
                if (dr4.IsOpen)
                {
                    overlaylevel = (float)(player.BoundingBox.Y - dr4.BoundingBox.Y) / 25;
                }
            }

            else
            {
                overlaylevel = 0;
            }
            if (overlaylevel >= 1)
            {
                foreach (var door in doors)
                {
                    if (player.BoundingBox.Intersects(door.BoundingBox))
                    {
                        door.Go();
                    }
                }
            }
            foreach (var door in doors)
            {
                door.Update(gameTime);
            }
            dead.Update();
            if (dead.flag)
            {
                Done = true;
                foreach (var door in doors)
                {
                    door.IsOpen = true;
                }

            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            dead.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
