using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public IEnumerator Shoot(Vector3 endPosition, float bulletSpeed)
    {
        var endPos = new Vector3(endPosition.x, endPosition.y + 1f, endPosition.z);
        var elapsed = 0f;
        while (Vector3.Distance(endPos, transform.position) > 0.1f)
        {
            transform.localPosition = Vector3.Lerp(transform.position, endPos, elapsed / bulletSpeed);
            elapsed += Time.fixedDeltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}