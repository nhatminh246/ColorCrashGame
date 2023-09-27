using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenColor : MonoBehaviour
{
    // Start is called before the first frame update
    public Color[] arrColor = new Color[9];
    private Color mainColor;
    private Color grayColor;
    private int numberChild;
    private int numberGrayColor;
    private bool isSpecial;
    void Awake()
    {
        /*random  màu chủ đạo cho enemy từ bảng color cài sẵn trong prefab */
        mainColor = arrColor[Random.Range(0,arrColor.Length)];
        grayColor = Color.gray;
        numberChild = transform.childCount;
        numberGrayColor = Random.Range(1, numberChild);
        SetUpColor();
        isSpecial = false;


    }

    // Update is called once per frame
    void SetUpColor()
    {

        List<int> indexColor = new List<int>();
        /*tạo màu chính cho lõi và tất cả các cạnh của hình học có thể tấn công được đồng 
        thời đưa index của cạnh vào trong 1 list cạnh*/
        for(int i= 0; i < numberChild; i++)
        {
            indexColor.Add(i);
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = mainColor;

            if(transform.GetChild(i).tag == "Core")
            {
                isSpecial = true;
                numberGrayColor--;
                continue;
            }
            transform.GetChild(i).tag = "OtherColor";

        }

        //tạo màu xám cho những cạnh ngẫu nhiên (random được ra từ list)có thể khiến người chơi bị thương 
        for(int i= 0; i < numberGrayColor; i++)
        {
            int lowestIndex = 0;
            if (isSpecial) lowestIndex = 1;
            int randomIndexGrayColor = indexColor[Random.Range(lowestIndex,indexColor.Count)];
            if (isSpecial && randomIndexGrayColor == 0) randomIndexGrayColor++;
            GameObject child = transform.GetChild(randomIndexGrayColor).gameObject;
            
            
            child.GetComponent<SpriteRenderer>().color = grayColor;
            child.tag = "GrayColor";
            child.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic ;
            indexColor.RemoveAt(indexColor[randomIndexGrayColor]);


        }

    }
}
