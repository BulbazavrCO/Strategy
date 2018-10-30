using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth  {

    void SetHeath(float value);

    int GetHealth();

    int GetMaxHealth();
}
