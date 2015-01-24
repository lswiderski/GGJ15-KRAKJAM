using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace WhatDoWeDoNow.Screens.MainScreen
{
    class Dead
    {
        private Texture2D Head;
        private Texture2D Comments;
        private Vector2 HeadPosition;
        private Vector2 CommentsPosition;
        private List<string[]> Zadania;

        public Dead(ContentManager _content)
        {
            Head = _content.Load<Texture2D>("smierc");
            Comments = _content.Load<Texture2D>("comments");
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
            spriteBatch.Draw(Comments, CommentsPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);


        }
        public void Update()
        {
          //  if()
        }
    }
}
