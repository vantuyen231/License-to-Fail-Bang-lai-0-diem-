using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
//using UnityEngine.Windows;
using System.IO;


public class SaveSystem : TuyenSingleton<SaveSystem>
{
    protected override void Awake()
    {
        base.Awake();
        this.LoadGame();
    }
    protected void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            this.SaveGame();
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            this.LoadGame();
        }
    }

    [Serializable]
    public class MainSaveObject { 
        public GameManager.SaveObject managerSaveObject;
    }

    public void SaveGame()
    {
        MainSaveObject mainSaveObject = new MainSaveObject
        {
            managerSaveObject = GameManager.Instance.Save(),
        };
        string json = JsonUtility.ToJson(mainSaveObject);
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.sav", json);
    }

    public void LoadGame()
    {
        string filePath = Application.persistentDataPath + "/saveFile.sav";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            MainSaveObject mainSaveObject = JsonUtility.FromJson<MainSaveObject>(json);
            GameManager.Instance.Load(mainSaveObject.managerSaveObject);
            Debug.Log("LoadGame Successfully");
        }else
        {
            Debug.LogWarning("No save file found");
        }
    }
}
