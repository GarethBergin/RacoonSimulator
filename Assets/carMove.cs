using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarSim
{
    public class CarMove : MonoBehaviour
    {
        public Transform[] pattern;
        private int patternIndex = 0;

        public float speed;

        private AnimationClip[] myClips;
        private Animator animator;

        public enum npcState
        {
            waypoint1,
        
        };


        void Start()
        {
            animator = GetComponent<Animator>();
            if (animator != null)
            {
                PlaySpecificClip("Walk"); 
            }

            patternIndex = FindClosestWaypointIndex();

        }

        void Update()
        {

            if (pattern == null || pattern.Length == 0) return;
            // Process the current instruction in our control data array
            Transform  waypoint = pattern[patternIndex];

            // Find the range to close vector
            Vector3 rangeToClose = waypoint.transform.position - transform.position;

            // Draw this vector at the position of the enemy
            //Debug.DrawRay(transform.position, rangeToClose, Color.cyan);

            // What's our distance to the waypoint?
            float distance = rangeToClose.magnitude;

            // How far do we move each frame
            float speedDelta = speed * Time.deltaTime;

            // If we're close enough to the current waypoint 
            // then increase the pattern index

            if (distance <= speedDelta)
            {
                patternIndex = (patternIndex + 1) % pattern.Length;

                // Process the current instruction in our control data array
                waypoint = pattern[patternIndex];

                // Find the new range to close vector
                rangeToClose = waypoint.position - transform.position;
            }

            // In what direction is our waypoint?
            Vector3 normalizedRangeToClose = rangeToClose.normalized;

            // Draw this vector at the position of the waypoint
            //Debug.DrawRay(transform.position, normalizedRangeToClose, Color.magenta);
            if (normalizedRangeToClose != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(normalizedRangeToClose);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Smooth rotation
            }

            Vector3 delta = speedDelta * normalizedRangeToClose;
            transform.Translate(delta, Space.World);
            
        }

        int FindClosestWaypointIndex()
        {
            int closestIndex = 0;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < pattern.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, pattern[i].position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        void PlaySpecificClip(string clipName)
        {
            if (animator == null) return;

            // Crossfade to the specified animation clip
            animator.CrossFadeInFixedTime(clipName, 0.1f);
        }

    }
}
