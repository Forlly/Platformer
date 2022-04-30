using System.Collections;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private InventorySystemTest _inventorySystemTest;
    [SerializeField] private Item _item;
    private bool isCollision = false;
    [SerializeField] public AudioSource pickUpSound;

    private void SearchInventorySystem()
    {
        _inventorySystemTest = FindObjectOfType<InventorySystemTest>();
    }

    public void TakeItem()
    {   
        SearchInventorySystem();
        _inventorySystemTest.AddItem(_item);
        pickUpSound.Play();
        StartCoroutine(DestroyItem());
    }

    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")&& !isCollision)
        {
            isCollision = true;
            TakeItem();
        }
    }
}
