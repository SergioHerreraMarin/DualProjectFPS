using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerHUD : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private Image healthBar;
    private GameObject target;
    private PlayerHealth healthRef;
    private int layerOtherUI;
    private float fillAmount;

    private void Awake() { //Al ser instanciado, el prefab de HUD se hará hijo del canvas. 
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    private void Start() {

        layerOtherUI = LayerMask.NameToLayer("OtherUI");

        if(!target.GetComponent<PhotonView>().IsMine){
            // this.gameObject.layer = layerOtherUI;
            // int childCount = gameObject.transform.childCount;

            // for(int i = 0; i < childCount; i++)
            // {
            //     Transform child = gameObject.transform.GetChild(i);
            //     child.gameObject.layer = layerOtherUI;
            // }
            gameObject.SetActive(false);
        }
    }

    public void setTarget(GameObject target){
        this.target = target;
        healthRef = this.target.GetComponent<PlayerHealth>();
        if(this.target != null)
        {
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
        }else
        {
            UpdateHealth(); //Chapuza
        }
    }

    private void UpdateHealth()
    {
        fillAmount = this.healthRef.GetHealth() / this.healthRef.GetMaxHealth();
        this.healthBar.fillAmount = fillAmount;
        Debug.Log("Fill amount health: " + this.healthBar.fillAmount + ", Player health: " + this.healthRef.GetHealth());
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
            
        if(stream.IsWriting){
            //Si el objeto stream se está escribiendo, enviamos el valor de la variable a través de la red. 
            stream.SendNext(fillAmount); 
        }
        else{
            //Si el objeto se está leyendo, recibimos el valor de la variable que se envió a través de la red. 
            fillAmount = (float)stream.ReceiveNext();
        }
    }
        

}
