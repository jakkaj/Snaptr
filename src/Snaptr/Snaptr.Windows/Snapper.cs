﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snaptr.Windows
{
    public class Snapper
    {
        Timer timer = null;

        Window _window;

        int count = 0;

        public Snapper(Window window)
        {
            _window = window;
            timer = new Timer(_snap);
        }
        public void DoSnapz()
        {
            var rootFolder = @"C:\Users\jorkni\Documents\temp\snapz";
            var dir = new DirectoryInfo(rootFolder);

            foreach (var f in dir.GetFiles())
            {
                f.Delete();
            }
            timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(200));
        }

        public void Stop()
        {
            timer.Change(Int32.MaxValue, Int32.MaxValue);
        }

        void _snap(object state)
        {
            _window.Dispatcher.Invoke(() =>
            {
                var result = CopyScreen(_window);

                var jpg = result.ToJpegData();

                File.WriteAllBytes($@"C:\Users\jorkni\Documents\temp\snapz\IMG_{count.ToString("D5")}.jpg", jpg);
            count++;
            });
            
        }

        public BitmapSource CopyScreen(Window window, bool fs = false)
        {
            var size = fs ? _getNativePrimaryScreenSize(window) : _getNativeWindowSize(window);

            var (x, y) = _getWindowPos(window);

            var offsetX = fs ? 0 : x;
            var offsetY = fs ? 0 : y;
            //size = new System.Drawing.Size(1000, 1000);

            using (var screenBmp = new Bitmap(
                size.Width,
                size.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = Graphics.FromImage(screenBmp))
                {
                    bmpGraphics.CopyFromScreen(offsetX, offsetY, 0, 0, screenBmp.Size);
                    return Imaging.CreateBitmapSourceFromHBitmap(
                        screenBmp.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }

        (int x, int y) _getWindowPos(Window window)
        {
            var (dpiHeightFactor, dpiWidthFactor) = _getFactors(window);
            return ((int)(window.Left * dpiWidthFactor),
                (int)(window.Top * dpiHeightFactor));
        }

        (double dpiHeight, double dpiWidth) _getFactors(Window window)
        {
            PresentationSource mainWindowPresentationSource = PresentationSource.FromVisual(window);
            Matrix m = mainWindowPresentationSource.CompositionTarget.TransformToDevice;
            var dpiWidthFactor = m.M11;
            var dpiHeightFactor = m.M22;

            return (dpiHeightFactor, dpiWidthFactor);
        }

        private System.Drawing.Size _getNativeWindowSize(Window window)
        {
            var (dpiHeightFactor, dpiWidthFactor) = _getFactors(window);

            return new System.Drawing.Size(
                (int)(window.ActualWidth * dpiWidthFactor), 
                (int)(window.ActualHeight * dpiWidthFactor));
        }

        private System.Drawing.Size _getNativePrimaryScreenSize(Window window)
        {
            var (dpiHeightFactor, dpiWidthFactor) = _getFactors(window);
            double screenHeight = SystemParameters.PrimaryScreenHeight * dpiHeightFactor;
            double screenWidth = SystemParameters.PrimaryScreenWidth * dpiWidthFactor;

            return new System.Drawing.Size((int)screenWidth, (int)screenHeight);
        }
    }
}
