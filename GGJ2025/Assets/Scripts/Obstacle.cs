
using UnityEngine;

public class Obstacle : MonoBehaviour
{
   public float speed;
   private void Update()
   {
      MoveTo();
   }

   private void MoveTo()
   {
      transform.position += Vector3.left * speed * Time.deltaTime;
   }
}
