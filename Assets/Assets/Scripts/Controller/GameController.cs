using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public float spawnStart;
	public float spawnWait;
	public float waveWait;

	public PlayerController player;
	public List<Enemy> enemies;
	public Vector3 spawnValues;

	public GUIText scoreText;
	public GUIText healthText;
	public GUIText restartText;
	public GUIText powerText;
	public GUIText gameOverText;
    public GameObject menu;

    private int _score;
    private bool _gameOver;
    private bool _isPause;
	private EnemyManager _enemyManager;
    private EnemyBoltManager _enemyBoltManager;
    private PlayerBoltManager _playerBoltManager;

    public EnemyManager EnemyManager { get { return _enemyManager; } }

    public EnemyBoltManager EnemyBoltManager { get { return _enemyBoltManager; } }

    public PlayerBoltManager PlayerBoltManager { get { return _playerBoltManager; } }

	private void Awake()
	{
        SetInitialState();
        //if (!PlayerPrefs.HasKey("saved")) PlayerPrefs.SetInt("saved", 0);
        Pause();
	}

	private void Start()
	{
		UpdateGUIText();
		StartCoroutine(SpawnEnemy());
        Cursor.lockState = CursorLockMode.Confined;
	}

	private void Update()
	{
	    if (Input.GetButton("Fire1"))
	    {
            PlayerBolt bolt = player.Fire();
            if (bolt != null)
                _playerBoltManager.AddBolt(bolt);
	    }
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (_isPause) UnPause();
			else Pause();
		}
		if (_gameOver)
		{
            if (Input.GetKeyDown(KeyCode.R))
                RestartGame();
		}
	}

    private void RestartGame()
    {
        ClearScene();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    //Not Implemented
	private void Pause()
	{
		menu.SetActive(true);
		Cursor.visible = true;
		Time.timeScale = 0;
		_isPause = true;
	}

    //Not Implemented
	private void UnPause()
	{
		menu.SetActive(false);
		Cursor.visible = false;
		Time.timeScale = 1;
		_isPause = false;
	}

    private Save CreatSaveGameObject()
    {
        Save save = new Save();
        save.health = player.Health;
        save.power = player.Power;
        save.score = _score;

        return save;
    }


	private IEnumerator SpawnEnemy()
	{
		yield return new WaitForSeconds(spawnStart);
		while (true) {
			for (var i = 0; i < 10; i++) {
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Enemy enemy = (Enemy)Instantiate(enemies[Random.Range(0, enemies.Count)], spawnPosition, Quaternion.identity);
				_enemyManager.AddEnemy(enemy);
				yield return new WaitForSeconds(spawnWait);
			}
			if (_gameOver) {
				UpdateRestartText();
				break;
			}
			yield return new WaitForSeconds(waveWait);
		}    
	}

    private void ClearScene()
    {
        EnemyManager.ClearEnemy();
        EnemyBoltManager.ClearBolt();
        PlayerBoltManager.ClearBolt();
    }

	private void UpdateGUIText()
	{
		UpdateScoreText();
		UpdatePowerText();
		UpdateHealthText();
		UpdateRestartText();
		UpdateGameOverText();
	}

    private void SetInitialState()
    {
        _enemyManager = new EnemyManager();
        _enemyBoltManager = new EnemyBoltManager();
        _playerBoltManager = new PlayerBoltManager();
        _score = 0;
        _gameOver = false;
        player.SetDefault();
    }

    public void NewGame()
    {
        //Clear objects in scene
        ClearScene();
        //Set point to default
        SetInitialState();
        //Update GUI Text
        UpdateGUIText();
        //Reopen || Open scene
        SceneManager.GetSceneByName(SceneManager.GetActiveScene().name);
        //Unpause
        UnPause();
    }

    public void Save()
    {
        //Save scores, healths, power points

        //Clear objects in scene

        //Update GUI Text

        throw new System.NotImplementedException();
    }

    public void Load()
    {
        throw new System.NotImplementedException();
    }

    public void GameOver()
    {
        _gameOver = true;
        UpdateGameOverText();
        UpdateRestartText();
    }

	public void UpdateScoreText() { scoreText.text = TextHelper.Score + _score; }
    public void UpdateHealthText() { healthText.text = TextHelper.Health + player.Health; }
    public void UpdatePowerText() { powerText.text = TextHelper.Power + player.Power; }

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


	public void AddScore(int score)
	{
		_score += score;
		UpdateScoreText();
	}

	public void ReduceHealth()
	{
        player.ReduceHealth();
		UpdateHealthText();
	}

	public void ReducePower()
	{
        player.ReducePower();
		UpdatePowerText();
	}

	public void IncreaseHealth()
	{
        player.AddHealth();
		UpdateHealthText();
	}

	public void IncreasePower()
	{
        player.AddPower();
		UpdatePowerText();
	}

    public bool IsGameOver() { return _gameOver || player.Health <= 0; }

}
