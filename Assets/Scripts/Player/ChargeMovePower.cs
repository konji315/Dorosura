using UnityEngine.Networking;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ChargeMovePower : MonoBehaviour {

    private int _power = 1000;
    private bool  _drag;
    private Vector3 _touchStartPos;
    private Vector3 _touchEndPos;

    [SerializeField]
    private GameObject _arrow;

    private PlayerController _playerController;

    // Use this for initialization
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //タッチ時
        if(Input.GetMouseButtonDown(0))
        {
            _touchStartPos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
            _drag = true;
            //矢印の表示
            _arrow.SetActive(true);
        }

        //離したとき
        if(Input.GetMouseButtonUp(0))
        {
            float distance = CreateDistance(_touchStartPos, _touchEndPos);
            float radius = CreateRadius(_touchStartPos, _touchEndPos);

            float addforceX = Mathf.Cos(radius) * distance * _power;
            float addforceY = Mathf.Sin(radius) * distance * _power;

            _playerController.Shoot(new Vector2(addforceX, addforceY));

            _drag = false;

            _arrow.SetActive(false);
        }

        //ドラッグ中の場合
        if (_drag)
        {
            _touchEndPos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
            float scale = CreateDistance(_touchStartPos, _touchEndPos);
            if (scale > 0.5f)
                scale = 0.5f;
            _arrow.transform.localScale = new Vector3(scale, _arrow.transform.localScale.y, _arrow.transform.localScale.z);

            //矢印の角度の設定
            float radius = CreateRadius(_touchStartPos, _touchEndPos);
            float deg = radius * Mathf.Rad2Deg;
            _arrow.transform.eulerAngles = new Vector3(0, 0, deg);
        }
    }

    //void OnMouseDown()
    //{
    //    //if (Input.touchCount > 0)
    //    //{
    //    //touch_start_pos = new Vector2(Input.touches[0].position.x / Screen.width, Input.touches[0].position.y / Screen.height);
    //    _touchStartPos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
    //    _drag = true;
    //        //矢印の表示
    //        _arrow.SetActive(true);
    //    //}
    //}

    //void OnMouseUp()
    //{
    //    float distance = CreateDistance(_touchStartPos,_touchEndPos);
    //    float radius = CreateRadius(_touchStartPos, _touchEndPos);

    //    float addforceX = Mathf.Cos(radius) * distance * _power;
    //    float addforceY = Mathf.Sin(radius) * distance * _power;

    //    _playerController.Shoot(new Vector2(addforceX, addforceY));

    //    _drag = false;

    //    _arrow.SetActive(false);
    //}

    /// <summary>
    /// 2点間の距離を求める
    /// </summary>
    /// <param name="start_pos">開始点</param>
    /// <param name="end_pos">終点</param>
    /// <returns>距離</returns>
    float CreateDistance(Vector2 start_pos,Vector2 end_pos)
    {
        float distance = Mathf.Sqrt((end_pos.x - start_pos.x) * (end_pos.x - start_pos.x) + (end_pos.y - start_pos.y) * (end_pos.y - start_pos.y));

        return distance * 2;
    }

    /// <summary>
    /// 2点間の角度を求める(ラジアン)
    /// </summary>
    /// <param name="start_pos">開始点</param>
    /// <param name="end_pos">終点</param>
    /// <returns>角度(ラジアン)</returns>
    float CreateRadius(Vector2 start_pos,Vector2 end_pos)
    {
        float radius = Mathf.Atan2(end_pos.y - start_pos.y,end_pos.x - start_pos.x);

        return radius;
    }
}
