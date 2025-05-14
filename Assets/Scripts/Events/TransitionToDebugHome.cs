using UnityEngine;

public class TransitionToDebugHome : BaseInteractOnTouch
{
    bool running;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!running)
            {
                running = true;
                base.OnTriggerEnter(other);

                MoveToDebugHome();
            }
        }               
    }

    void MoveToDebugHome()
    {
        StartCoroutine(scriptedEvent.TransitionToScene(0, new Vector3(-67.37f, .187f, 22.74f)));
    }
}
