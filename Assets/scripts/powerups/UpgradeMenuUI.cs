using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyUI.PickerWheelUI;

public class UpgradeMenuUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject meleePanel;
    [SerializeField] private GameObject rangedPanel;

    [Header("Buttons")]
    [SerializeField] private Button meleeButton;
    [SerializeField] private Button rangedButton;
    [SerializeField] private Button continueButton;

    [Header("Wheels")]
    [SerializeField] private PickerWheel meleeWheel;
    [SerializeField] private PickerWheel rangedWheel;

    private readonly List<WeaponUpgradeButton> meleeButtons =
        new List<WeaponUpgradeButton>();

    private readonly List<WeaponUpgradeButton> rangedButtons =
        new List<WeaponUpgradeButton>();

    private bool editingMelee;

    private WeaponUpgradeButton firstSelection;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        meleeButton.onClick.AddListener(OpenMelee);
        rangedButton.onClick.AddListener(OpenRanged);
        continueButton.onClick.AddListener(ContinuePressed);

        CollectButtons();

        RefreshButtons();
    }

    void CollectButtons()
    {
        meleeButtons.Clear();
        rangedButtons.Clear();

        foreach (WeaponUpgradeButton b in meleePanel.GetComponentsInChildren<WeaponUpgradeButton>())
            meleeButtons.Add(b);

        foreach (WeaponUpgradeButton b in rangedPanel.GetComponentsInChildren<WeaponUpgradeButton>())
            rangedButtons.Add(b);

        for (int i = 0; i < meleeButtons.Count; i++)
        {
            meleeButtons[i].Initialize(
                this,
                meleeWheel.wheelPieces[i]);
        }

        for (int i = 0; i < rangedButtons.Count; i++)
        {
            rangedButtons[i].Initialize(
                this,
                rangedWheel.wheelPieces[i]);
        }
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);

        OpenMelee();
    }

    void OpenMelee()
    {
        editingMelee = true;

        meleePanel.SetActive(true);
        rangedPanel.SetActive(false);
    }

    void OpenRanged()
    {
        editingMelee = false;

        meleePanel.SetActive(false);
        rangedPanel.SetActive(true);
    }

    public void SelectPiece(WeaponUpgradeButton button)
    {
        if (firstSelection == null)
        {
            firstSelection = button;
            return;
        }

        if (button == firstSelection)
        {
            firstSelection = null;
            return;
        }

        if (firstSelection.Piece.Chance < 10)
        {
            firstSelection = null;
            return;
        }

        firstSelection.Piece.Chance -= 10;
        button.Piece.Chance += 10;

        RefreshButtons();

        meleeWheel.RefreshWheel();
        rangedWheel.RefreshWheel();

        firstSelection = null;
    }

    void RefreshButtons()
    {
        foreach (WeaponUpgradeButton b in meleeButtons)
            b.Refresh();

        foreach (WeaponUpgradeButton b in rangedButtons)
            b.Refresh();
    }

    void ContinuePressed()
    {
        gameObject.SetActive(false);

        Debug.Log("Upgrade Saved");
    }
}