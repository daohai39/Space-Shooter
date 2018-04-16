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

	public EnemyManager EnemyManager
	{
	    get { return _enemyManager; }
	}

    public BoltManager BoltManager
    {
        get { return _boltManager; }
    }

	private EnemyManager _enemyManager;
	private BoltManager _boltManager;
	private int _score;
	private bool _gameOver;
	private bool _isPause;
	private PlayerController _playerController;

	private void Start()
	{
		_enemyManager = new EnemyManager();
	    _boltManager = new BoltManager();
		_score = 0;
		_gameOver = false;
		_playerController = player.GetComponent<PlayerController>();
		_isPause = false;
		UpdateGUIText();
		StartCoroutine(SpawnEnemy());
	}

	private void Update()
	{
	    if (Input.GetButton("Fire1"))
	    {
	        PlayerBolt bolt = _playerController.Fire();
	        if (bolt != null)
	            _boltManager.AddPlayerBolt(bolt);
	    }
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
				enemy.ID = index;
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

	public void UpdateScoreText() { scoreText.text = TextHelper.Score + _score; }
	public void UpdateHealthText() { healthText.text = TextHelper.Health + _playerController.Health; }
	public void UpdatePowerText() { powerText.text = TextHelper.Power + _playerController.Power; }

	public void UpdateGameOverText()
	{
		if (_gameOver)
			gameOverText.text = TextHelper.GameOver;
		else gameOverText.text = TextHelper.Empty;
	}
	public void UpdateRestartText()
	{
		if (_gameOver)
			restartText.text = TextHelper.Restart;
		else restartText.text = TextHelper.Empty;
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
