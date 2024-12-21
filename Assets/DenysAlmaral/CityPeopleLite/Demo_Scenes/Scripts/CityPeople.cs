using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CitySim
{
    public class CityPeople : MonoBehaviour
    {
        public Transform[] pattern;
        private int patternIndex = 0;

        public float speed;

        public bool isHit = false;
        public bool ragdollOn = false;

        private AnimationClip[] myClips;
        private Animator animator;

        public List<Collider> RagdollParts = new List<Collider>();
        public Rigidbody rb;

        public AudioSource audioSource; // Reference to the AudioSource component
        public AudioClip hitSound;      // The sound to play when hit

        public enum npcState

        {
            hit,
            waypoint1,
        
        };

        public npcState state = npcState.waypoint1;

        void Start()
        {
            if(isHit == false)
            {
                animator = GetComponent<Animator>();
                if (animator != null)
                {
                    PlaySpecificClip("Walk"); 
                }

                patternIndex = FindClosestWaypointIndex();
            }



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


            if (state == npcState.waypoint1)
            {


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

            if(state == npcState.hit)
            {
                TurnOnRagdoll();
            }

            switch (state)
            {
            case npcState.waypoint1:
                if (isHit == true)
                {
                   state = npcState.hit;
                }
                break;
            
            case npcState.hit:
                if (isHit == false)
                {
                   state = npcState.waypoint1;
                }
                break;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "car")
            {
                audioSource.PlayOneShot(hitSound);
                rb.AddForce(1000f * Vector3.up);
                StartCoroutine(DelayedHit(1.0f));

            }
        }

        private IEnumerator DelayedHit(float delay)
        {
            yield return new WaitForSeconds(delay);
            isHit = true;
        }

        private void Awake()
        {
            SetRagdollParts();
        }


        private void SetRagdollParts()
        {
            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

            foreach(Collider c in colliders)
            {
                if(c.gameObject != this.gameObject) 
                {
                    c.isTrigger = true;
                    RagdollParts.Add(c);
                }
            }
        }

        public void TurnOnRagdoll()
        {
            //rb.useGravity = false;
            //rb.velocity = Vector3.zero;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            animator.enabled = false;
            animator.avatar = null;

            foreach(Collider c in RagdollParts)
            {
                c.isTrigger = false;
                //c.attachedRigidbody.velocity = Vector3.zero;
            }
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
