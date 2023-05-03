using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayLength;  
    [SerializeField] private float rayUpPosition;  
    private Transform playerTransform;
    private Ray rayW, rayS, rayA, rayD;
    private bool canMoveW, canMoveS, canMoveA, canMoveD;

    public bool GetCanMoveW()
    {
        return this.canMoveW;
    }

    public bool GetCanMoveS()
    {
        return this.canMoveS;
    }

    public bool GetCanMoveA()
    {
        return this.canMoveA;
    }

    public bool GetCanMoveD()
    {
        return this.canMoveD;
    }


    private void Awake() {
        playerTransform = GetComponent<Transform>();
    }


    private void Update() {
        rayW = new Ray(playerTransform.position + (Vector3.up * rayUpPosition), playerTransform.forward);
        rayS = new Ray(playerTransform.position + (Vector3.up * rayUpPosition), playerTransform.forward * -1);
        rayA = new Ray(playerTransform.position + (Vector3.up * rayUpPosition), playerTransform.right * -1);
        rayD = new Ray(playerTransform.position + (Vector3.up * rayUpPosition), playerTransform.right);


        if(Physics.Raycast(rayW, rayLength, layerMask))
        {   
            canMoveW = false;
        }else{
            canMoveW = true;
        }

        if(Physics.Raycast(rayS, rayLength, layerMask))
        {
            canMoveS = false;
        }else{
            canMoveS = true;
        }
        if(Physics.Raycast(rayA, rayLength, layerMask))
        {
            canMoveA = false;
        }else{
            canMoveA = true;
        }

        if(Physics.Raycast(rayD, rayLength, layerMask))
        {
            canMoveD = false;
        }else{
            canMoveD = true;
        }


        Debug.DrawRay(rayW.origin, rayW.direction * rayLength, Color.red);
        Debug.DrawRay(rayS.origin, rayS.direction * rayLength, Color.red);
        Debug.DrawRay(rayA.origin, rayA.direction * rayLength, Color.red);
        Debug.DrawRay(rayD.origin, rayD.direction * rayLength, Color.red);
    }



}
