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
using System.Diagnostics;

namespace WhatDoWeDoNow.Screens.MainScreen
{
    class Dead
    {
        private Texture2D Head1;
        private Texture2D Head2;
        private Texture2D Head3;
        private Texture2D Head4;
        private Texture2D Comments;
        private Vector2 HeadPosition;
        private Vector2 CommentsPosition;
        private List<string[]> Zadania;
        private SpriteFont mySpriteFont;
        private String label = "CLICK ME";
        private int i = 0;

        public Dead(ContentManager _content)
        {
            Head1 = _content.Load<Texture2D>("smierc1");
            Head2 = _content.Load<Texture2D>("smierc2");
            Head3 = _content.Load<Texture2D>("smierc3");
            Head4 = _content.Load<Texture2D>("smierc4");
            Comments = _content.Load<Texture2D>("comments");
            mySpriteFont = _content.Load<SpriteFont>("MySpriteFont");
            HeadPosition = new Vector2(1070,20);
            CommentsPosition = new Vector2(1060, 20 + Head1.Height);
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
        private String parseText(String text, int width)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (mySpriteFont.MeasureString(line + word).Length() > width)
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + ' ';
            }

            return returnString + line;
        }
        private int licznik = 0;
        private int licznik2 = 0;
        public void Draw(SpriteBatch spriteBatch)
        {

            if (View)
            {
                licznik++;
                if (licznik < 10) spriteBatch.Draw(Head1, HeadPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                else if (licznik < 20) spriteBatch.Draw(Head2, HeadPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                else if (licznik < 30) spriteBatch.Draw(Head3, HeadPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                else if (licznik < 40) spriteBatch.Draw(Head4, HeadPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                if (licznik == 39 && licznik2 < label.Count()/6)
                {
                    licznik = 0;
                    licznik2++;
                }
                if (licznik2 >= label.Count()/6) spriteBatch.Draw(Head1, HeadPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                spriteBatch.Draw(Comments, CommentsPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
               
                spriteBatch.DrawString(mySpriteFont, parseText(label, 250) , new Vector2(1080, 75 + Head1.Height), Color.Black);
            }
            else
            {
                spriteBatch.Draw(Head1, HeadPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
        bool MouseLeftTemp;
        bool View = false;
        public void Update()
        {
            i = 0;
            foreach(string[] temp in Zadania){
                
                if (temp[0] == "null" && temp[4] == SCREEN_MANAGER.ActiveScreen.Name && View == false)
                {
                    Debug.WriteLine(i);
                    View = true;
                    Zadania[i][0] = "yes";
                    label = Zadania[i][3];
                    break;
                }
                i++;
               // Debug.WriteLine(i);
            }


           // if (View == false)
           // do{
           //     if (i >= Zadania.Count()-1) break;
           //     i++;
           //     label = Zadania[i][3];
           //     if (i >= Zadania.Count() - 1) break;
           //     if (Zadania[i][0] != "null") i++;
           // }
           // while ((Zadania[i][4] != SCREEN_MANAGER.ActiveScreen.Name));
           // if(i != -1 && i<= Zadania.Count()){
           //     View = true;
           //     Zadania[i][0] = "yes";
           // }
           // else View = false;
            if (View)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().X > CommentsPosition.X && Mouse.GetState().Y > CommentsPosition.Y &&
                    Mouse.GetState().X < CommentsPosition.X + Comments.Width && Mouse.GetState().Y < CommentsPosition.Y + Comments.Height)
                {
                    MouseLeftTemp = true;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Released && MouseLeftTemp)
                {
                    MouseLeftTemp = false;
                    View = false;
                    i = 0;
                    licznik2 = 0;
                    licznik = 0;
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("Zadania.txt"))
                    {
                        foreach (string[] line in Zadania)
                        {
                          
                                file.WriteLine(line[0]+";"+line[1]+";"+line[2]+";"+line[3]+";"+line[4]);
                            
                        }
                    }

                }

            }
        }
    }
}
