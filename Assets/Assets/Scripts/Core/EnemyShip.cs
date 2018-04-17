using UnityEngine;

public class EnemyShip : Enemy
{
	public float speed;
	public Transform shotSpawn;
	public EnemyBolt enemyBolt;

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


	protected override void Move()
	{
		GetComponent<Rigidbody>().velocity = speed * transform.forward;
	}

	public void AutoShot() 
	{
		EnemyBolt bolt = (EnemyBolt)Instantiate(enemyBolt, shotSpawn.position, shotSpawn.rotation);
        _gameController.EnemyBoltManager.AddBolt(bolt);
	}

}
