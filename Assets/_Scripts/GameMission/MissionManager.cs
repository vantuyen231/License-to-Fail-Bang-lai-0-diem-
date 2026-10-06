using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : TuyenMonoBehaviour
{
    [Header("Num Mission To Do")]
    [SerializeField] protected int indexMissionPlayer = 0;

    [Header("Base Mission")]
    [SerializeField] protected List<BaseMission> missions = new List<BaseMission>();

    [Header("Mission To Do")]
    [SerializeField] protected List<BaseMission> missionsPlayer = new List<BaseMission>();
    [SerializeField] protected List<BaseMission> tempMission = new List<BaseMission>();

    [Header("Status Mision")]
    [SerializeField] protected float timeNextMission = 2.5f;
    [SerializeField] protected bool isDone = false;
    [SerializeField] protected float timer = 0;
    [SerializeField] protected int curretMission = 0;
    [SerializeField] protected bool isCoolingDown = false;
    [SerializeField] protected int missionFail = 0;
    [SerializeField] protected int missionSucess = 0;
    [SerializeField] protected int maxPossiblePass = 0;

    [Header("WinGameStatus")]
    [SerializeField] protected int scoreMission = 0;
    [SerializeField] protected float bonus = 0;
    [SerializeField] protected int pedestrial = 0;
    [SerializeField] protected int vehical = 0;
    [SerializeField] protected bool isWinGame = false;

    [Header("Buster Details")]
    [SerializeField] protected float currentBuster = 0;
    [SerializeField] protected float increaseRate = 25f;
    [SerializeField] protected float decreaseRate = 15f;
    [SerializeField] protected int policeCount = 0;
    [SerializeField] protected bool isBusterBoom = false;
    [SerializeField] protected bool isEscapeMission = false;
    [SerializeField] protected int maxBuster = 100;

    public static event Action<bool,int, int> EndGame;
    public static event Action<float> UpdateBusterStatus;


    protected override void Start()
    {
        base.Start();
        this.InitShift();
    }

    protected virtual void Update()
    {
        this.BusterCheck();
        if (isCoolingDown == true)
        {
            this.CoolDownMission();
            return;
        }
    }

    protected void OnEnable()
    {
        BusterChecker.OnCheckPlayerBustedStatus += CheckPoliceHit;
        BaseMission.UpdateStateMission += CheckMissionEscape;
    }

    protected void OnDisable()
    {
        BusterChecker.OnCheckPlayerBustedStatus -= CheckPoliceHit;
        BaseMission.UpdateStateMission -= CheckMissionEscape;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetListMission();
    }

    protected virtual void GetListMission()
    {
        if( missions.Count > 0 ) return;
        missions.AddRange(GetComponentsInChildren<BaseMission>(true));
    }

    protected virtual void InitShift()
    {
        this.GetNumMission();
        this.GetRandomMission();

        maxPossiblePass = Mathf.CeilToInt(indexMissionPlayer / 2.0f);

        curretMission = 0;
        missionSucess = 0;
        missionFail = 0;
        this.DoMission();
    }


    protected virtual void GetNumMission()
    {
        if (GameManager.Instance == null) return;
        indexMissionPlayer = GameManager.Instance.IndexMission;

    }

    protected virtual void GetRandomMission()
    {
        if (missions.Count == 0 || indexMissionPlayer <= 0) return;
        missionsPlayer.Clear();

        tempMission = new List <BaseMission>(missions);
        for (int i = 0; i < indexMissionPlayer; i++)
        {
            if (tempMission.Count == 0) break;

            int index = UnityEngine.Random.Range(0, tempMission.Count);

            missionsPlayer.Add(tempMission[index]);
            tempMission.RemoveAt(index);
        }
    }

    protected virtual void DoMission()
    {
        if( missionsPlayer == null ) return ;

        this.ResetBuster();

        if (curretMission < indexMissionPlayer)
        {
            missionsPlayer[curretMission].gameObject.SetActive(true);
            Debug.Log(missionsPlayer[curretMission].name);
        }
    } 

    protected virtual void CoolDownMission()
    {
        timer += Time.deltaTime;
        if( timer >= timeNextMission )
        {
            timer = 0;
            isCoolingDown = false;
            Debug.Log("Do next mission");
            this.DoMission();
        }
    }

    public virtual void CheckDoneMission(BaseMission mission, bool status)
    {
        if (status == true)
        {
            Debug.Log("ManagerMission: sucess");
            missionSucess++;
        }
        else
        {
            Debug.Log("ManagerMission: fail");
            missionFail++;
        }

        this.CheckWinGame();

    }

    protected virtual void CheckWinGame()
    {
        curretMission++;

        if (missionFail >= maxPossiblePass)
        {
            GameManager.Instance.PauseGame();
            EndGame?.Invoke(false,missionSucess, indexMissionPlayer);

            return;
        }

        if (curretMission >= indexMissionPlayer)
        {
            Debug.Log("Game manager: Done game");
            if (missionFail >= maxPossiblePass)
            {
                GameManager.Instance.PauseGame();
                EndGame?.Invoke(false, missionSucess, indexMissionPlayer);
            }
            else
            {
                GameManager.Instance.PauseGame();
                EndGame?.Invoke(true, missionSucess, indexMissionPlayer);
            }
                return;
        }
        isCoolingDown = true ;
        timer = 0;
    }

    protected virtual void CheckMissionEscape(MissionType mission, MissionState state, float duration)
    {
        if (mission == MissionType.EscapePolice)
        {
            isEscapeMission = true ;
        }
        else
        {
            isEscapeMission = false ;
        }
    }

    protected void CheckPoliceHit(BusterChecker police, bool checkBust)
    {
        if (checkBust)
        {
            policeCount++;
        }
        else
        {
            policeCount = Mathf.Max(0, policeCount-1);
        }
    }

    protected virtual void BusterCheck()
    {
        if (isBusterBoom) return;
        if (policeCount > 0)
        {
            currentBuster += increaseRate * Time.deltaTime;
        }else if (currentBuster > 0)
        {
            currentBuster -= decreaseRate * Time.deltaTime;
        }

        currentBuster = Mathf.Clamp(currentBuster, 0, maxBuster);
        UpdateBusterStatus?.Invoke(currentBuster / maxBuster);

        if (currentBuster >= maxBuster)
        {
            isBusterBoom = true ;
            this.BusterOver();
        }
    }

    protected void BusterOver()
    {
        if (isEscapeMission)
        {
            if (curretMission < missionsPlayer.Count)
            {
                Debug.Log("Mission Escape Police fail");
                missionsPlayer[curretMission].ForceFail(false);
                this.ResetBuster();
            }
        }
        else
        {
            GameManager.Instance.PauseGame();
            EndGame?.Invoke(false, missionSucess, indexMissionPlayer);
            Debug.Log("Game over");
            return;
        }
    }

    private void ResetBuster()
    {
        isBusterBoom = false;
        currentBuster = 0f;
    }
}
