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
    private Collider physicalField;
    private MedicineChestSpawner chestSpawner;

    public float HealthRegen => healthRegen;

    public void Setup(Collider physicalField, MedicineChestSpawner chestSpawner)
    {
        this.physicalField = physicalField;
        this.chestSpawner = chestSpawner;
    }

    private void OnEnable()
    {
        ID_randHealthRegen = Random.Range(0, healthRegens.Count);
        healthRegen = healthRegens[ID_randHealthRegen];
    }

    private void FixedUpdate()
    {
        transformObj.Rotate(rotationVector * rotationSpeed);
    }

    private void OnTriggerExit(Collider other)
    {
        if (physicalField.Equals(other))
        {
            chestSpawner.AddMedicineChestToQueue(this);
            this.gameObject.SetActive(false);
        }
    }
}
