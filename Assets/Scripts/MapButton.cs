using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animation mapAnimation;
    public string sceneName;

    void Awake()
    {
        mapAnimation = transform.GetComponent<Animation>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mapAnimation.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mapAnimation.Stop();
        transform.localScale = new Vector3(1, 1, 1);

    }

    public void OnPointerClick()
    {
        if (SceneManager.GetActiveScene().name != sceneName)
            StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
        StageManager.loadingImage.SetActive(true);
        StageManager.loadingImage.GetComponent<Animation>().Play();

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
