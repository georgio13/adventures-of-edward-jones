using System.Collections;
using UnityEngine;

public class FirstSceneManager : MonoBehaviour
{
    private GameObject cutscene;
    private GameObject chapter;

    private void Awake()
    {
        cutscene = GameObject.Find("Cutscene");
        chapter = GameObject.Find("OpenScene");
        StartCoroutine(OpenScene());
    }

    IEnumerator OpenScene()
    {
        yield return new WaitForSeconds(cutscene.GetComponent<Animation>().clip.length);

        cutscene.SetActive(false);

        chapter.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(chapter.GetComponent<Animation>().clip.length);

        chapter.SetActive(false);
    }
}
