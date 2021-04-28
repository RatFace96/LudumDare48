using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public bool IsStart = false;

    public GameObject EndMenu;
    public GameObject StartMenu;

    public Text score;

    public Player Player;
    public Slider Cheesebar;

    public Button StartBtn;
    public Button RestartBtn;

    public AudioSource GameAudio;

    public float StartGameDelay;
    public float EndGameDelay;
    public float RestartGameDelay;

    private void Start()
    {
        StartMenu.SetActive(true);
        EndMenu.SetActive(false);
        RestartBtn.onClick.AddListener(RestartGame);
        StartBtn.onClick.AddListener(HideStartMenu);
        Player.OnDead += ShowEnd;
    }

    void HideStartMenu()
    {
        StartDelay(StartGameDelay, () =>
        {
            StartMenu.SetActive(false);
            Player.ateCheeses = 0;
            GameAudio.Play();
            IsStart = true;
        });
    }

    void ShowEnd()
    {
        StartDelay(EndGameDelay, () =>
        {
            GameAudio.Stop();
            EndMenu.SetActive(true);
            score.text += Player.ateCheeses.ToString();
            IsStart = false;
        });
    }

    Coroutine startDelay;

    void StartDelay(float delay, Action onFinish)
    {
        if (startDelay != null) StopCoroutine(startDelay);
        startDelay = StartCoroutine(Delay(delay, onFinish));
    }

    IEnumerator Delay(float delay, Action onFinish)
    {
        yield return null;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        onFinish?.Invoke();
        startDelay = null;
    }

    void RestartGame()
    {
        StartDelay(RestartGameDelay, () =>
        {
            SceneManager.LoadScene("Game");
        });
    }

    private void Update()
    {
        if (Player) 
            Cheesebar.value = Player.EatPoints / Player.MaxEatPoints;
    }

    private void OnDestroy()
    {
        if(Player)
            Player.OnDead -= ShowEnd;
        RestartBtn.onClick.RemoveAllListeners();
        StartBtn.onClick.RemoveAllListeners();
    }

}
