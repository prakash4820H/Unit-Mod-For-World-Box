using UnityEngine;
using UnityEngine.UI;

public class WhiteScreenTrait : MonoBehaviour
{
    public Actor actor;
    private GameObject whiteScreenOverlay;
    private bool isWhiteScreenActive = false;

    private void Start()
    {
        CreateWhiteScreenOverlay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) // Change this condition to trigger the white screen as needed
        {
            ToggleWhiteScreen();
        }
        if (Input.GetKeyDown(KeyCode.O)) // Change this condition to open the URL as needed
        {
            OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }
    }

    private void CreateWhiteScreenOverlay()
    {
        whiteScreenOverlay = new GameObject("WhiteScreenOverlay");
        Canvas canvas = whiteScreenOverlay.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler canvasScaler = whiteScreenOverlay.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
        GraphicRaycaster raycaster = whiteScreenOverlay.AddComponent<GraphicRaycaster>();

        GameObject imageObject = new GameObject("WhiteScreenImage");
        imageObject.transform.SetParent(whiteScreenOverlay.transform);
        Image image = imageObject.AddComponent<Image>();
        image.color = Color.white;
        RectTransform rectTransform = image.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        whiteScreenOverlay.SetActive(false);
    }
    private void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    private void ToggleWhiteScreen()
    {
        isWhiteScreenActive = !isWhiteScreenActive;
        whiteScreenOverlay.SetActive(isWhiteScreenActive);
    }
}
