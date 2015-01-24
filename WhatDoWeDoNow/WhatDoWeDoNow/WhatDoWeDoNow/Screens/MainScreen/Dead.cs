using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework.Input;
using WhatDoWeDoNow.ScreenManager;

namespace WhatDoWeDoNow.Screens.MainScreen
{
    class Dead
    {
        private Texture2D Head;
        private Texture2D Comments;
        private Vector2 HeadPosition;
        private Vector2 CommentsPosition;
        private List<string[]> Zadania;
        private SpriteFont mySpriteFont;
        private String label = "sdf";
        private int i = 0;

        public Dead(ContentManager _content)
        {
            Head = _content.Load<Texture2D>("smierc1");
            Comments = _content.Load<Texture2D>("comments");
            mySpriteFont = _content.Load<SpriteFont>("MySpriteFont");
            HeadPosition = new Vector2(1070,20);
            CommentsPosition = new Vector2(1060, 20 + Head.Height);
            Zadania = new List<string[]>();
            using (var stream = TitleContainer.OpenStream("Zadania.txt"))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (reader.Peek() >= 0)
                    Zadania.Add(reader.ReadLine().Split(';'));
                }
            }
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Head, HeadPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            if (View)
            {
                spriteBatch.Draw(Comments, CommentsPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(mySpriteFont, label + i, new Vector2(1070, 70 + Head.Height), Color.Black);
            }
        }
        bool MouseLeftTemp;
        bool View = true;
        public void Update()
        {
            if (View) { 
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().X > CommentsPosition.X && Mouse.GetState().Y > CommentsPosition.Y &&
                    Mouse.GetState().X < CommentsPosition.X+ Comments.Width && Mouse.GetState().Y < CommentsPosition.Y + Comments.Height)
                {
                    MouseLeftTemp = true;      
                }
                if (Mouse.GetState().LeftButton == ButtonState.Released && MouseLeftTemp)
                {
                    MouseLeftTemp = false;
                    label = Zadania[i][3];
                    i++;
                }
                
            }
            if (Zadania[i][4] == SCREEN_MANAGER.ActiveScreen.Name)
            {
                View = true;
            }
            else View = false;
        }
    }
}
