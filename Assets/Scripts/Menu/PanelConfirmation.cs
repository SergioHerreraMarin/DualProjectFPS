using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/* Clase para manejar este panel que servira tanto para pedir confirmacion
o rechazar, y para pedir algun input al usuario

Para la confirmacion tiene el booleano, cuando el mediator lo llame,
este mostrara el mensaje de este panel y si el usuario accepta o no
el booleano cambiara, asi el mediator sabra si el usuario ha aceptado o no
similar para el caso de que haga algun input de texto */
public class PanelConfirmation : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button buttonAccept;
    [SerializeField] private Button buttonReject;
    [SerializeField] private TMP_InputField confirmationInput;
    [SerializeField] private TextMeshProUGUI inputLabel;
    private MenuMediator mediator;
    private bool accepted = false;
    private string contentInput = "";

    private void Awake(){
        canvas.sortingOrder = 1;
        buttonAccept.onClick.AddListener(Accept);
        buttonReject.onClick.AddListener(Reject);
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
        accepted = true;
        if(confirmationInput.gameObject.activeSelf){
            contentInput = confirmationInput.text;
        }
        mediator.hideConfirmationPanel();
    }

    private void Reject(){
        accepted = false;
        mediator.hideConfirmationPanel();
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
        confirmationInput.gameObject.SetActive(false);
    }

    public void Show(string message, string labelContent){
        // Debug.Log("PanelMessage: Show");
        canvas.gameObject.SetActive(true);
        gameObject.SetActive(true);
        this.SetMessage(message);
        this.inputLabel.text = labelContent;
        confirmationInput.gameObject.SetActive(true);
    }

    public void Hide(){
        // Debug.Log("PanelMessage: Hide");
        canvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
        this.SetMessage("");
        this.inputLabel.text = "";
        confirmationInput.gameObject.SetActive(false);
    }

 
}
