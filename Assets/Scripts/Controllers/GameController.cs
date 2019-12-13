using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    MENU,
    GAME
}
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private Transform _treeSpawningPosition;

    [SerializeField] private GameStates _gameState;

    [SerializeField] private List<GameObject> treePool;

    [Header("Layouts")]
    [SerializeField] private Layout[] _layouts;

    private TreeController _currentTree;

    private int _treeLeftToSpawn = 0;
    private int _treeLeftToPassTheLevel = 0;

    void Awake()
    {
        Init();
        ResetGame();
    }

    void Init()
    {
        treePool = new List<GameObject>();
        for (int i = 0; i < 15; i++)
        {
            GameObject obj = Instantiate(treePrefab);
            obj.SetActive(false);
            treePool.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _gameState == GameStates.GAME)
        {
            if (_currentTree != null)
            {
                if (!_currentTree.isMoving)
                    _currentTree.ActivateTree();
                else
                {
                    _currentTree.canSpawnTree = false;
                    ActivateNextTree();
                    _currentTree.ActivateTree();
                }
            }

        }
    }

    private GameObject GetNextFreeTree()
    {
        for (int i = 0; i < treePool.Count; i++)
        {
            if (!treePool[i].activeSelf)
            {
                return treePool[i];
            }
        }

        GameObject obj = Instantiate(treePrefab);
        obj.SetActive(false);
        treePool.Add(obj);
        return obj;
    }

    public void ActivateNextTree()
    {
        if (_treeLeftToSpawn <= 0 && _gameState == GameStates.GAME)
            return;

        _treeLeftToSpawn--;

        TreeController tree = GetNextFreeTree().GetComponent<TreeController>();

        tree.transform.parent = _treeSpawningPosition.parent;
        tree.transform.position = _treeSpawningPosition.position;
        tree.transform.rotation = _treeSpawningPosition.rotation;

        tree.SpawnTree();

        _currentTree = tree;
    }

    public void StartGame()
    {
        ResetGame();
        _treeLeftToPassTheLevel = 10;
        _treeLeftToSpawn = _treeLeftToPassTheLevel;

        _gameState = GameStates.GAME;
        GameManager.Instance.uIController.ToGame();

        GameManager.Instance.uIController.UpdateTreeCount(_treeLeftToPassTheLevel);

        GameManager.Instance.groundController.ResetRotation(false);

        ActivateNextTree();

        _layouts[Utils.Constants.CURRENT_LAYOUT].InitLayout();
    }

    private void ResetGame()
    {
        for (int i = 0; i < treePool.Count; i++)
        {
            treePool[i].SetActive(false);
        }

        for (int i = 0; i < _layouts.Length; i++)
        {
            _layouts[i].gameObject.SetActive(false);
        }
    }

    public void TreePlanted()
    {
        _treeLeftToPassTheLevel--;

        GameManager.Instance.uIController.UpdateTreeCount(_treeLeftToPassTheLevel);

        if (_treeLeftToPassTheLevel <= 0)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        Utils.Constants.CURRENT_LEVEL++;
        _gameState = GameStates.MENU;

        Utils.Constants.CURRENT_LAYOUT = Random.Range(0, _layouts.Length);

        GameManager.Instance.SaveProgress();

        GameManager.Instance.groundController.ResetRotation(true);

        GameManager.Instance.uIController.ToMenu();
        ResetGame();
    }

    public void GameOver()
    {
        _gameState = GameStates.MENU;

        GameManager.Instance.uIController.ToMenu();
        ResetGame();

    }
}
