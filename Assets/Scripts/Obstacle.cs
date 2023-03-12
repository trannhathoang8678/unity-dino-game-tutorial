using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float leftEdge;
    private float rightEdge;
    private float downEdge;

    private void Start()
    {
        leftEdge = -11f;     //Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
        rightEdge = 11f;
        downEdge = 0f;
    }

    private void Update()
    {
        if (this.gameObject.tag == "Left")
        {        
            transform.position -= Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;
        }
        if (this.gameObject.tag == "Right")
        {
            transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;
        }
        if (this.gameObject.tag == "Meteor")
        {
            if (this.gameObject.name.Contains("Meteorite_01"))
            {
                transform.position += new Vector3(0f, -0.5f, 0) * GameManager.Instance.gameSpeed * Time.deltaTime;
            } else
            {
                transform.position += new Vector3(-0.5f, -0.5f, 0) * GameManager.Instance.gameSpeed * Time.deltaTime;
            }
            
        }
        if (this.gameObject.name.Contains("Bird_01"))
        {
            transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime * 0.5f;

        }

        if (transform.position.x < leftEdge || transform.position.x > rightEdge 
            || transform.position.y < downEdge) {
            Destroy(gameObject);
        }
    }

}
