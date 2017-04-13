using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snaptr.Windows
{
    public class Snapper
    {
        public async Task DoSnapz(Window window)
        {
            int count = 0;

            while (true)
            {
                var result = CopyScreen(window);

                var jpg = result.ToJpegData();

                File.WriteAllBytes($@"C:\Users\jorkni\Documents\temp\snapz\IMG_{count.ToString("D3")}.jpg", jpg);
                count++;
                await Task.Delay(200);
            }
        }

        public BitmapSource CopyScreen(Window window)
        {
            var size = _getNativePrimaryScreenSize(window);

            size = new System.Drawing.Size(1000, 1000);

            using (var screenBmp = new Bitmap(
                size.Width,
                size.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = Graphics.FromImage(screenBmp))
                {
                    bmpGraphics.CopyFromScreen(200, 200, 0, 0, screenBmp.Size);
                    return Imaging.CreateBitmapSourceFromHBitmap(
                        screenBmp.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }

        private System.Drawing.Size _getNativePrimaryScreenSize(Window window)
        {
            PresentationSource mainWindowPresentationSource = PresentationSource.FromVisual(window);
            Matrix m = mainWindowPresentationSource.CompositionTarget.TransformToDevice;
            var dpiWidthFactor = m.M11;
            var dpiHeightFactor = m.M22;
            double screenHeight = SystemParameters.PrimaryScreenHeight * dpiHeightFactor;
            double screenWidth = SystemParameters.PrimaryScreenWidth * dpiWidthFactor;

            return new System.Drawing.Size((int)screenWidth, (int)screenHeight);
        }
    }
}
