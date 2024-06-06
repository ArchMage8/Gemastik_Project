using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMover : MonoBehaviour
{
    public GameObject prefab;
    public GameObject destination;
    public float timeDelay = 15f;
    public float speed;
    public float fadeRate;

    private bool canFadeOut = true;

    void Start()
    {
        StartCoroutine(ObjectGenerator());
    }

    private IEnumerator ObjectGenerator()
    {
        while (true)
        {
            GameObject newObj = Instantiate(prefab, transform.position, Quaternion.identity);
            newObj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); // Set alpha to 0
            
            MoveAndFadeOut(newObj);
            
            yield return new WaitForSeconds(timeDelay);
        }
    }

    public void FadeIn(GameObject obj)
    {
        StartCoroutine(FadeInCoroutine(obj));
    }

    public void FadeOut(GameObject obj)
    {
        StartCoroutine(FadeOutCoroutine(obj));
    }

    private void MoveAndFadeOut(GameObject obj)
    {
        StartCoroutine(MoveAndFadeOutCoroutine(obj));
    }

    private IEnumerator MoveAndFadeOutCoroutine(GameObject obj)
    {
        if (obj != null)
        {
            FadeIn(obj);

            while (Vector2.Distance(obj.transform.position, destination.transform.position) > 0.1f)
            {
                obj.transform.position = Vector2.MoveTowards(obj.transform.position, destination.transform.position, speed * Time.deltaTime);

                // Check if the object is 2 units away from the destination
                if (Vector2.Distance(obj.transform.position, destination.transform.position) < 1f && canFadeOut)
                {
                    Debug.Log("Test");
                    // Start fading out
                    FadeOut(obj);
                    canFadeOut = false;
                }

                yield return null;
            }
        }
    }

    private IEnumerator FadeInCoroutine(GameObject obj)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
        

        while (spriteRenderer.color.a < 1.0f)
        {
            color.a += fadeRate * Time.deltaTime;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        color.a = 1.0f;
        spriteRenderer.color = color;
    }

    private IEnumerator FadeOutCoroutine(GameObject obj)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1.0f;
        spriteRenderer.color = color;

        while (spriteRenderer.color.a > 0.0f)
        {
            color.a -= fadeRate * Time.deltaTime;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        color.a = 0.0f;
        spriteRenderer.color = color;
        Destroy(obj);
        canFadeOut = true;
    }
}