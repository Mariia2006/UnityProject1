using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float dayLengthInSeconds = 120f;
    public Gradient lightColor;

    [Range(0f, 1f)]
    public float timeOfDay = 0.25f;

    void Update()
    {
        timeOfDay += Time.deltaTime / dayLengthInSeconds;

        if (timeOfDay >= 1f)
        {
            timeOfDay -= 1f;
        }

        float sunRotation = (timeOfDay * 360f) - 90f;
        directionalLight.transform.rotation = Quaternion.Euler(sunRotation, -30f, 0f);

        if (directionalLight != null)
        {
            directionalLight.color = lightColor.Evaluate(timeOfDay);
        }
    }
}