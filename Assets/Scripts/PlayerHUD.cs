using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using DG.Tweening;

public class PlayerHUD : MonoBehaviourPun
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image screenBloodVFX;
    [SerializeField] private float screenBloodSpeed;
    private GameObject target;
    private PlayerHealth healthRef;
    private int layerOtherUI;
    private float fillAmount;
    private Tween screenBloodTween;

    private void Awake() { //Al ser instanciado, el prefab de HUD se hará hijo del canvas. 
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    private void Start() {

        DOTween.Init();
        layerOtherUI = LayerMask.NameToLayer("OtherUI");
        screenBloodVFX.color = new Color(screenBloodVFX.color.r,screenBloodVFX.color.g,screenBloodVFX.color.b,0);

        if(!target.GetComponent<PhotonView>().IsMine){
            gameObject.SetActive(false);
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

    private void UpdateHealth()
    {
        fillAmount = this.healthRef.GetHealth() / this.healthRef.GetMaxHealth();
        this.healthBar.fillAmount = fillAmount;
        Debug.Log("Fill amount health: " + this.healthBar.fillAmount + ", Player health: " + this.healthRef.GetHealth());
    }


    public void StartScreenBlood()
    {
        screenBloodVFX.color = new Color(screenBloodVFX.color.r,screenBloodVFX.color.g,screenBloodVFX.color.b,1);
        screenBloodVFX.DOFade(0, screenBloodSpeed).Restart();
    }

    
    private void OnDestroy() {
        healthRef.HealthUpdateEvent -= StartScreenBlood;
        healthRef.HealthUpdateEvent -= UpdateHealth;
    }

}
