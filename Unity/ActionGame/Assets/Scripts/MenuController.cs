using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    public Color purple;
    public SkinnedMeshRenderer head;
    public Mesh[] headMesh;
    public int headMeshIdx = 0;

    public SkinnedMeshRenderer hand;
    public Mesh[] handMesh;
    public int handMeshIdx = 0;

    public SkinnedMeshRenderer[] bodyMesh;

    public Color[] colors;
    public int colorIdx = -1;
	// Use this for initialization
	void Start () {
	    colors = new Color[]{Color.blue,Color.cyan,Color.green,purple,Color.red};
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnHeadMeshNext()
    {
        headMeshIdx++;
        headMeshIdx %= headMesh.Length;
        head.sharedMesh = headMesh[headMeshIdx];
    }

    public void OnHandMeshNext()
    {
        handMeshIdx++;
        handMeshIdx %= handMesh.Length;
        hand.sharedMesh = handMesh[handMeshIdx];
    }

    public void OnChangeColorBlue()
    {
        colorIdx = 1;
        OnChangeColor(Color.blue);
    }
    public void OnChangeColorRed()
    {
        colorIdx = 5;
        OnChangeColor(Color.red);
    }
    public void OnChangeColorCyan()
    {
        colorIdx = 2;
        OnChangeColor(Color.cyan);
    }
    public void OnChangeColorGreen()
    {
        colorIdx = 3;
        OnChangeColor(Color.green);
    }
    public void OnChangeColorPurple()
    {
        colorIdx = 4;
        OnChangeColor(purple);
    }

    private void OnChangeColor(Color c)
    {
        foreach(SkinnedMeshRenderer sr in bodyMesh)
        {
            sr.material.color = c;
        }
    }

    private void save()
    {
        PlayerPrefs.SetInt("headIdx", headMeshIdx);
        PlayerPrefs.SetInt("handIdx",handMeshIdx);
        PlayerPrefs.SetInt("colorIdx",colorIdx);
    }

    public void OnPlay()
    {
        save();
        Application.LoadLevel(2);
    }
}
