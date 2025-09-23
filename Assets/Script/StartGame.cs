using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class StartGame : MonoBehaviour
{

    private Button button;

    private float reloadDelay = 2f;

    public string sceneToLoad = "Level1";

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClip);

    }

    private void TaskOnClip()
    {
        Debug.Log("You have clicked the button!");

        if (reloadDelay <= 0f) SceneManager.LoadScene(sceneToLoad);
        else StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(reloadDelay);
        SceneManager.LoadScene(sceneToLoad);
    }

}
