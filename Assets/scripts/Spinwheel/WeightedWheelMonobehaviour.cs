using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class WeightedWheel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private WeightedWheelRenderer renderer;

    [Header("Wheel Data")]
    public WeightedWheelPiece[] wheelPieces;

    [Header("Spin Settings")]
    [Range(1, 20)]
    public int spinDuration = 5;

    [Range(1, 10)]
    public int extraSpins = 5;

    private bool isSpinning;

    public bool IsSpinning => isSpinning;

    public UnityEvent OnSpinStart;

    [System.Serializable]
    public class WheelResultEvent : UnityEvent<WeightedWheelPiece> { }

    public WheelResultEvent OnSpinEnd;

    private void Start()
    {
        renderer.BuildWheel(wheelPieces);
    }

    public void RebuildWheel()
    {
        renderer.BuildWheel(wheelPieces);
    }
[ContextMenu("Rebuild Wheel")]
    public void Spin()
    {
        if (isSpinning)
            return;

        isSpinning = true;

        OnSpinStart?.Invoke();

        WeightedWheelPiece winner = PickWinner();

        float targetAngle =
            Random.Range(
                winner.StartAngle + 1f,
                winner.EndAngle - 1f);

        float finalRotation =
            -(targetAngle + (extraSpins * 360f));

        wheelTransform
            .DORotate(
                Vector3.forward * finalRotation,
                spinDuration,
                RotateMode.FastBeyond360)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                isSpinning = false;

                OnSpinEnd?.Invoke(winner);

                Debug.Log(
                    $"Winner: {winner.Label} | Amount: {winner.Amount}");
            });
    }

    private WeightedWheelPiece PickWinner()
    {
        float totalWeight = 0f;

        foreach (var piece in wheelPieces)
            totalWeight += piece.Chance;

        float roll =
            Random.Range(0f, totalWeight);

        float current = 0f;

        foreach (var piece in wheelPieces)
        {
            current += piece.Chance;

            if (roll <= current)
                return piece;
        }

        return wheelPieces[0];
    }

}