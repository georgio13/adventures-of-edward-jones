using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private AudioSource musicSource;
    private AudioSource soundEffectsSource;
    private AudioSource speechSource;

    public bool hasSceneTransition;
    private GameObject sceneTransition;
    public AudioClip sceneTransitionClip;

    public bool hasChapterTransition;
    private GameObject chapterTransition;
    public AudioClip backgroundClip;

    public bool stageInitialized = false;

    public static GameObject loadingImage;

    private void Awake()
    {
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");

        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        soundEffectsSource.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");

        speechSource = GameObject.Find("Speech").GetComponent<AudioSource>();
        speechSource.volume = PlayerPrefs.GetFloat("SpeechVolume");

        loadingImage = GameObject.Find("LoadingImage");
        loadingImage.SetActive(false);

        if (hasSceneTransition)
            sceneTransition = GameObject.Find("SceneTransition");
        
        if (hasChapterTransition)
            chapterTransition = GameObject.Find("ChapterTransition");

        if (hasSceneTransition)
            StartCoroutine(PlaySceneTransition());
        else if (hasChapterTransition)
            StartCoroutine(PlayChapterTransition());
        else
            stageInitialized = true;
    }

    IEnumerator PlaySceneTransition()
    {
        speechSource.clip = sceneTransitionClip;
        speechSource.Play();
        sceneTransition.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(sceneTransition.GetComponent<Animation>().clip.length);

        sceneTransition.SetActive(false);

        if (hasChapterTransition)
            StartCoroutine(PlayChapterTransition());
        else
            stageInitialized = true;
    }

    IEnumerator PlayChapterTransition()
    {
        chapterTransition.GetComponent<Animation>().Play();
        musicSource.clip = backgroundClip;
        musicSource.PlayDelayed(chapterTransition.GetComponent<Animation>().clip.length / 2);

        yield return new WaitForSeconds(chapterTransition.GetComponent<Animation>().clip.length);

        chapterTransition.SetActive(false);

        stageInitialized = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && sceneTransition.GetComponent<Animation>().isPlaying)
        {
            sceneTransition.SetActive(false);
            speechSource.Stop();

            if (hasChapterTransition)
                StartCoroutine(PlayChapterTransition());
        }
    }
}