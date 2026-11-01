using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/VehicalSound")]
public class VehicalAudioSO : ScriptableObject
{
    public List<AudioClipData> vehicalClips;

    private Dictionary<string, AudioClipData> clipCollection;

    private void OnEnable()
    {
        clipCollection = new Dictionary<string, AudioClipData>();

        this.AddToCollection(vehicalClips);

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
