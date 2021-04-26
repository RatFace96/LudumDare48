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

    public Player Player;
    public Image Cheesebar;

    public Button StartBtn;
    public Button RestartBtn;

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
        IsStart = true;
    }

    void ShowEnd()
    {
        EndMenu.SetActive(true);
        IsStart = false;
    }

    void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void Update()
    {
        if (Player) 
            Cheesebar.fillAmount = Player.EatPoints / Player.MaxEatPoints;
    }

    private void OnDestroy()
    {
        if(Player)
            Player.OnDead -= ShowEnd;
        RestartBtn.onClick.RemoveAllListeners();
        StartBtn.onClick.RemoveAllListeners();
    }

}
