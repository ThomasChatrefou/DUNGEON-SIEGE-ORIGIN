using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WeaponChoiceUI : MonoBehaviour, IDataPersistence
{

    public MenuUI menu;
    [SerializeField] private Image weaponIcon;

    [SerializeField] private WeaponDataSO swordData;
    [SerializeField] private WeaponDataSO spellbookData;
    [SerializeField] private WeaponDataSO wandData;

    [SerializeField] private TextMeshProUGUI swordAD;
    [SerializeField] private TextMeshProUGUI swordAS;
    [SerializeField] private TextMeshProUGUI swordAR;
    [SerializeField] private Sprite swordIcon;



    [SerializeField] private TextMeshProUGUI spellbookAD;
    [SerializeField] private TextMeshProUGUI spellbookAS;
    [SerializeField] private TextMeshProUGUI spellbookAR;
    [SerializeField] private Sprite spellbookIcon;

    [SerializeField] private TextMeshProUGUI wandAD;
    [SerializeField] private TextMeshProUGUI wandAS;
    [SerializeField] private TextMeshProUGUI wandAR;
    [SerializeField] private Sprite wandIcon;

    private byte newWeaponID = 0;

    // Start is called before the first frame update
    void Start()
    {
    
        swordAD.text = swordData.Damages.ToString();
        swordAS.text = swordData.AttackSpeed.ToString();
        swordAR.text = swordData.Range.ToString();

        spellbookAD.text = spellbookData.Damages.ToString();
        spellbookAS.text = spellbookData.AttackSpeed.ToString();
        spellbookAS.text = spellbookData.Range.ToString();

        wandAD.text = wandData.Damages.ToString();
        wandAS.text = wandData.AttackSpeed.ToString();
        wandAR.text = wandData.Range.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeapon(int _weaponID)
    {
        newWeaponID = ((byte)_weaponID);
        ChangeIcon();
        /* To link here */
        menu.WeaponsBack();
    }

    private void ChangeIcon()
    {
        switch (newWeaponID)
        {
            case 0:
                weaponIcon.sprite = swordIcon;
                break;
            case 1:
                weaponIcon.sprite = spellbookIcon;
                break;
            case 2:
                weaponIcon.sprite = wandIcon;
                break;
            default:
                break;
        }
    }

    public void LoadData(GameData data)
    {
        newWeaponID = data.weaponID;
        ChangeIcon();
    }
    
    public void SaveData(ref GameData data)
    {
        Debug.Log("New Weapon ID: " + newWeaponID);
        data.weaponID = newWeaponID;
        Debug.Log("data.weaponID: " + data.weaponID);
        data.characterID = newWeaponID;
        Debug.Log("data.characterID: " + data.characterID);
    }
}
