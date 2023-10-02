using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TravelInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fuelText;
    [SerializeField] private TextMeshProUGUI hullText;

    public void UpdateFuelText(string fuel)
    {
        fuelText.text = "Fuel: " + fuel;
    }

    public void UpdateHullText(string hull)
    {
        hullText.text = "Hull: " + hull;
    }
}
