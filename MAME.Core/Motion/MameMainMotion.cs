using mame;
using MAME.Core.run_interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace MAME.Core.Motion
{
    public class MameMainMotion
    {
        public string tsslStatus;
        public CheatMotion cheatmotion;
        public M68000Motion m68000motion;
        public Z80Motion z80motion;
        public M6809Motion m6809motion;
        public CpsMotion cpsmotion;
        public NeogeoMotion neogeomotion;
        public Konami68000Motion konami68000motion;
        public string sSelect;
        public static Thread mainThread;

        public static IResources resource;
        public bool bRom => Machine.bRom;

        public MameMainMotion()
        {
            neogeomotion = new NeogeoMotion();
            cheatmotion = new CheatMotion();
            m68000motion = new M68000Motion();
            m6809motion = new M6809Motion();
            z80motion = new Z80Motion();
            cpsmotion = new CpsMotion();
            konami68000motion = new Konami68000Motion();
        }

        public void Init(
            string RomDir,
            ILog ilog,
            IResources iRes,
            IVideoPlayer ivp,
            ISoundPlayer isp,
            IKeyboard ikb,
            IMouse imou
            )
        {
            Mame.RomRoot = RomDir;
            EmuLogger.BindFunc(ilog);
            Video.BindFunc(ivp);
            Sound.BindFunc(isp);
            resource = iRes;

            sSelect = string.Empty;

            RomInfo.Rom = new RomInfo();
            LoadROMXML();
            Keyboard.InitializeInput(ikb);
            Mouse.InitialMouse(imou);
        }

        private void LoadROMXML()
        {
            XElement xe = XElement.Parse(resource.mame);
            IEnumerable<XElement> elements = from ele in xe.Elements("game") select ele;
            showInfoByElements(elements);
        }

        public Dictionary<string, RomInfo> GetGameList()
        {
            return RomInfo.dictName2Rom;
        }

        public void GetGameScreenSize(out int _width, out int _height,out IntPtr _framePtr)
        {
            //_width = Video.fullwidth;
            //_height = Video.fullheight;
            //_framePtr = Video.bitmapcolor_Ptr;
            _width = Video.width;
            _height = Video.height;
            _framePtr = Video.bitmapcolorRect_Ptr;
        }

        private void showInfoByElements(IEnumerable<XElement> elements)
        {
            RomInfo.romList = new List<RomInfo>();
            RomInfo.dictName2Rom = new Dictionary<string, RomInfo>();
            foreach (var ele in elements)
            {
                RomInfo rom = new RomInfo();
                rom.Name = ele.Attribute("name").Value;
                rom.Board = ele.Attribute("board").Value;
                rom.Parent = ele.Element("parent").Value;
                rom.Direction = ele.Element("direction").Value;
                rom.Description = ele.Element("description").Value;
                rom.Year = ele.Element("year").Value;
                rom.Manufacturer = ele.Element("manufacturer").Value;
                RomInfo.romList.Add(rom);
                RomInfo.dictName2Rom[rom.Name] = rom;
                //loadform.listView1.Items.Add(new ListViewItem(new string[] { rom.Description, rom.Year, rom.Name, rom.Parent, rom.Direction, rom.Manufacturer, rom.Board }));
            }
        }

        public void LoadRom(string Name)
        {
            RomInfo.Rom = RomInfo.GetRomByName(Name);
            if (RomInfo.Rom == null)
            {
                EmuLogger.Log("Not Found");
                return;
            }

            mame.Timer.lt = new List<mame.Timer.emu_timer>();
            sSelect = RomInfo.Rom.Name;
            Machine.mainMotion = this;
            Machine.rom = RomInfo.Rom;
            Machine.sName = Machine.rom.Name;
            Machine.sParent = Machine.rom.Parent;
            Machine.sBoard = Machine.rom.Board;
            Machine.sDirection = Machine.rom.Direction;
            Machine.sDescription = Machine.rom.Description;
            Machine.sManufacturer = Machine.rom.Manufacturer;
            Machine.lsParents = RomInfo.GetParents(Machine.sName);
            int i;
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":

                    Video.nMode = 1;
                    Video.iMode = 2;
                    //Video.nMode = 3;
                    itemSelect();
                    CPS.CPSInit();
                    break;
                case "Data East":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Dataeast.DataeastInit();
                    break;
                case "Tehkan":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Tehkan.PbactionInit();
                    break;
                case "Neo Geo":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Neogeo.NeogeoInit();
                    break;
                case "SunA8":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    SunA8.SunA8Init();
                    break;
                case "Namco System 1":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Namcos1.Namcos1Init();
                    break;
                case "IGS011":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    IGS011.IGS011Init();
                    break;
                case "PGM":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    PGM.PGMInit();
                    break;
                case "M72":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    M72.M72Init();
                    break;
                case "M92":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    M92.M92Init();
                    break;
                case "Taito":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Taito.TaitoInit();
                    break;
                case "Taito B":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Taitob.TaitobInit();
                    break;
                case "Konami 68000":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Konami68000.Konami68000Init();
                    break;
                case "Capcom":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Capcom.CapcomInit();
                    break;
            }
            if (Machine.bRom)
            {
                EmuLogger.Log("MAME.NET: " + Machine.sDescription + " [" + Machine.sName + "]");
                Mame.init_machine();
                Generic.nvram_load();
            }
            else
            {
                EmuLogger.Log("error rom");
            }
        }

        public void StartGame()
        {
            M68000Motion.iStatus = 0;
            M68000Motion.iValue = 0;
            Mame.exit_pending = false;
            MameMainMotion.mainThread = new Thread(Mame.mame_execute);
            MameMainMotion.mainThread.Start();
        }

        public void StopGame()
        {
            if (Machine.bRom)
            {
                Mame.exit_pending = true;
                Thread.Sleep(100);
            }
        }

        private void itemSelect()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 0;
                        Video.width = 512;
                        Video.height = 512;
                    }
                    else if (Video.iMode == 1)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 256;
                        Video.width = 512;
                        Video.height = 256;
                    }
                    else if (Video.iMode == 2)
                    {
                        Video.offsetx = 64;
                        Video.offsety = 272;
                        Video.width = 384;
                        Video.height = 224;
                    }
                    break;
                case "Data East":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 16;
                        Video.width = 256;
                        Video.height = 224;
                    }
                    break;
                case "Tehkan":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 16;
                        Video.width = 256;
                        Video.height = 224;
                    }
                    break;
                case "Neo Geo":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 30;
                        Video.offsety = 16;
                        Video.width = 320;
                        Video.height = 224;
                    }
                    break;
                case "SunA8":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 16;
                        Video.width = 256;
                        Video.height = 224;
                    }
                    break;
                case "Namco System 1":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 73;
                        Video.offsety = 16;
                        Video.width = 288;
                        Video.height = 224;
                    }
                    break;
                case "IGS011":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 0;
                        Video.width = 512;
                        Video.height = 240;
                    }
                    break;
                case "PGM":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 0;
                        Video.width = 448;
                        Video.height = 224;
                    }
                    break;
                case "M72":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 64;
                        Video.offsety = 0;
                        Video.width = 384;
                        Video.height = 256;
                    }
                    break;
                case "M92":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 80;
                        Video.offsety = 8;
                        Video.width = 320;
                        Video.height = 240;
                    }
                    break;
                case "Taito":
                    if (Video.iMode == 0)
                    {
                        switch (Machine.sName)
                        {
                            case "tokio":
                            case "tokioo":
                            case "tokiou":
                            case "tokiob":
                            case "bublbobl":
                            case "bublbobl1":
                            case "bublboblr":
                            case "bublboblr1":
                            case "boblbobl":
                            case "sboblbobl":
                            case "sboblbobla":
                            case "sboblboblb":
                            case "sboblbobld":
                            case "sboblboblc":
                            case "bub68705":
                            case "dland":
                            case "bbredux":
                            case "bublboblb":
                            case "bublcave":
                            case "boblcave":
                            case "bublcave11":
                            case "bublcave10":
                                Video.offsetx = 0;
                                Video.offsety = 16;
                                Video.width = 256;
                                Video.height = 224;
                                break;
                            case "opwolf":
                            case "opwolfa":
                            case "opwolfj":
                            case "opwolfu":
                            case "opwolfb":
                            case "opwolfp":
                                Video.offsetx = 0;
                                Video.offsety = 8;
                                Video.width = 320;
                                Video.height = 240;
                                break;
                        }
                    }
                    break;
                case "Taito B":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 16;
                        Video.width = 320;
                        Video.height = 224;
                    }
                    break;
                case "Konami 68000":
                    if (Video.iMode == 0)
                    {
                        switch (Machine.sName)
                        {
                            case "cuebrick":
                            case "mia":
                            case "mia2":
                            case "tmnt2":
                            case "tmnt2a":
                            case "tmht22pe":
                            case "tmht24pe":
                            case "tmnt22pu":
                            case "qgakumon":
                                Video.offsetx = 104;
                                Video.offsety = 16;
                                Video.width = 304;
                                Video.height = 224;
                                break;
                            case "tmnt":
                            case "tmntu":
                            case "tmntua":
                            case "tmntub":
                            case "tmht":
                            case "tmhta":
                            case "tmhtb":
                            case "tmntj":
                            case "tmnta":
                            case "tmht2p":
                            case "tmht2pa":
                            case "tmnt2pj":
                            case "tmnt2po":
                            case "lgtnfght":
                            case "lgtnfghta":
                            case "lgtnfghtu":
                            case "trigon":
                            case "blswhstl":
                            case "blswhstla":
                            case "detatwin":
                                Video.offsetx = 96;
                                Video.offsety = 16;
                                Video.width = 320;
                                Video.height = 224;
                                break;
                            case "punkshot":
                            case "punkshot2":
                            case "punkshotj":
                            case "glfgreat":
                            case "glfgreatj":
                            case "ssriders":
                            case "ssriderseaa":
                            case "ssridersebd":
                            case "ssridersebc":
                            case "ssridersuda":
                            case "ssridersuac":
                            case "ssridersuab":
                            case "ssridersubc":
                            case "ssridersadd":
                            case "ssridersabd":
                            case "ssridersjad":
                            case "ssridersjac":
                            case "ssridersjbd":
                            case "thndrx2":
                            case "thndrx2a":
                            case "thndrx2j":
                            case "prmrsocr":
                            case "prmrsocrj":
                                Video.offsetx = 112;
                                Video.offsety = 16;
                                Video.width = 288;
                                Video.height = 224;
                                break;
                        }
                    }
                    break;
                case "Capcom":
                    if (Video.iMode == 0)
                    {
                        switch (Machine.sName)
                        {
                            case "gng":
                            case "gnga":
                            case "gngbl":
                            case "gngprot":
                            case "gngblita":
                            case "gngc":
                            case "gngt":
                            case "makaimur":
                            case "makaimurc":
                            case "makaimurg":
                            case "diamond":
                                Video.offsetx = 0;
                                Video.offsety = 16;
                                Video.width = 256;
                                Video.height = 224;
                                break;
                            case "sf":
                            case "sfua":
                            case "sfj":
                            case "sfjan":
                            case "sfan":
                            case "sfp":
                                Video.offsetx = 64;
                                Video.offsety = 16;
                                Video.width = 384;
                                Video.height = 224;
                                break;
                        }
                    }
                    break;
            }
            switch (Machine.sDirection)
            {
                case "":
                case "180":
                    TempWidth = Video.width + 38;
                    TempHeight = Video.height + 108;
                    break;
                case "90":
                case "270":
                    TempWidth = Video.height + 38;
                    TempHeight = Video.width + 108;
                    break;
            }
            ResizeMain();
        }

        int TempWidth = 0;
        int TempHeight = 0;
        private void ResizeMain()
        {
            int deltaX, deltaY;
            //switch (Machine.sDirection)
            //{
            //    case "":
            //    case "180":
            //        deltaX = TempWidth - (Video.width + 38);
            //        deltaY = TempHeight - (Video.height + 108);
            //        pictureBox1.Width = Video.width + deltaX;
            //        pictureBox1.Height = Video.height + deltaY;
            //        break;
            //    case "90":
            //    case "270":
            //        deltaX = TempWidth - (Video.height + 38);
            //        deltaY = TempHeight - (Video.width + 108);
            //        pictureBox1.Width = Video.height + deltaX;
            //        pictureBox1.Height = Video.width + deltaY;
            //        break;
            //}

            //TODO reset 宽高
        }

    }
}
