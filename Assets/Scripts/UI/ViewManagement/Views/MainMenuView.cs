using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _credits;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;
    public override void Initialize()
    {
        _startGameButton.onClick.AddListener(() => ViewManager.Show<ChooseShipView>());
        _settingsButton.onClick.AddListener(() => ViewManager.Show<SettingsMenuView>());
        _credits.onClick.AddListener(() => ViewManager.Show<CreditsMenuView>());
        _quitButton.onClick.AddListener(() => Application.Quit());

    }
}
