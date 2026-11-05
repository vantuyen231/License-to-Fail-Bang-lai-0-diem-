using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarData",menuName = "GameData/SoundCarSO")]
public class SoundCarPlayerSO : ScriptableObject
{
    [Header("Start Car Sound")]
    public List<AudioClipData> startCarSound;

    //[Header("Brake Sound")]
    //public List<AudioClipData> brakeCarSound;

    //[Header("HornCarSound")]
    //public List<AudioClipData> hornCarSound;


    [Header("Engine Configurations")]
    public float minRPM = 1000f;
    public float rpmStep = 500f;
    private Dictionary<string, AudioClipData> clipCollection;

    private void OnEnable()
    {
        clipCollection = new Dictionary<string, AudioClipData>();

        this.AddToCollection(startCarSound);
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
