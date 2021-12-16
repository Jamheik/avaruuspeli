using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Playerinputactions pia;
    CharacterController player;
    public GameObject plr;
    public Transform cam;
    float speed = 10;
    public Animator ar;
    Vector3 velocity;
    public Text ballcount;
    int ballamount;
    public GameObject ballprefab;

    public float gravity = -10f;
    public float jumpForce = 5;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        pia = new Playerinputactions();
        pia.Enable();
        Cursor.visible = false;
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        jump();
        Movement();
        shoot();
    }
    void Movement()
    {
        Vector2 move = pia.player.move.ReadValue<Vector2>();
        Vector3 moveDirection = transform.forward * move.y + transform.right * move.x;
        player.Move(moveDirection * Time.deltaTime * speed);

        if(pia.player.move.ReadValue<Vector2>() != Vector2.zero ||velocity.y < -1.2f)
        {
            ar.SetBool("walking", true);
            transform.GetComponent<AudioSource>().UnPause();
        }
        else
        {
            ar.SetBool("walking", false);
            transform.GetComponent<AudioSource>().Pause();
        }


    }
    void Gravity()
    {
        if (!player.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            player.Move(velocity * Time.deltaTime);
        }
        if (player.isGrounded)
        {
            velocity.y = 0;
        }

    }
    void jump()
    {
        if (pia.player.jump.ReadValue<float>() > 0 && player.isGrounded)
        {
            velocity.y += jumpForce;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ballcollective")
        {
            ballamount += 1;
            ballcount.text = ballamount.ToString();
            Destroy(other.gameObject);
        }

        if(other.tag == "endscene")
        {
            SceneManager.LoadScene(2);
        }
    }
    void shoot()
    {
        if (pia.player.Shoot.triggered && ballamount > 0)
        {
            Vector3 shootpos = cam.transform.position + cam.transform.forward * 0.5f;

            Vector3 shootDirection = cam.transform.forward;
            GameObject ball = Instantiate(ballprefab, shootpos, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(shootDirection * 500);

            ballamount -= 1;
            ballcount.text = ballamount.ToString();
        }
    }
}
