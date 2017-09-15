/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   15/9/2017
 *
 *  File:           MainMenuManager.cs
 *
 *  This class handles all the functionality of the scene.
 *
 *----------------------------------------------------------------*/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private AudioSource musicSource;                    // This is the reference to the music audio source.
    public static AudioSource soundEffectsSource;       // This is the reference to the sound effects audio source.
    public static AudioSource speechSource;             // This is the reference to the speech audio source.
    public bool hasSceneTransition;                     // This is true if the scene has scene transition.
    private GameObject sceneTransition;                 // This is the reference to the scene transition.
    public AudioClip sceneTransitionClip;               // This is the clip that will be played with the scene transition.
    public bool hasChapterTransition;                   // This is true if the scene has chapter transition.
    private GameObject chapterTransition;               // This is the reference to the chapter transition.
    private GameObject fadeOutTransition;               // This is the reference to the fade out transition.
    public AudioClip backgroundClip;                    // This is the background music of the scene.
    public static GameObject loadingImage;              // This is the reference to the loading image.
    public static GameObject fadeInTransition;          // This is the reference to the fade in transition.
    public bool stageInitialized = false;               // This variable help us to know when the player can move to the scene.
    private Item[] gameplayObjects;                     // This array contains all the Items of the scene.

    /// <summary>
    /// On the initialization we take the references of all the audio sources, to which 
    /// we initialize their volumes from the saved variables. Also, we check if the scene
    /// has scene transition or chapter transition. Furthermore, we hide the loading image
    /// and we check which Gameplay objects are saved to our Game Data, so to hide them
    /// from the scene and we also check if the scene is saved to our GameData so to 
    /// skip its transitions.
    /// </summary>
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

        fadeOutTransition = GameObject.Find("FadeOut");
        fadeOutTransition.SetActive(false);

        fadeInTransition = GameObject.Find("FadeIn");
        fadeInTransition.SetActive(false);

        // We get all the Gameplay objects of the scene and check which are saved. 
        // After that, we hide all the Gameplay objects which are saved.
        gameplayObjects = FindObjectsOfType<Item>();

        for (int i = 0; i < gameplayObjects.Length; i++)
        {
            if (DataHandler.instance.gameData.inventory.Contains(gameplayObjects[i].itemName))
                Destroy(gameplayObjects[i].gameObject);
        }

        if (hasSceneTransition)
            sceneTransition = GameObject.Find("SceneTransition");

        if (hasChapterTransition)
            chapterTransition = GameObject.Find("ChapterTransition");

        // We check if the scene is saved.
        if (DataHandler.instance.gameData.sceneCondition.Contains(SceneManager.GetActiveScene().name))
        {
            // If the scene is saved we hide the transitions and start to play the background music.
            if (hasSceneTransition)
                sceneTransition.SetActive(false);

            if (hasChapterTransition)
                chapterTransition.SetActive(false);

            fadeOutTransition.SetActive(true);
            StartCoroutine(PlayFadeOutTransition());
        }
        else
        {
            // If the scene is not saved we play the transitions. If the scene has not transitions
            // then we upadte our saved data which concern the scene.
            if (hasSceneTransition)
                StartCoroutine(PlaySceneTransition());
            else if (hasChapterTransition)
            {
                musicSource.clip = backgroundClip;
                musicSource.PlayDelayed(chapterTransition.GetComponent<Animation>().clip.length / 2);
                StartCoroutine(PlayChapterTransition());
            }
            else
            {
                stageInitialized = true;

                if (!DataHandler.instance.gameData.sceneCondition.Contains(SceneManager.GetActiveScene().name))
                    DataHandler.instance.gameData.sceneCondition.Add(SceneManager.GetActiveScene().name);

                DataHandler.instance.SaveData();

                musicSource.clip = backgroundClip;
                musicSource.Play();
            }
        }
    }

    /// <summary>
    /// In this coroutine we play the clip and the animation of the scene transition and after 
    /// we disable it. We check if there is and a chapter transition. If there is we call 
    /// the propriate function to play it. Also, we update our saved data which concern the scenes.
    /// </summary>
    /// <returns>There is nothing to return.</returns>
    IEnumerator PlaySceneTransition()
    {
        speechSource.clip = sceneTransitionClip;
        speechSource.Play();
        sceneTransition.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(sceneTransition.GetComponent<Animation>().clip.length);

        sceneTransition.SetActive(false);

        if (hasChapterTransition)
        {
            musicSource.clip = backgroundClip;
            musicSource.PlayDelayed(chapterTransition.GetComponent<Animation>().clip.length / 2);
            StartCoroutine(PlayChapterTransition());
        }
        else
        {
            stageInitialized = true;

            if (!DataHandler.instance.gameData.sceneCondition.Contains(SceneManager.GetActiveScene().name))
                DataHandler.instance.gameData.sceneCondition.Add(SceneManager.GetActiveScene().name);

            DataHandler.instance.SaveData();
        }
    }

    /// <summary>
    /// In this coroutine we play the animation of the chapter transition and in the middle
    /// of it we start to play the background music of the scene. Also, we update our saved 
    /// data which concern the scenes.
    /// </summary>
    /// <returns>There is nothing to return.</returns>
    IEnumerator PlayChapterTransition()
    {
        chapterTransition.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(chapterTransition.GetComponent<Animation>().clip.length);

        chapterTransition.SetActive(false);

        stageInitialized = true;

        if (!DataHandler.instance.gameData.sceneCondition.Contains(SceneManager.GetActiveScene().name))
            DataHandler.instance.gameData.sceneCondition.Add(SceneManager.GetActiveScene().name);

        DataHandler.instance.SaveData();
    }

    /// <summary>
    /// In this coroutine we play the fade out animation which will be played when
    /// we have meet again the scene or the scene has not scene transition or chapter
    /// transition.
    /// </summary>
    /// <returns>There is nothing to return.</returns>
    IEnumerator PlayFadeOutTransition()
    {
        fadeOutTransition.GetComponent<Animation>().Play();
        musicSource.clip = backgroundClip;
        musicSource.PlayDelayed(fadeOutTransition.GetComponent<Animation>().clip.length / 2);

        yield return new WaitForSeconds(fadeOutTransition.GetComponent<Animation>().clip.length);

        fadeOutTransition.SetActive(false);

        stageInitialized = true;
    }

    /// <summary>
    /// On the Update we check if the user press the space button to skip the cutscene. If he do it
    /// we stop the cutscene and go to the chapter transition.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && sceneTransition.GetComponent<Animation>().isPlaying)
        {
            sceneTransition.SetActive(false);
            speechSource.Stop();

            if (hasChapterTransition)
            {
                musicSource.clip = backgroundClip;
                musicSource.PlayDelayed(chapterTransition.GetComponent<Animation>().clip.length / 2);
                StartCoroutine(PlayChapterTransition());
            }
        }
    }
}