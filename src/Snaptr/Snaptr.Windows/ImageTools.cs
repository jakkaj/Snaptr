using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Snaptr.Windows
{
    public static class ImageTools
    {
        public static byte[] ToJpegData(this BitmapSource bm)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.QualityLevel = 85;
            // byte[] bit = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Frames.Add(BitmapFrame.Create(bm));
                encoder.Save(stream);
                byte[] bit = stream.ToArray();
                stream.Close();
                return stream.ToArray();
            }            
        }
    }
}
