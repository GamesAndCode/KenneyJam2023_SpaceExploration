using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI fuelText;
    [SerializeField] private TextMeshProUGUI hullText;

    private void Start()
    {
        Spaceshuttle shuttle = GameManager.instance.GetCurrentSpaceship();
        UpdateHullText(shuttle.GetHull());
        UpdateFuelText(shuttle.GetFuel());

        shuttle.OnFuelChanged += UpdateFuelText;
        shuttle.OnHullChanged += UpdateHullText;
    }

    private void UpdateFuelText(int fuel)
    {
        fuelText.text = $"Fuel: {fuel.ToString()}";
    }

    private void UpdateHullText(int constitution)
    {
        hullText.text = $"Hull: {constitution.ToString()}";
    }

}
