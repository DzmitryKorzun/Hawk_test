using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineChest : MonoBehaviour
{
    [SerializeField] private List<float> healthRegens;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 rotationVector;
    [SerializeField] private Transform transformObj;

    private float healthRegen;
    private int ID_randHealthRegen;

    public float HealthRegen => healthRegen;

    private void OnEnable()
    {
        ID_randHealthRegen = Random.Range(0, healthRegens.Count);
        healthRegen = healthRegens[ID_randHealthRegen];
    }

    private void FixedUpdate()
    {
        transformObj.Rotate(rotationVector * rotationSpeed);
    }
}
