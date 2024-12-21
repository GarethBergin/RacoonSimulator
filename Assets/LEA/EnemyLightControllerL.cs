using UnityEngine;


public class EnemyLightControllerL : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D enemyLight;
    public Color idleColor = Color.white;
    public Color chaseColor = Color.red;
    public float flashSpeed = 5f;

    private bool isChasing = false;

    void Update()
    {
        if (isChasing)
        {
            float lerp = Mathf.PingPong(Time.time * flashSpeed, 1f);
            enemyLight.color = Color.Lerp(idleColor, chaseColor, lerp);
        }
        else
        {
            enemyLight.color = idleColor;
        }
    }

    public void SetChasing(bool chasing)
    {
        isChasing = chasing;
    }
}
