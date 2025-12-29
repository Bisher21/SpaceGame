using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float worldSpeed;
    public float adjustedworldSpeed;

    private ObjectPooler boss1Pool;

    public int crittierCount;

    private void Start()
    {
        boss1Pool = GameObject.Find("Boss1Pool").GetComponent<ObjectPooler>();
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
        adjustedworldSpeed= worldSpeed*Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Pause();
            AudioManager.Instance.playSound(AudioManager.Instance.pauseAndUnpause);
        }

        if (crittierCount >= 5)
        {
            crittierCount = 0;
            GameObject spawnBoss = boss1Pool.GetPoolGameObjects();
            spawnBoss.transform.position = new Vector2(13f, 0);
            spawnBoss.transform.rotation = Quaternion.Euler(0, 0, -90);
            spawnBoss.SetActive(true); 
            
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
