using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spaceshuttle : MonoBehaviour
{
    public UnityAction<int> OnFuelChanged;
    public UnityAction<int> OnHullChanged;
    public UnityAction<int> OnTravel;

    [SerializeField] public float travelRadius = 1.3f;
    [SerializeField] private int fuel = 100;
    [SerializeField] private int hull = 50;
    [SerializeField] private List<AudioClip> travelSounds;

    private int travelpointsVisited = 0;

    public void Travel()
    {
        fuel -= 10;
        hull -= 5;
        travelpointsVisited++;
        OnTravel?.Invoke(travelpointsVisited);
    }

    internal void AddResources(int fuelEffect, int repairEffect)
    {
        fuel += fuelEffect;
        OnFuelChanged?.Invoke(fuel);
        hull += repairEffect;
        OnHullChanged?.Invoke(hull);
        CheckTravelCondition();
    }

    private void CheckTravelCondition()
    {
        if (hull <= 0)
        {
            ShowGameOverUI("Destoyed hull. Your spaceshuttle is too damaged to explore space further. \r\n You have reached " + travelpointsVisited + " travel points!");
        }
        if (fuel <= 0)
        {
            ShowGameOverUI("Out of fuel! You glide aimlessly through space... \r\n You have reached " + travelpointsVisited + " travel points!");
        }
        if (!AvailableTravelpoints())
        {
            ShowGameOverUI("You are lost in time and space because there is no nearby destination. \r\n You have reached " + travelpointsVisited + " travel points!");
        }
    }

    private void ShowGameOverUI(string gameOverText)
    {
        GameOverUI.instance.GameOver(gameOverText);
        GameManager.instance.activeGame = false;
    }

    private bool AvailableTravelpoints()
    {
        return Physics2D.OverlapCircleAll(transform.position, travelRadius).Length != 0;
    }

    public int GetFuel() { return fuel; }
    public int GetHull() { return hull; }
    public float GetTravelRadius() { return travelRadius; }
    public int GetTravelPoints() { return travelpointsVisited; }
    public List<AudioClip> GetTravelSounds() { return travelSounds; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, travelRadius);
    }

}
