using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SimpleMovement : MonoBehaviour
{
    public Transform Object;
    public Rigidbody2D RB2D;
    public GameObject GameOverCanvas;
    public TextMeshProUGUI Text;
    public AudioSource Source;
    public AudioClip Jump;
    public AudioClip GameOver;
    public AudioClip GameCompleted;
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    public int TotalLife;
    public float Speed;
    public float Force;
    public bool Over = false;
    private Vector3 _initialPosition;
    void Start()
    {
       // Object = GetComponent<Transform>();
        RB2D = GetComponent<Rigidbody2D>();
        _initialPosition = Object.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Over"))
        {
            Object.position = _initialPosition;
            if (TotalLife > 1)
            {
                if (TotalLife == 3)
                {
                    Life1.gameObject.SetActive(false);
                }else if (TotalLife == 2)
                {
                    Life2.gameObject.SetActive(false);
                }
                TotalLife--;
                Source.clip = GameOver;
                Source.Play();
                return;
            }
            Life3.gameObject.SetActive(false);
            Over = true;
            GameOverCanvas.SetActive(true);
            Text.text = "You Lost";
            Source.clip = GameOver;
            Source.Play();
            StartCoroutine(Restart());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameOverCanvas.SetActive(true);
            Over = true;
            Source.clip = GameCompleted;
            Source.Play();
            StartCoroutine(Restart());
            
        }

    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);
        for(int i = 3; i > 0; i--)
        {
            Text.text = "Restarting in " + i;
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Over) return;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 pos = Object.position;
            pos.x += Speed * Time.deltaTime;
            Object.position = pos;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 pos = Object.position;
            pos.x -= Speed * Time.deltaTime;
            Object.position = pos;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Source.clip = Jump;
            Source.Play();
            RB2D.AddForce(new Vector2(0, Force));
        }
    }
}
