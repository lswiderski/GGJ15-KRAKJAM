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
        private Texture2D Button;
        private Vector2 Button1;
        private Vector2 Button2;
        private Vector2 Button3;
        private Vector2 Button4;
        private Vector2 HeadPosition;
        private Vector2 CommentsPosition;
        private List<string[]> Zadania;
        private SpriteFont mySpriteFont;
        private String[] label = {"CLICK ME","false","","","","",""};
        private int i = 0;
        public bool flag = false;
        public bool odpdone = false;
        private bool goodanswer = false;

        public bool GoodAnswer
        {
            get
            {
                return goodanswer;
            }
            set
            {
                goodanswer = value;
                if (value == false)
                {
                   
                }
            }
        }

        public Dead(ContentManager _content)
        {
            Head1 = _content.Load<Texture2D>("smierc1");
            Head2 = _content.Load<Texture2D>("smierc2");
            Head3 = _content.Load<Texture2D>("smierc3");
            Head4 = _content.Load<Texture2D>("smierc4");
            Button = _content.Load<Texture2D>("Button");
            Button1 = new Vector2(1070, 405);
            Button2 = new Vector2(1070, 465);
            Button3 = new Vector2(1070, 525);
            Button4 = new Vector2(1070, 585);
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
                if (licznik == 39 && licznik2 < label[0].Count()/6)
                {
                    licznik = 0;
                    licznik2++;
                }
                if (licznik2 >= label[0].Count()/6) spriteBatch.Draw(Head1, HeadPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                spriteBatch.Draw(Comments, CommentsPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
               
                spriteBatch.DrawString(mySpriteFont, parseText(label[0], 250) , new Vector2(1080, 75 + Head1.Height), Color.Black);
                if (label[1] == "true")
                {
                    spriteBatch.Draw(Button,Button1,Color.White);
                    spriteBatch.Draw(Button, Button2, Color.White);
                    spriteBatch.Draw(Button, Button3, Color.White);
                    spriteBatch.Draw(Button, Button4, Color.White);

                    spriteBatch.DrawString(mySpriteFont, label[2], new Vector2(1090, 420), Color.Black);
                    spriteBatch.DrawString(mySpriteFont, label[3] ,new Vector2(1090, 480), Color.Black);
                    spriteBatch.DrawString(mySpriteFont, label[4] ,new Vector2(1090, 540), Color.Black);
                    spriteBatch.DrawString(mySpriteFont, label[5] , new Vector2(1090, 600), Color.Black);

                }
            }
            else
            {
                spriteBatch.Draw(Head1, HeadPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                if (odpdone)
                {
                    flag = true;
                    spriteBatch.Draw(Comments, CommentsPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                    spriteBatch.DrawString(mySpriteFont, parseText("Prawidłowa odpowiedz to nr. " + label[6] + ". Wybierz kolejny pokój.", 250), new Vector2(1080, 75 + Head1.Height), Color.Black);
                
                    Color c1,c2,c3,c4;
                    if(label[6] == "1")
                        c1= Color.Green;
                    else
                        c1= Color.Red;
                    spriteBatch.Draw(Button, Button1, c1);
                    if(label[6] == "2")
                        c2 = Color.Green;
                    else
                        c2 = Color.Red;
                    spriteBatch.Draw(Button, Button2,c2);
                    if(label[6] == "3")
                        c3 = Color.Green;
                    else
                        c3 = Color.Red;
                    spriteBatch.Draw(Button, Button3, c3);
                    if (label[6] == "4")
                        c4 = Color.Green;
                    else
                        c4 = Color.Red;
                    spriteBatch.Draw(Button, Button4, c4);

                    spriteBatch.DrawString(mySpriteFont, label[2], new Vector2(1090, 420), Color.Black);
                    spriteBatch.DrawString(mySpriteFont, label[3], new Vector2(1090, 480), Color.Black);
                    spriteBatch.DrawString(mySpriteFont, label[4], new Vector2(1090, 540), Color.Black);
                    spriteBatch.DrawString(mySpriteFont, label[5], new Vector2(1090, 600), Color.Black);
                }

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
                    View = true;
                    Zadania[i][0] = "yes";
                    label[0] = Zadania[i][3];
                    label[1] = Zadania[i][1];
                    if (label[1] == "true")
                    {
                        label[2] = Zadania[i][5];
                        label[3] = Zadania[i][6];
                        label[4] = Zadania[i][7];
                        label[5] = Zadania[i][8];
                        label[6] = Zadania[i][2];
                        odpdone = true;
                    }
                    break;
                }
                i++;
               // Debug.WriteLine(i);
            }


            if (View && label[1] == "false")
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

                            file.WriteLine(line[0] + ";" + line[1] + ";" + line[2] + ";" + line[3] + ";" + line[4] + ";" + line[5] + ";" + line[6] + ";" + line[7] + ";" + line[8]);
                            
                        }
                    }

                }

            }
            if (View && label[1] == "true")
            {

                if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().X > Button1.X && Mouse.GetState().Y > Button1.Y &&
                    Mouse.GetState().X < Button1.X + Button.Width && Mouse.GetState().Y < Button1.Y + Button.Height)
                {
                    if (label[6] == "1")
                    {
                        GoodAnswer = true;
                    }
                    else GoodAnswer = false;
                    MouseLeftTemp = true;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().X > Button2.X && Mouse.GetState().Y > Button2.Y &&
                   Mouse.GetState().X < Button2.X + Button.Width && Mouse.GetState().Y < Button2.Y + Button.Height)
                {

                    if (label[6] == "2")
                    {
                        GoodAnswer = true;
                    }
                    else GoodAnswer = false;
                    MouseLeftTemp = true;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().X > Button3.X && Mouse.GetState().Y > Button3.Y &&
                   Mouse.GetState().X < Button3.X + Button.Width && Mouse.GetState().Y < Button3.Y + Button.Height)
                {

                    if (label[6] == "3")
                    {
                        GoodAnswer = true;
                    }
                    else GoodAnswer = false;
                    MouseLeftTemp = true;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().X > Button4.X && Mouse.GetState().Y > Button4.Y &&
                   Mouse.GetState().X < Button4.X + Button.Width && Mouse.GetState().Y < Button4.Y + Button.Height)
                {

                    if (label[6] == "4")
                    {
                        GoodAnswer = true;
                    }
                    else GoodAnswer = false;
                    MouseLeftTemp = true;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Released && MouseLeftTemp)
                {
                    if (!GoodAnswer)
                    {
                        Game1.timer.currentLevel -= (20f * 1000);
                    }
                    MouseLeftTemp = false;
                    View = false;
                    i = 0;
                    licznik2 = 0;
                    licznik = 0;
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("Zadania.txt"))
                    {
                        foreach (string[] line in Zadania)
                        {

                            file.WriteLine(line[0] + ";" + line[1] + ";" + line[2] + ";" + line[3] + ";" + line[4] + ";" + line[5] + ";" + line[6] + ";" + line[7] + ";" + line[8]);
               
                        }
                    }
               
                }

            }
        }
    }
}
