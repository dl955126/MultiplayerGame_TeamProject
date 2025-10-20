using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour

{
    [SerializeField] public float moveSpeed = 5;
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0, v);

    }

    // Update is called once per frame
    void Update()
    {
        	
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * (moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * (moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * (moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += -Vector3.forward * (moveSpeed * Time.deltaTime);
        }
    }
}