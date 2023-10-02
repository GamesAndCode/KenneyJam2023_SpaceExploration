using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TravelpointFactory : MonoBehaviour
{

    [SerializeField] private List<Travelpoint> regularTravelpoints;
    [SerializeField] private Travelpoint specialTravelpoint;
    [SerializeField] private Travelpoint unknownTravelpoint;
    private Random random;

    private void Awake()
    {
        random = new();
    }

    public Travelpoint CreateTravelpoint(TravelpointType travelpointType)
    {
        Travelpoint travelpointPrefab = null;

        int fuelEffect = 0;
        int repairEffect = 0;

        switch (travelpointType)
        {
            case TravelpointType.Unknown:
                travelpointPrefab = unknownTravelpoint;
                if (random.NextDouble() < 0.5f)
                {
                    fuelEffect = -5;
                    repairEffect = -5;
                }
                else
                {
                    fuelEffect = 15;
                    repairEffect = 15;
                }
                break;
            case TravelpointType.RepairShipAndGetFuelLow:
                travelpointPrefab = specialTravelpoint;
                fuelEffect = 5;
                repairEffect = 5;
                break;
            case TravelpointType.RepairShipAndGetFuelMedium:
                travelpointPrefab = specialTravelpoint;
                fuelEffect = 10;
                repairEffect = 10;
                break;
            case TravelpointType.RepairShipLow:
                travelpointPrefab = regularTravelpoints[0];
                repairEffect = 5;
                break;
            case TravelpointType.RepairShipMedium:
                travelpointPrefab = regularTravelpoints[1];
                repairEffect = 10;
                break;
            case TravelpointType.RepairShipHigh:
                travelpointPrefab = regularTravelpoints[2];
                repairEffect = 15;
                break;
            case TravelpointType.GetFuelLow:
                travelpointPrefab = regularTravelpoints[3];
                fuelEffect = 5;
                break;
            case TravelpointType.GetFuelMedium:
                travelpointPrefab = regularTravelpoints[4];
                fuelEffect = 10;
                break;
            case TravelpointType.GetFuelHigh:
                travelpointPrefab = regularTravelpoints[5];
                fuelEffect = 15;
                break;
        }

        Travelpoint travelpoint =  Instantiate(travelpointPrefab, transform);
        travelpoint.SetFuelEffect(fuelEffect);
        travelpoint.SetRepairEffect(repairEffect);
        travelpoint.SetTravelPointType(travelpointType);
        return travelpoint;
    }
}
