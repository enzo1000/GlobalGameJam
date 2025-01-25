using UnityEngine;

public class DynamicMusicManager : MonoBehaviour
{
    public AudioSource audioSource; // The main AudioSource component
    public AudioClip mainTheme; // Main theme music
    public AudioClip goodVariation; // Good variation music
    public AudioClip badVariation; // Bad variation music

    public float fadeDuration = 1.0f; // Duration for fade-out and fade-in

    private bool isPlayingVariation = false;

    void Start()
    {
        PlayMainTheme();
    }

    // Main theme in a loop
    public void PlayMainTheme()
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = mainTheme;
        audioSource.loop = true;
        audioSource.volume = 0.5f; // A mettre dans une variable qui pourrait être modifiée avec des paramètres !!!!!!
        audioSource.Play();
    }

    public void PlayGoodVariation()
    {
        if (isPlayingVariation) return;
        StartCoroutine(PlayVariation(goodVariation));
    }

    public void PlayBadVariation()
    {
        if (isPlayingVariation) return; 
        StartCoroutine(PlayVariation(badVariation));
    }

    private System.Collections.IEnumerator PlayVariation(AudioClip variation)
    {
        isPlayingVariation = true;

        // Save the current position of the main theme
        float mainThemeTime = audioSource.time;

        // Fade out the main theme
        yield return StartCoroutine(FadeOut(fadeDuration));

        // Play the variation
        audioSource.clip = variation;
        audioSource.loop = false;
        audioSource.volume = 0.5f;// A mettre dans une variable qui pourrait être modifiée avec des paramètres !!!!!!
        audioSource.Play();

        // Wait until the variation finishes
        yield return new WaitForSeconds(variation.length);

        // Fade out the variation
        yield return StartCoroutine(FadeOut(fadeDuration));

        audioSource.clip = mainTheme;
        audioSource.loop = true;
        audioSource.time = mainThemeTime;

        // Fade in the main theme
        audioSource.Play();
        yield return StartCoroutine(FadeIn(fadeDuration));

        isPlayingVariation = false;
    }

    // Fade in and out functions

    private System.Collections.IEnumerator FadeOut(float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration); 
            yield return null;
        }

        audioSource.volume = 0;
    }

    private System.Collections.IEnumerator FadeIn(float duration)
    {
        float startVolume = 0;
        audioSource.volume = startVolume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 1, t / duration);
            yield return null;
        }

        audioSource.volume = 1;
    }
}
