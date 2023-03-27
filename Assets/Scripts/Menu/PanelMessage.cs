using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        this.Hide();
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}
