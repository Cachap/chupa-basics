using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextColorOnHoverChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //<summary> Target text </summary>
    [SerializeField] private Text targetText;

    //<summary> Hover color </summary>
    [SerializeField] private Color hoverColor;

    //<summary> Animation duration </summary>
    [SerializeField] private float fadeDuration;

    //<summary> Initial color of the text </summary>
    private Color defaultColor;

    //<summary> Sets true when the cursor has been hovered on a text </summary>
    private bool pointerIsEnter = false;

    //<summary> Initial color of the duration </summary>
    private float defaultDuration = 0.1f;

    //<summary> Current value of the duration </summary>
    private float currentDuration;
    private void Start()
    {
        currentDuration = fadeDuration = defaultDuration;
        defaultColor = targetText.color;
    }

    private void Update() {
       LookAtChange();
    }

    public void OnPointerEnter(PointerEventData eventData) => pointerIsEnter = true;

    public void OnPointerExit(PointerEventData eventData) {

        ResetColor();
        currentDuration = fadeDuration;
        
    }

    public void ResetColor() {
        targetText.color = defaultColor;
        pointerIsEnter = false;
    }

    public void SetColor(Color color) => targetText.color = new Color(color.a, color.b, color.g);

    public void LookAtChange() {

        if (pointerIsEnter) {
            currentDuration -= Time.deltaTime;

            if (currentDuration < 0) {
                SetColor(hoverColor);
            }
            
        } else {
            ResetColor();
        }

    }
    
}
