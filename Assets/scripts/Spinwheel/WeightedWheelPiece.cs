using UnityEngine;

[System.Serializable]
public class WeightedWheelPiece
{
    [Header("Display")]
    public Sprite Icon;
    public string Label;
    public int Amount;

    [Header("Weight")]
    [Min(0.01f)]
    public float Chance = 1f;

    [Header("Visual")]
    public Color PieceColor = Color.white;

    [HideInInspector]
    public float StartAngle;

    [HideInInspector]
    public float EndAngle;

    [HideInInspector]
    public float CenterAngle;

    [HideInInspector]
    public float SectorAngle;
}