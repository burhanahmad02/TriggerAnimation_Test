using UnityEngine;
using DG.Tweening;

public class CameraViewportManager : MonoBehaviour
{
    [System.Serializable]
    public class CameraGroup
    {
        public Camera cam;
        [HideInInspector] public Tween currentTween;
    }

    public CameraGroup red;
    public CameraGroup green;
    public CameraGroup blue;
    public CameraGroup yellow;

    [Header("Container Box (Normalized Screen Space)")]
    public float containerX = 0.1f;
    public float containerY = 0.1f;
    public float containerWidth = 0.8f;
    public float containerHeight = 0.8f;

    [Header("Focused Camera Size (Relative to Container)")]
    public float focusWidth = 0.6f;
    public float focusHeight = 0.6f;

    [Header("Animation")]
    public float animationDuration = 1f;

    private CameraGroup focusedCamera;

    void Start()
    {
        FocusOn(red);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) FocusOn(red);
        if (Input.GetKeyDown(KeyCode.G)) FocusOn(green);
        if (Input.GetKeyDown(KeyCode.B)) FocusOn(blue);
        if (Input.GetKeyDown(KeyCode.Y)) FocusOn(yellow);
    }

    public void FocusOn(CameraGroup target)
    {
        focusedCamera = target;
        AnimateRects();
    }

    void AnimateRects()
    {
        float remainingWidth = containerWidth - focusWidth * containerWidth;
        float remainingHeight = containerHeight - focusHeight * containerHeight;

        AnimateCamera(red, GetRectFor("Red", remainingWidth, remainingHeight));
        AnimateCamera(green, GetRectFor("Green", remainingWidth, remainingHeight));
        AnimateCamera(blue, GetRectFor("Blue", remainingWidth, remainingHeight));
        AnimateCamera(yellow, GetRectFor("Yellow", remainingWidth, remainingHeight));
    }

    Rect GetRectFor(string camName, float remainingWidth, float remainingHeight)
    {
        if (focusedCamera == red && camName == "Red")
            return new Rect(containerX, containerY, focusWidth * containerWidth, focusHeight * containerHeight);
        if (focusedCamera == red && camName == "Green")
            return new Rect(containerX + focusWidth * containerWidth, containerY, remainingWidth, focusHeight * containerHeight);
        if (focusedCamera == red && camName == "Blue")
            return new Rect(containerX, containerY + focusHeight * containerHeight, focusWidth * containerWidth, remainingHeight);
        if (focusedCamera == red && camName == "Yellow")
            return new Rect(containerX + focusWidth * containerWidth, containerY + focusHeight * containerHeight, remainingWidth, remainingHeight);

        if (focusedCamera == green && camName == "Green")
            return new Rect(containerX + remainingWidth, containerY, focusWidth * containerWidth, focusHeight * containerHeight);
        if (focusedCamera == green && camName == "Red")
            return new Rect(containerX, containerY, remainingWidth, focusHeight * containerHeight);
        if (focusedCamera == green && camName == "Yellow")
            return new Rect(containerX + remainingWidth, containerY + focusHeight * containerHeight, focusWidth * containerWidth, remainingHeight);
        if (focusedCamera == green && camName == "Blue")
            return new Rect(containerX, containerY + focusHeight * containerHeight, remainingWidth, remainingHeight);

        if (focusedCamera == blue && camName == "Blue")
            return new Rect(containerX, containerY + remainingHeight, focusWidth * containerWidth, focusHeight * containerHeight);
        if (focusedCamera == blue && camName == "Red")
            return new Rect(containerX, containerY, focusWidth * containerWidth, remainingHeight);
        if (focusedCamera == blue && camName == "Yellow")
            return new Rect(containerX + focusWidth * containerWidth, containerY + remainingHeight, remainingWidth, focusHeight * containerHeight);
        if (focusedCamera == blue && camName == "Green")
            return new Rect(containerX + focusWidth * containerWidth, containerY, remainingWidth, remainingHeight);

        if (focusedCamera == yellow && camName == "Yellow")
            return new Rect(containerX + remainingWidth, containerY + remainingHeight, focusWidth * containerWidth, focusHeight * containerHeight);
        if (focusedCamera == yellow && camName == "Green")
            return new Rect(containerX + remainingWidth, containerY, focusWidth * containerWidth, remainingHeight);
        if (focusedCamera == yellow && camName == "Blue")
            return new Rect(containerX, containerY + remainingHeight, remainingWidth, focusHeight * containerHeight);
        if (focusedCamera == yellow && camName == "Red")
            return new Rect(containerX, containerY, remainingWidth, remainingHeight);

        return new Rect(0, 0, 0, 0); // Shouldn't hit this
    }

    void AnimateCamera(CameraGroup group, Rect targetRect)
    {
        if (group.currentTween != null && group.currentTween.IsActive())
            group.currentTween.Kill();

        // Animate all 4 rect properties separately
        float startX = group.cam.rect.x;
        float startY = group.cam.rect.y;
        float startW = group.cam.rect.width;
        float startH = group.cam.rect.height;

        group.currentTween = DOTween.To(() => new Vector4(startX, startY, startW, startH),
            val => group.cam.rect = new Rect(val.x, val.y, val.z, val.w),
            new Vector4(targetRect.x, targetRect.y, targetRect.width, targetRect.height),
            animationDuration)
            .SetEase(Ease.OutSine);
    }
}
