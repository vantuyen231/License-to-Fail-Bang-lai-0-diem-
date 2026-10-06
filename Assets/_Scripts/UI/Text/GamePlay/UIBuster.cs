using UnityEngine;
using UnityEngine.UI;

public class UIBuster : TuyenMonoBehaviour
{
    [SerializeField] protected Slider slider;
    [SerializeField] protected float busterValue = 0;

    protected override void Start()
    {
        this.Show();
    }

    protected virtual void OnEnable()
    {
        MissionManager.UpdateBusterStatus += UpdateBusterUI;
    }


    protected virtual void OnDisable()
    {
        MissionManager.UpdateBusterStatus -= UpdateBusterUI;
    }

    private void UpdateBusterUI(float indexBuster)
    {
        if (slider == null) return;
        busterValue = indexBuster;
        slider.value = busterValue;
        this.Show();
    }

    public virtual void Show()
    {
        if (slider == null) return;
        bool shouldShow = busterValue > 0;

        if (slider.gameObject.activeSelf != shouldShow)
        {
            slider.gameObject.SetActive(shouldShow);
        }
    }

    protected override void LoadComponents()
    {
        this.LoadSlider();
    }

    protected virtual void LoadSlider()
    {
        if (slider != null) return;
        slider = GetComponentInChildren<Slider>();
        Debug.Log(transform.name + ": LoadSlider", gameObject);
    }
}
