using System.Drawing;

namespace MAME.Core.run_interface
{
    public interface IVideoPlayer
    {
        void SubmitVideo(Bitmap Bitmap);
    }
}
