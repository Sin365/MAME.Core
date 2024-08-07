using MAME.Core.run_interface;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UniVideoPlayer : MonoBehaviour, IVideoPlayer
{
    [SerializeField]
    private int mWidth;
    [SerializeField]
    private int mHeight;
    [SerializeField]
    private int mDataLenght;
    [SerializeField]
    private Texture2D m_rawBufferWarper;
    [SerializeField]
    private RawImage m_drawCanvas;
    [SerializeField]
    private RectTransform m_drawCanvasrect;
    int[] mFrameData;
    IntPtr mFrameDataPtr;

    private TimeSpan lastElapsed;
    public double videoFPS { get; private set; }
    bool bInitTexture = false;

    private void Awake()
    {
        m_drawCanvas = GameObject.Find("GameRawImage").GetComponent<RawImage>();
        m_drawCanvasrect = m_drawCanvas.GetComponent<RectTransform>();
    }
    public void Initialize(int width, int height,IntPtr framePtr)
    {
        m_drawCanvas.color = Color.white;
        //384 * 264
        mWidth = width;
        mHeight = height;
        mDataLenght = width * height * 4;
        mFrameDataPtr = framePtr;
        //m_rawBufferWarper = new Texture2D(mWidth, mHeight,TextureFormat.RGBA32,false);
        //MAME来的是BGRA32，好好好
        m_rawBufferWarper = new Texture2D(mWidth, mHeight, TextureFormat.BGRA32, false);
        m_rawBufferWarper.filterMode = FilterMode.Point;

        m_drawCanvas.texture = m_rawBufferWarper;
        mFrameData = new int[mWidth * mHeight];
        bInitTexture = true;

        float targetWidth = ((float)mWidth / mHeight) * m_drawCanvasrect.rect.height ;
        m_drawCanvasrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
    }

    void Update()
    {
        if (!bInitTexture) return;

        //m_rawBufferWarper.LoadRawTextureData(mFrameDataPtr, mFrameData.Length * 4);
        m_rawBufferWarper.LoadRawTextureData(mFrameDataPtr, mDataLenght);
        m_rawBufferWarper.Apply();
    }

    public void SubmitVideo(int[] data)
    {
        var current = UMAME.sw.Elapsed;
        var delta = current - lastElapsed;
        lastElapsed = current;
        videoFPS = 1d / delta.TotalSeconds;
        mFrameData = data;
    }
}
