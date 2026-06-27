using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EasyUI.PickerWheelUI;

public class WeaponUpgradeButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text oddsText;

    private UpgradeMenuUI menu;

    public WheelPiece Piece { get; private set; }

    public void Initialize(
        UpgradeMenuUI owner,
        WheelPiece piece)
    {
        menu = owner;
        Piece = piece;

        icon.sprite = piece.Icon;
        weaponName.text = piece.Label;

        Refresh();
    }

    public void Refresh()
    {
        oddsText.text = Piece.Chance.ToString("0") + "%";
    }

    public void Click()
    {
        menu.SelectPiece(this);
    }
}