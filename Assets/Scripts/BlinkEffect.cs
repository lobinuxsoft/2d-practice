using System.Threading.Tasks;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] Color colorToBlink = Color.red;
    [SerializeField] AnimationCurve blinkBahaviour;
    [SerializeField] float blinkSpeed = 1;

    Renderer rendererComponent;

    private void Awake() => rendererComponent = GetComponent<Renderer>();

    public async void BlinkNoTask(float duration) => await BlinkTask(duration);

    public async Task BlinkTask(float duration)
    {
        float end = Time.time + duration;

        float lerp = 0;

        while (Time.time < end)
        {
            lerp += blinkSpeed * Time.deltaTime;

            rendererComponent.material.color = Color.Lerp(Color.white, colorToBlink, blinkBahaviour.Evaluate(Mathf.PingPong(lerp, 1)));

            await Task.Yield();
        }

        rendererComponent.material.color = Color.white;
    }
}
