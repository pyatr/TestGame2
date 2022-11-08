using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const string ATTEMPTS_COUNTER_PREFS_KEY = "Attempts";

    public static event Action OnGameStart = delegate { };
    public static event Action OnGameEnd = delegate { };

    public Vector3 GameSpeed => gameSpeed;

    public int AttemptsMade
    {
        get => PlayerPrefs.GetInt(ATTEMPTS_COUNTER_PREFS_KEY, 0);
        set => PlayerPrefs.SetInt(ATTEMPTS_COUNTER_PREFS_KEY, value);
    }
    public float Timer => timer;

    [SerializeField]
    private List<ObstacleSegment> obstacleSegments;
    [SerializeField]
    private List<ParallaxSprite> parallaxes;

    [SerializeField]
    private GameObject gameOverObject;
    [SerializeField]
    private GameObject worldObject;

    [SerializeField]
    private Ball playerTemplate;
    [SerializeField]
    private Transform playerSpawnPlace;
    [SerializeField]
    private Transform obstaclesStartPlace;

    [SerializeField, Range(0.1f, 30f)]
    private float startGameSpeed = 1f;

    [SerializeField, Range(0.01f, 10f)]
    private float defaultGravityScale = 0.25f;
    [SerializeField, Range(0.01f, 10f)]
    private float gravityScaleIncrement = 0.05f;
    [SerializeField]
    private float speedGrowthDelayTime = 15f;
    [SerializeField]
    private float maxSegmentDistanceX = -10f;

    private WaitForSeconds speedGrowthDelay;
    private Coroutine gameCoroutine = null;
    private Coroutine speedGrowth = null;
    private Ball player;

    private Vector3 gameSpeed;
    private Vector3 obstacleMoveDistance;

    private float startGravityScale = 0f;
    private float timer = 0f;

    private void Awake()
    {
        speedGrowthDelay = new WaitForSeconds(speedGrowthDelayTime);
        float obstaclesLength = 0f;
        foreach (ObstacleSegment segment in obstacleSegments)
        {
            obstaclesLength += segment.Width;
            segment.GenerateWalls();
        }

        obstacleMoveDistance = new Vector3(obstaclesLength, 0, 0);
    }

    private void ArrangeObstacles()
    {
        obstacleSegments[0].transform.localPosition = obstaclesStartPlace.localPosition;
        for (int i = 1; i < obstacleSegments.Count; i++)
        {
            Transform current = obstacleSegments[i].transform;
            current.localPosition = new Vector3(obstacleSegments[i - 1].transform.localPosition.x + obstacleSegments[i - 1].Width, current.localPosition.y, current.localPosition.z);
        }
    }

    private IEnumerator GameRoutine()
    {
        while (isActiveAndEnabled)
        {
            yield return null;
            timer += Time.deltaTime;
            for (int i = 0; i < obstacleSegments.Count; i++)
            {
                ObstacleSegment segment = obstacleSegments[i];
                segment.transform.localPosition += gameSpeed * Time.deltaTime;
                if (segment.transform.localPosition.x <= maxSegmentDistanceX)
                {
                    segment.transform.localPosition += obstacleMoveDistance;
                    segment.GenerateWalls();
                }
            }
        }
        StopGame();
    }

    public void SetStartGravityScale(float newScale)
    {
        Debug.Log($"Gravity scale changed from {startGravityScale} to {newScale}");
        startGravityScale = newScale;
    }

    public void SetGameSpeed(float newSpeed)
    {
        Debug.Log($"Game speed changed from {startGameSpeed} to {newSpeed}");
        startGameSpeed = newSpeed;
    }

    [ContextMenu("Start")]
    public void StartGame()
    {
        if (gameCoroutine == null)
        {
            gameOverObject.SetActive(false);
            worldObject.SetActive(true);
            gameSpeed = Vector3.left * startGameSpeed;
            player = Instantiate(playerTemplate);
            player.transform.position = playerSpawnPlace.position;
            player.GameController = this;
            player.GravityScale = startGravityScale == 0f ? defaultGravityScale : startGravityScale;
            timer = 0f;
            parallaxes.ForEach(p => p.Restore());
            ArrangeObstacles();
            gameCoroutine = StartCoroutine(GameRoutine());
            speedGrowth = StartCoroutine(SpeedGrowth());
            OnGameStart.Invoke();
        }
    }

    [ContextMenu("Stop")]
    public void StopGame()
    {
        if (gameCoroutine != null)
        {
            AttemptsMade++;
            StopCoroutine(gameCoroutine);
            StopCoroutine(speedGrowth);
            gameCoroutine = null;
            speedGrowth = null;
            gameSpeed = Vector3.zero;
            gameOverObject.SetActive(true);
            worldObject.SetActive(false);
            OnGameEnd.Invoke();
        }
    }

    private IEnumerator SpeedGrowth()
    {
        while (isActiveAndEnabled)
        {
            yield return speedGrowthDelay;
            player.GravityScale += gravityScaleIncrement;
            Debug.Log($"Gravity scale increased to {player.GravityScale}");
        }
    }
}
