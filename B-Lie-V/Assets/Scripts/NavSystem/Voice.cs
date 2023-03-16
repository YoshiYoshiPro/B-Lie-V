using UnityEngine;
using VoicevoxBridge;

public class Voice : MonoBehaviour
{
    [SerializeField] VOICEVOX voicevox;

    public async void VoiceStart(string voiceText)
    {
        int speaker = 1; // ずんだもん あまあま
        Debug.Log("ボイス変換開始");
        string text = voiceText;
        await voicevox.PlayOneShot(speaker, text);
        Debug.Log("ボイス再生終了");
    }
}