using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    Transform BottomLeft;
    [SerializeField]
    Transform TopRight;
    Vector2 bottomLeftpos;
    Vector2 topRightpos;

    [SerializeField]
    bool colorPaletteOverride;
    [SerializeField]
    bool useDiscreteAlphas;
    [SerializeField]
    bool useDiscreteScaling;
    [SerializeField]
    bool useDiscreteSpeeds;
    [Tooltip("Continuous Values")]
    [SerializeField]
    Color minAlpha;
    [SerializeField]
    Color maxAlpha;
    [SerializeField]
    float minSize;
    [SerializeField]
    float maxSize;
    [SerializeField]
    float minSpeed;
    [SerializeField]
    float maxSpeed;

    [Tooltip("Discrete Values")]
    [SerializeField]
    Color[]     colorPalette;
    [SerializeField]
    Color[]     alphaPalette;
    [SerializeField]
    Vector2[]   sizePalette;
    [SerializeField]
    float[]     speedPalette;


    [SerializeField]
    float frequency;
    [SerializeField]
    float probability;
    [SerializeField]
    GameObject bubbleInstance;
    void Start()
    {
        bottomLeftpos = BottomLeft.transform.position;
        topRightpos = TopRight.transform.position;
        StartCoroutine(updateLoop());
    }
    IEnumerator updateLoop()
    {
        while (true) { 
            yield return new WaitForSeconds(frequency);
            if(Random.Range(0.0f,1.0f) > probability)
            {
                //spawn shi
                GameObject spawnobj = Instantiate(bubbleInstance);
                float alpha = 0;
                if (useDiscreteAlphas)
                {
                    alpha = alphaPalette[Random.Range(0, alphaPalette.Length - 1)].a;
                }
                else
                {
                    alpha = Random.Range(minAlpha.a, maxAlpha.a);
                }
                float speed = 1.0f;
                if (useDiscreteSpeeds)
                {
                    speed = speedPalette[Random.Range(0, speedPalette.Length - 1)];
                }
                else
                {
                    speed = Random.Range(minSpeed,maxSpeed);
                }
                Vector2 size = Vector2.one;
                if (useDiscreteScaling)
                {
                    size = sizePalette[Random.Range(0, sizePalette.Length - 1)];
                }
                else
                {
                    size *= Random.Range(minSize,maxSize);
                }
                Vector2 position = new Vector2(Random.Range(bottomLeftpos.x, topRightpos.x), Random.Range(bottomLeftpos.y, topRightpos.y));
                Color color = colorPalette[Random.Range(0, colorPalette.Length-1)];
                spawnobj.transform.position = position;
                spawnobj.transform.localScale = new Vector3(size.x, size.y, spawnobj.transform.localScale.z);
                spawnobj.GetComponent<Animator>().speed = speed;
                spawnobj.GetComponent<SpriteRenderer>().color = new Color(color.r,color.g,color.b,alpha);
            }
            //check to spawn
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
