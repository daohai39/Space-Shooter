using UnityEngine;

public class EnemyShip : Enemy, IAutoMoveable
{
	public float speed;
	public Transform shotSpawn;
	public GameObject enemyBolt;

	private void Start()
	{
		var gameControllerObject = GameObject.FindWithTag(Tag.GameController);
		if (gameControllerObject != null)
			_gameController = gameControllerObject.GetComponent<GameController>();
		if (_gameController == null)
			Debug.Log("Can not find 'GameController' script");
		Move();
		InvokeRepeating("AutoShot", 0.5f, 1.0f);
	}


	public override void Move()
	{
		GetComponent<Rigidbody>().velocity = speed * transform.forward;
	}

	public void AutoShot() 
	{
		EnemyBolt emBolt = (EnemyBolt)Instantiate(enemyBolt, shotSpawn.position, shotSpawn.rotation);
	    _gameController.BoltManager.Add(emBolt);
	}

}
