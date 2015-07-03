using UnityEngine;
using System.Collections;
/// <summary>
/// @Intro: 
/// @Binding to: Null 
/// @Author: wenyueyun
/// </summary>
public class Candy : MonoBehaviour {
    public int rowIdx = 0;
    public int culumnIdx = 0;
    public float xOff = -4.5f;
    public float yOff = -3f;
    public int candyTypeNum = 6;
    public GameObject[] items;
    private GameObject item;
    public int type = 0;
    public GameController game;
    private SpriteRenderer sr;
    public bool selected {
        set
        {
            if (sr)
            {
                sr.color = value ? Color.blue : Color.white;
            }
        }
     }
	// Use this for initialization
	void Start () {
        //int idx = Random.Range(0, items.Length);
        //item = (GameObject)Instantiate(items[idx]);
        //item.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

     void OnMouseDown()
    {
        game.Select(this);
    }


    private void addItem()
    {
        if (item == null)
        {
            type = Random.Range(0, Mathf.Min(candyTypeNum, items.Length));
            item = (GameObject)Instantiate(items[type]);
            item.transform.parent = this.transform;

            sr = item.GetComponent<SpriteRenderer>();
        }
    }

    public void UpdatePos()
    {
        addItem();
        transform.position = new Vector3(culumnIdx+xOff,rowIdx+yOff,0f);
    }


    public void TweenPos()
    {
        addItem();
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "x", culumnIdx + xOff,
            "y", rowIdx + yOff,
            "time",0.3f
            ));
    }

    public void Dispose()
    {
        game = null;
        Destroy(item.gameObject);
        Destroy(this.gameObject);
    }

}
