using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToDebugField : BaseInteractOnTouch
{
    bool running;

    protected override void OnTriggerEnter(Collider other)
    {
        if (!running)
        {
            running = true;
            base.OnTriggerEnter(other);

            MoveToDebugField();
        }             
    }

    void MoveToDebugField()
    {
        StartCoroutine(scriptedEvent.TransitionToScene(1, new Vector3(32, 99.61497f, 200)));
    }
}
