using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Travelpoint : MonoBehaviour
{
    private TravelpointType travelpointType;
    private int fuelEffect = 0;
    private int repairEffect = 0;
    private Collider2D clickCollider;

    private void Awake()
    {
        clickCollider = GetComponent<Collider2D>();
    }

    public void SetFuelEffect(int fuelEffect) { this.fuelEffect = fuelEffect; }
    public void SetRepairEffect(int repairEffect) { this.repairEffect = repairEffect; }
    public void SetTravelPointType(TravelpointType travelpointType) { this.travelpointType = travelpointType; }

    public int GetFuelEffect() { return fuelEffect; }
    public int GetRepairEffect() { return repairEffect; }

    private void OnMouseDown()
    {
        if (clickCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            TravelManager.instance.TravelTo(this);
        }
    }

    private void OnMouseOver()
    {
        if (clickCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            if (travelpointType == TravelpointType.Unknown)
            {
                TravelManager.instance.travelInfoUI.UpdateFuelText("???");
                TravelManager.instance.travelInfoUI.UpdateHullText("???");
            }
            else
            {
                TravelManager.instance.travelInfoUI.UpdateFuelText((fuelEffect == 0 ? "" : "+") + fuelEffect.ToString());
                TravelManager.instance.travelInfoUI.UpdateHullText((repairEffect == 0 ? "" : "+") + repairEffect.ToString());
            }
        }
    }


}
