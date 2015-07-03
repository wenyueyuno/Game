using UnityEngine;
using System.Collections;
/// <summary>
/// @Intro: 
/// @Binding to: Null 
/// @Author: wenyueyun
/// </summary>
public class GameController : MonoBehaviour {
    public int columnNum = 10;//列

    public int rowNum = 7;//行

    public Candy candy;

    public GameObject game;

    public AudioClip swapClip;

    public AudioClip explodeClip;

    public AudioClip matchClip;

    public AudioClip wrongClip;

    public AudioSource audio;

    private ArrayList candyArr;
	// Use this for initialization
	void Start () {
        Debug.Log("Candy start");

        candyArr = new ArrayList();

        for (int rowIdx = 0; rowIdx < rowNum; rowIdx++ )
        {
            ArrayList temp = new ArrayList();
            for (int columnIdx = 0; columnIdx < columnNum; columnIdx++)
            {
                Candy c = crateCandy(rowIdx, columnIdx);
                temp.Add(c);
            }

            candyArr.Add(temp);
        }

        if (CheckMatchs())
        {
            RemoveMatchs();
        }
	}

    //获取一个二维数组元素
    public Candy GetCandy(int row,int culumn)
    {
        ArrayList tmp = candyArr[row] as ArrayList;
        Candy c = tmp[culumn] as Candy;
        return c;
    }

    //设置二维数组元素
    public void SetCandy(int row,int culumn,Candy c)
    {
        ArrayList tmp = candyArr[row] as ArrayList;
        tmp[culumn] = c;
    }

    private Candy crt;
	public void Select(Candy c)
    {
        //Remove(c); return;
        if (crt == null)
        {
            crt = c;
            crt.selected = true;
        }
        else
        {
            if(Mathf.Abs(crt.rowIdx - c.rowIdx)+Mathf.Abs(crt.culumnIdx - c.culumnIdx) == 1)
            {
                //协程
                StartCoroutine(Exchange2(crt, c));
            }
            else
            {
                audio.PlayOneShot(wrongClip);
            }
            crt.selected = false;
            crt = null;
        }
    }

    IEnumerator Exchange2(Candy c1,Candy c2)
    {
        Exchange(c1, c2);

        yield return new WaitForSeconds(0.4f);
        //等待0.4秒
        if (CheckMatchs())
        {
            RemoveMatchs();
        }
        else
        {
            Exchange(c1, c2);
        }
    }

    //交换两个糖果
    private void Exchange(Candy c1,Candy c2)

    {
        //play autio 
        audio.PlayOneShot(swapClip);

        SetCandy(c1.rowIdx, c1.culumnIdx, c2);
        SetCandy(c2.rowIdx, c2.culumnIdx, c1);
        int culumn = c1.culumnIdx;
        int row = c1.rowIdx;

        c1.culumnIdx = c2.culumnIdx;
        c1.rowIdx = c2.rowIdx;

        c2.culumnIdx = culumn;
        c2.rowIdx = row;

        //c1.UpdatePos();
        //c2.UpdatePos();
        c1.TweenPos();
        c2.TweenPos();
    }

    //添加爆炸效果
    private void AddEffect(Vector3 pos)
    {
        Instantiate(Resources.Load("Prefabs/Explosion2"),pos,Quaternion.identity);
        CameraShake.shakeFor(0.2f, 0.1f);
    }

    //删除单个糖果，并在同一列最顶部添加一个糖果
    private void Remove(Candy c)
    {
        //爆炸效果
        AddEffect(c.transform.position);
        //播放音效
        GetComponent<AudioSource>().PlayOneShot(explodeClip);
        c.Dispose();

        int row = c.rowIdx;
        int culumn = c.culumnIdx;

        //move up
        for (int rowIdx = c.rowIdx + 1; rowIdx < rowNum; rowIdx++)
        {
            Candy c2 = GetCandy(rowIdx, culumn);
            c2.rowIdx--;
            //c2.UpdatePos();
            c2.TweenPos();
            SetCandy(rowIdx - 1,culumn,c2);
        }

        //add new
        Candy c3 = crateCandy(rowNum - 1, culumn);
        c3.rowIdx = rowNum;
        c3.UpdatePos();
        c3.rowIdx--;
        c3.TweenPos();
        SetCandy(rowNum - 1, culumn, c3);
    }

    //创建一个糖果
    private Candy crateCandy(int row,int culumu)
    {
        Object o = Instantiate(candy);
        Candy c = o as Candy;
        c.transform.parent = game.transform;
        c.culumnIdx = culumu;
        c.rowIdx = row;
        c.UpdatePos();
        c.game = this;
        return c;
    }

    //检测是否可消除
    private bool CheckMatchs()
    {
        return CheckHorMatchs() || CheckVerMatchs();
    }

    //检测水平方向是否可消除
    private bool CheckHorMatchs()
    {
        //int rowIdx = rowNum - 1;

        for (int row = 0; row < rowNum; row++)
        {
            for (int column = 0; column < columnNum - 2; column++)
            {
                if ((GetCandy(row, column).type == GetCandy(row, column + 1).type) &&
                    (GetCandy(row, column + 2).type == GetCandy(row, column + 1).type))
                {
                    Debug.Log(row + " " + column + " " + (column + 1) + " " + (column + 2));
                    GetComponent<AudioSource>().PlayOneShot(matchClip);
                    addMatch(GetCandy(row, column));
                    addMatch(GetCandy(row, column + 1));
                    addMatch(GetCandy(row, column + 2));
                    return true;
                }
            }
        }
        return false;
    }

    //检测垂直方向是否可消除
    private bool CheckVerMatchs()
    {
        for (int column = 0; column < columnNum;column++ )
        {
            for (int row = 0; row < rowNum - 2; row++)
            {
                if ((GetCandy(row, column).type == GetCandy(row+1, column).type) &&
                    (GetCandy(row, column).type == GetCandy(row+2, column).type))
                {
                    Debug.Log(column + " " + row + " " + (row + 1) + " " + (row + 2));
                    GetComponent<AudioSource>().PlayOneShot(matchClip);
                    addMatch(GetCandy(row, column));
                    addMatch(GetCandy(row+1, column));
                    addMatch(GetCandy(row+2, column));
                    return true;
                }
            }
        }
        return false;
    }

    //记录可消除糖果
    private ArrayList matchs;
    private void addMatch(Candy c)
    {
        if(matchs == null)
        {
            matchs = new ArrayList();
        }

        int index = matchs.IndexOf(c);

        if (index == -1)
        {
            matchs.Add(c);
        }
    }
    //删除所有可消除的糖果
    private void RemoveMatchs()
    {
        if (matchs == null) return;
        Candy c;
        for (int i = 0; i < matchs.Count;i++ )
        {
            c = matchs[i] as Candy;
            Remove(c);
        }

        matchs = new ArrayList();

        StartCoroutine(WaitAndCheck());
    }

    //延迟0.5秒检测新加的糖果是否可消除
    IEnumerator WaitAndCheck()
    {
        yield return new WaitForSeconds(0.5f);

        if (CheckMatchs())
        {
            RemoveMatchs();
        }

    }

}
