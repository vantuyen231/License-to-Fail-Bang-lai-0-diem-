using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio DataBase")]
public class AudioDatabaseSO : ScriptableObject
{
    public List<AudioClipData> uiIAudio;
    public List<AudioClipData> npcAudio;

    public List<AudioClipData> bgmSound;

    private Dictionary<string, AudioClipData> clipCollection;

    private void OnEnable()
    {
        clipCollection = new Dictionary<string, AudioClipData>();

        this.AddToCollection(this.uiIAudio);
        this.AddToCollection(npcAudio);
        this.AddToCollection(bgmSound);
    }

    public AudioClipData GetSound(string groundName)
    {
        return clipCollection.TryGetValue(groundName, out var data) ? data : null;
    }

    private void AddToCollection(List<AudioClipData> listToAdd)
    {
        foreach (var data in listToAdd)
        {
            if (data != null && clipCollection.ContainsKey(data.audioName) == false)
            {
                clipCollection.Add(data.audioName, data);
            }
        }
    }
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