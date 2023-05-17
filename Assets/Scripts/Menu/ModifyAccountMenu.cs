using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModifyAccountMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button modifyButton;
    [SerializeField] private Button enableNameButton;
    [SerializeField] private Button enablePasswordButton;
    [SerializeField] private Button deleteAccountButton;
    [SerializeField] private TMP_InputField newNameInput;
    [SerializeField] private TMP_InputField oldPasswordInput;
    [SerializeField] private TMP_InputField newPasswordInput;
    [SerializeField] private TMP_InputField newPasswordRepeatInput;
    private MenuMediator mediator;

    private string newUserName = "";
    private string oldPassword = "";
    private string newUserPassword = "";
    private string newUserPasswordRepeat = "";
    private bool modifyingName = false;
    private bool modifyingPassword = false;

    private bool fromRanking = false;

    private void Awake(){
        backButton.onClick.AddListener(() => getBack());
        modifyButton.onClick.AddListener(() => confirmModify());
        enableNameButton.onClick.AddListener(() => buttonNamePressed());
        enablePasswordButton.onClick.AddListener(() => buttonPasswordPressed());
        deleteAccountButton.onClick.AddListener(() => deleteAccount());
        newNameInput.text = MenuMediator.GetCurrentUser().GetUserName();
        DisableNameModification();
        DisablePasswordModification();
    }

    private void getBack(){
        if(fromRanking){
            mediator.OpenRankingMenu();
        }else{
            mediator.OpenProfileMenu();
        }
    }

    private void confirmModify(){
        string oldName = MenuMediator.GetCurrentUser().GetUserName();
        Debug.Log("ModifyAccountMenu: ConfirmModify");
        newUserName = MenuMediator.GetCurrentUser().GetUserName();
        newUserPassword = MenuMediator.GetCurrentUser().GetUserPassword();
        if(oldPasswordInput.text == MenuMediator.GetCurrentUser().GetUserPassword()){
            if(modifyingName){
                newUserName = newNameInput.text;
            }
            if(modifyingPassword){
                if(newPasswordInput.text == newPasswordRepeatInput.text){
                    newUserPassword = newPasswordInput.text;
                }else{
                    mediator.ShowMessagePanel("the new passwords don't match");
                    return;
                }
            }
            mediator.ModifyAccount(newUserName, newUserPassword, oldName);

            newUserName = "";
            newUserPassword = "";
        }else{
            if(modifyingPassword || modifyingName)
            {
                mediator.ShowMessagePanel("Current password incorrect");
            }else{
                mediator.ShowMessagePanel("No field seleted to modify");
            }
            
        }
    }

    private void deleteAccount()
    {
        mediator.DeleteAccount();
    }

    private void buttonNamePressed(){
        if(modifyingName){
            DisableNameModification();
        }else{
            EnableNameModification();
        }
    }

    private void buttonPasswordPressed(){
        if(modifyingPassword){
            DisablePasswordModification();
        }else{
            EnablePasswordModification();
        }
    }

    private void EnableNameModification(){
        modifyingName = true;
        oldPasswordInput.interactable = true;
        newNameInput.interactable = true;
    }

    private void EnablePasswordModification(){
        modifyingPassword = true;
        oldPasswordInput.interactable = true;
        newPasswordInput.interactable = true;
        newPasswordRepeatInput.interactable = true;
    }

    private void DisableNameModification(){
        modifyingName = false;
        if(!modifyingPassword && !modifyingName){
            oldPasswordInput.interactable = false;
        }
        newNameInput.interactable = false;
    }

    private void DisablePasswordModification(){
        modifyingPassword = false;
        if(!modifyingPassword && !modifyingName){
            oldPasswordInput.interactable = false;
        }
        newPasswordInput.interactable = false;
        newPasswordRepeatInput.interactable = false;
    }

    public void Configure(MenuMediator mediator)
    {
        this.mediator = mediator;
        Debug.Log("LoginMenu: Configure");
    }

    public void Show(){
        // Debug.Log("Modify Account Menu: Show");
        enableButtons();
        newNameInput.text = MenuMediator.GetCurrentUser().GetUserName();
        gameObject.SetActive(true);
    }

    public void Show(bool fromRanking){
        // Debug.Log("Modify Account Menu: Show");
        this.fromRanking = fromRanking;
        enableButtons();
        newNameInput.text = MenuMediator.GetCurrentUser().GetUserName();
        gameObject.SetActive(true);
    }

    public void Hide(){
        // Debug.Log("Modify Account Menu: Hide");
        oldPasswordInput.text = "";
        newPasswordInput.text = "";
        newPasswordRepeatInput.text = "";
        gameObject.SetActive(false);
    }

    public void enableButtons(){
        // Debug.Log("Modify Account Menu: Enable Buttons");
        backButton.interactable = true;
        modifyButton.interactable = true;
        enableNameButton.interactable = true;
        enablePasswordButton.interactable = true;
        deleteAccountButton.interactable = true;
    }

    public void disableButtons(){
        // Debug.Log("Modify Account Menu: Disable Buttons");
        backButton.interactable = false;
        modifyButton.interactable = false;
        enableNameButton.interactable = false;
        enablePasswordButton.interactable = false;
        deleteAccountButton.interactable = false;
    }

}
