using Unity.VisualScripting;
using UnityEngine;

public class BallJump : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speedmaxtocheck = 0.5f;
    [SerializeField] private float colorchangespeed = 5;
    private Renderer renderera;
    private Rigidbody rb;

    float inrangetimer = 0f;
    Color defaultcolor;
       
    void Start()
    {
        defaultcolor = gameObject.GetComponent<Renderer>().material.color;
        renderera = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }
    float hue;
    private void ColorBall()
    {
            hue += Time.deltaTime * colorchangespeed;
            if (hue >1f) hue = 0f;
            renderera.material.color = Color.HSVToRGB(hue, 1, 1);
    }
    float currentspeed = 100000f;
    private void FixedUpdate()
    {
        currentspeed = rb.velocity.magnitude;
    }
    float LogTimer = 0f;
    void Update()
    {
        if (currentspeed > speedmaxtocheck)
        {
            if (inrangetimer > 0f)
            {
                inrangetimer = 0f;
            }
            if (renderera.material.color != defaultcolor) renderera.material.color = defaultcolor;
        }
        else
        {
            inrangetimer += Time.deltaTime;
            LogTimer += Time.deltaTime;
            if (LogTimer > 0.5f) // to prevent debug spam.
            {
                LogTimer = 0f;
                Debug.Log(inrangetimer);
                ColorBall();
            }
        }
    }
}
