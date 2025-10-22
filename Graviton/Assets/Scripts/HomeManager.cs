using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}
