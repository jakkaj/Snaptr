using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snaptr.Windows
{
    public class VideoProd
    {
        public async Task<bool> Produce()
        {
            var rootFolder = @"C:\Users\jorkni\Documents\temp\snapz";

            var ffMpeg = @"C:\tools\ffmpeg\ffmpeg-20170411-f1d80bc-win64-static\bin";

            if (ffMpeg.ToLower().IndexOf(".exe") == -1)
            {
                ffMpeg = Path.Combine(ffMpeg, "ffmpeg.exe");
            }

            var paramsTemplate =
                "-vcodec mjpeg -framerate {0} -i img_%03d.jpg -c:v libx264 -pix_fmt yuv420p output.mp4";

            var param = string.Format(paramsTemplate, 5);

            var si = new ProcessStartInfo
            {
                FileName = ffMpeg,
                Arguments = param,
                WorkingDirectory = rootFolder
            };

            System.Diagnostics.Process.Start(si);

            return true;
        }
    }
}
