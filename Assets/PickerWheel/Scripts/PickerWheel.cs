using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

namespace EasyUI.PickerWheelUI
{
    public class PickerWheel : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private Transform linesParent;

        [SerializeField] private Transform wheelCircle;

        [SerializeField] private GameObject wheelPiecePrefab;
        [SerializeField] private GameObject sectorPrefab;

        [SerializeField] private Transform wheelPiecesParent;

        [Header("Settings")]
        [SerializeField] private float spinDuration = 3f;
        [SerializeField] private int extraRotations = 6;

        [Header("Pieces")]
        public WheelPiece[] wheelPieces;

        private UnityAction onSpinStartEvent;
        private UnityAction<WheelPiece> onSpinEndEvent;

        private bool isSpinning;

        public bool IsSpinning => isSpinning;

        private float pieceAngle;

        private void Awake()
        {
            pieceAngle = 360f / wheelPieces.Length;

            GenerateWheel();
        }

        public void OnSpinStart(UnityAction action)
        {
            onSpinStartEvent = action;
        }

        public void OnSpinEnd(UnityAction<WheelPiece> action)
        {
            onSpinEndEvent = action;
        }

        public void Spin()
        {
            if (isSpinning)
                return;

            isSpinning = true;

            onSpinStartEvent?.Invoke();

            int chosenIndex = GetWeightedIndex();

            WheelPiece chosenPiece =
                wheelPieces[chosenIndex];

            Debug.Log(
                "Chosen Piece = " +
                chosenPiece.Label);

            float centerAngle =
                -(chosenIndex * pieceAngle);

            float randomOffset =
                Random.Range(
                    -pieceAngle * 0.4f,
                     pieceAngle * 0.4f);

            float finalAngle =
                centerAngle +
                randomOffset;

            float currentRotation =
                wheelCircle.eulerAngles.z;

            float targetRotation =
                currentRotation +
                (extraRotations * 360f) +
                (360f - finalAngle);

            wheelCircle
                .DORotate(
                    new Vector3(
                        0,
                        0,
                        targetRotation),
                    spinDuration,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    isSpinning = false;

                    onSpinEndEvent?.Invoke(
                        chosenPiece);
                });
        }

        private int GetWeightedIndex()
        {
            float totalWeight = 0f;

            for (int i = 0; i < wheelPieces.Length; i++)
            {
                totalWeight +=
                    Mathf.Max(
                        0,
                        wheelPieces[i].Chance);
            }

            float randomValue =
                Random.Range(
                    0f,
                    totalWeight);

            float runningTotal = 0f;

            for (int i = 0; i < wheelPieces.Length; i++)
            {
                runningTotal +=
                    Mathf.Max(
                        0,
                        wheelPieces[i].Chance);

                if (randomValue <= runningTotal)
                {
                    Debug.Log(
                        "Random = " +
                        randomValue +
                        " -> " +
                        wheelPieces[i].Label);

                    return i;
                }
            }

            return wheelPieces.Length - 1;
        }

        private void GenerateWheel()
        {
            foreach (Transform child in wheelPiecesParent)
                Destroy(child.gameObject);

            foreach (Transform child in linesParent)
                Destroy(child.gameObject);

            for (int i = 0; i < wheelPieces.Length; i++)
            {
                CreateSector(i);
                CreatePiece(i);
                CreateLine(i);
            }
        }

        private void CreateSector(int index)
        {
            WheelPiece piece = wheelPieces[index];

            GameObject sector =
                Instantiate(
                    sectorPrefab,
                    wheelPiecesParent);

            sector.transform.SetAsFirstSibling();

            Image img =
                sector.GetComponent<Image>();

            img.color = piece.PieceColor;

            img.fillAmount =
                1f / wheelPieces.Length;

            RectTransform rt =
                sector.GetComponent<RectTransform>();

            rt.localPosition =
                Vector3.zero;

            rt.localScale =
                Vector3.one;

            rt.localRotation =
                Quaternion.Euler(
                    0,
                    0,
                    -(index * pieceAngle));
        }
private void CreatePiece(int index)
{
    WheelPiece piece = wheelPieces[index];

    GameObject obj =
        Instantiate(
            wheelPiecePrefab,
            wheelPiecesParent);

    RectTransform rt =
        obj.transform.GetChild(0)
        .GetComponent<RectTransform>();

    float radius = 130f;

    float angle =
        index * pieceAngle * Mathf.Deg2Rad;

  

rt.localPosition = Vector3.zero;

obj.transform.Rotate(
    Vector3.forward,
    -(index * pieceAngle));
rt.RotateAround(
    wheelPiecesParent.position,
    Vector3.back,
    pieceAngle * index);
   

    rt.GetChild(0)
        .GetComponent<Image>()
        .sprite = piece.Icon;

    rt.GetChild(1)
        .GetComponent<Text>()
        .text = piece.Label;

    rt.GetChild(2)
        .GetComponent<Text>()
        .text = piece.Amount.ToString();
}

        private void CreateLine(int index)
        {
            Transform line =
                Instantiate(
                    linePrefab,
                    linesParent)
                .transform;

            line.localPosition =
                Vector3.zero;

            line.localRotation =
                Quaternion.Euler(
                    0,
                    0,
                    -(index * pieceAngle));
        }
    }
}