using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopupHandler : MonoBehaviour
{
    [SerializeField] float riseDistance = 40f;          // pixels to move up
    [SerializeField] float duration = 1.0f;             // total lifetime
    [SerializeField] AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

    RectTransform rectTransform;
    RectTransform GetRectTransform()
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        return rectTransform;
    }

    TextMeshProUGUI textMesh;

    public static DamagePopupHandler Create(Vector3 position, int damageAmount)
    {
        Canvas canvas = GameObject.Find("[UI]/BattleCanvas").GetComponent<Canvas>();

        GameObject damagePopupObj = Instantiate(PrefabManager.i.BattleDamagePopup, canvas.transform, false);

        DamagePopupHandler damagePopupHandler = damagePopupObj.GetComponent<DamagePopupHandler>();

        // Adjust position to be above the object
        Vector3 targetPosition = position + new Vector3(0, 2.5f, 0); // testing this

        // position: world -> screen -> canvas local point
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 screenPos = Camera.main.WorldToScreenPoint(targetPosition);
        Vector2 localPoint;

        // if the canvas is ScreenSpaceOverlay, camera param may be null
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main, out localPoint);
        damagePopupHandler.GetRectTransform().anchoredPosition = localPoint;

        damagePopupHandler.Setup(damageAmount);

        return damagePopupHandler;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();     
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());

        // start animate and cleanup
        StartCoroutine(AnimateAndDestroy());
    }

    IEnumerator AnimateAndDestroy()
    {
        float timer = 0f;
        Vector2 startPos = GetRectTransform().anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0f, riseDistance);

        // capture initial color
        Color startColor = textMesh.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            float eased = ease.Evaluate(t);

            // position
            GetRectTransform().anchoredPosition = Vector2.Lerp(startPos, endPos, eased);

            // fade out in last half
            float alpha = Mathf.Lerp(1f, 0f, eased);
            textMesh.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            // optional: scale pop effect
            //float scale = 1f + 0.25f * (1f - Mathf.Abs(0.5f - eased) * 2f); // small pop
            //GetRectTransform().localScale = Vector3.one * scale;

            yield return null;
        }

        // ensure invisible and destroy
        Destroy(gameObject);
        // TODO: replace Destroy with pooling.Recycle(this) for production
    }
}
