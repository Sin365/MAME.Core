using mame;
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
    private Texture2D m_rawBufferWarper;
    [SerializeField]
    private RawImage m_drawCanvas;
    int[] mFrameData;
    Color32[] result;

    private TimeSpan lastElapsed;
    public double videoFPS { get; private set; }
    bool bInitTexture = false;

    private void Awake()
    {
        m_drawCanvas = GameObject.Find("GameRawImage").GetComponent<RawImage>();
    }
    public void Initialize(int width,int height)
    {
        //384 * 264
        mWidth = width;
        mHeight = height;
        m_rawBufferWarper = new Texture2D(mWidth, mHeight);
        mFrameData = new int[mWidth * mHeight];
        result = new Color32[mFrameData.Length];
        bInitTexture = true;
    }

    void Update()
    {
        if (!bInitTexture) return;
        var colors = GetUnityColor(mFrameData);
        m_rawBufferWarper.SetPixels32(colors);
        m_rawBufferWarper.Apply();
        Graphics.Blit(m_rawBufferWarper, m_drawCanvas.texture as RenderTexture);
    }


    public Color32[] GetUnityColor(int[] mFrameData)
    {
        for (int i = 0; i < mFrameData.Length; i++)
        {
            int argb = mFrameData[i];
            result[i].a = (byte)((argb >> 24) & 0xFF);
            result[i].r = (byte)((argb >> 16) & 0xFF);
            result[i].g = (byte)((argb >> 8) & 0xFF);
            result[i].b = (byte)(argb & 0xFF);
        }
        return result;
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
