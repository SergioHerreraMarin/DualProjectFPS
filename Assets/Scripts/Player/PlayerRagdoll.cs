using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    private Rigidbody[] ragdollRigs;
    [SerializeField] private Animator animator;

    private void Awake() {
        ragdollRigs = GetComponentsInChildren<Rigidbody>();
    }

    private void Start() {
        DesactiveRagdoll();
        if(ragdollRigs[0] != null){
            Debug.Log("Hola");
        }
    }


    public void DesactiveRagdoll(){
        animator.enabled = true;
        foreach(Rigidbody rig in ragdollRigs){
            rig.isKinematic = true;
        }
    }

    public void ActiveRagdoll(){
        animator.enabled = false;
        foreach(Rigidbody rig in ragdollRigs){
            rig.isKinematic = false;
        }

     
        
    }
}
