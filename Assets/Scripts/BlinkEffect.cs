using System.Threading.Tasks;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField, ColorUsage(false, true)] Color colorToBlink = Color.red;
    [SerializeField] AnimationCurve blinkBahaviour;
    [SerializeField] float blinkSpeed = 1;

    Renderer rendererComponent;
    Material material;

    private void Awake()
    { 
        rendererComponent = GetComponent<Renderer>();
        material = rendererComponent.material;
    }

    public async void BlinkNoTask(float duration) => await BlinkTask(duration);

    public async Task BlinkTask(float duration)
    {
        float end = Time.time + duration;

        float lerp = 0;

        while (Time.time < end)
        {
            lerp += blinkSpeed * Time.deltaTime;

            Color lerpColor = Color.Lerp(Color.clear, colorToBlink, blinkBahaviour.Evaluate(Mathf.PingPong(lerp, 1)));

            if(material) material.SetColor("_EmissionColor", lerpColor);

            await Task.Yield();
        }

        if (material) material.SetColor("_EmissionColor", Color.clear);
    }
}
