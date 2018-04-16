﻿using System.Collections;
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
        Enemy enemyShip = other.GetComponent<EnemyShip>();
        if (enemyShip != null)
        {
            _gameController._enemyManager.DestroyEnemy(enemyShip.id);
        }
        Enemy hazard = other.GetComponent<Hazard>();
        if (hazard != null)
        {
            _gameController._enemyManager.DestroyEnemy(hazard.id);
        }
            
        Bolt bolt = other.GetComponent<Bolt>();
        if (bolt != null)
        {
            Destroy(bolt);
        }
	}

}