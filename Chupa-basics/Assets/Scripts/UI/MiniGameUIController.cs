using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUIController : MonoBehaviour
{
    private readonly float _defaultTime = 30f;

    public Image timerBar;
    public TextMeshProUGUI timeBarText;
    public float time = 30f;

    public Camera firstCamera;
    public Camera secondCamera;

    public GameObject miniGameCanvas;

    public void Start()
    {
        secondCamera.enabled = false;
        time = _defaultTime;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            firstCamera.enabled = true;
            secondCamera.enabled = false;
            time = _defaultTime;
        }

        time -= Time.deltaTime;

        UpdateTimer(time, 100);
        UpdateTimeText(time);
    }

    public void UpdateTimeText(float time)
    {
        timeBarText.text = ((int)time).ToString();
    }

    public void UpdateTimer(float timeLeft, float totalTime)
    {
        timerBar.fillAmount = 100;
    }

    public void ShowUI(bool show)
    {
        SwitchCam();
    }

    public void SwitchCam()
    {
        firstCamera.enabled = !firstCamera.enabled;
        secondCamera.enabled = !secondCamera.enabled;
    }

    public void StartMiniGame(MiniGame miniGame)
    {
        miniGameCanvas.SetActive(true);
        miniGame.OnMiniGameComplete += OnMiniGameComplete;
        miniGame.StartMiniGame();
    }

    private void OnMiniGameComplete(bool success)
    {
        Debug.Log("Mini-game finished. Success: " + success);
        miniGameCanvas.SetActive(false);
    }
}
