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






    protected override void Start()
    {
        base.Start();
        this.GetNumMission();
        this.GetRandomMission();
    }

    protected virtual void Update()
    {
        if (isCoolingDown == true)
        {
            this.CoolDownMission();
            return;
        }else
        {
            this.DoMission();

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
        //List<BaseMission> tempMission = new List<BaseMission>(missions);
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

        if (curretMission >= missionsPlayer.Count)
        {
            GameManager.Instance.WinGame();
            return;
        }

        missionsPlayer[curretMission].SetActive(true);
        if (isDone == false) return;
        isDone = false;
        missionsPlayer[curretMission].SetActive(false);

        isCoolingDown = true;

    }

    protected virtual void CoolDownMission()
    {
        isCoolingDown = true;
        timer += Time.deltaTime;
        if( timer >= timeNextMission )
        {
            timer = 0;
            curretMission++;
            isCoolingDown = false;
            Debug.Log("Do next mission");
        }
    }

    public virtual void CheckDoneMission(bool status)
    {
        isDone  = status;
    }
}
