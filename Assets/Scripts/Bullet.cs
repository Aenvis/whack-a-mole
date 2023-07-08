using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public IEnumerator Shoot(Vector3 endPosition, float bulletSpeed)
    {
        var elapsed = 0f;
        while (Vector3.Distance(endPosition, transform.position) > 0.1f)
        {
            transform.localPosition = Vector3.Lerp(transform.position, endPosition, elapsed / bulletSpeed);
            elapsed += Time.fixedDeltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}