using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour, ICollectable
{
    public void Collected()
    {
        GameOver();
    }

   private void GameOver()
   {
        gameObject.SetActive(false);
        throw new System.NotImplementedException();
   }

}
