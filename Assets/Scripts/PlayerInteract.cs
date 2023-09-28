using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    bool isdead = false;
    [SerializeField] GameObject spawnManager;
    [SerializeField] GameObject enemyTotal;
    [SerializeField] public Text scoreIngame;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        HightScore.Instance.ResetScore();
        scoreIngame.text = "Score :" +0;


    }

    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
        }

        if (col.gameObject.tag == "GrayColor")
        {
            EndGame();
        }
    
        if (col.gameObject.tag == "OtherColor")
        {
            KillEnemy(col);
            //Debug.Log(col.gameObject.tag);
    }

    
    void EndGame()
    {
        SoundsManager.Instance.PlayOneShotAudioDie();
        SoundsManager.Instance.audioSource.clip = null;

        //Ngừng spawn enemy và tắt component<EnemyFollow> để địch không đuổi theo người chơi nữa và dùng 1 lực đẩy player
        //về hướng ngược lại
        spawnManager.SetActive(false);
        ScreensManager.Instance.ChangeScreen(Screens.END_GAME);
        rb.AddForce((-transform.up - new Vector3(0, Random.Range(0.3f, 0.3f), 0)) * 10, ForceMode2D.Impulse);
        Camera.main.transform.GetComponent<CameraFollow>().enabled = false;
        for (int i = 0; i < enemyTotal.transform.childCount; i++)
        {
            GameObject enemy = enemyTotal.transform.GetChild(i).gameObject;
            enemy.GetComponent<EnemyFollow>().enabled = false;

        }
        isdead = true;

    }
    void KillEnemy(Collision2D col)
        {
            /*khi va chạm vào 1 cạnh có thể tấn công được(có màu sáng) thì addforce cho parent của nó và các cạnh cùng cấp của nó theo hướng trước mặt  của player, disable colider và destroy các object sau vài giây */
            SoundsManager.Instance.PlayOneShotAudioKill();
            GameObject parent = col.gameObject.transform.parent.gameObject;
            parent.GetComponent<EnemyFollow>().enabled = false;
            for (int i = col.gameObject.transform.parent.childCount - 1; i >= 0; i--)
            {
                GameObject childObject = parent.transform.GetChild(i).gameObject;                
                Rigidbody2D rbChild = parent.transform.GetChild(i).GetComponent<Rigidbody2D>();

                if (parent.transform.GetChild(i).tag == "Core") continue;
                rbChild.bodyType = RigidbodyType2D.Dynamic;
                rbChild.AddForce(transform.up * 10, ForceMode2D.Impulse);
                
                //Debug.Log(childObject.name);
                //Debug.Log("TestInteractCompelete");


                rbChild.AddForce((transform.up + new Vector3(0, Random.Range(0.2f, 1.2f), 0)) * 7, ForceMode2D.Impulse);
                DisableColider(childObject);
                Destroy(childObject, Random.Range(2f, 3.5f));


            }

            parent.GetComponent<Rigidbody2D>().AddForce((transform.up + new Vector3(0, Random.Range(0.2f, 0.8f), 0)) * 7, ForceMode2D.Impulse);
            DisableColider(parent);
            PlusScore(parent);
            parent.transform.SetParent(null);
            Destroy(parent, 4f);
        }
    }
    void DisableColider(GameObject gameObject)
    {
        if (gameObject.GetComponent<BoxCollider2D>() == null)
        {
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    void PlusScore(GameObject gameObject)
    {
        int score = PlayerPrefs.GetInt("Score");
        int numberPlus = gameObject.transform.childCount;
        if (gameObject.transform.GetChild(0).tag == "Core") numberPlus += 3;
        PlayerPrefs.SetInt("Score", score + numberPlus);
        score = PlayerPrefs.GetInt("Score");
        scoreIngame.text = "Score: " + score;

        //Debug.LogWarning("Score now " + score);

    }
}
