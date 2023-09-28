using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float stopDistance;
    [SerializeField] float speedRotation;
    [SerializeField]  public float speed;
    [SerializeField]  public Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*nếu enemy xa player quá thì tăng tốc cho chúng  còn nếu không thì enemy speed = speed trong prefab*/
        if (Vector2.Distance(transform.position, target.position) > 15)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 10f * Time.deltaTime);

        }
        else if (Vector2.Distance(transform.position, target.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.Rotate(0f, 0f, speedRotation * Time.deltaTime);

        }

    }
    
}   
