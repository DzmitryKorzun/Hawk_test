using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineChestSpawner : MonoBehaviour
{
    [SerializeField] private MedicineChest refMedicineChest;
    [SerializeField] private float percentageChanceOfGeneration;

    private Queue<MedicineChest> queueMedicineChest = new Queue<MedicineChest>();
    private Collider playingField;

    public void Setup(Collider playingField)
    {
        this.playingField = playingField;
    }

    public void AddMedicineChestToQueue(MedicineChest medicineChest)
    {
        queueMedicineChest.Enqueue(medicineChest);
    }

    private MedicineChest GetMedicineChest()
    {
        if (queueMedicineChest.Count == 0)
        {
            MedicineChest medicineChest = Instantiate(refMedicineChest);
            medicineChest.Setup(playingField, this);
            return medicineChest;
        }
        else
        {
            MedicineChest medicineChest = queueMedicineChest.Dequeue();
            medicineChest.gameObject.SetActive(true);
            return medicineChest;
        }
    }

    public MedicineChest TryGenerateMedicineChest()
    {
        int randNum = Random.Range(0, 100);
        if (randNum < percentageChanceOfGeneration)
        {
            return GetMedicineChest();
        }
        return null;
    }
}
