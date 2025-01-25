using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
   public List<TrackModifier> trackModifier;
   public float speed;
   private void Update()
   {
      MoveTo();
   }

   private void MoveTo()
   {
      transform.position += Vector3.left * (speed * Time.deltaTime);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Bubble"))
      {
         foreach (TrackModifier modifier in trackModifier)
         {
            modifier.time = TimeManager.Instance.GetNormalizedTime;
         }
         var bubble = other.GetComponent<MoveFromLoudness>();
         bubble.AddModifier(trackModifier);
         Destroy(gameObject);
      }
   }
}
