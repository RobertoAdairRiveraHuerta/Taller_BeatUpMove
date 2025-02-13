using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Jugador : MonoBehaviour
{
    private bool AtaquePunch = true;
    private float tiempoSiguienteAtaque;
    private float tiempoEntreAtaque;
    private float verticalSpeed;
    private float horizontalSpeed;
    public GameObject BoxAtaque;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        tiempoEntreAtaque = 0.5f;
        tiempoSiguienteAtaque = 0.5f;
        verticalSpeed = 1f;
        horizontalSpeed = 1f;
    }

    Vector2 cntrl;
    void Update()
    {
        cntrl = new Vector2(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

        if (cntrl.x != 0)
        {
            sr.flipX = cntrl.x < 0;
        }

        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.I) && tiempoSiguienteAtaque <= 0)
        {
            anim.SetTrigger("SendPunch");
            tiempoSiguienteAtaque = tiempoEntreAtaque;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            CameraShake cameraShake = FindObjectOfType<CameraShake>();

            if (cameraShake != null)
            {
                cameraShake.StartShake(0.3f, 2f, 2f);
                Debug.Log("✅ StartShake() ejecutado correctamente.");
            }
            else
            {
                Debug.LogError("❌ No se encontró CameraShake en la Main Camera.");
            }
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Ataque 2 0")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Ataque 3 0")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            anim.SetBool("IsWalking", cntrl.magnitude != 0);
            rb.velocity = new Vector2(cntrl.x * horizontalSpeed, cntrl.y * verticalSpeed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public void AtaqueGolpe()
    {
        if (AtaquePunch == true)
        {
            BoxAtaque.SetActive(true);
            AtaquePunch = false;
        }
        else if (AtaquePunch == false)
        {
            BoxAtaque.SetActive(false);
            AtaquePunch = true;
        }
    }
}
