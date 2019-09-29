using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float speed = 3;
    public float rotationSpeed = 90;
    private CharacterController _controller;
    public void Start()
    {
        _controller = GetComponent<CharacterController>();
       
        Transform current = transform;
        for (int i = 0; i < 3; i++)
        {
            Tail tail = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Tail>();
            tail.transform.position = current.transform.position - current.transform.forward * 2;
            tail.transform.rotation = transform.rotation;
            tail.target = current.transform;
            tail.targetDistance = 1;
            Destroy(tail.GetComponent<Collider>());
            current = tail.transform;
        }
    }
    private bool _testing = false;
    public void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0, rotationSpeed * Time.deltaTime * horizontal, 0);
        _controller.Move(transform.forward * speed * Time.deltaTime);
    }
    GameObject food;
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "Food")
        {
            food = GameObject.Find("Food");
            var pos = new Vector3(Random.Range(-20, 0), 0, Random.Range(-20, 0));
            food.transform.position = pos;
        }
        else
        {
            Application.LoadLevel("GameOver");
        }
    }
}