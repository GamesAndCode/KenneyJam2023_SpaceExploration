using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelManager : MonoBehaviour
{
    public static TravelManager instance;
    public TravelInfoUI travelInfoUI;

    private Spaceshuttle spaceship;
    private bool canTravel = false;
    private bool isTraveling = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        spaceship = GameManager.instance.GetCurrentSpaceship();
    }

    public void TravelTo(Travelpoint travelpoint)
    {
        if (!GameManager.instance.activeGame) { return; }

        canTravel = CheckTravelPossibility(travelpoint);

        if (canTravel && !isTraveling)
        {
            canTravel = false;
            isTraveling = true;
            spaceship.Travel();

            MoveToTravelpoint(travelpoint);
            AudioManager.instance.PlayRandomSFXSound(spaceship.GetTravelSounds());

            LeanTween.scale(travelpoint.gameObject, Vector3.zero, 0.4f).setEase(LeanTweenType.easeOutQuart).setOnComplete(() =>
            {
                travelpoint.gameObject.SetActive(false);
            });
        }
    }

    private void MoveToTravelpoint(Travelpoint travelpoint)
    {
        Vector2 directionVector = travelpoint.transform.position - spaceship.transform.position;
        float angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        LeanTween.rotateZ(spaceship.gameObject, angle - 90, 0.2f).setEase(LeanTweenType.easeOutQuart).setOnComplete(() =>
        {
            LeanTween.move(spaceship.gameObject, travelpoint.transform.position, 0.5f).setEase(LeanTweenType.easeOutQuart).setOnComplete(() =>
            {
                isTraveling = false;
                //calculate new values
                spaceship.AddResources(travelpoint.GetFuelEffect(), travelpoint.GetRepairEffect());
                Destroy(travelpoint.gameObject);
            });
        });
    }

    private bool CheckTravelPossibility(Travelpoint travelpoint)
    {
        //Check if spaceship is in range of travelpoint
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spaceship.transform.position, spaceship.GetTravelRadius());
        foreach (Collider2D collider in colliders)
        {
            Travelpoint possibleDestination = collider.gameObject.GetComponent<Travelpoint>();
            if (possibleDestination != null && possibleDestination == travelpoint)
            {
                return true;
            }
        }
        return false;
    }
}
