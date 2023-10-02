using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/*
 * Room for improvement to make this more dynamic
 */
public class ChooseShipView : View
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _ship1Button;
    [SerializeField] private TextMeshProUGUI _ship1statsText;
    [SerializeField] private Button _ship2Button;
    [SerializeField] private TextMeshProUGUI _ship2statsText;
    [SerializeField] private TextMeshProUGUI ship2RequireText;
    [SerializeField] private Button _ship3Button;
    [SerializeField] private TextMeshProUGUI _ship3statsText;
    [SerializeField] private TextMeshProUGUI ship3RequireText;
    [SerializeField] private Image lockeShip2;
    [SerializeField] private Image lockeShip3;

    private int ship2TravelPoints = 40;
    private int ship3TravelPoints = 70;

    public override void Initialize()
    {
        List<Spaceshuttle> spaceships = GameManager.instance.GetSpaceships();
        for(int i = 0; i < spaceships.Count; i++)
        {
            //Set Ship stats text
            SetShipText(_ship1statsText, spaceships[i].GetFuel(), spaceships[i].GetHull());
        }

        //Set Ship require text
        ship2RequireText.text = $"Requires {ship2TravelPoints} travel points";
        ship3RequireText.text = $"Requires {ship3TravelPoints} travel points";

        //Choose ship buttons
        _ship1Button.onClick.AddListener(() =>
        {
            GameManager.instance.LoadGame(0);
        });
        _ship2Button.onClick.AddListener(() =>
        {
            if (GameManager.instance.GetHighestTravelpointScore() >= ship2TravelPoints)
            {
                GameManager.instance.LoadGame(1);
            }
        });
        _ship3Button.onClick.AddListener(() =>
        {
            if (GameManager.instance.GetHighestTravelpointScore() >= ship3TravelPoints)
            {
                GameManager.instance.LoadGame(2);
            }
        });

        //Lock images
        lockeShip2.gameObject.SetActive(GameManager.instance.GetHighestTravelpointScore() < ship2TravelPoints);
        lockeShip3.gameObject.SetActive(GameManager.instance.GetHighestTravelpointScore() < ship3TravelPoints);

        _backButton.onClick.AddListener(() => ViewManager.ShowLast());
    }

    private void SetShipText(TextMeshProUGUI text, int fuel, int hull)
    {
        text.text = $"Fuel: {fuel} \r\n Hull: {hull}";
    }

}
