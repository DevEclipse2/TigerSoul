using UnityEngine;

public class BoardMortar : MonoBehaviour
{
    Collider2D Exitcollider;
    public GameObject player;
    Animator animator;
    public GameObject animator2;
    public GameObject camera;
    public GameObject sceneloader;
    LevelTransition leveltrans;
    public bool load;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leveltrans = sceneloader.GetComponent<LevelTransition>();
        animator = GetComponent<Animator>();
        Exitcollider = GetComponent<Collider2D>();        
    }
    void Update()
    {
        if (load)
        {
            Debug.Log("load");
            leveltrans.LoadScene(1);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(player.gameObject.GetComponent<Rigidbody2D>());
            Destroy(player.GetComponent<PlayerMove>());
            animator.SetBool("Play" , true);
            animator2.GetComponent<SimpleAnimation>().ovveride = true;
            player.SetActive(false);
            camera.transform.position = Vector2.zero;
            camera.GetComponent<CameraBounds>().enabled = false;
        }
    }

}
