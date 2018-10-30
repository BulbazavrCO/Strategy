using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawn
{
    float TimeSpawn { get; set; }

    void Spawn();
}
