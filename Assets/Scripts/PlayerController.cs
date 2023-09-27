using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float power = 10f;
    private Rigidbody2D rb;

    [SerializeField ] private Vector2 minPower;
    [SerializeField] private Vector2 maxPower;
    [SerializeField] private GameObject rayLine;

    Camera cam;
    private Vector2 force;
    private Vector2 startPoint;
    private Vector2 direcPoint;
    private Vector2 endPoint;
    private bool isHolding;
    private TrajectoryLine tl;
    //Vector2 rotate;

    void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        isHolding = false;
        tl = GetComponent<TrajectoryLine>();
    }

    // Update is called once per frame
    void Update()
    {
        
        DragAndShoot();
        RotatePlayer();
    }
    void RotatePlayer()
    {
        direcPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        if (direcPoint != startPoint && isHolding == true)
        {
            Vector2 direction = direcPoint - startPoint;
            // goc quay
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle + 90f);
            transform.rotation = rotation;
        }
    }
    void DragAndShoot()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() )
        {
            ScreensManager.Instance.ChangeScreen(Screens.IN_GAME);
            GetStartPointShoot();
        }

        if (Input.GetMouseButton(0) && ScreensManager.Instance.currentScreen == ScreensManager.Instance.gamePanel)
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            tl.RenderLine(startPoint, currentPoint);
        }

        if (Input.GetMouseButtonUp(0) && ScreensManager.Instance.currentScreen == ScreensManager.Instance.gamePanel)
        {
            AudioClip shoot = SoundsManager.Instance.shoot;
            SoundsManager.Instance.audioSource.PlayOneShot(shoot);
            Shoot();
        }


    }
    
    void GetStartPointShoot()
    {
        /*setup khi người chơi bắt đầu bấm vào màn hình*/

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        isHolding = true;
        rayLine.SetActive(true);
        SlowMotion(); 
    }
    void Shoot()
    {

        /* giới hạn  lực kéo của player, lấy hướng vector để đẩy nhân vật */
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Vector3.Distance(startPoint, endPoint) > 8f)
        {
            Vector2 direc = endPoint - startPoint;
            endPoint  = startPoint + (direc.normalized * 8f);
        }
        

        force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
        //Debug.LogWarning("Force: " + force);
        rb.AddForce(force* power, ForceMode2D.Impulse);
        //Debug.LogWarning("Total Force:" + force* power);
        isHolding = false;
        rayLine.SetActive(false);
        Time.timeScale = 1f;

        tl.GetComponent<LineRenderer>().positionCount = 0;


        //rotate = startPoint - endPoint;
        //transform.rotation = Quaternion.LookRotation(rotate);

        //Debug.Log(startPoint);
        //Debug.Log(endPoint);
        ////Debug.Log(rotate);
    }
    void SlowMotion()
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = Time.timeScale * 0.01f;
    }


}
