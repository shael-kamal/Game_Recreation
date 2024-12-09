using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    public class MushroomSpawner : MonoBehaviour
    {
        public GameObject mushroomPrefab; // The mushroom prefab to instantiate
        public float riseHeight = 1.0f; // Height the mushroom rises
        public float riseSpeed = 1.0f; // Speed of rising
        public float moveSpeed = 0f; // Speed of movement to the right
        public float colliderEnableDelay = 0.1f; // Small delay before enabling the collider


        private bool isSpawning = false; // Prevent multiple spawns


        private void Update()
        {
            
            

        }

        public void SpawnMushroom()
        {
            if (!isSpawning && mushroomPrefab != null)
            {
                isSpawning = true;
                GameObject mushroom = Instantiate(mushroomPrefab, transform.position, Quaternion.identity);
                StartCoroutine(RiseMushroom(mushroom));
            }
        }

        private IEnumerator RiseMushroom(GameObject mushroom)
        {
            Vector3 startPos = mushroom.transform.position;
            Vector3 endPos = startPos + Vector3.up * riseHeight;

            // Get the Rigidbody2D and Collider2D components
            Rigidbody2D mushroomRigidbody = mushroom.GetComponent<Rigidbody2D>();
            Collider2D mushroomCollider = mushroom.GetComponent<Collider2D>();

            if (mushroomCollider != null)
            {
                mushroomCollider.enabled = false;
            }

            // Slowly rise up
            float elapsedTime = 0f;
            while (elapsedTime < riseHeight / riseSpeed)
            {
                mushroom.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime * riseSpeed / riseHeight);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure it reaches the final position
            mushroom.transform.position = endPos;

            // Enable the Rigidbody2D and Collider2D after a short delay
            if (mushroomCollider != null)
            {
                yield return new WaitForSeconds(colliderEnableDelay);
                mushroomCollider.enabled = true;
            }

            
        }
    }
}
