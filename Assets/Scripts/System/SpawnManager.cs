using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// Purpose: Used to manage the player being forcibly placed at a point in the world (like resetting position when going to next week)
// Directions: Attach to the [System] GameObject
// Other notes: 

public class SpawnManager : MonoBehaviour
{
    Vector3 playerSpawnPosition;
    public Vector3 GetPlayerSpawnPosition() { return playerSpawnPosition; }
    public void SetPlayerSpawnPosition(Vector3 playerSpawnPosition) { this.playerSpawnPosition = playerSpawnPosition; }

    GameObject player;

    public static SpawnManager i;

    bool gameSet;

    bool runningFade;

    public int sceneToUnload;

    void Awake()
    {
        if (i == null || !i.gameSet)
        {
            Debug.Log("Setting up spawn manager");
            Singleton();

            i.gameSet = true;                        

            i.player = GameObject.FindWithTag("Player");

            i.playerSpawnPosition = i.player.transform.position;            
        } else
        {
            if (!i.runningFade)
            {
                Debug.Log("Running this");

                PostSceneTransition();
            }            
        }
    }

    void Singleton()
    {
        if (i != null)
            Destroy(gameObject);
        else
            i = this;

        DontDestroyOnLoad(gameObject);
    }

    void PostSceneTransition()
    {
        MovePlayerToSpawnPosition();

        StartCoroutine(FadeAndEnableMovement());
    }

    IEnumerator FadeAndEnableMovement()
    {
        yield return new WaitForSeconds(1.5f); // need to figure out a way to wait until the scene is fully loaded before we can unload the next scene.  For now we will artifically wait 1.5 seconds.

        Debug.Log("Starting coroutine");
        i.runningFade = true;        

        // Unload the last scene
        SceneManager.UnloadSceneAsync(sceneToUnload);

        Debug.Log("Enabling movement");
        PlayerMovement.i.ToggleMovement(true);

        StartCoroutine(UIManager.i.FadeToBlack(false));        
    }

    /// <summary>
    /// Sets the player's position to the position when the game was launched
    /// </summary>
    public void MovePlayerToSpawnPosition()
    {
        i.player.transform.position = i.playerSpawnPosition;
    }
}
