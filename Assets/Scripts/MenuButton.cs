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
    public Color32 clickColor;

    void Awake()
    {
        textReference = transform.gameObject;
        buttonsAudio = GameObject.Find("ButtonsSFX").GetComponent<AudioSource>();
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
        textReference.GetComponent<Text>().color = clickColor;
        buttonsAudio.clip = buttonClick;
        buttonsAudio.Play();
    }
}
