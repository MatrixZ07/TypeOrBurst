using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using TMPro;

public  class ColorPicker :MonoBehaviour
{
    public Material[] materials;

    public Material GetRandomMaterial() {
        return materials[Random.Range(0, materials.Length)];
    }
    
}
