using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HexGridLayout)), RequireComponent(typeof(TravelpointRarity)), RequireComponent(typeof(TravelpointFactory))]
public class WorldCreator : MonoBehaviour
{
    private TravelpointRarity travelpointRarity;
    private TravelpointFactory travelpointFactory;
    private HexGridLayout hexGridLayout;
    private Vector3 centerOfGrid;

    private void Awake()
    {
        travelpointRarity = GetComponent<TravelpointRarity>();
        travelpointFactory = GetComponent<TravelpointFactory>();
        hexGridLayout = GetComponent<HexGridLayout>();
    }

    private void Start()
    {
        Dictionary<int, List<HexRenderer>> hexRows = hexGridLayout.GetHexRows();
        centerOfGrid = hexGridLayout.GetCenterOfGrid();

        GameManager.instance.GetCurrentSpaceship().transform.position = centerOfGrid;

        //iterate grid
        foreach (List<HexRenderer> rows in hexRows.Values)
        {
            foreach (HexRenderer hex in rows)
            {
                //Currently set two points on each hex, which nearly fills the grid with travelpoints. Empty spaces at the edges are possible.
                PostionTravelPoint(hex.transform.position + hex.GetFace(0).GetMiddlePointRight());
                PostionTravelPoint(hex.transform.position + hex.GetFace(0).GetMiddlePointLeft());
            }
        }
    }

    private void PostionTravelPoint(Vector3 spawnPoint)
    {
        Travelpoint travelpoint = travelpointFactory.CreateTravelpoint(travelpointRarity.GetRandomTravelpointType());
        travelpoint.transform.position = spawnPoint;
        if (centerOfGrid == spawnPoint)
        {
            Destroy(travelpoint.gameObject);
        }
    }
}
