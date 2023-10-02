using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static LTDescr delay;
    public string header;

    [Multiline()]
    public string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(0.4f, () =>
        {
            TooltipSystem.Show(content, header);
            Cursor.visible = false;
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.visible = true;
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }
}
