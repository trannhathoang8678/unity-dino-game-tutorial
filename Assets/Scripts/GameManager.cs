using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public TextMeshProUGUI gameOverText;
    public Button retryButton;

    private Player player;
    private Spawner[] spawners;

    private Vector3 startPosition = new Vector3(-6, 0, 0);

    private float score;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawners = FindObjectsOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles) {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        for (int i = 0; i < spawners.Length; i++)
        {
            for (int j = 0; j < spawners[i].objects.Length; j++)
            {
                if (spawners[i].objects[j].prefab.name == "Bird" ||
                    spawners[i].objects[j].prefab.name == "Meteor" ||
                    spawners[i].objects[j].prefab.name == "Meteorite_01")
                {
                    spawners[i].objects[j].spawnChance = 0f;
                }
                
            }
        }

        player.gameObject.SetActive(true);
        foreach (Spawner spawner in spawners)
        {
            spawner.gameObject.SetActive(true);
        }
            
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHiscore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        player.gameObject.transform.position = startPosition;
        foreach (Spawner spawner in spawners)
        {
            spawner.gameObject.SetActive(false);
        }
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiscore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime * 2f;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        if (score >= 100)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                for (int j = 0; j < spawners[i].objects.Length; j++)
                {
                    if (spawners[i].objects[j].prefab.name == "Bird")
                    {
                        spawners[i].objects[j].spawnChance = 0.2f;
                    }
                }
            }
        }
        if (score >= 200)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                for (int j = 0; j < spawners[i].objects.Length; j++)
                {
                    if (spawners[i].objects[j].prefab.name == "Meteor")
                    {
                        spawners[i].objects[j].spawnChance = 0.16f;
                    }
                    if (spawners[i].objects[j].prefab.name == "Meteorite_01")
                    {
                        spawners[i].objects[j].spawnChance = 0.2f;
                    }
                }
            }

        }


    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

}
