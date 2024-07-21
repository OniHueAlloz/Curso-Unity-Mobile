using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoMaisUm : MonoBehaviour
{
    private PlayerScript PlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = FindObjectOfType<PlayerScript>();

        if (PlayerScript.Sentido == Vector2.right)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(100,200), Random.Range(300,400)));
        }
        else 
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100,-200), Random.Range(300,400)));
        }

        PlayerScript.Moedas++;
        PlayerScript.DisplayMoedas.text = PlayerScript.Moedas.ToString();
        Destroy(gameObject, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
