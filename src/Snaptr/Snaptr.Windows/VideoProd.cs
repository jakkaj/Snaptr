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
        public async Task<bool> Produce(int scale = 720)
        {
            var rootFolder = @"C:\Users\jorkni\Documents\temp\snapz";
            

            var paramsTemplate =
                "-vcodec mjpeg -framerate {0} -i img_%05d.jpg -c:v libx264 -pix_fmt yuv420p output.mp4";

            var param = string.Format(paramsTemplate, 5);

            _runFf(param, rootFolder);

            _runFf(
                "-y -i \"output.mp4\" -vf fps=5,scale=320:-1:flags=lanczos,palettegen palette.png",
                rootFolder);

            _runFf(
                $"-i \"output.mp4\" -i palette.png -filter_complex \"fps=10,scale={scale}:-1:flags=lanczos[x];[x][1:v]paletteuse\" output.gif",
                rootFolder);


            var dir = new DirectoryInfo(rootFolder);

            foreach (var f in dir.GetFiles("*.jpg"))
            {
                f.Delete();
            }


            return true;
        }

        void _runFf(string param, string rootFolder)
        {
            var ffMpeg = @"C:\tools\ffmpeg\ffmpeg-20170411-f1d80bc-win64-static\bin";

            if (ffMpeg.ToLower().IndexOf(".exe") == -1)
            {
                ffMpeg = Path.Combine(ffMpeg, "ffmpeg.exe");
            }

            var si = new ProcessStartInfo
            {
                FileName = ffMpeg,
                Arguments = param,
                WorkingDirectory = rootFolder
            };

            var p = System.Diagnostics.Process.Start(si);
            p.WaitForExit();

        }
    }
}
