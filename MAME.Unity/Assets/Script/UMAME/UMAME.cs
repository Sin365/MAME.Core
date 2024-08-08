using Assets.Script.UMAME.Common;
using mame;
using MAME.Core.Motion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UMAME : MonoBehaviour
{
    MameMainMotion emu;
    UniLog mUniLog;
    UniMouse mUniMouse;
    UniVideoPlayer mUniVideoPlayer;
    UniSoundPlayer mUniSoundPlayer;
    UniKeyboard mUniKeyboard;
    UniResources mUniResources;
    public Text mFPS;
    public Button btnStop;
    public Button btnStart;
    public Button btnRomDir;
    public Dictionary<string, RomInfo> ALLGame;
    public List<RomInfo> HadGameList = new List<RomInfo>();
    string mChangeRomName = string.Empty;
    public bool bQuickTestRom = false;
    public string mQuickTestRom = string.Empty;

    Dropdown optionDropdown;

    public static System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
    public static bool bInGame { get; private set; }


#if UNITY_EDITOR_WIN
    public static string RomPath => "G:\\MAME.NET.Rom";
#elif UNITY_ANDROID
    public static string RomPath => Application.persistentDataPath;
#elif UNITY_PSP2
	public static string RomPath => Application.dataPath;
#else
    public static string RomPath => Application.persistentDataPath;
#endif

    private void Awake()
    {
        mFPS = GameObject.Find("FPS").GetComponent<Text>();
        optionDropdown = GameObject.Find("optionDropdown").GetComponent<Dropdown>();
        emu = new MameMainMotion();
        mUniLog = new UniLog();
        mUniMouse = this.gameObject.AddComponent<UniMouse>();
        mUniVideoPlayer = this.gameObject.AddComponent<UniVideoPlayer>();
        mUniSoundPlayer = GameObject.Find("Audio").transform.GetComponent<UniSoundPlayer>();
        mUniKeyboard = this.gameObject.AddComponent<UniKeyboard>();
        mUniResources = new UniResources();
        mChangeRomName = UniMAMESetting.instance.LastGameRom;

        emu.Init(RomPath, mUniLog, mUniResources, mUniVideoPlayer, mUniSoundPlayer, mUniKeyboard, mUniMouse);
        ALLGame = emu.GetGameList();

        Debug.Log($"ALLGame:{ALLGame.Count}");

#if !UNITY_EDITOR
        bQuickTestRom = false;
#endif

        if (bQuickTestRom)
            mChangeRomName = mQuickTestRom;

        GetHadRomList();

        if (bQuickTestRom)
            LoadGame();
    }

    void OnEnable()
    {
        btnStop.onClick.AddListener(StopGame);
        btnStart.onClick.AddListener(LoadGame);
    }

    void LoadGame()
    {
        mChangeRomName = HadGameList[optionDropdown.value].Name;
        UniMAMESetting.instance.LastGameRom = mChangeRomName;
        StopGame();
        //读取ROM
        emu.LoadRom(mChangeRomName);
        //读取成功
        if (emu.bRom)
        {
            //读取ROM之后获得宽高初始化画面
            emu.GetGameScreenSize(out int _width, out int _height, out IntPtr _framePtr);
            Debug.Log($"_width->{_width}, _height->{_height}, _framePtr->{_framePtr}");
            mUniVideoPlayer.Initialize(_width, _height, _framePtr);
            //初始化音频
            mUniSoundPlayer.Initialize();
            //开始游戏
            emu.StartGame();
            bInGame = true;
        }
        else
        {
            Debug.Log($"ROM加载失败");
        }
    }
    private void Update()
    {
        mFPS.text = ($"fpsv {mUniVideoPlayer.videoFPS.ToString("F2")} fpsa {mUniSoundPlayer.audioFPS.ToString("F2")}");
    }

    void StopGame()
    {
        if (bInGame)
        {
            emu.StopGame();
            mUniVideoPlayer.StopVideo();
            mUniSoundPlayer.StopPlay();
            bInGame = false;
        }
    }

    void GetHadRomList()
    {
        HadGameList.Clear();
        optionDropdown.options.Clear();

        Debug.Log($"GetHadRomList:{RomPath + "/roms/"}");
        string[] directoryEntries = Directory.GetDirectories(RomPath + "/roms/");
        for (int i = 0; i < directoryEntries.Length; i++)
        {
            string path = directoryEntries[i];
            string dirName = Path.GetFileName(path);
            if (ALLGame.ContainsKey(dirName))
            { 
                HadGameList.Add(ALLGame[dirName]);
                optionDropdown.options.Add(new Dropdown.OptionData(dirName));
            }
        }
        Debug.Log($"HadGameList:{HadGameList.Count}");

        RomInfo tempCurrRom = HadGameList.Where(w => w.Name == mChangeRomName).FirstOrDefault();
        if (tempCurrRom != null)
        {
            optionDropdown.value = HadGameList.IndexOf(tempCurrRom);
        }
        else
        {
            optionDropdown.value = 0;
        }
        optionDropdown.RefreshShownValue();
    }

    void OpenFolderRomPath()
    {
        System.Diagnostics.Process.Start("explorer.exe", "/select," + RomPath);
    }
}
