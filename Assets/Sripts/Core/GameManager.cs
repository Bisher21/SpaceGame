using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public float worldSpeed;
    [SerializeField] private GameObject boss1;

    public int crittierCount;

    private void Start()
    {
        crittierCount = 0;
    }
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Pause();
            AudioManager.Instance.playSound(AudioManager.Instance.pauseAndUnpause);
        }

        if (crittierCount > 15)
        {
            crittierCount = 0;
            Instantiate(boss1,new Vector2(13f,0),Quaternion.Euler(0,0,-90));
            AudioManager.Instance.playSound(AudioManager.Instance.spawn);
        }
    }
    public void Pause()
    {
        if (UIController.Instance.pausePanel.activeSelf == false)
        {
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
            PlayerMovement.Instance.ExitBoost();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GameOver()
    {
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameOver");
    }
    public void SetWorldSpeed(float speed)
    {
        worldSpeed=speed;

    }
}
