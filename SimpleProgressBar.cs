using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a simple progress bar with various customization options.
/// </summary>
[ExecuteInEditMode]
public class SimpleProgressBar : MonoBehaviour
{
    [Header("Graphic Settings")]
    [Tooltip("The Image component used for the progress bar.")]
    public Image graphic;
    [Tooltip("The default sprite for the progress bar.")]
    public Sprite sprite;
    [Tooltip("The size of the progress bar.")]
    public Vector2 size = new Vector2(600, 40);
    [Tooltip("The gradient used for color interpolation.")]
    public Gradient gradient;
    [Tooltip("The single color used for fill.")]
    public Color color;
    [Tooltip("The minimum color for gradient fill.")]
    public Color minColor;
    [Tooltip("The maximum color for gradient fill.")]
    public Color maxColor;
    [Tooltip("The background Image component.")]
    public Image background;
    [Tooltip("The outline Image component.")]
    public Image outlinePerimeter;
    [Tooltip("The color of the outline.")]
    public Color outlineColor;
    [Tooltip("The single color used for fill background.")]
    public Color backgroundColor;
    [Tooltip("The thickness of the outline.")]
    public Vector2 outlineThickness = new Vector2(10, 8);

    [Header("Progress Settings")]
    [Tooltip("The type of progress visualization.")]
    public ProgressType progressType = ProgressType.FillColor;
    [Tooltip("The type of fill color.")]
    public FillType fillType = FillType.Gradient;
    [Tooltip("The fill method for the progress bar.")]
    public Image.FillMethod fillMethod = Image.FillMethod.Horizontal;
    [Tooltip("The current value of the progress bar.")]
    public float value = 1;
    [Tooltip("The minimum value of the progress bar.")]
    public float min = 0;
    [Tooltip("The maximum value of the progress bar.")]
    public float max = 1;
    /// <summary>
    /// The calculated filled amount of the progress bar.
    /// </summary>

    public bool showPercentage = false;
    public TMP_Text percentage;
    public float Filled => (value - min) / (max - min);

    private float _value = -1;

    public void Start()
    {
        if (!Application.isPlaying)
        {
            Reset();
        }
    }

    private void Update()
    {
        value = Mathf.Clamp(value, min, max);
        if (value != _value)
        {
            _value = value;
            if (graphic)
            {
                float amount = Filled;
                switch (progressType)
                {
                    case ProgressType.ColorOnly:
                        {
                            SetColor(amount);
                        }
                        break;
                    case ProgressType.FillOnly:
                        {
                            SetFill(amount);
                        }
                        break;
                    case ProgressType.FillColor:
                        {
                            SetFill(amount);
                            SetColor(amount);
                        }
                        break;
                }
            }
            if(showPercentage && percentage)
            {
                percentage.text = (int)(Filled * 100) + "%";
            }
        }
    }

    /// <summary>
    /// Sets the fill amount of the graphic based on the calculated filled value.
    /// </summary>
    private void SetFill(float amount)
    {
        graphic.fillAmount = amount;
    }

    /// <summary>
    /// Sets the color of the graphic based on the fill type.
    /// </summary>
    private void SetColor(float amount)
    {
        switch (fillType)
        {
            case FillType.SingleColor:
                {
                    graphic.color = color;
                }
                break;
            case FillType.MinMaxColor:
                {
                    graphic.color = Color.Lerp(minColor, maxColor, amount);
                }
                break;
            case FillType.Gradient:
                {
                    graphic.color = gradient.Evaluate(amount);
                }
                break;
        }
    }

    /// <summary>
    /// Sets the minimum value of the progress bar.
    /// </summary>
    public void SetMin(float min)
    {
        this.min = min;
        Update();
    }

    /// <summary>
    /// Sets the maximum value of the progress bar.
    /// </summary>
    public void SetMax(float max)
    {
        this.max = max;
        Update();
    }

    /// <summary>
    /// Sets both the minimum and maximum values of the progress bar.
    /// </summary>
    public void SetMinMax(float min, float max)
    {
        this.min = min;
        this.max = max;
        Update();
    }

    /// <summary>
    /// Sets the value of the progress bar and updates its display.
    /// </summary>
    public void SetValue(float value)
    {
        if (value != this.value)
        {
            this.value = value;
            Update();
        }
    }

    /// <summary>
    /// Resets the progress bar to its default settings.
    /// </summary>
    public void Reset()
    {
        min = 0;
        max = 1;
        value = 1;
        color = Color.white;
        minColor = Color.red;
        maxColor = Color.green;
        fillMethod = Image.FillMethod.Horizontal;
        fillType = FillType.Gradient;
        gradient = new Gradient();
        gradient.mode = GradientMode.Blend;
        var keys = new GradientColorKey[2];
        var alphaKeys = new GradientAlphaKey[2];
        keys[0] = new GradientColorKey(Color.red, 0);
        alphaKeys[0] = new GradientAlphaKey(1, 0);
        keys[1] = new GradientColorKey(Color.green, 100);
        alphaKeys[1] = new GradientAlphaKey(1, 1);
        gradient.SetKeys(keys, alphaKeys);

        outlineThickness = new Vector2(10, 8);
        outlineColor = Color.yellow;
        backgroundColor = Color.black;
        size = new Vector2(600, 40);
        if (graphic && background && outlinePerimeter)
        {
        }
        else
        {
            GameObject bubble = new GameObject("outline");
            bubble.transform.parent = transform;
            bubble.transform.localPosition = Vector3.zero;
            bubble.transform.localScale = Vector3.one;
            graphic = bubble.AddComponent<Image>();

            outlinePerimeter = graphic;
            if (!background)
            {
                background = Instantiate(outlinePerimeter, outlinePerimeter.transform);
                outlinePerimeter.name = "background";
            }
            graphic = Instantiate(background, transform);
            graphic.name = "fill";
        }
        graphic.type = Image.Type.Filled;
        SetDefaultSprite();
        UpdateGraphic();
        Update();
        _value = -1;
    }

    /// <summary>
    /// Sets the default sprite for the progress bar if none is assigned.
    /// </summary>
    public void SetDefaultSprite()
    {
        if (graphic == null)
        {
            Debug.LogError("Image component is null.");
            return;
        }

        if (graphic.sprite == null)
        {
            graphic.sprite = sprite;
            if (background)
            {
                background.sprite = sprite;
                outlinePerimeter.sprite = sprite;
            }
        }
    }

    /// <summary>
    /// Updates the graphic settings of the progress bar.
    /// </summary>
    public void UpdateGraphic()
    {
        if (graphic)
        {
            graphic.fillMethod = fillMethod;
            graphic.rectTransform.sizeDelta = size;
            if (background)
            {
                background.type = Image.Type.Simple;
                background.rectTransform.sizeDelta = size;
                background.color = backgroundColor;

                outlinePerimeter.type = Image.Type.Simple;
                outlinePerimeter.rectTransform.sizeDelta = size + outlineThickness;
                outlinePerimeter.color = outlineColor;
            }
        }
    }
    public void Add(float amount)
    {
        value += amount;
        Update();
    }

    /// <summary>
    /// The type of progress visualization.
    /// </summary>
    public enum ProgressType { ColorOnly, FillOnly, FillColor }

    /// <summary>
    /// The type of fill color.
    /// </summary>
    public enum FillType { SingleColor, MinMaxColor, Gradient }
}
