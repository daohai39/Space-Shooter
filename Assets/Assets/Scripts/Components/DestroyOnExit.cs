using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : MonoBehaviour {
	private GameController _gameController;

	private void Start()
	{
		var gameControllerObject = GameObject.FindWithTag(Tag.GameController);
		if (gameControllerObject != null)
			_gameController = gameControllerObject.GetComponent<GameController>();
		if (_gameController == null)
			Debug.Log("Can not find 'GameController' script");
	}

	private void OnTriggerExit(Collider other)
	{
        EnemyShip enemyShip = other.GetComponent<EnemyShip>();
		if (enemyShip != null)
		{
			_gameController.EnemyManager.DestroyEnemy(enemyShip.Id);
		}

        Hazard hazard = other.GetComponent<Hazard>();
		if (hazard != null)
		{
			_gameController.EnemyManager.DestroyEnemy(hazard.Id);
		}
			
        EnemyBolt enemyBolt = other.GetComponent<EnemyBolt>();
        if (enemyBolt != null)
		{
            _gameController.EnemyBoltManager.RemoveBolt(enemyBolt.Id);
		}

        PlayerBolt playerBolt = other.GetComponent<PlayerBolt>();
        if (playerBolt != null)
	    {
            _gameController.PlayerBoltManager.RemoveBolt(playerBolt.Id);
	    }
	}

}
