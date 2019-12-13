using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameController gameController;
    public UIController uIController;
    public GroundController groundController;


    private int _coinCount;

    void Awake()
    {
        _instance = this;

        LoadProgress();
    }

    public void IncreaseCoinCount(int val)
    {
        _coinCount += val;
        uIController.UpdateCoinCount(_coinCount);
    }

    void OnApplicationQuit()
    {
        SaveProgress();
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("CurentLevel", Utils.Constants.CURRENT_LEVEL);
        PlayerPrefs.SetInt("CurentLayout", Utils.Constants.CURRENT_LAYOUT);
        PlayerPrefs.SetInt("CurentRotationLayout", Utils.Constants.CURRENT_ROTATION_LAYOUT);
        PlayerPrefs.SetInt("CoinCount", _coinCount);

        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        Utils.Constants.CURRENT_LEVEL = PlayerPrefs.GetInt("CurentLevel", 1);
        Utils.Constants.CURRENT_LAYOUT = PlayerPrefs.GetInt("CurentLayout", 0);
        Utils.Constants.CURRENT_ROTATION_LAYOUT = PlayerPrefs.GetInt("CurentRotationLayout", 0);
        _coinCount = PlayerPrefs.GetInt("CoinCount", 0);

        uIController.UpdateCoinCount(_coinCount);
    }
}
