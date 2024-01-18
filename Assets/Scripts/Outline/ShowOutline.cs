using cakeslice;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(cakeslice.Outline))]
public class ShowOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline _outline;

    private void OnEnable()
    {
        CacheOutlineComponent();
        Hide();
    }

    public void OnPointerEnter(PointerEventData eventData) => 
        Show();

    public void OnPointerExit(PointerEventData eventData) => 
        Hide();
    private void CacheOutlineComponent() =>
        _outline = GetComponent<Outline>();

    private void Show()
    {
        try
        {
            _outline.eraseRenderer = false;
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    private void Hide()
    {
        try
        {
            _outline.eraseRenderer = true;
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }
}
