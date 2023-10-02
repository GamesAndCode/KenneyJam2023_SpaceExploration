using UnityEngine;

public class TooltipSystem : MonoBehaviour

{
    private static TooltipSystem current;

    public Tooltip tooltip;

    private void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "")
    {
        current.tooltip.SetText(content, header);
        current.tooltip.gameObject.SetActive(true);
        LeanTween.alphaCanvas(current.tooltip.GetComponent<CanvasGroup>(), 1.0f, 0.4f);

    }
    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
        current.tooltip.GetComponent<CanvasGroup>().alpha = 0;

    }

}
