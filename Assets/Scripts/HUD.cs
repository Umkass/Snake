using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Singleton<HUD>
{
    #region Field Declarations

    [Header("UI Components")]
    [Space]
    [SerializeField] Button rightButton;
    [SerializeField] Button leftButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button exitButton;
    [SerializeField] TextMeshProUGUI _statusText;
    public bool Right { get; private set; }
    public bool Left { get; private set; }
    public event Action OnRight = delegate { };
    public event Action OnLeft = delegate { };

    #endregion

    void Start()
    {   
        DontDestroyOnLoad(gameObject);
        rightButton.onClick.AddListener(HandleOnRight);
        leftButton.onClick.AddListener(HandleOnLeft);
        restartButton.onClick.AddListener(HandleOnRestart);
        exitButton.onClick.AddListener(HandleOnExit);
        _statusText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }
    void HandleOnRight()
    {
        OnRight?.Invoke();
    }
    void HandleOnLeft()
    {
        OnLeft?.Invoke();
    }
    void HandleOnRestart()
    {
        Time.timeScale = 1;
        GameManager.Instance.StartGame();
        restartButton.gameObject.SetActive(false);
        _statusText.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }
    void HandleOnExit()
    {
        Application.Quit();
    }
    public void ShowLevelCompleteButtons()
    {
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }
    #region Show status
    public void ShowStatus(string newStatus)
    {
        _statusText.gameObject.SetActive(true);
        StartCoroutine(ChangeStatus(newStatus));
    }

    IEnumerator ChangeStatus(string displayText)
    {
        _statusText.text = displayText;
        yield return new WaitForSeconds(1f);
        _statusText.gameObject.SetActive(false);
        yield break;
    }

    #endregion
}
