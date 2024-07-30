using MAME.Core.run_interface;
using System;
using UnityEngine;

public class UniSoundPlayer : MonoBehaviour, ISoundPlayer
{
    public int mWrite_position = 0;
    public int mPlay_position =0;

    [SerializeField]
    private AudioSource m_as;
    private RingBuffer<float> _buffer = new RingBuffer<float>(4096);
    private TimeSpan lastElapsed;
    public double audioFPS { get; private set; }
    float lastData = 0;

    public void Initialize()
    {
        AudioClip dummy = AudioClip.Create("dummy", 1, 1, AudioSettings.outputSampleRate, false);
        dummy.SetData(new float[] { 1,1 }, 0);
        m_as.clip = dummy;
        m_as.loop = true;
        m_as.spatialBlend = 1;
        m_as.Play();
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!UMAME.bStart) return;
        int step = channels;
        mWrite_position = 0;
        for (int i = 0; i < data.Length; i += step)
        {
            float rawFloat = lastData;
            if (_buffer.TryRead(out float rawData))
            { 
                rawFloat = rawData;
                mWrite_position++;
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
        float[] floatdata = ConvertByteArrayToFloatArray(buffer, samples_a,2);
        for (int i = 0; i < samples_a; i++)
        {
            _buffer.Write(floatdata[i]);
        }
    }
    public float[] ConvertByteArrayToFloatArray(byte[] bytes, int sampleRate, int channels)
    {
        int sampleCount = bytes.Length / (channels * 2); // 16λ������ÿ������2�ֽ�  
        float[] floatArray = new float[sampleCount * channels];

        for (int i = 0; i < sampleCount; i++)
        {
            // ��ȡ��������  
            short left = BitConverter.ToInt16(bytes, i * channels * 2);
            short right = BitConverter.ToInt16(bytes, i * channels * 2 + 2);

            //short left = (short)BitConverter.ToUInt16(bytes, i * channels * 2);
            //short right = (short)BitConverter.ToUInt16(bytes, i * channels * 2 + 2);

            // ת��Ϊ-1.0��1.0�ĸ�����  
            floatArray[i] = left / 32767.0f; // 32767��16λ���������ֵ
            floatArray[i + 1] = right / 32767.0f;
        }

        return floatArray;
    }

    public void BufferWirte(int Off, byte[] Data)
    {
        //var current = sw.Elapsed;
        //var delta = current - lastElapsed;
        //lastElapsed = current;

        //FPS = 1d / delta.TotalSeconds;

        //for (int i = Off; i < Data.Length; i++)
        //{
        //    _buffer.Write(Data[i]);
        //}
    }

    public void GetCurrentPosition(out int play_position, out int write_position)
    {
        play_position = mPlay_position;
        write_position = mWrite_position;
    }

    public void SetVolume(int Vol)
    {
    }

}
