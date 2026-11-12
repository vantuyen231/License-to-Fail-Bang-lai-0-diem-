using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : TuyenSingleton<GameManager>
{
    [Header("Player Status")]
    //[SerializeField] protected int mission = 0;
    [SerializeField] protected int coin = 0;

    [Header("State Game")]
    [SerializeField] protected bool isWinGame = false;
    [SerializeField] protected bool isLoseGame = false;
    [SerializeField] protected bool isPauseGame = false;


    [Header("Mission Game")]
    [SerializeField] protected int maxMission = 5;
    [SerializeField] protected int indexMission = 0;

    [Header("Car Database Reference")]
    [SerializeField] protected CarPlayerBaseDataSO carDatabase;

    [Header("PlayerData")]    
    [SerializeField] protected CarPlayerDataSO carPlayerData;
    [SerializeField] protected List<string> unlockedCarIDs = new List<string> { "001" };
    [SerializeField] protected string selectedCarID = "001";


    #region (Public Value)
    public int Coin => coin;
    public int IndexMission => indexMission;
    public CarPlayerDataSO CarPlayerData => carPlayerData;
    #endregion


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    protected override void LoadInstance()
    {
        base.LoadInstance();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject );
            Debug.Log("Do Destroy Singletion: " + gameObject);
            return;
        }
    }

    public void UpdateCarData()
    {
        if (carDatabase != null)
        {
            CarPlayerDataSO foundCar = carDatabase.GetCarByID(selectedCarID);

            if (foundCar != null)
            {
                carPlayerData = foundCar;
                Debug.Log("Vehicle successfully synchronized: " + carPlayerData.nameCar + " with ID: " + selectedCarID);
                return;
            }
        }
        Debug.LogWarning("Can't found ID: " + selectedCarID);
    }

    public void GetUseCar(CarPlayerDataSO car)
    {
        this.carPlayerData = car;
        if (car != null)
        {
            this.selectedCarID = car.carID;
        }
    }

    public void CoinPlayer(int indexCoin)
    {
        this.coin += indexCoin;
    }

    #region (PauseGame);
    public void PauseGame()
    {
        isPauseGame = true;
        Time.timeScale = 0f;
        MenuUI.Instance.MuteSFXGame();
    }

    public void ContinueGame()
    {
        isPauseGame = false;
        Time.timeScale = 1f;
        MenuUI.Instance.SetOnSFXGame();
    }
    #endregion

    public void WinGame()
    {
        Debug.Log("You Win");
        //this.PauseGame();
        WinUI.Instance.Show();
    }

    public void LoseGame()
    {
        Debug.Log("You Lose");
        LoseUI.Instance.Show();
    }

    public void GetRandomNumMission()
    {
        indexMission = UnityEngine.Random.Range(1, maxMission);
    }

    public void SetCoinPlayer(int coinBuyCar)
    {
        coin = coinBuyCar;
        Debug.Log(coinBuyCar + "gameManager");

        Debug.Log(coin +"gameManager");
    }

    public bool IsCarUnlocked(string carID)
    {
        return unlockedCarIDs.Contains(carID);
    }

    public bool IsCarSelected(string carID)
    {
        return selectedCarID == carID;
    }

    public void SetCarUnlocked(string carID)
    {
        if (!IsCarUnlocked(carID))
        {
            unlockedCarIDs.Add(carID);
        }
    }

    public void SetSelectCar(string carID)
    {
        if (!IsCarSelected(carID))
        {
            selectedCarID = carID;
            this.UpdateCarData();
        }
    }

    [Serializable]
    public class SaveObject
    {
        public int coinPlayer;
        public List<string> unlockedCarIDs;
        public string selectedCarID;
    }

    public SaveObject Save()
    {
        return new SaveObject { 
            coinPlayer = this.coin,
            unlockedCarIDs = new List<string>(this.unlockedCarIDs),
            selectedCarID = this.selectedCarID
        };
    }

    public void Load(SaveObject saveObject)
    {
        coin = saveObject.coinPlayer;
        if (saveObject.unlockedCarIDs != null)
        {
            unlockedCarIDs = saveObject.unlockedCarIDs;
        }
        selectedCarID = saveObject.selectedCarID;
        this.UpdateCarData();
    }
}