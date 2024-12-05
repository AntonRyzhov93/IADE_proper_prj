using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupOpener : MonoBehaviour
{
    public GameObject PopUp;

    public void OpenPopUp()
    {
        if (PopUp != null)
        {
            PopUp.SetActive(true);
        }
    }
}
    
 

