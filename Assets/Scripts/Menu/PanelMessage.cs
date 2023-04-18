using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelMessage : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button buttonAccept;
    private MenuMediator mediator;

    private void Awake(){
        canvas.sortingOrder = 1;
        buttonAccept.onClick.AddListener(Accept);
        messageText.text = "";
    }

    public void Configure(MenuMediator mediator){
        this.mediator = mediator;
    }

    public void SetMessage(string message){
        if(message.Length < 100){
            messageText.fontSize = 64;
        }else if(message.Length < 200){
            messageText.fontSize = 48;
        }else if(message.Length < 300){
            messageText.fontSize = 32;
        }else{
            messageText.fontSize = 24;
        }

        messageText.text = message;
    }

    private void Accept(){
        mediator.hideMessagePanel();
    }

    public void Show(){
        // Debug.Log("PanelMessage: Show");
        canvas.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Show(string message){
        // Debug.Log("PanelMessage: Show");
        canvas.gameObject.SetActive(true);
        gameObject.SetActive(true);
        this.SetMessage(message);
    }

    public void Hide(){
        // Debug.Log("PanelMessage: Hide");
        canvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

}
