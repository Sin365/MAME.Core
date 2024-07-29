namespace MAME.Core.run_interface
{
    public interface IKeyboard
    {
        bool IsPressed(Corekey key);
        bool IsTriggered(Corekey key);
    }
}
