using UnityEngine;

public class EnemySelectArrowBehavior : MonoBehaviour
{
    float bobAmplitude = 0.25f;   // how far up/down (in local units)
    float bobFrequency = 2f;      // cycles per second
    float spinSpeed = 180f;        // degrees per second

    Vector3 startLocalPos;

    MeshRenderer meshRender;

    bool isVisible; // serialized for testing

    public void ToggleVisibility(bool visible) {  isVisible = visible; meshRender.enabled = visible; }

    void Awake()
    {
        startLocalPos = transform.localPosition;
        meshRender = GetComponent<MeshRenderer>();

        meshRender.enabled = false;
    }

    void Update()
    {
        if (isVisible)
        {
            // Bob up/down using a sine wave (local space so it's relative to parent)
            float bobOffset = Mathf.Sin(Time.time * bobFrequency * Mathf.PI * 2f) * bobAmplitude;
            transform.localPosition = new Vector3(startLocalPos.x, startLocalPos.y + bobOffset, startLocalPos.z);

            // Spin around local Y axis // <-- this is spinning the wrong way
            transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime, Space.Self);
        }        
    }
}
