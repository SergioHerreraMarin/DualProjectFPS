using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using DG.Tweening;
using TMPro;

public class PlayerHUD : MonoBehaviourPun
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image screenBloodVFX;
    [SerializeField] private GameObject finishGamePanel;
    [SerializeField] private float screenBloodSpeed;
    [SerializeField] private Button leaveGameButton;
    [SerializeField] private TextMeshProUGUI roundLabel;
    [SerializeField] private TextMeshProUGUI masterClientPointsLabel;
    [SerializeField] private TextMeshProUGUI secondClientPointsLabel;
    private GameObject target;
    private PlayerHealth healthRef;
    private GameManager gameManagerRef;
    private int layerOtherUI;
    private float fillAmount;
    private Tween screenBloodTween;

    private void Awake() { //Al ser instanciado, el prefab de HUD se hará hijo del canvas. 
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        leaveGameButton.onClick.AddListener(() => LeaveRoom());
    }

    private void Start() {

        if(!target.GetComponent<PhotonView>().IsMine)
        {
            gameObject.SetActive(false);
        }

        DOTween.Init();
        layerOtherUI = LayerMask.NameToLayer("OtherUI");
        screenBloodVFX.color = new Color(screenBloodVFX.color.r,screenBloodVFX.color.g,screenBloodVFX.color.b,0);
        finishGamePanel.SetActive(false);

        if(PhotonNetwork.IsConnected)
        {
            gameManagerRef = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            if(gameManagerRef != null)
            {   
                gameManagerRef.ChangeRoundEvent += UpdateRoundLabel;
                gameManagerRef.FinishGameEvent += OpenFinishGamePanel;
                gameManagerRef.UpdateMasterClientPointsEvent += UpdateMasterClientPointsLabel;
                gameManagerRef.UpdateSecondClientPointsEvent += UpdateSecondClientPointsLabel;
                UpdateRoundLabel();
            }else
            {
                Debug.LogError("No se encontró el game manager.");
            }

            InitializeFinishGamePanelLabels();
        }
        
    }

    public void setTarget(GameObject target){
        this.target = target;
        
        if(this.target != null)
        {   
            healthRef = this.target.GetComponent<PlayerHealth>();
            //Suscripción a eventos
            healthRef.HealthUpdateEvent += StartScreenBlood;
            healthRef.HealthUpdateEvent += UpdateHealth;
            Debug.Log("Target HUD added");
        }
    }


    public void SetHealthBarValue(float health){
        fillAmount = health / 100; //Contando que el máximo de vida sea 100 
        healthBar.fillAmount = fillAmount;
    }

    private void Update() {

        if(this.target == null)
        {
            Destroy(this.gameObject);
        }
    }


    private void UpdateRoundLabel()
    {
        roundLabel.text = "ROUND " + gameManagerRef.GetCurrentRound() + "/" + gameManagerRef.GetRounds();
    }
    
    public void UpdateMasterClientPointsLabel()
    {   
        ExitGames.Client.Photon.Hashtable properties = PhotonNetwork.CurrentRoom.CustomProperties;
        
        Debug.Log("Update master client");
        masterClientPointsLabel.text = PhotonNetwork.NickName + ": " + gameManagerRef.GetPlayerPoints(); 
    }

    public void UpdateSecondClientPointsLabel()
    {
        Debug.Log("Update second client");
        secondClientPointsLabel.text = PhotonNetwork.NickName + ": " + gameManagerRef.GetPlayerPoints(); 
    }

    [PunRPC]
    private void UpdateHealth()
    {
        fillAmount = this.healthRef.GetHealth() / this.healthRef.GetMaxHealth();
        this.healthBar.fillAmount = fillAmount;
        Debug.Log("Fill amount health: " + this.healthBar.fillAmount + ", Player health: " + this.healthRef.GetHealth());
    }

    [PunRPC]
    public void StartScreenBlood()
    {
        screenBloodVFX.color = new Color(screenBloodVFX.color.r,screenBloodVFX.color.g,screenBloodVFX.color.b,1);
        screenBloodVFX.DOFade(0, screenBloodSpeed).Restart();
    }

    public void OpenFinishGamePanel()
    {
        Cursor.visible = true;
        finishGamePanel.SetActive(true);
    }


    private void InitializeFinishGamePanelLabels()
    {
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if(PhotonNetwork.PlayerList[i].IsMasterClient)
            {
                masterClientPointsLabel.text = PhotonNetwork.PlayerList[i].NickName + ": " + gameManagerRef.GetPlayerPoints();
                Debug.Log("Master client: " + PhotonNetwork.PlayerList[i].NickName);
            }else
            {
                secondClientPointsLabel.text = PhotonNetwork.PlayerList[i].NickName + ": " + gameManagerRef.GetPlayerPoints();
                Debug.Log("Second client: " + PhotonNetwork.PlayerList[i].NickName);
            }
        }
    }


    //Sale de la sala actual y vuelve al MasterServer en donde se puede volver a crear o unir a salas.
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    private void OnDestroy() 
    {
        healthRef.HealthUpdateEvent -= StartScreenBlood;
        healthRef.HealthUpdateEvent -= UpdateHealth;
        gameManagerRef.FinishGameEvent -= OpenFinishGamePanel;
        gameManagerRef.ChangeRoundEvent -= UpdateRoundLabel;
        gameManagerRef.UpdateMasterClientPointsEvent -= UpdateMasterClientPointsLabel;
        gameManagerRef.UpdateSecondClientPointsEvent -= UpdateSecondClientPointsLabel;
    }

}
