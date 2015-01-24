using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WhatDoWeDoNow.ScreenManager;

namespace WhatDoWeDoNow
{
    public class Door
    {
        private Texture2D open1Texture;
        private Texture2D open2Texture;
        private Texture2D open3Texture;
        public Rectangle BoundingBox;
        private string nextRoom;
        private bool isOpen = false;
        private bool play = false;
        private int frame = 0;
        private int licznik = 0;

        public bool IsOpen
        {
            get
            {
                return isOpen;
            }
            set
            {
                isOpen = value;
                if (value = true)
                {
                    play = true;
                }
            }
        }

        private PLAYER_ENTER_FROM doorPosType;
        private Vector2 origin;
        private float rotate;
        public Door(Rectangle rec, string destroom, PLAYER_ENTER_FROM type, ContentManager content)
        {
            BoundingBox = rec;
            nextRoom = destroom;
            IsOpen = false;
            play = false;

            if (type == PLAYER_ENTER_FROM.Down || type == PLAYER_ENTER_FROM.Up)
            {
                open1Texture = content.Load<Texture2D>("doorUp1");
                open2Texture = content.Load<Texture2D>("doorUp2");
                open3Texture = content.Load<Texture2D>("doorUp3");
            }
            if (type == PLAYER_ENTER_FROM.Left || type == PLAYER_ENTER_FROM.Right)
            {
                open1Texture = content.Load<Texture2D>("doorLeft1");
                open2Texture = content.Load<Texture2D>("doorLeft2");
                open3Texture = content.Load<Texture2D>("doorLEft3");
            }
            switch (type)
            {
                case PLAYER_ENTER_FROM.Down:
                    rotate = MathHelper.ToRadians(180);
                    doorPosType = PLAYER_ENTER_FROM.Up;
                    break;
                case PLAYER_ENTER_FROM.Up:
                    rotate = MathHelper.ToRadians(0);
                    doorPosType = PLAYER_ENTER_FROM.Down;
                    break;
                case PLAYER_ENTER_FROM.Left:
                    rotate = MathHelper.ToRadians(270);
                    doorPosType = PLAYER_ENTER_FROM.Right;
                    break;
                case PLAYER_ENTER_FROM.Right:
                    rotate = MathHelper.ToRadians(90);
                   doorPosType = PLAYER_ENTER_FROM.Left;
                    break;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsOpen)
            {
                switch (frame)
                {
                    case 1:
                        if(doorPosType == PLAYER_ENTER_FROM.Up)
                        spriteBatch.Draw(open1Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One,
                            SpriteEffects.FlipVertically, 0f);
                        else if (doorPosType == PLAYER_ENTER_FROM.Left)
                        {
                            spriteBatch.Draw(open1Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One,
                            SpriteEffects.FlipHorizontally, 0f);
                        }
                        else
                        {
                            spriteBatch.Draw(open1Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One,
                            SpriteEffects.None, 0f);
                        }
                        break;
                    case 2:
                        if (doorPosType == PLAYER_ENTER_FROM.Up)
                            spriteBatch.Draw(open2Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One,
                                SpriteEffects.FlipVertically, 0f);
                        else if (doorPosType == PLAYER_ENTER_FROM.Left)
                        {
                            spriteBatch.Draw(open2Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One,
                            SpriteEffects.FlipHorizontally, 0f);
                        }
                        else
                        {
                            spriteBatch.Draw(open2Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One,
                            SpriteEffects.None, 0f);
                        }
                        break;
                    case 3:
                        if (doorPosType == PLAYER_ENTER_FROM.Up)
                            spriteBatch.Draw(open3Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One,
                                SpriteEffects.FlipVertically, 0f);
                        else if (doorPosType == PLAYER_ENTER_FROM.Left)
                        {
                            spriteBatch.Draw(open3Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One,
                            SpriteEffects.FlipHorizontally, 0f);
                        }
                        else
                        {
                            spriteBatch.Draw(open3Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One,
                            SpriteEffects.None, 0f);
                        }
                        break;
                    default:
                        break;
                }
                
            }
                
          
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

        public void Update(GameTime gameTime)
        {
            if (play)
            {
                licznik++;
                if (licznik < 20)
                {
                    frame = 1;
                }
                else if (licznik < 40 && licznik > 20)
                {
                    frame = 2;
                }
                else if (licznik > 40)
                {
                    frame = 3;
                }
            }
        }
    }
}
