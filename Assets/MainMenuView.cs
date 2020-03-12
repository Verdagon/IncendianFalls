using AthPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

  }

  public void OnBackstoryClicked() {
    RootPresenter.sceneInitParamStartLevel = -4;
    SceneManager.LoadScene("AtScene");
  }

  public void OnIntroductionClicked() {
    RootPresenter.sceneInitParamStartLevel = -3;
    SceneManager.LoadScene("AtScene");
  }

  public void OnRetreatChallengeClicked() {
    RootPresenter.sceneInitParamStartLevel = -2;
    SceneManager.LoadScene("AtScene");
  }

  public void OnAmbushChallengeClicked() {
    RootPresenter.sceneInitParamStartLevel = -1;
    SceneManager.LoadScene("AtScene");
  }

  public void OnStartGameClicked() {
    RootPresenter.sceneInitParamStartLevel = 0;
    SceneManager.LoadScene("AtScene");
  }

  public void OnExitClicked() {
    Application.Quit();
  }

  public void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha5)) {
      RootPresenter.sceneInitParamStartLevel = -5;
      SceneManager.LoadScene("AtScene");
    }
  }
}
