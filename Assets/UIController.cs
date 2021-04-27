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
        StartMenu.SetActive(false);
        Player.ateCheeses = 0;
        GameAudio.Play();
        IsStart = true;
    }

    void ShowEnd()
    {
        GameAudio.Stop();
        EndMenu.SetActive(true);
        score.text += Player.ateCheeses.ToString();
        IsStart = false;
    }

    void RestartGame()
    {
        SceneManager.LoadScene("Game");
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
