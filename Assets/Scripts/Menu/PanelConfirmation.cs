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
    private string contentInput = "";
    private bool confirmationSubmitted = false;

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

    public bool isSubmitted(){
        return this.confirmationSubmitted;
    }

    private void Accept(){
        Debug.Log("PanelConfirmation: Accept");
        contentInput ="";
        confirmationSubmitted = true;
        if(confirmationInput.gameObject.activeSelf){
            contentInput = confirmationInput.text;
            confirmationInput.text = "";
        }
        mediator.setConfirmationAccepted(true);
        mediator.hideConfirmationPanel();
    }

    private void Reject(){
        Debug.Log("PanelConfirmation: Reject");
        confirmationSubmitted = true;
        confirmationInput.text = "";
        mediator.setConfirmationAccepted(false);
        mediator.hideConfirmationPanel();
    }

    public void Show(bool askInput){
        Debug.Log("PanelConfirmation: Show");
        confirmationSubmitted = false;
        canvas.gameObject.SetActive(true);
        gameObject.SetActive(true);
        if(askInput){
            confirmationInput.gameObject.SetActive(true);
        }else{
            confirmationInput.gameObject.SetActive(false);
        }
        buttonAccept.gameObject.SetActive(true);
        buttonReject.gameObject.SetActive(true);
    }

    public void Show(bool askInput, string message){
        Debug.Log("PanelConfirmation: Show");
        confirmationSubmitted = false;
        canvas.gameObject.SetActive(true);
        gameObject.SetActive(true);
        this.SetMessage(message);
        if(askInput){
            confirmationInput.gameObject.SetActive(true);
        }else{
            confirmationInput.gameObject.SetActive(false);
        }
        buttonAccept.gameObject.SetActive(true);
        buttonReject.gameObject.SetActive(true);
    }

    public void Show(bool askInput, string message, string labelContent){
        Debug.Log("PanelConfirmation: Show");;
        confirmationSubmitted = false;
        canvas.gameObject.SetActive(true);
        gameObject.SetActive(true);
        this.SetMessage(message);
        this.inputLabel.text = labelContent;
        if(askInput){
            confirmationInput.gameObject.SetActive(true);
        }else{
            confirmationInput.gameObject.SetActive(false);
        }
    }

    public void Hide(){
        Debug.Log("PanelConfirmation: Hide");
        canvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
        this.SetMessage("");
        this.inputLabel.text = "";
        confirmationInput.gameObject.SetActive(false);
    }
}
 
