using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject container;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private AudioClip gameOverSound;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        mainMenuButton.onClick.AddListener(() => { GameManager.instance.GameOver(); });
        quitButton.onClick.AddListener(() => { Application.Quit(); });
    }

    private void Start()
    {
        container.gameObject.SetActive(false);
    }

    public void GameOver(string text)
    {
        AudioManager.instance.sfxAudioSource.PlayOneShot(gameOverSound);
        gameOverText.text = text;
        container.gameObject.transform.localScale = Vector3.zero;
        container.gameObject.SetActive(true);
        float animationTime = 0.3f;
        LeanTween.scale(container.gameObject, Vector3.one, animationTime).setIgnoreTimeScale(true);
    }


}
