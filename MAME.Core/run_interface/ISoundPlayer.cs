namespace MAME.Core.run_interface
{
    public interface ISoundPlayer
    {
        void BufferWirte(int Off, byte[] Data);
        void SetVolume(int Vol);
        void GetCurrentPosition(out int play_position, out int write_position);
    }
}
