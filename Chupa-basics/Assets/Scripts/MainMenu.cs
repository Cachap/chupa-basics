using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //<summary> The launch of the game </summary>
    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //<summary> Quit from game </summary>
    public void ExitGame() {
        Application.Quit();
    }

}
