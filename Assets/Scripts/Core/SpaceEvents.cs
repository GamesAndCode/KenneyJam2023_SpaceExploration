using System.Collections.Generic;
using UnityEngine;

public class SpaceEvents : MonoBehaviour
{
    private ScreenSpawner screenSpawner;

    [SerializeField] private GameObject ufo;
    [SerializeField] private GameObject astronaut;
    [SerializeField] private List<GameObject> meteors;
    [SerializeField] private GameObject coffee;
    [SerializeField] private GameObject planet;
    [SerializeField] private List<GameObject> satelites;
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject teamug;
    [SerializeField] private GameObject rocket;

    void Start()
    {
        screenSpawner = new ScreenSpawner();
        GameManager.instance.GetCurrentSpaceship().OnTravel += EventSpawner;
    }

    public void EventSpawner(int traveledPlanets)
    {
        switch (traveledPlanets)
        {
            case 5:
                SpawnObject(ufo, false, Random.Range(5, 7));
                break;
            case 15:
                SpawnObject(astronaut, true, Random.Range(12, 15));
                break;
            case 25:
                SpawnObject(coffee, true, Random.Range(5, 7));
                break;
            case 35:
                SpawnObjects(meteors, true, Random.Range(5, 7));
                break;
            case 45:
                SpawnObject(teamug, true, Random.Range(5, 7));
                break;
            case 55:
                SpawnObject(spaceShip, true, Random.Range(7, 9));
                break;
            case 70:
                SpawnObject(rocket, true, Random.Range(12, 15));
                break;
            case 85:
                SpawnObjects(satelites, true, Random.Range(12, 15));
                break;
            case 100:
                SpawnObject(planet, false, Random.Range(12, 15));
                break;
        }
    }

    private void SpawnObject(GameObject objPrefab, bool rotate, float duration)
    {
        Vector2 startingPoint = screenSpawner.GetRandomPositionOutOfScreen(Random.value > 0.5, Random.value > 0.5, Random.value > 0.5, Random.value > 0.5);
        Vector2 endPoint = screenSpawner.MirrorPointAtMiddleOfScreen(startingPoint);

        GameObject obj = Instantiate(objPrefab, startingPoint, Quaternion.identity, transform);
        if (rotate)
        {
            LeanTween.rotateAround(obj, Vector3.back, Random.Range(180, 360), duration);
        }
        LeanTween.move(obj, endPoint, duration).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.scale(obj, Vector3.zero, 0.5f).setEaseOutCubic().setOnComplete(() =>
            {
                Destroy(obj);
            });
        });
    }

    private void SpawnObjects(List<GameObject> objs, bool rotate, float duration)
    {
        foreach (GameObject objPrefab in objs)
        {
            SpawnObject(objPrefab, rotate, duration);
        }
    }

}
