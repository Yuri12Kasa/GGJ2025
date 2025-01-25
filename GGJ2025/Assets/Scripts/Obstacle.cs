
using System;
using System.Collections.Generic;
using UnityEngine;
using Yuri;

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
      transform.position += Vector3.left * speed * Time.deltaTime;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Bubble")
      {
         var bubblee = other.GetComponent<MoveFromLoudness>();
         bubblee.AddModifier(trackModifier);
         Destroy(this.gameObject);
      }
   }
}
