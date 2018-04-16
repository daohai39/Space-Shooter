using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {
    public float spawnStart;
    public float spawnWait;
    public float waveWait;

    public GameObject player;
    public List<Enemy> enemies;
    public Vector3 spawnValues;

    public GUIText scoreText;
    public GUIText healthText;
    public GUIText restartText;
    public GUIText powerText;
    public GUIText gameOverText;

    public EnemyManager _enemyManager;
    //private BoltManager _boltManager;
    private int _score;
    private bool _gameOver;
    private bool _isPause;
    private PlayerController _playerController;

	private void Start()
	{
        _enemyManager = new EnemyManager();
        _score = 0;
        _gameOver = false;
        _playerController = player.GetComponent<PlayerController>();
        _isPause = false;
        UpdateGUIText();
        StartCoroutine(SpawnEnemy());
	}

	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPause) Pause();
            else UnPause();
        }
        if (_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Pause()
    {
        //menu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        _isPause = true;
    }

    private void UnPause()
    {
        //menu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        _isPause = false;
    }


    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnStart);
        int index = 0;
        while (true) {
            for (var i = 0; i < 10; i++) {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Enemy enemy = (Enemy)Instantiate(enemies[Random.Range(0, enemies.Count)], spawnPosition, Quaternion.identity);
                enemy.SetID(index);
                index++;
                _enemyManager.AddEnemy(enemy);
                Debug.Log(enemy.name);
                yield return new WaitForSeconds(spawnWait);
            }
            if (_gameOver) {
                UpdateRestartText();
                break;
            }
            yield return new WaitForSeconds(waveWait);
        }    
    }

    private void UpdateGUIText()
    {
        UpdateScoreText();
        UpdatePowerText();
        UpdateHealthText();
        UpdateRestartText();
        UpdateGameOverText();
    }

    public void UpdateScoreText() { scoreText.text = "Score: " + _score; }
    public void UpdateHealthText() { healthText.text = "Health: " + _playerController.Health; }
    public void UpdatePowerText() { powerText.text = "Power: " + _playerController.Power; }

    public void UpdateGameOverText()
    {
        if (_gameOver)
            gameOverText.text = "Game Over!";
        else gameOverText.text = "";
    }
    public void UpdateRestartText()
    {
        if (_gameOver)
            restartText.text = "Press 'R' to restart!";
        else restartText.text = "";
    }

    public void GameOver()
    {
        _gameOver = true;
        UpdateGameOverText();
        UpdateRestartText();
    }


    public void AddScore(int score)
    {
        _score += score;
        UpdateScoreText();
    }

    public void ReduceHealth()
    {
        _playerController.ReduceHealth();
        UpdateHealthText();
    }

    public void ReducePower()
    {
        _playerController.ReducePower();
        UpdatePowerText();
    }

    public void IncreaseHealth()
    {
        _playerController.AddHealth();
        UpdateHealthText();
    }

    public void IncreasePower()
    {
        _playerController.AddPower();
        UpdatePowerText();
    }

    public bool IsGameOver() { return _gameOver || _playerController.Health <= 0; }

}
