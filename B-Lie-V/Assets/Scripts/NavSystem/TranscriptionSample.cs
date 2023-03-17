#nullable enable
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using System.IO;

namespace Mochineko.Whisper_API.Transcription.Samples
{
    /// <summary>
    /// A sample component to transcribe speech into text by Whisper transcription API on Unity.
    /// </summary>
    public sealed class TranscriptionSample : MonoBehaviour
    {
        /// <summary>
        /// API key generated by OpenAPI.
        /// </summary>
        [SerializeField] private string apiKey = "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";

        /// <summary>
        /// File path of speech audio.
        /// </summary>
        [SerializeField] private string filePath = "Assets" + "/test.wav";
        [SerializeField] private string OutputData;
        [SerializeField] private Mochineko.ChatGPT_API.Samples.ChatCompletionSample chatCompletionSample;

        private WhisperTranscriptionConnection? connection;

        private void Start()
        {
            // API Key must be set.
            Assert.IsNotNull(apiKey);

            // Create instance of WhisperTranscriptionConnection.
            connection = new WhisperTranscriptionConnection(apiKey);
            
            // If you want to specify response format, language, etc..., please use other initialization:
            // connection = new WhisperTranscriptionConnection(apiKey, new APIRequestBody(
            //     file: "",
            //     model: "whisper-1",
            //     prompt: "Some prompts",
            //     responseFormat: "json",
            //     temperature: 1f,
            //     language: "ja"));
        }

        [ContextMenu(nameof(Transcribe))]
        public async void Transcribe()
        {
            // Validations
            if (connection == null)
            {
                Debug.LogError($"[Whisper_API.Transcription.Samples] Connection is null.");
                return;
            }

            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogError($"[Whisper_API.Transcription.Samples] File path is null or empty.");
                return;
            }

            string result;
            try
            {
                // Transcribe speech into text by Whisper transcription API.
                result = await connection
                    .TranscribeFromFileAsync(Application.persistentDataPath + "/test.wav", this.GetCancellationTokenOnDestroy());
            }
            catch (Exception e)
            {
                // Exceptions should be caught.
                Debug.LogException(e);
                return;
            }

            // Default text response format is JSON.
            var text = APIResponseBody.FromJson(result)?.Text;

            // Log text result.
            Debug.Log($"[Whisper_API.Transcription.Samples] Result:\n{text}");
            OutputData = text;
            chatCompletionSample.SendChat(OutputData);
        }
    }
}