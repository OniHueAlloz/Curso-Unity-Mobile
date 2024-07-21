using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrilScript : MonoBehaviour
{

    public GameObject Moeda;
    public GameObject Barril;
    public int Contador;
    private PlayerScript PlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            if (PlayerScript.PlayerRigidbody.velocity.y > 0)
            {
                PlayerScript.PlayerRigidbody.AddForce(new Vector2(0, -300));

            }
            else
            {
                PlayerScript.PlayerRigidbody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            }

            Contador++;

            if (Contador > 5)
            {
                return;
            }

            GetComponent<Animator>().SetTrigger("Ativar");
            GetComponent<Animator>().SetInteger("Contador", Contador);

            StartCoroutine("GerarMoedas");
            
        }
    }

    IEnumerator GerarMoedas()
    {
        int TotalMoedas = Random.Range(8,16);
        for (int i = 0; i < TotalMoedas; i++)
        {
            GameObject MoedaTemp = Instantiate(Moeda, transform.position, transform.rotation);
            if (Random.Range (1,50) < 26)
            {
                MoedaTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50,-20), Random.Range(200,300)));
                PlayerScript.PlayerAnimator.SetTrigger("Jump");
            }
            else 
            {
                MoedaTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(20,50), Random.Range(200,300)));
            }
            yield return new WaitForSeconds (0.1f);
        }
    }

    void DesativarBarril()
    {
        Barril.SetActive(false);
    }

    void Interagir ()
    {
        StartCoroutine("DestruirBarril");
    }


    IEnumerator DestruirBarril()
    {
        yield return new WaitForSeconds(0.4f);
        GetComponent<Animator>().SetTrigger("Ativar");
        GetComponent<Animator>().SetInteger("Contador", 5);
    }
}
