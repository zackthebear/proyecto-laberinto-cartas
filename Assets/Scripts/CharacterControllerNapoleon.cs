using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterControllerNapoleon : MonoBehaviour
{
    public float speed;
    public float hp;
    public float str;
    public float def;

    private float tempSpeed;
    private float tempSTR;
    private float tempDEF;

    private CharacterController controller;
    public float smoothing = 0.1f;
    private float turnSmooth;

    public float gravity = -9.8f;

    Vector3 velocity;

    public float jumpHeight;

    public Transform groundCheck;
    public float radius;
    public LayerMask groundMask;
    bool isgrounded;

    private List<Cartas> cartasCharacter;
    public Canvas miCanvas;
    public Slider lifeBar;
    public Text textSTR;
    public Text textDEF;
    public Text textSpeed;

    private int currentIndexCard = 1;

    private float nextActivateCard = 0.0f;
    private bool isBuffActivated = false;

    public Transform shootPosition;
    public GameObject bulletPrefab;

    public Light worldLight;
    public Light playerLight;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cartasCharacter = new List<Cartas>();
        speed = 5f;
        hp = 100f;
        str = 5f;
        def = 5f;
    }

    private void Update()
    {
        movement();
        cardManager();
        shoot();
        //isgrounded = Physics.CheckSphere(groundCheck.position, radius, groundMask);

        //if(isgrounded && velocity.y < 0)
        //{
        //    velocity.y = 0;
        //}


        //Vector3 direction = new Vector3(horizontal,0,vertical).normalized;

        //if(direction.magnitude >= .1f)
        //{
        //    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooth, smoothing);
        //    transform.rotation = Quaternion.Euler(0, angle, 0);

        //    controller.Move(direction * speed * Time.deltaTime);
        //}

        //if(Input.GetKeyDown(KeyCode.Space) && isgrounded)
        //{
        //    velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        //}

        //velocity.y += gravity * Time.deltaTime;

        //controller.Move(velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(hp <= 0)
        {
            SceneManager.LoadScene(3);
        }
        lifeBar.value = (hp / 100);
        textSTR.text = "STR: " + str.ToString();
        textDEF.text = "DEF: " + def.ToString();
        textSpeed.text = "SPEED: " + speed.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("CardArea"))
        {
            Cartas statsCarta = other.gameObject.GetComponent<Carta>().Stats;
            cartasCharacter.Add(statsCarta);
            if (cartasCharacter.Count == 1)
            {
                miCanvas.GetComponentInChildren<SpriteRenderer>().sprite = GetSpriteFromTexture2D(statsCarta.imagen);
            }
        }

        if (other.tag.Equals("Enemy"))
        {
            hp -= (10 - def);
            Vector3 knockbackVector = (transform.position - other.gameObject.transform.position);
            Vector3 force = new Vector3(knockbackVector.x * 100, 0, knockbackVector.z * 100);

            controller.Move(force * Time.deltaTime);
        }

        if (other.tag.Equals("Exit"))
        {
            //Go to Win
            SceneManager.LoadScene(2);
        }
    }

    private Sprite GetSpriteFromTexture2D(Texture2D image)
    {
        return Sprite.Create(image, new Rect(0, 0, 797, 1250), Vector2.zero);
    }

    private void movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 directionFacing = new Vector3(horizontal, 0, vertical).normalized;

        if (directionFacing.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(directionFacing.x, directionFacing.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooth, smoothing);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            controller.Move(directionFacing * speed * Time.deltaTime);
        }
    }
    private void cardManager()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            currentIndexCard++;
            if (currentIndexCard > cartasCharacter.Count)
                currentIndexCard = 1;
            miCanvas.GetComponentInChildren<SpriteRenderer>().sprite = GetSpriteFromTexture2D(cartasCharacter[currentIndexCard - 1].imagen);
        }

        if (Input.GetKeyDown(KeyCode.X) && Time.time > nextActivateCard)
        {
            nextActivateCard = Time.time + 10.0f;
            currentIndexCard = 1;
            tempSpeed = speed;
            tempSTR = str;
            tempDEF = def;
            Cartas cartas = cartasCharacter[currentIndexCard - 1];
            if (cartas.name == "Aracnofobia")
            {

            }
            else if (cartas.name == "Sol")
            {
                worldLight.intensity = 1.83f;
            }
            else if (cartas.name == "Oscuridad")
            {
                worldLight.intensity = 0.10f;
                playerLight.intensity = 0.10f;
            }
            else
            {
                speed += cartas.SPEED;
                hp += cartas.HP;
                str += cartas.STR;
                def += cartas.DEF;
            }
            cartasCharacter.RemoveAt(currentIndexCard - 1);
            currentIndexCard--;
            isBuffActivated = true;
            if (cartasCharacter.Count > 0)
                miCanvas.GetComponentInChildren<SpriteRenderer>().sprite = GetSpriteFromTexture2D(cartasCharacter[currentIndexCard - 1].imagen);
            else
                miCanvas.GetComponentInChildren<SpriteRenderer>().sprite = null;

        }

        if (isBuffActivated && Time.time > nextActivateCard)
        {
            speed = tempSpeed;
            str = tempSTR;
            def = tempDEF;
            isBuffActivated = false;
            playerLight.intensity = 1.2f;
            worldLight.intensity = 0.45f;

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            hp -= 10;
            Debug.Log(hp);
        }
    }

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject myBullet = Instantiate(bulletPrefab, shootPosition.position, shootPosition.rotation);
            myBullet.GetComponent<Rigidbody>().AddForce(shootPosition.forward * 2000);

            Destroy(myBullet, 3f);
        }
    }
}
