using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    private ScreenSpawner screenSpawner;

    [SerializeField] private GameObject LeftToRightUfo;
    [SerializeField] private GameObject RightToLeftUfo;
    [SerializeField] private GameObject spark1;
    [SerializeField] private GameObject spark2;

    void Start()
    {
        screenSpawner = new ScreenSpawner();
        StartCoroutine(MoveFromLeftToRight());
        StartCoroutine(MoveFromRightToLeft());
    }


    private IEnumerator MoveFromLeftToRight()
    {
        while (true)
        {
            Vector3 spawnPoint = screenSpawner.GetRandomPositionOutOfScreen(true, false, false, false);
            Vector3 endPoint = screenSpawner.MirrorPointAtMiddleOfScreen(spawnPoint);
            
            GameObject ufo = Instantiate(LeftToRightUfo, new Vector3(spawnPoint.x, Random.Range(1, 3), 0), Quaternion.Euler(0,0, -12), transform);
            LeanTween.moveX(ufo, endPoint.x, Random.Range(8, 12)).setEaseInOutSine();
            yield return new WaitForSeconds(Random.Range(12, 18));
            Destroy(ufo);
        }
    }

    private IEnumerator MoveFromRightToLeft()
    {
        while (true)
        {
            Vector3 spawnPoint = screenSpawner.GetRandomPositionOutOfScreen(false, false, true, false);
            Vector3 endPoint = screenSpawner.MirrorPointAtMiddleOfScreen(spawnPoint);

            GameObject ufo = Instantiate(RightToLeftUfo, new Vector3(spawnPoint.x, Random.Range(-1, -3), 0), Quaternion.Euler(0, 0, 12), transform);
            LeanTween.moveX(ufo, endPoint.x, Random.Range(8, 12)).setEaseInOutSine();
            yield return new WaitForSeconds(Random.Range(12, 18));
            Destroy(ufo);
        }
    }
}
