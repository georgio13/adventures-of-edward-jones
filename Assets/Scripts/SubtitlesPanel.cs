using UnityEngine;
using UnityEngine.UI;

public class SubtitlesPanel : MonoBehaviour
{
    public static SubtitlesPanel instance;
    private GameObject textReference;
    private AudioSource subtitleSound;

    void Awake()
    {
        instance = this;
        textReference = transform.GetChild(0).gameObject;
        instance.gameObject.SetActive(false);
    }

    public void TurnOn(GameplayObject selectedItem, string subtitleText)
    {
        instance.gameObject.SetActive(true);

        textReference.GetComponent<Text>().text = subtitleText;
        subtitleSound = selectedItem.GetComponent<AudioSource>();
        subtitleSound.Play();
        Invoke("TurnOff", subtitleSound.clip.length);
    }

    public void TurnOn(GameplayObject selectedItem, string[] subtitlesText, AudioClip[] subtitlesSound)
    {
        instance.gameObject.SetActive(true);

        subtitleSound = selectedItem.GetComponent<AudioSource>();

        for (int i = 0; i < subtitlesText.Length; i++)
        {
            textReference.GetComponent<Text>().text = subtitlesText[i];
            subtitleSound.GetComponent<AudioSource>().clip = subtitlesSound[i];
            subtitleSound.Play();
            Invoke("ClearSubtitles", subtitleSound.clip.length);
        }
    }

    public void TurnOff()
    {
        instance.gameObject.SetActive(false);

        for (int i = 0; i < ActionButtons.gameplayObjects.Length; i++)
            ActionButtons.gameplayObjects[i].GetComponent<Image>().raycastTarget = true;
    }

    private void ClearSubtitles()
    {
        textReference.GetComponent<Text>().text = "";
    }
}