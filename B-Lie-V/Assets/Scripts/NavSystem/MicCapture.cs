using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unityで録音し、メモリ上に保持した録音サウンドを再生するサンプルコード
/// OnGUIでデバッグボタンを表示しています
/// </summary>
public class MicCapture : MonoBehaviour
{
    /// <summary>
    /// 録音するAudioClip
    /// </summary>
    private AudioClip _recordedClip;

    /// <summary>
    /// 再生させるオーディオソース
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// 録音機材名リスト
    /// </summary>
    private readonly List<string> _micNames = new List<string>();

    /// <summary>
    /// 録音に使用している機材名
    /// </summary>
    private string _recordingMicName;

    [SerializeField] private Mochineko.Whisper_API.Transcription.Samples.TranscriptionSample transcriptionSample;
    void Start()
    {
        _micNames.Clear();
        
        foreach (string device in Microphone.devices)
        {
            Debug.Log("DeviceName: " + device);
            _micNames.Add(device);
        }
    }

    /// <summary>
    /// 録音機材名を指定して録音開始
    /// </summary>
    public void StartRecord()
    {
        foreach (var micName in _micNames)
        {
            if (string.IsNullOrEmpty(_recordingMicName) == false || string.IsNullOrEmpty(micName))
            {
                return;
            }


            _recordingMicName = micName;
            _recordedClip = Microphone.Start(micName, false, 10, 44100);
            
        }
    }

    /// <summary>
    /// 現在使用している機材の録音を停止
    /// </summary>
    public void EndRecord()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();

        if (string.IsNullOrEmpty(_recordingMicName) == false && Microphone.IsRecording(_recordingMicName))
        {
            Microphone.End(_recordingMicName);
        }

        _audioSource.clip = _recordedClip;
        SavWav.SaveWav($"{Application.persistentDataPath}/test", _recordedClip);

        _recordingMicName = null;

        transcriptionSample.Transcribe();
    }

    /// <summary>
    /// メモリ上に保持した録音したオーディオを再生
    /// </summary>
    public void PlayRecordedAudioClip()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        _audioSource.clip = _recordedClip;
        SavWav.SaveWav($"{Application.persistentDataPath}/test", _recordedClip);
        // byte[] mp3 = WavToMp3.ConvertWavToMp3(_recordedClip, 128);
        // EncodeMP3.SaveMp3(mp3, $"{Application.dataPath}/mp3File", 128);
        _audioSource.Play();
    }

}
