using UnityEngine;



public class EnemyBolt : Bolt, IDestroyOnContact
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
        Destroy(other);
    }

    public void Destroy(Collider other)
    {
        if (other.CompareTag(Tag.Boundary) 
            || other.CompareTag(Tag.Enemy) 
            || other.CompareTag(Tag.PlayerBolt))
            return;
        if (other.CompareTag(Tag.Player))
        {
            _gameController.ReducePower();
            _gameController.ReduceHealth();
            if (_gameController.IsGameOver())
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
                _gameController.GameOver();
            }
        }
        Destroy(gameObject);
    }
}
