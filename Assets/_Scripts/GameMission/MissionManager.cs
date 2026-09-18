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


    protected override void Start()
    {
        base.Start();
        this.InitShift();
        //this.GetNumMission();
        //this.GetRandomMission();
        ////this.CheckWinGame();
    }

    protected virtual void Update()
    {
        if (isCoolingDown == true)
        {
            this.CoolDownMission();
            return;
        }
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

            int index = Random.Range(0, tempMission.Count);

            missionsPlayer.Add(tempMission[index]);
            tempMission.RemoveAt(index);
        }
    }

    protected virtual void DoMission()
    {
        if( missionsPlayer == null ) return ;

        if (curretMission < indexMissionPlayer)
        {
            missionsPlayer[curretMission].SetActive(true);
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
            GameManager.Instance.LoseGame();
            return;
        }

        if (curretMission > indexMissionPlayer)
        {
            if (missionFail >= maxPossiblePass) GameManager.Instance.LoseGame();
            else GameManager.Instance.WinGame();
            return;
        }
        isCoolingDown = true ;
        timer = 0;
    }
}
