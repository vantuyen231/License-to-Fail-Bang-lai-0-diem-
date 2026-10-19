using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class SaveSystem : TuyenSingleton<SaveSystem>
{
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
        public GameManager.SaveObject coinManagerSaveObject;
    }

    protected void SaveGame()
    {
        MainSaveObject mainSaveObject = new MainSaveObject
        {
            coinManagerSaveObject = GameManager.Instance.Save(),
        };
        Debug.Log("SaveGame");
    }

    protected void LoadGame()
    {
        Debug.Log("LoadGame");
    }
}
