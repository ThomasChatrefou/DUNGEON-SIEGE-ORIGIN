using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WeaponChoiceUI : MonoBehaviour
{

    public MenuUI menu;

    [SerializeField] private WeaponDataSO swordData;
    [SerializeField] private WeaponDataSO spellbookData;
    [SerializeField] private WeaponDataSO wandData;


    [SerializeField] private TextMeshProUGUI swordAD;
    [SerializeField] private TextMeshProUGUI swordAS;
    [SerializeField] private TextMeshProUGUI swordAR;

    [SerializeField] private TextMeshProUGUI spellbookAD;
    [SerializeField] private TextMeshProUGUI spellbookAS;
    [SerializeField] private TextMeshProUGUI spellbookAR;

    [SerializeField] private TextMeshProUGUI wandAD;
    [SerializeField] private TextMeshProUGUI wandAS;
    [SerializeField] private TextMeshProUGUI wandAR;
    

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

    public void ChangeWeapon()
    {
        /* To link here */
        menu.WeaponsBack();
    }
}
