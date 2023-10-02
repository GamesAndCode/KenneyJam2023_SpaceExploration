using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TraveledPlanets : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI traveledPlanetsText;

    private void Start()
    {
        GameManager.instance.GetCurrentSpaceship().OnTravel += SetText;
    }

    private void SetText(int traveledPlanets)
    {
        traveledPlanetsText.text = traveledPlanets.ToString();
    }
}
