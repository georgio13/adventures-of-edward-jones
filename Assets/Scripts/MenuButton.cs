using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject textReference;
    private AudioSource buttonsAudio;
    public AudioClip buttonHover;
    public AudioClip buttonClick;
    public Color32 startColor;
    public Color32 hoverColor;

    void Awake()
    {
        textReference = transform.gameObject;
        buttonsAudio = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textReference.GetComponent<Text>().color = hoverColor;
        buttonsAudio.clip = buttonHover;
        buttonsAudio.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textReference.GetComponent<Text>().color = startColor;
    }

    public void OnPointerClick()
    {
        buttonsAudio.clip = buttonClick;
        buttonsAudio.Play();
        textReference.GetComponent<Text>().color = startColor;
    }
}
