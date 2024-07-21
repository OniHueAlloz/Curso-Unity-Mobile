using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BauScript : MonoBehaviour
{
    public GameObject Moeda;
    public bool BauAberto; 
    private Animator BauAnimator;

    // Start is called before the first frame update
    void Start()
    {
        BauAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interagir()
    {
        BauAnimator.SetTrigger("Ativar");
        BauAberto = true;
    }

    IEnumerator GerarMoedas()
    {
        int TotalMoedas = Random.Range(16,32);
        for (int i = 0; i < TotalMoedas; i++)
        {
            GameObject MoedaTemp = Instantiate(Moeda, transform.position, transform.rotation);
            if (Random.Range (1,50) < 26)
            {
                MoedaTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50,-20), Random.Range(200,300)));
            }
            else 
            {
                MoedaTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(20,50), Random.Range(200,300)));
            }
            yield return new WaitForSeconds (0.1f);
        }
    }

    public void RetirarColisor()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
    }

    public void ReativarColisor()
    {
        GetComponent<Collider2D>().enabled = true;
    }

}
