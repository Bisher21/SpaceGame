using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LostWhale : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level 1 Complete");
        }
    }
}
