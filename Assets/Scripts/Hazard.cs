using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] int hazardDamage = 50;

    public int GetHazardDamage()
    {
        return hazardDamage;
    }
   
    
}
