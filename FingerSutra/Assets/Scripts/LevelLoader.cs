using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        StartCoroutine(Loader());
    }

    IEnumerator Loader()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            slider.value = progress;
            Debug.Log(progress);
            yield return null;
        }

       
    }

}
