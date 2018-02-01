using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {

    public Transform _spawnPoint;
    public int _dropId;

    [System.NonSerialized]
    public bool _isFall;
    private float _scale;

    private int _lastCollisionId = -1;  //最後に接触したドロップのID

    // Use this for initialization
    public virtual void Start () {
        gameObject.transform.position = _spawnPoint.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        _isFall = false;

        _scale = 1.0f;
        gameObject.transform.localScale = new Vector3(_scale, _scale, 1);
    }

    // Update is called once per frame
    public virtual void Update () {

        if(_isFall)
        {
            if (_scale >= 0)
            {
                _scale -= 0.01f;

                gameObject.transform.localScale = new Vector3(_scale, _scale, 1);
            }
            else
            {
                //倒したドロップに点数加算
                if (_lastCollisionId >= 0)
                {
                    AudioManager.Instance.PlaySE("チーン");
                    PointManager.Instance.AddPoint(_lastCollisionId);
                }
                //初期化
                this.Start();
            }
        }
    }

    public void Shoot(Vector2 shootVec)
    {
        if (!_isFall)
            GetComponent<Rigidbody2D>().AddForce(shootVec);
    }

    public virtual void OnTriggerExit2D(Collider2D col2D)
    {
        //ステージ外にでたら落ちる
        if (col2D.tag == "Stage")
        {
            _isFall = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col2D)
    {

        //最後に接触したドロップの情報を記憶
        if(col2D.gameObject.tag == "Drop" && !_isFall)
            _lastCollisionId = col2D.gameObject.GetComponent<Drop>()._dropId;

        //接触点にエフェクト生成
        foreach (ContactPoint2D point in col2D.contacts)
        {
            AudioManager.Instance.PlaySE("DropBounce");
            ParticleManager.Instance.Create("HitEffect", point.point);
        }
    }
}
