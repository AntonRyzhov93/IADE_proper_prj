using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointHUD:MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;

     int points = 0;

     private void Awake()
     {
         updateHUD();
     }

     public int Points
     {
         get
         {
             return points; 
         }
         set
         {
             points = value;
             updateHUD();
         }
     }

     private void updateHUD ()
     {
         pointText.text = points.ToString();
     }
    
}
