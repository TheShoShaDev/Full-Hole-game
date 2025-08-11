using UnityEngine;

public class DisappearHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.GetComponent<ICollectable>().Collected();
        }
    }
}
