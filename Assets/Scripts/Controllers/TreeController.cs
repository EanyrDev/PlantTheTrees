using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _treeSprite;

    public bool isMoving = false;
    public bool canSpawnTree = false;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.down * Time.deltaTime * Utils.Constants.TREE_SPEED);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            isMoving = false;
            transform.parent = col.transform;
            _collider.isTrigger = false;
            GameManager.Instance.gameController.TreePlanted();

            if (canSpawnTree)
            {
                GameManager.Instance.gameController.ActivateNextTree();
            }
        }
        else if ((col.tag == "Tree" || col.tag == "Obstacle") && isMoving)
        {
            isMoving = false;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.angularVelocity = Utils.Constants.ROTATION_SPEED;
            GameManager.Instance.gameController.GameOver();
        }
        else if (col.tag == "Coin")
        {
            GameManager.Instance.IncreaseCoinCount(1);
            col.gameObject.SetActive(false);
        }
    }

    public void SpawnTree()
    {
        gameObject.SetActive(true);
        isMoving = false;
        _collider.isTrigger = true;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        canSpawnTree = true;
    }

    public void ActivateTree()
    {
        isMoving = true;
    }
}
