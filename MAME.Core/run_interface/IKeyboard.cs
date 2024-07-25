namespace MAME.Core.run_interface
{
    public interface IKeyboard
    {
        bool IsPressed(Key key);
        bool IsTriggered(Key key);
    }
}
