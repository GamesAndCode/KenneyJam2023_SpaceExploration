using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Texture2D customCursor;
    [SerializeField] private List<Spaceshuttle> spaceships;

    private Spaceshuttle currentSpaceship;
    private int highestTravelpointScore = 0;

    public bool activeGame { get; set; }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject.transform.parent);

        SceneManager.sceneLoaded += OnSceneLoaded;

        //Starting the game in game scene
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndexes.GAME)
        {
            SetCurrentSpaceship(0);
        }

        //Starting the game in persistence scene
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndexes.MANAGER)
        {
            SceneManager.LoadSceneAsync((int)SceneIndexes.UI_OVERLAY, LoadSceneMode.Additive);
        }

        //Load the persistence scene if not already loaded
        if (SceneManager.GetActiveScene().buildIndex != (int)SceneIndexes.MANAGER)
        {
            SceneManager.LoadSceneAsync((int)SceneIndexes.MANAGER, LoadSceneMode.Additive);
        }

    }
    private void Start()
    {
        //Add cursor to the game
        //Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
    }

    public void LoadGame(int shipNumber)
    {
        activeGame = true;
        SetCurrentSpaceship(shipNumber);

        SceneManager.LoadSceneAsync((int)SceneIndexes.GAME, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync((int)SceneIndexes.UI_OVERLAY);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == (int)SceneIndexes.GAME)
        {
            currentSpaceship.gameObject.SetActive(true);
            activeGame = true;
        }
        if (scene.buildIndex == (int)SceneIndexes.MANAGER)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)SceneIndexes.MANAGER));
        }
    }

    internal void GameOver()
    {
        highestTravelpointScore = currentSpaceship.GetTravelPoints() > highestTravelpointScore ? currentSpaceship.GetTravelPoints() : highestTravelpointScore;
        SceneManager.LoadScene((int)SceneIndexes.UI_OVERLAY, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync((int)SceneIndexes.GAME);
        Destroy(currentSpaceship.gameObject);
        activeGame = false;
    }

    public void SetCurrentSpaceship(int spaceshipNumber)
    {
        currentSpaceship = Instantiate(spaceships[spaceshipNumber]);
        // Will be activated when the game scene is loaded
        currentSpaceship.gameObject.SetActive(false);
    }

    public Spaceshuttle GetCurrentSpaceship() { return currentSpaceship; }
    public int GetHighestTravelpointScore() { return highestTravelpointScore; }
    public List<Spaceshuttle> GetSpaceships() { return spaceships; }

}
