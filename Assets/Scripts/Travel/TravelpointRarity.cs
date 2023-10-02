using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TravelpointRarity : MonoBehaviour
{
    //The higher the int, the more likely the travelpointType is chosen
    private Dictionary<TravelpointType, int> travelpointRarity;
    private Random random;
    private int totalRarity;

    private void Awake()
    {
        travelpointRarity = new();
        random = new();
        travelpointRarity.Add(TravelpointType.RepairShipLow, 5);
        travelpointRarity.Add(TravelpointType.RepairShipMedium, 5);
        travelpointRarity.Add(TravelpointType.RepairShipHigh, 5);
        travelpointRarity.Add(TravelpointType.GetFuelLow, 10);
        travelpointRarity.Add(TravelpointType.GetFuelMedium, 10);
        travelpointRarity.Add(TravelpointType.GetFuelHigh, 10);
        travelpointRarity.Add(TravelpointType.RepairShipAndGetFuelLow, 5);
        travelpointRarity.Add(TravelpointType.RepairShipAndGetFuelMedium, 5);
        travelpointRarity.Add(TravelpointType.Unknown, 10);

        foreach (int rarity in travelpointRarity.Values)
        {
            totalRarity += rarity;
        }
    }

    public TravelpointType GetRandomTravelpointType()
    {
        TravelpointType nextTravePointType = TravelpointType.RepairShipLow;
        int randomNumber = random.Next(0, totalRarity);
        int cumulativeRarity = 0;
        foreach (KeyValuePair<TravelpointType, int> entry in travelpointRarity)
        {
            cumulativeRarity += entry.Value;
            //if the random number is smaller than the cumulative rarity, the travelpointType is chosen
            if (randomNumber < cumulativeRarity)
            {
                nextTravePointType = entry.Key;
                break;
            }
        }
        return nextTravePointType;
    }

}
