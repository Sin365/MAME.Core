using MAME.Core.run_interface;
using System;
using UnityEngine;

public class UniSoundPlayer : MonoBehaviour, ISoundPlayer
{
    [SerializeField]
    private AudioSource m_as;
    private RingBuffer<float> _buffer = new RingBuffer<float>(4096);
    private TimeSpan lastElapsed;
    public double audioFPS { get; private set; }
    float lastData = 0;


    void Awake()
    {
        AudioClip dummy = AudioClip.Create("dummy", 1, 2, AudioSettings.outputSampleRate, false);
        dummy.SetData(new float[] { 1, 1 }, 0);
        m_as.clip = dummy;
        m_as.loop = true;
        m_as.spatialBlend = 1;
    }

    public void Initialize()
    {
        if (!m_as.isPlaying)
        {
            m_as.Play();
        }
    }

    public void StopPlay()
    {
        if (m_as.isPlaying)
        { 
            m_as.Stop();
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!UMAME.bInGame) return;
        int step = channels;
        for (int i = 0; i < data.Length; i += step)
        {
            float rawFloat = lastData;
            if (_buffer.TryRead(out float rawData))
            { 
                rawFloat = rawData;
            }

            data[i] = rawFloat;
            for (int fill = 1; fill < step; fill++)
                data[i + fill] = rawFloat;
            lastData = rawFloat;
        }
    }

    public void SubmitSamples(byte[] buffer, int samples_a)
    {
        var current = UMAME.sw.Elapsed;
        var delta = current - lastElapsed;
        lastElapsed = current;
        audioFPS = 1d / delta.TotalSeconds;


        for (int i = 0; i < samples_a; i++)
        {
            short left = BitConverter.ToInt16(buffer, i * 2 * 2);
            //short right = BitConverter.ToInt16(buffer, i * 2 * 2 + 2);
            _buffer.Write(left / 32767.0f);
            //_buffer.Write(right / 32767.0f);
        }
        return;

        float[] floatdata = ConvertByteArrayToFloatArray(buffer, samples_a, 2);
        for (int i = 0; i < samples_a; i++)
        {
            _buffer.Write(floatdata[i]);
        }
    }

    float[] floatArray = new float[960];
    public float[] ConvertByteArrayToFloatArray(byte[] buffer, int samples_a, int channels)
    {
        //int sampleCount = buffer.Length / (channels * 2); // 16位，所以每个样本2字节  
        

        for (int i = 0; i < samples_a; i++)
        {
            // 读取左右声道  
            //short left = BitConverter.ToInt16(buffer, i * channels * 2);
            //short right = BitConverter.ToInt16(buffer, i * channels * 2 + 2);
            // 转换为-1.0到1.0的浮点数  
            //floatArray[i] = left / 32767.0f; // 32767是16位整数的最大值
            //floatArray[i + 1] = right / 32767.0f;

            short onedata = BitConverter.ToInt16(buffer, i * channels * 2);
            floatArray[i] = onedata / 32767.0f;
        }

        return floatArray;
    }

    public void BufferWirte(int Off, byte[] Data)
    {
    }

    public void GetCurrentPosition(out int play_position, out int write_position)
    {
        play_position = 0;
        write_position = 0;
    }

    public void SetVolume(int Vol)
    {
        if (m_as)
            return;
        m_as.volume = Vol;
    }
}
