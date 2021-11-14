using System.Collections;
using UnityEngine;

public static class Utillity
{
    public static IEnumerator Move(this Transform transform, Vector3 movePoint, float duration)
    {
        Vector3 startPosition = transform.position;
        float lostTime = 0;

        while(lostTime < 1)
        {
            transform.position = Vector3.Lerp(startPosition, movePoint, lostTime);
            lostTime += Time.deltaTime / duration;
            yield return null;
        }
    }

    public static IEnumerator Rotate(this Transform transform, Quaternion rotatePoint, float duration)
    {
        Quaternion startRotation = transform.rotation;
        float lostTime = 0;

        while(lostTime < 1)
        {
            transform.rotation = Quaternion.Lerp(startRotation, rotatePoint, lostTime);
            lostTime += Time.deltaTime / duration;
            yield return null;
        }
    }
}
