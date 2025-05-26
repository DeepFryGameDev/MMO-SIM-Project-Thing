using System.Collections;
using UnityEngine;

// Purpose: This mainly just houses some UI tasks and allows the [UI] object to persist through scenes via singleton.
// Directions: Assign to the [UI] object
// Other notes: 

public class UIManager : MonoBehaviour
{
    public static UIManager i;

    [SerializeField] CanvasGroup fadeCanvasGroup;

    string heroesTransformName = "[Heroes]";
    public string GetHeroesTransformName() { return heroesTransformName; }

    void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (i != null)
            Destroy(gameObject);
        else
            i = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Coroutine to process fade in/out. Used by adjusting the alpha of the fadeCanvasGroup.
    /// </summary>
    /// <param name="fadeIn"></param>
    /// <returns>Waits for end of frame using DateSettings.fadeSettings as the timer</returns>
    public IEnumerator FadeToBlack(bool fadeIn)
    {
        float timer;

        switch (fadeIn)
        {
            case true:
                timer = 0; // Should go from 0 to 1

                while (timer < DateSettings.fadeSeconds)
                {
                    timer += Time.deltaTime;
                    fadeCanvasGroup.alpha = timer / DateSettings.fadeSeconds; // setting alpha from 0 to 1

                    yield return new WaitForEndOfFrame();
                }
                break;
            case false:
                timer = DateSettings.fadeSeconds; // Should go from 1 to 0

                while (timer > 0)
                {
                    timer -= Time.deltaTime;
                    fadeCanvasGroup.alpha = timer / DateSettings.fadeSeconds; // setting alpha from 1 to 0

                    yield return new WaitForEndOfFrame();
                }
                break;
        }
    }
}
