using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : SingletonMonoBehaviour<PointManager>
{

    private const int DROP_NUM = 4;
    private int[] _point = new int[DROP_NUM] { 0, 0, 0, 0 };
    private bool _isFinish = false;

    public GameObject _win;
    public GameObject _lose;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
    }

    public void AddPoint(int id)
    {
        //終了
        if (_isFinish)
            return;

        //ポイント加算
        _point[id]++;

        //5点以上とったらWin取られたらLose
        for (int i = 0; i < DROP_NUM; i++)
        {
            if(_point[i] >= 5 && !_isFinish)
            {
                _isFinish = true;

                if (i == 0)
                {
                    _win.SetActive(true);
                    AudioManager.Instance.PlayBGM("Win");
                }
                else
                {
                    _lose.SetActive(true);
                    AudioManager.Instance.PlayBGM("Lose");
                }
            }
        }

        //ポイントUI設置
        GameObject img = (GameObject)Resources.Load("Prefabs/UI/PointUI");

        if(img != null)
        {
            GameObject ui_prefab = Instantiate(img);
            GameObject mask_panel = GameObject.Find("Canvas/Mask");
            ui_prefab.transform.SetParent(mask_panel.transform,false);

            switch (id)
            {
                case 0:
                    ui_prefab.GetComponent<Image>().color = Color.red;
                    break;
                case 1:
                    ui_prefab.GetComponent<Image>().color = Color.blue;
                    break;
                case 2:
                    ui_prefab.GetComponent<Image>().color = Color.yellow;
                    break;
                case 3:
                    ui_prefab.GetComponent<Image>().color = Color.green;
                    break;
                default:
                    break;
            }

            Vector3 root_pos = new Vector3(240,200,0);

            ui_prefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(root_pos.x + ((_point[id] - 1) * 30), root_pos.y - (50 * id), 0);
        }
        else
        {
            print("Missing Image");
        }
    }
}
