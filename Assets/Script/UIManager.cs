using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _currentAmmoText;
    [SerializeField]
    private Text _maxAmmoText;
    // Start is called before the first frame update

    public void UpdateAmmo(int count)
    {
        _currentAmmoText.text =  count + "";
    }

    public void UpdateMaxAmmo(int count)
    {
        _maxAmmoText.text = count + "";
    }
}
