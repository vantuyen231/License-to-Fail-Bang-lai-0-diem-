using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio DataBase")]
public class AudioDatabaseSO : ScriptableObject
{
    public List<AudioClipData> uiIAudio;
}

[System.Serializable]
public class AudioClipData
{
    public string audioName;
    public List<AudioClip> clips = new List<AudioClip>();
    [Range(0f, 1f)] public float volume = 1f;

    public AudioClip GetRandomClip()
    {
        if(clips.Count == 0 || clips == null) return null;

        return clips[Random.Range(0, clips.Count)];
    }
}