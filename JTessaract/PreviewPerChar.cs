﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace JTessaract
{
    public partial class PreviewPerChar : UserControl
    {
        private bool mouseDown = false;
        private Image mainImage = null;
        private float offsetX = 0;
        private float offsetY = 0;
        private float scale = 1;
        private float oldOffsetX = 0;
        private float oldOffsetY = 0;
        private float startOffsetX = 0;
        private float startOffsetY = 0;
        private float oldBoxX = 0;
        private float oldBoxY = 0;
        private float startBoxX = 0;
        private float startBoxY = 0;
        private HotspotStatus hotspotStatus = HotspotStatus.NONE;
        private float hotspotWidth = 4;

        private float previewCharHeight = 10.0f;

        public float PreviewCharHeight
        {
            get { return previewCharHeight; }
            set { previewCharHeight = value; }
        }

        public Image MainImage
        {
            get { return mainImage; }
            set
            {
                mainImage = value;
                scale = 1;
                CenterBox();
            }
        }
        private Box currentBox = null;

        public Box CurrentBox
        {
            get { return currentBox; }
            set
            {
                if (value != null)
                {
                    currentBox = value;

                    if (Math.Abs(currentBox.Y2 - currentBox.Y1) > previewCharHeight)
                    {
                        // character should be 85% of the preview height
                        scale = 0.85f * Height / Math.Abs(currentBox.Y2 - currentBox.Y1);
                    }
                    else
                    {
                        scale = 0.85f * Height / previewCharHeight;
                    }

                    scale = 1 / scale;

                    offsetX = -(currentBox.X2 + currentBox.X1) / (2.0f * scale) + Width / 2.0f;
                    offsetY = 0.075f * Height - (mainImage.Height - currentBox.Y2) / scale;
                }
            }
        }

        public PreviewPerChar()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(Preview_MouseWheel);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        void Preview_MouseWheel(object sender, MouseEventArgs e)
        {
            //if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            //{
            //    float oldScale;
            //    oldScale = scale;

            //    if (e.Delta > 0)
            //    {
            //        scale *= 0.95f;
            //    }
            //    else if (e.Delta < 0)
            //    {
            //        scale *= 1.05f;
            //    }

            //    offsetX = e.X - (e.X - offsetX) * (oldScale / scale);
            //    offsetY = e.Y - (e.Y - offsetY) * (oldScale / scale);

            //    this.Invalidate();
            //}
        }

        private void Preview_MouseDown(object sender, MouseEventArgs e)
        {
            //mouseDown = true;
            //if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            //{
            //    oldOffsetX = offsetX;
            //    oldOffsetY = offsetY;
            //    startOffsetX = e.X;
            //    startOffsetY = e.Y;
            //}
            //else
            //{

            //    hotspotStatus = (HotspotStatus)IsOnHotspot(e.X, e.Y);

            //    if (hotspotStatus == HotspotStatus.ON_HOTSPOT_1)
            //    {
            //        startBoxX = e.X;
            //        startBoxY = e.Y;
            //        oldBoxX = currentBox.X1;
            //        oldBoxY = currentBox.Y1;
            //    }
            //    else if (hotspotStatus == HotspotStatus.ON_HOTSPOT_2)
            //    {
            //        startBoxX = e.X;
            //        startBoxY = e.Y;
            //        oldBoxX = currentBox.X2;
            //        oldBoxY = currentBox.Y2;
            //    }
            //    this.Invalidate();
            //}
        }

        private void Preview_MouseUp(object sender, MouseEventArgs e)
        {
            //mouseDown = false;
            //if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            //{

            //}
            //else
            //{
            //    hotspotStatus = (HotspotStatus)IsOnHotspot(e.X, e.Y);
            //    this.Invalidate();
            //}

        }

        private void Preview_MouseMove(object sender, MouseEventArgs e)
        {
            //if (mouseDown)
            //{
            //    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            //    {
            //        offsetX = oldOffsetX + (e.X - startOffsetX);
            //        offsetY = oldOffsetY + (e.Y - startOffsetY);

            //        this.Invalidate();
            //    }
            //    else
            //    {
            //        if (hotspotStatus == HotspotStatus.ON_HOTSPOT_1)
            //        {
            //            currentBox.X1 = (int)Math.Round(oldBoxX + (e.X - startBoxX) * scale);
            //            currentBox.Y1 = (int)Math.Round(oldBoxY - (e.Y - startBoxY) * scale);
            //        }
            //        else if (hotspotStatus == HotspotStatus.ON_HOTSPOT_2)
            //        {
            //            currentBox.X2 = (int)Math.Round(oldBoxX + (e.X - startBoxX) * scale);
            //            currentBox.Y2 = (int)Math.Round(oldBoxY - (e.Y - startBoxY) * scale);
            //        }
            //        this.Invalidate();
            //    }
            //}
        }

        private void Preview_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void CenterBox()
        {
            if (mainImage != null)
            {
                offsetX = (this.Width - mainImage.Width) / (scale * 2.0f);
                offsetY = (this.Height - mainImage.Height) / (scale * 2.0f);
            }
        }

        private void Preview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            

            if (currentBox != null)
            {
                g.SetClip(new RectangleF(offsetX + currentBox.X1 / scale, offsetY + (mainImage.Height - currentBox.Y2) / scale, (currentBox.X2 - currentBox.X1) / scale, (currentBox.Y2 - currentBox.Y1) / scale));

                if (mainImage != null)
                {
                    g.DrawImage(mainImage, offsetX, offsetY, mainImage.Width / scale, mainImage.Height / scale);
                }

                //Pen nonSelectedPen = new Pen(Color.Red, 1.0f);
                //Pen selectedPen = new Pen(Color.Blue, 1.2f);
                //Pen fillPen = new Pen(Color.FromArgb(50, Color.Blue));

                //g.DrawRectangle(selectedPen, offsetX + currentBox.X1 / scale, offsetY + (mainImage.Height - currentBox.Y2) / scale, (currentBox.X2 - currentBox.X1) / scale, (currentBox.Y2 - currentBox.Y1) / scale);

                //if (hotspotStatus == HotspotStatus.ON_HOTSPOT_1)
                //{
                //    g.DrawRectangle(nonSelectedPen, offsetX + currentBox.X1 / scale - hotspotWidth, offsetY + (mainImage.Height - currentBox.Y1) / scale - hotspotWidth, 2 * hotspotWidth, 2 * hotspotWidth);
                //}
                //else
                //{
                //    g.DrawRectangle(selectedPen, offsetX + currentBox.X1 / scale - hotspotWidth, offsetY + (mainImage.Height - currentBox.Y1) / scale - hotspotWidth, 2 * hotspotWidth, 2 * hotspotWidth);
                //}

                //if (hotspotStatus == HotspotStatus.ON_HOTSPOT_2)
                //{
                //    g.DrawRectangle(nonSelectedPen, offsetX + currentBox.X2 / scale - hotspotWidth, offsetY + (mainImage.Height - currentBox.Y2) / scale - hotspotWidth, 2 * hotspotWidth, 2 * hotspotWidth);
                //}
                //else
                //{
                //    g.DrawRectangle(selectedPen, offsetX + currentBox.X2 / scale - hotspotWidth, offsetY + (mainImage.Height - currentBox.Y2) / scale - hotspotWidth, 2 * hotspotWidth, 2 * hotspotWidth);
                //}


                //fillPen.Dispose();
                //nonSelectedPen.Dispose();
                //selectedPen.Dispose();
                g.ResetClip();
            }
        }

        private HotspotStatus IsOnHotspot(int x, int y)
        {
            //if (currentBox != null)
            //{
            //    float bottomLeftX = offsetX + currentBox.X1 / scale;
            //    float bottomLeftY = offsetY + (mainImage.Height - currentBox.Y1) / scale;
            //    float topRightX = offsetX + currentBox.X2 / scale;
            //    float topRightY = offsetY + (mainImage.Height - currentBox.Y2) / scale;

            //    if ((Math.Abs(bottomLeftX - x) < hotspotWidth) && (Math.Abs(bottomLeftY - y) < hotspotWidth))
            //    {
            //        return HotspotStatus.ON_HOTSPOT_1;
            //    }

            //    if ((Math.Abs(topRightX - x) < hotspotWidth) && (Math.Abs(topRightY - y) < hotspotWidth))
            //    {
            //        return HotspotStatus.ON_HOTSPOT_2;
            //    }

            //    if ((x >= (bottomLeftX - hotspotWidth)) && (x <= (topRightX + hotspotWidth)) && (y <= (bottomLeftY - hotspotWidth)) && (y >= (topRightY + hotspotWidth)))
            //    {
            //        return HotspotStatus.ON_BOUNDARY;
            //    }
            //}

            return HotspotStatus.NONE; // not on the Hotspot or Boundary
        }
    }
}
