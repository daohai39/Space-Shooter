using UnityEngine;



public class EnemyBolt : Bolt
{
    private GameController _gameController;

    public GameObject playerExplosion;

    private void Start()
    {
        var gameControllerObject = GameObject.FindWithTag(Tag.GameController);
        if (gameControllerObject != null)
            _gameController = gameControllerObject.GetComponent<GameController>();
        if (_gameController == null)
            Debug.Log("Can not find 'GameController' script");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Boundary) || other.CompareTag(Tag.Enemy) || other.CompareTag(Tag.PlayerBolt))
            return;
        
        DestroySelf();

        Destroy(other);
    }

    private void Destroy(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player!=null)
        {
            _gameController.ReducePower();
            _gameController.ReduceHealth();
            if (_gameController.IsGameOver())
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                _gameController.player.DestroySelf();
                _gameController.GameOver();
            }
        }
    }
}
