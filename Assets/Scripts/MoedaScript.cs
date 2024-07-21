using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoedaScript : MonoBehaviour
{
    public GameObject TextoMaisUm;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D ObjetoQueColidiu)
    {
        if (ObjetoQueColidiu.CompareTag("Player"))
        {
            GameObject TextoMaisUmTemp = Instantiate(TextoMaisUm, transform.position, transform.rotation);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

}
