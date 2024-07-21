using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{
    public GameObject Menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressionarBotao()
    {
        EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().fontSize = 30;
    }

    public void LiberarBotao()
    {
        EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
        EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().fontSize = 24;
    }

    public void SaveBotao()
    {

    }

    public void LoadBotao()
    {

    }

    public void StartBotao()
    {

    }

    public void QuitBotao()
    {

    }

    public void SaveSlot1()
    {

    }

    public void SaveSlot2()
    {

    }

    public void SaveSlot3()
    {

    }

    public void LoadSlot1()
    {

    }

    public void LoadSlot2()
    {

    }

    public void LoadSlot3()
    {

    }

    public void Save()
    {

    }

    public void Load()
    {

    }
}
