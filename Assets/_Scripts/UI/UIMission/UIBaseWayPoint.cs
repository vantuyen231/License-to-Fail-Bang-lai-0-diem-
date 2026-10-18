using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseWayPoint : BaseUIMission, HUDWayPoint
{
    [SerializeField] protected UIDestinationWP destinationWP;
    [SerializeField] protected UIArrowWayPoint arrowWayPointUI;

    public UIArrowWayPoint ArrowWayPoint => arrowWayPointUI;

    protected override void Awake()
    {
        base.Awake();
        Hide(destinationWP.transform);
        Hide(arrowWayPointUI.transform);
    }

    public override void ShowUI(MissionType missionType, MissionState state, float displayDuration)
    {
        base.ShowUI(missionType, state, displayDuration);
        switch (state)
        {

            case MissionState.ActiveGameplay:
                if (destinationWP != null)
                {
                    this.Show(destinationWP.transform);
                    this.Show(arrowWayPointUI.transform);
                }
                break;
            case MissionState.Success:
                if (destinationWP != null)
                {
                    this.Hide(arrowWayPointUI.transform);
                    this.Hide(destinationWP.transform);
                }
                break;
            case MissionState.Failed:
                if (destinationWP != null)
                {
                    this.Hide(arrowWayPointUI.transform);
                    this.Hide(destinationWP.transform);
                }
                break;

            default:
                break;
        }
    }


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIDestinationWP();
        this.LoadUIArrowWayPoint();
    }

    protected virtual void LoadUIDestinationWP()
    {
        if (this.destinationWP != null) return;
        destinationWP = FindAnyObjectByType<UIDestinationWP>();
        Debug.Log(transform.name + ": LoadUIDestinationWP", gameObject);
    }

    protected virtual void LoadUIArrowWayPoint()
    {
        if (this.arrowWayPointUI != null) return;
        arrowWayPointUI = FindAnyObjectByType<UIArrowWayPoint>();
        Debug.Log(transform.name + ": LoadUIArrowWayPoint", gameObject);
    }
}
