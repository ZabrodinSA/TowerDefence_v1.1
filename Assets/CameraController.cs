using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/CameraController")]
public class CameraController : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 1.0f;
    public float limitedViewX = 1.0f;
    public float limitedViewUp = -4.0f;
    public float limitedViewDown = 12.0f;
    private float positionY;
    private GameObject floor;
    private MeshFilter meshFloor;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        positionY = transform.position.y;
        floor = GameObject.FindGameObjectWithTag("Floor");
        meshFloor = floor.GetComponent<MeshFilter>();

    }
    void Update()
    {

        float deltaX = Input.GetAxis("Horizontal") * speed;  //обрабатываем перемещение и ограничеваем его по оси Y и краям карты
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        characterController.Move(movement);
        transform.position = new Vector3(transform.position.x, positionY, transform.position.z);
        
        
        if (transform.position.x > meshFloor.mesh.bounds.max.x+limitedViewX)
        {
            transform.position = new Vector3(meshFloor.mesh.bounds.max.x+limitedViewX, positionY, transform.position.z);
        } else if (transform.position.x < meshFloor.mesh.bounds.min.x-limitedViewX) {
            transform.position = new Vector3(meshFloor.mesh.bounds.min.x-limitedViewX, positionY, transform.position.z);
        }

        if (transform.position.z > meshFloor.mesh.bounds.max.z+limitedViewUp)
        {
            transform.position = new Vector3(transform.position.x, positionY, meshFloor.mesh.bounds.max.z+limitedViewUp);
        } else if (transform.position.z < meshFloor.mesh.bounds.min.z-limitedViewDown){
            transform.position = new Vector3(transform.position.x, positionY, meshFloor.mesh.bounds.min.z-limitedViewDown);
        }

    }
}