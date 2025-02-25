using UnityEngine;

public class Ball2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed;
    public float width, height, xOffset, yOffset;

    private void Update()
    {
        float t = Time.time;

        // Multiply inside of sine/cosine to modify the speed
        // Multiply outside of sine/cosine to modify size
        // Add outside of sine/cosine to modify offset

        float x = width * Mathf.Cos(t * speed) + xOffset;
        float y = height * Mathf.Sin(t * speed) + yOffset;
        

        transform.position = new(x*t, y*t);
    }

}
