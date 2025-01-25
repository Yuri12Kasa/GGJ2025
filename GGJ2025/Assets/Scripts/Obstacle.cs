
using System;
using UnityEngine;
using Yuri;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
   public TrackModifier trackModifier;
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
      
   }
}
