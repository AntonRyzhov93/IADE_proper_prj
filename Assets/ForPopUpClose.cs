using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPopUpClose : MonoBehaviour
{
    public GameObject PopUpClose;

    public void ClosePopUp()
    {
        if (PopUpClose != null)
        {
            PopUpClose.SetActive(false);
        }
    }
}
