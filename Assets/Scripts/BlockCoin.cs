using UnityEngine;
using System.Collections;


public class Coin : MonoBehaviour
{
    public float moveDistance = 1.0f; // Distance the coin moves up
    public float moveSpeed = 5f; // Speed of the movement
    public float destroyDelay = 0.2f; // Delay before the coin is destroyed


    private void Start()
    {
        // Start the movement animation and destruction process
        StartCoroutine(MoveAndDestroy());
    }

    private IEnumerator MoveAndDestroy()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * moveDistance;

        // Move the coin up
        float elapsedTime = 0f;
        while (elapsedTime < moveDistance / moveSpeed)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime * moveSpeed / moveDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move the coin down
        elapsedTime = 0f;
        while (elapsedTime < moveDistance / moveSpeed)
        {
            transform.position = Vector3.Lerp(endPos, startPos, elapsedTime * moveSpeed / moveDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy the coin after a short delay
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
