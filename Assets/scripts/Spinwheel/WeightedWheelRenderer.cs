using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeightedWheelRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform wheelRoot;
    [SerializeField] private Transform sectorParent;
    [SerializeField] private GameObject sectorPrefab;

    [Header("Lines")]
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform lineParent;

    [Header("Piece UI")]
    [SerializeField] private GameObject piecePrefab;

    [Header("Layout")]
    [SerializeField] private float iconRadius = 140f;
    [SerializeField] private float labelRadius = 95f;
    [SerializeField] private Transform pieceParent;

    private readonly List<GameObject> spawnedObjects = new();

    public void BuildWheel(WeightedWheelPiece[] pieces)
    {
        ClearWheel();

        if (pieces == null || pieces.Length == 0)
            return;

        float totalChance = 0f;

        foreach (var piece in pieces)
            totalChance += piece.Chance;

        float currentAngle = 0f;

        for (int i = 0; i < pieces.Length; i++)
        {
            WeightedWheelPiece piece = pieces[i];

            float sectorAngle =
                (piece.Chance / totalChance) * 360f;

            piece.StartAngle = currentAngle;
            piece.EndAngle = currentAngle + sectorAngle;
            piece.CenterAngle = currentAngle + sectorAngle * 0.5f;
            piece.SectorAngle = sectorAngle;

            CreateSector(piece);
            CreateLine(piece.StartAngle);
            CreatePieceVisual(piece);

            currentAngle += sectorAngle;
        }
    }

    private void CreateSector(WeightedWheelPiece piece)
    {
        GameObject sector =
            Instantiate(sectorPrefab, sectorParent);

        spawnedObjects.Add(sector);

        Image img =
            sector.GetComponent<Image>();

        img.color = piece.PieceColor;

        img.fillAmount =
            piece.SectorAngle / 360f;

        RectTransform rt =
            sector.GetComponent<RectTransform>();

        rt.localPosition = Vector3.zero;
        rt.localScale = Vector3.one;

        rt.localRotation =
            Quaternion.Euler(
                0f,
                0f,
                -piece.StartAngle);
    }

    private void CreateLine(float angle)
    {
        if (linePrefab == null || lineParent == null)
            return;

        GameObject line =
            Instantiate(linePrefab, lineParent);

        spawnedObjects.Add(line);

        RectTransform rt =
            line.GetComponent<RectTransform>();

        rt.localPosition = Vector3.zero;

        rt.localRotation =
            Quaternion.Euler(
                0f,
                0f,
                -angle);
    }

   private void CreatePieceVisual(WeightedWheelPiece piece)
{
    GameObject obj =
        Instantiate(piecePrefab, pieceParent);

    spawnedObjects.Add(obj);

    Transform holder =
        obj.transform.GetChild(0);

    holder.GetChild(0)
        .GetComponent<Image>()
        .sprite = piece.Icon;

    holder.GetChild(1)
        .GetComponent<Text>()
        .text = piece.Label;

    holder.GetChild(2)
        .GetComponent<Text>()
        .text = piece.Amount.ToString();

    // --------------------------------------------------
    // OLD PICKER WHEEL POSITIONING LOGIC
    // --------------------------------------------------

    RectTransform holderRT =
        holder.GetComponent<RectTransform>();

    float pieceWidth =
        Mathf.Lerp(
            81f,
            144f,
            1f - Mathf.InverseLerp(
                2f,
                12f,
                pieceParent.childCount));

    float pieceHeight =
        Mathf.Lerp(
            146f,
            213f,
            1f - Mathf.InverseLerp(
                2f,
                12f,
                pieceParent.childCount));

    holderRT.SetSizeWithCurrentAnchors(
        RectTransform.Axis.Horizontal,
        pieceWidth);

    holderRT.SetSizeWithCurrentAnchors(
        RectTransform.Axis.Vertical,
        pieceHeight);

    // Start directly above center
    holder.position =
        wheelRoot.position +
        Vector3.up * (wheelRoot.rect.width * 0.28f);

    // Rotate into sector center
    holder.RotateAround(
        wheelRoot.position,
        Vector3.back,
        piece.CenterAngle);

    // Keep text/icons upright
    holder.rotation =
        Quaternion.identity;
}
    public void ClearWheel()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj == null)
                continue;

            if (Application.isPlaying)
                Destroy(obj);
            else
                DestroyImmediate(obj);
        }

        spawnedObjects.Clear();
    }
}