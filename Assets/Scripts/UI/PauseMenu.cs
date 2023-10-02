using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Button keepPlayingButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private AudioClip gameOverSound;

    private bool isPaused = false;

    private void Awake()
    {
        keepPlayingButton.onClick.AddListener(() => { KeepPlaying(); });
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1;
            GameManager.instance.GameOver(); 
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            KeepPlaying();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        container.gameObject.transform.localScale = Vector3.zero;
        container.gameObject.SetActive(true);
        LeanTween.scale(container.gameObject, Vector3.one, 0.3f).setIgnoreTimeScale(true);
        AudioManager.instance.sfxAudioSource.PlayOneShot(gameOverSound);
    }

    private void KeepPlaying()
    {
        isPaused = false;
        Time.timeScale = 1;
        container.gameObject.SetActive(false);
    }
}
