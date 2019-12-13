using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshPro _treeLeftText;
    [SerializeField] private TextMeshProUGUI _coinCountText;

    [Header("Menu")]
    [SerializeField] private GameObject menuScreen;

    [Header("Game")]
    [SerializeField] private GameObject gameScreen;
    // Start is called before the first frame update

    public void UpdateCoinCount(int val)
    {
        _coinCountText.text = "COINS: " + val;
    }

    public void UpdateTreeCount(int treeLeft)
    {
        _treeLeftText.text = treeLeft.ToString();
    }

    void Start()
    {
        ToMenu();
    }

    public void ToGame()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void ToMenu()
    {
        menuScreen.SetActive(true);
        gameScreen.SetActive(false);
    }
}
