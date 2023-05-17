using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeChoiceScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _topText;
    [SerializeField] TextMeshProUGUI _bottomText;

    Choice _choice;
    bool _isATrade = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (_isATrade)
        {

        }
    }

    public void SetChoice(Choice choice)
    {
        _choice = choice;
        _topText.text = _choice.name;
        _bottomText.text = _choice.description;
    }

    public void SetIsATrade(bool boolean)
    {
        _isATrade = boolean;
    }
}
