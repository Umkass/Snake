using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Fruits")]
    [SerializeField] GameObject[] _fruitPrefab;
    [Space]
    [SerializeField] GameObject _obstaclePrefab;
    int amountOfObstacles;
    [Space]
    [SerializeField] float repeatSpawnFoodRate;
    [Space]
    [SerializeField] GameObject SnakePrefab;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        StartCoroutine(SpawnSnakeCoroutine());
        InvokeRepeating("SpawnFood", 2, repeatSpawnFoodRate);
        StartCoroutine(SpawnObstacleCoroutine());
    }

    void SpawnFood()
    {
        int x = (int)Random.Range(ScreenBounds.left, ScreenBounds.right);
        int y = (int)Random.Range(ScreenBounds.bottom, ScreenBounds.top);
        Instantiate(_fruitPrefab[Random.Range(0, _fruitPrefab.Length)], new Vector2(x, y), Quaternion.identity);
    }
    IEnumerator SpawnObstacleCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        amountOfObstacles = Random.Range(3, 6);
        SpawnObstacle(amountOfObstacles);
        yield break;
    }
    void SpawnObstacle(int amount)
    {
        while (amount > 0)
        {
            int x = (int)Random.Range(ScreenBounds.left,ScreenBounds.right);
            int y = (int)Random.Range(ScreenBounds.bottom,ScreenBounds.top);
            //избегаем 0;
            if (x < 2 && x >= 0)
                x += 2;
            else if (x > -2 && x < 0)
                x -= 2;
            if (y < 2 && y >= 0)
                y += 2;
            else if (y > -2 && y < 0)
                y -= 2;
            Instantiate(_obstaclePrefab, new Vector2(x, y), Quaternion.identity);
            amount--;
        }
    }
    IEnumerator SpawnSnakeCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        SpawnSnake();
        yield break;
    }
    void SpawnSnake()
    {
        Instantiate(SnakePrefab, new Vector2(0, 0), Quaternion.identity);
    }

    public void GameOver()
    {
        StopAllCoroutines();
        CancelInvoke("SpawnFood");
        Time.timeScale = 0;
        HUD.Instance.ShowStatus("Game Over!");
        Time.timeScale = 1;
        StartCoroutine(StartGameCoroutine());
    }
    public void LevelComplete()
    {
        StopAllCoroutines();
        CancelInvoke("SpawnFood");
        Time.timeScale = 0;
        HUD.Instance.ShowStatus("How did you win?");
        HUD.Instance.ShowLevelCompleteButtons();
    }
    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        StartGame();
        yield break;
    }
}
