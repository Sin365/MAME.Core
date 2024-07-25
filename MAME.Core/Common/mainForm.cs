using mame;
using MAME.Core.run_interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MAME.Core.Common
{
    public class mainForm
    {
        public string tsslStatus;
        public cheatForm cheatform;
        public m68000Form m68000form;
        public z80Form z80form;
        public m6809Form m6809form;
        public cpsForm cpsform;
        public neogeoForm neogeoform;
        public konami68000Form konami68000form;
        public string sSelect;

        public static IResources resource;

        public mainForm()
        {
            neogeoform = new neogeoForm(this);
            cheatform = new cheatForm(this);
            m68000form = new m68000Form(this);
            m6809form = new m6809Form(this);
            z80form = new z80Form(this);
            cpsform = new cpsForm(this);
            konami68000form = new konami68000Form(this);
        }

        public void Init(IResources iRes,
            IVideoPlayer ivp,
            ISoundPlayer isp,
            IKeyboard ikb,
            IMouse imou)
        {
            Video.BindFunc(ivp);
            Sound.BindFunc(isp);
            resource = iRes;

            StreamReader sr1 = new StreamReader("mame.ini");
            sr1.ReadLine();
            sSelect = sr1.ReadLine();
            sr1.Close();


            RomInfo.Rom = new RomInfo();
            LoadROMXML();
            //TODO Wavebuffer
            //desc1.BufferBytes = 0x9400;
            Keyboard.InitializeInput(this, ikb);
            Mouse.InitialMouse(this, imou);
        }

        private void LoadROMXML()
        {
            XElement xe = XElement.Parse(resource.Get_mame_xml());
            IEnumerable<XElement> elements = from ele in xe.Elements("game") select ele;
            showInfoByElements(elements);
        }

        private void showInfoByElements(IEnumerable<XElement> elements)
        {
            RomInfo.romList = new List<RomInfo>();
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
                //loadform.listView1.Items.Add(new ListViewItem(new string[] { rom.Description, rom.Year, rom.Name, rom.Parent, rom.Direction, rom.Manufacturer, rom.Board }));
            }
        }

        public void LoadRom(string Name)
        {
            RomInfo.Rom = RomInfo.GetRomByName(Name);
            if (RomInfo.Rom == null)
            {
                Console.WriteLine("Not Found");
                return;
            }

            mame.Timer.lt = new List<mame.Timer.emu_timer>();
            sSelect = RomInfo.Rom.Name;
            Machine.FORM = this;
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
                    Video.nMode = 3;
                    itemSelect();
                    CPS.CPSInit();
                    CPS.GDIInit();
                    break;
                case "Data East":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Dataeast.DataeastInit();
                    Dataeast.GDIInit();
                    break;
                case "Tehkan":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Tehkan.PbactionInit();
                    Tehkan.GDIInit();
                    break;
                case "Neo Geo":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Neogeo.NeogeoInit();
                    Neogeo.GDIInit();
                    break;
                case "SunA8":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    SunA8.SunA8Init();
                    SunA8.GDIInit();
                    break;
                case "Namco System 1":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Namcos1.Namcos1Init();
                    Namcos1.GDIInit();
                    break;
                case "IGS011":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    IGS011.IGS011Init();
                    IGS011.GDIInit();
                    break;
                case "PGM":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    PGM.PGMInit();
                    PGM.GDIInit();
                    break;
                case "M72":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    M72.M72Init();
                    M72.GDIInit();
                    break;
                case "M92":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    M92.M92Init();
                    M92.GDIInit();
                    break;
                case "Taito":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Taito.TaitoInit();
                    Taito.GDIInit();
                    break;
                case "Taito B":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Taitob.TaitobInit();
                    Taitob.GDIInit();
                    break;
                case "Konami 68000":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Konami68000.Konami68000Init();
                    Konami68000.GDIInit();
                    break;
                case "Capcom":
                    Video.nMode = 1;
                    Video.iMode = 0;
                    itemSelect();
                    Capcom.CapcomInit();
                    Capcom.GDIInit();
                    break;
            }
            if (Machine.bRom)
            {
                Console.Write("MAME.NET: " + Machine.sDescription + " [" + Machine.sName + "]");
                Mame.init_machine(this);
                Generic.nvram_load();
            }
            else
            {
                Console.Write("error rom");
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
