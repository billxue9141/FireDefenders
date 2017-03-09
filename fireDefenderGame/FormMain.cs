﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fireDefenderGame
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// number of rows in the game
        /// </summary>
        static int ROW = 15;

        /// <summary>
        /// number of columns in the game
        /// </summary>
        static int COL = 15;

        /// <summary>
        /// maximum allowed ticks per sec
        /// </summary>
        static int MAX_TICK_PER_SEC = 50;

        /// <summary>
        /// minimum allowed ticks per sec
        /// </summary>
        static int MIN_TICK_PER_SEC = 2;

        /// <summary>
        /// initial ticks per sec
        /// </summary>
        static int INITIAL_TICK_PER_SEC = 10;

        /// <summary>
        /// length of each rectangle
        /// </summary>
        int length;

        //graphics variables
        Graphics terrainLayer;
        Rectangle r;

        //fps counter
        int currentSecond = DateTime.Now.Second;
        int ticksPerSec = 0;
        int totalTicks = 0;
        int initialTicksPerSec;

        //game running?
        bool isRunning = true;

        //game objects
        GameBoard gameBoard;

        public FormMain()
        {
            InitializeComponent();
            length = (mapPanel.Height) / ROW;
            mapPanel.Paint += new PaintEventHandler(mapPanel_paint);
            this.MinimumSize = new Size(800, 600);
            gameBoard = new GameBoard(ROW, COL);
            initialTicksPerSec = INITIAL_TICK_PER_SEC;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Show();
            this.Focus();

            terrainLayer = this.CreateGraphics();
            StartGameLoop();

        }

        private void StartGameLoop()
        {
            //create & start the tick timer
            Timer t = new Timer();
            int prevTick = 0, currTick;
            startTimer(t);

            while (isRunning)
            {
                //keep app responsive ok
                Application.DoEvents();
                currTick = ticksPerSec;
                // 1. check user input
                updateCursor();
                // 2. check AI
                // 3. update object data (position, status etc.)
                if (currTick != prevTick)
                {
                    //update all objects (fire for now)
                }
                // 4. check triggers and conditions
                // 5. draw graphics
                mapPanel.Update();
                // 6. sound effects & music

                // 7. update tick counter
                t.Interval = 1000 / initialTicksPerSec;
                prevTick = ticksPerSec;

            }
        }

        private void updateCursor()
        {
            Point point = Control.MousePosition;
            labelmouseXDisplay.Text = point.X.ToString();
            labelMouseYDisplay.Text = point.Y.ToString();
        }


        private void startTimer(Timer t)
        {
            t.Interval = 1000 / initialTicksPerSec; // specify interval time as you want
            t.Tick += new EventHandler(timer_Tick);
            t.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (currentSecond == DateTime.Now.Second && isRunning)
            {
                ticksPerSec = ticksPerSec + 1;
                totalTicks++;
                labelTotalTicksDisplay.Text = totalTicks.ToString();
            }
            else
            {
                labelTicksPerSec.Text = ticksPerSec.ToString();
                ticksPerSec = 0;
                currentSecond = DateTime.Now.Second;
            }
        }

        private void mapPanel_paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            terrainLayer = e.Graphics;

            terrainLayer.FillRectangle(new SolidBrush(Color.FromArgb(0, Color.Black)), p.DisplayRectangle);

            int length = p.Height / ROW;

            for (int i = 0; i < COL; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    r = new Rectangle(i * length, j * length, length, length);

                    try
                    {
                        Image newImage = Image.FromFile(gameBoard.board[i, j].terrain.getImageDebugLocation());
                        terrainLayer.DrawImage(newImage, r);
                    }
                    catch (Exception)
                    {
                        //if image file can not be found, print tile with green brush.
                        terrainLayer.FillRectangle(Brushes.Green, r);
                    }


                    if (gameBoard.board[i, j].fire != null)
                    {
                        Image newImage = Image.FromFile("../../resources/Structure/medievalStructure_20.png");
                        terrainLayer.DrawImage(newImage, r);
                    }

                    //boardGraphicsObject.DrawRectangle(Pens.Black, r);
                }
            }
        }


        private void buttonSpeedUp_Click(object sender, EventArgs e)
        {
            if (initialTicksPerSec < MAX_TICK_PER_SEC)
                initialTicksPerSec++;
        }

        private void buttonSlowDown_Click(object sender, EventArgs e)
        {
            if (initialTicksPerSec > MIN_TICK_PER_SEC)
                initialTicksPerSec--;
        }
    }
}
