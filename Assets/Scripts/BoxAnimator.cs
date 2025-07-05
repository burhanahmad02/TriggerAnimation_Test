using UnityEngine;
using DG.Tweening;

public class BoxAnimator : MonoBehaviour
{
    public Vector2 expandedSize;
    public Vector2 collapsedSize;
    private RectTransform rectTransform;
    private Tween currentTween;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Expand()
    {
        AnimateTo(expandedSize);
    }

    public void Collapse()
    {
        AnimateTo(collapsedSize);
    }

    private void AnimateTo(Vector2 targetSize)
    {
        if (currentTween != null && currentTween.IsActive()) currentTween.Kill();

        currentTween = rectTransform
            .DOSizeDelta(targetSize, 1f)
            .SetEase(Ease.OutSine);
    }
}
