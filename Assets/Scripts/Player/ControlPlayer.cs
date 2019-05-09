using UnityEngine;
using UnityEngine.UI;

public class ControlPlayer : MonoBehaviour
{
    public float         speed;
    public GameObject    bullet;
    public GameObject    bulletBonus;
    public Transform     spawnPoint;
    public Text          hpText;
    public Text          bonusCountText;
    public Slider        hpSlider;
    public GameObject    normalBulletUi;
    public GameObject    bonusBulletUi;
    public AudioClip     Fire;
    public AudioSource   AudioSource1;

    public AudioClip     Damage;
    public AudioSource   AudioDamage;
    public AudioClip     Dead;
    public AudioSource   AudioDead;
    public AudioClip     Powerup;
    public AudioSource   AudioPowerup;

    private int          _hp = 100;
    private float        _runSpeed = 0.5f;
    private int          _bonusCount;
    private Camera       _mainCamera;
    private Animator     _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _mainCamera = Camera.main;
        // _mainCamera = FindObjectOfType<Camera>();
        speed = 5f;
        AudioSource1.clip = Fire;
        AudioDamage.clip = Damage;
        AudioDead.clip = Dead;
        AudioPowerup.clip = Powerup;
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("GunAim", true);
        _anim.SetFloat("WalkSpeed", 0);

        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if(ground.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
    
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W)) {
                 _anim.SetFloat("WalkSpeed", _runSpeed);
                 transform.position = Vector3.MoveTowards(transform.position, pointToLook, speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
                _anim.SetFloat("WalkSpeed", _runSpeed);
                transform.position = Vector3.MoveTowards(transform.position, pointToLook, -speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A)) {
                _anim.SetFloat("WalkSpeed", _runSpeed);
                transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                _anim.SetFloat("WalkSpeed", _runSpeed);
                transform.Translate(Vector3.right * Time.deltaTime * speed, Space.Self);
            }
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            _anim.SetTrigger("Jumping");
            transform.Translate(Vector3.up * 20 * Time.deltaTime, Space.World);
            _anim.SetTrigger("Landing");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _runSpeed = 1.0f;
            speed = 10.0f;
        }
        else
        {
            _runSpeed = 0.5f;
            speed = 5.0f;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (_bonusCount > 0)
            {
                AudioSource1.Play();
                AudioSource1.Play();
                AudioSource1.Play();
                Instantiate(bulletBonus, spawnPoint.position, spawnPoint.rotation);
                _bonusCount--;
                bonusCountText.text = _bonusCount.ToString();
            }
            else
            {
                AudioSource1.Play();
                Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);                
            }
        }

        if (_bonusCount == 0)
        {
            bonusBulletUi.SetActive(false);
            normalBulletUi.SetActive(true);
        }

    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("health"))
        {
            AudioPowerup.Play();
            ChangeHp(100);
            Destroy(collision.transform.gameObject);
        }

        if (collision.gameObject.CompareTag("bullet"))
        {
            AudioPowerup.Play();
            bonusBulletUi.SetActive(true);
            normalBulletUi.SetActive(false);
            _bonusCount = 10;
            Destroy(collision.transform.gameObject);
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            ChangeHp(_hp -25);
            if (_hp <= 0)
            {
                AudioDead.Play();

                Debug.Log("Player is dead.");
                UnityEngine.SceneManagement.SceneManager.LoadScene(CONSTANTS.GLOBAL.SCENES.GAME_OVER);
            }
            else
                AudioDamage.Play();

        }
        if (collision.gameObject.CompareTag("MinionSlap"))
        {
            ChangeHp(_hp - 10);
            if (_hp <= 0)
            {
                AudioDead.Play();

                Debug.Log("Player is dead.");
                UnityEngine.SceneManagement.SceneManager.LoadScene(CONSTANTS.GLOBAL.SCENES.GAME_OVER);
            }
            else
                AudioDamage.Play();

        }
    }
    private void ChangeHp(int hp)
    {
        _hp = hp;
        hpSlider.value = _hp;
        hpText.text = _hp + " HP";
    }
}

