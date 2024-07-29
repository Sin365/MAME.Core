namespace MAME.Core.run_interface
{
    public interface IResources
    {
        byte[] Get_sfix();
        byte[] Get__000_lo();
        byte[] Get_sm1();
        byte[] Get_mainbios();
        byte[] Get_pgmmainbios();
        byte[] Get_pgmvideobios();
        byte[] Get_pgmaudiobios();
        byte[] Get_mcu();
        string Get_mame_xml();
    }
}
