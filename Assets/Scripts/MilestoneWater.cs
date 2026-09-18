using JetBrains.Annotations;
using UnityEngine;

public class MilestoneWater : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Renderer waterRenderer;

    [Header("Ocean Water Colors")]
    public Color finalColor;
    public Color initialColor;

    [Header("Ocean Water Milestone Colors")]
    public Color milestone1Color;
    public Color milestone2Color;

    //Methods for Inspector to call on milestone events
    public void ApplyInitialWater() => SetColor(initialColor);
    public void ApplyMilestone1() => SetColor(milestone1Color);
    public void ApplyMilestone2() => SetColor(milestone2Color);
    public void ApplyFinalWater() => SetColor(finalColor);

    private void SetColor(Color color)
    {
        if (waterRenderer != null)
        {
            waterRenderer.material.color = color;
        }
    }
}
