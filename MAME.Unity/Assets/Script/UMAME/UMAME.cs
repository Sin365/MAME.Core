using mame;
using MAME.Core.Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UMAME : MonoBehaviour
{
    mainMotion mainmotion;
    UniLog mUniLog;
    UniMouse mUniMouse;
    UniVideoPlayer mUniVideoPlayer;
    UniSoundPlayer mUniSoundPlayer;
    UniKeyboard mUniKeyboard;
    UniResources mUniResources;
    public Text mFPS;

    public Button btnOpenRomPath;
    public Button btnStart;
    public Button btnRomDir;
    public Dictionary<string, RomInfo> ALLGame;
    public List<RomInfo> HadGameList = new List<RomInfo>();

    Dropdown optionDropdown;

    public static System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
    public static bool bStart { get; private set; }


#if UNITY_EDITOR_WIN
    public static string RomPath => "G:\\MAME.NET.Rom";
#elif UNITY_ANDROID
    public static string RomPath => Application.persistentDataPath;
#elif UNITY_PSP2
	public static string RomPath => Application.dataPath;
#else
    public static string RomPath => Application.persistentDataPath;
#endif


    public string mChangeRomName = "mslug3";
    private void Awake()
    {
        mFPS = GameObject.Find("FPS").GetComponent<Text>();
        optionDropdown = GameObject.Find("optionDropdown").GetComponent<Dropdown>();
        mainmotion = new mainMotion();
        mUniLog = new UniLog();
        mUniMouse = this.gameObject.AddComponent<UniMouse>();
        mUniVideoPlayer = this.gameObject.AddComponent<UniVideoPlayer>();
        mUniSoundPlayer = GameObject.Find("Audio").transform.GetComponent<UniSoundPlayer>();
        mUniKeyboard = this.gameObject.AddComponent<UniKeyboard>();
        mUniResources = new UniResources();

        if (mainMotion.t1 != null)
        {
            mainMotion.t1.Abort();
        }
        mainmotion.Init(RomPath, mUniLog, mUniResources, mUniVideoPlayer, mUniSoundPlayer, mUniKeyboard, mUniMouse);
        ALLGame = mainmotion.GetGameList();

        Debug.Log($"ALLGame:{ALLGame.Count}");
    }

    void OnEnable()
    {
        btnOpenRomPath.onClick.AddListener(OpenFolderRomPath);
        btnStart.onClick.AddListener(LoadGame);
        btnRomDir.onClick.AddListener(() => GetHadRomList());
    }

    void LoadGame()
    {
        if (mainMotion.t1 != null)
        {
            mainMotion.t1.Abort();
        }

        mChangeRomName = HadGameList[optionDropdown.value].Name;

        mainmotion.LoadRom(mChangeRomName);
        if (Machine.bRom)
        {
            m68000Motion.iStatus = 0;
            m68000Motion.iValue = 0;
            Mame.exit_pending = false;
            mame.Motion.init();
            mainMotion.t1 = new Thread(Mame.mame_execute);
            mainMotion.t1.Start();

            StartCoroutine(StartGame());
        }
    }
    private void Update()
    {
        mFPS.text = ($"fpsv {mUniVideoPlayer.videoFPS.ToString("F2")} fpsa {mUniSoundPlayer.audioFPS.ToString("F2")}");
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        StartEmu();
    }

    void StartEmu()
    {
        if (bStart)
        {
            return;
        }
        mUniSoundPlayer.Initialize();
        mainmotion.GetGameScreenSize(out int _width, out int _height);
        mUniVideoPlayer.Initialize(_width, _height);
        Mame.mame_pause(false);
        bStart = true;
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
