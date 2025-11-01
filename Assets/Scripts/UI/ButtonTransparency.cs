using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTransparency : MonoBehaviour
{
    [Range(0f, 1f)][SerializeField] private float transparency = 1f;
    private Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        ApplyTransparency();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (button == null)
            button = GetComponent<Button>();
        ApplyTransparency();
    }
#endif

    void ApplyTransparency()
    {
        ColorBlock colors = button.colors;

        colors.normalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, transparency);
        colors.highlightedColor = new Color(colors.highlightedColor.r, colors.highlightedColor.g, colors.highlightedColor.b, transparency);
        colors.pressedColor = new Color(colors.pressedColor.r, colors.pressedColor.g, colors.pressedColor.b, transparency);
        colors.selectedColor = new Color(colors.selectedColor.r, colors.selectedColor.g, colors.selectedColor.b, transparency);
        colors.disabledColor = new Color(colors.disabledColor.r, colors.disabledColor.g, colors.disabledColor.b, transparency);

        button.colors = colors;
    }
}
