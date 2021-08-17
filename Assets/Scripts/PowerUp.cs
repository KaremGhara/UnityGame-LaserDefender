using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{ 
    [SerializeField] int HealBouns = 100;

    public int GetHeal()
    {
        return HealBouns;
    }


}
