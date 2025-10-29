using UnityEngine;

public class seguimientoCosmetico : MonoBehaviour
{
    public Transform player; 
    private Vector3 offset;  
    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        
        transform.position = player.position + offset;

       
        transform.rotation = Quaternion.identity;
       
    }
}