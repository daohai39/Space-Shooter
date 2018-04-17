using UnityEngine;

public class Hazard : Enemy
{
    public float tumble;
    public float speed;

    private void Start()
    {
        var gameControllerObject = GameObject.FindWithTag(Tag.GameController);
        if (gameControllerObject != null)
            _gameController = gameControllerObject.GetComponent<GameController>();
        if (_gameController == null)
            Debug.Log("Can not find 'GameController' script");
        Move();
    }

    protected override void Move()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
}
