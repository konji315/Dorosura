using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveMent : Drop {

    public float _shootInterval = 2;
    public float _shootPower = 300;

    private float _shootTimer;
    private Vector3 _playerPos;
    private bool _isLocatePlayer;

    // Use this for initialization
    public override void Start () {
        base.Start();

        _shootTimer = _shootInterval;
        _isLocatePlayer = false;
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();

        _shootTimer -= Time.deltaTime;

        //間隔タイマーが0
        if (_shootTimer <= 0)
        {
            Vector2 vec;

            //プレイヤー発見時はプレイヤーの方向へ未発見時はランダム
            if (_isLocatePlayer)
                vec = _playerPos - this.gameObject.transform.position;
            else
                vec = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

            vec.Normalize();

            //発射
            Shoot(vec * Random.Range(_shootPower / 2, _shootPower));

            //間隔タイマーのセット
            _shootTimer = Random.Range(0, _shootInterval);
        }
    }

    void OnTriggerStay2D(Collider2D col2D)
    {
        //プレイヤー発見
        if (col2D.tag == "Drop")
        {
            _playerPos = col2D.transform.position;
            _isLocatePlayer = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D col2D)
    {
        base.OnTriggerExit2D(col2D);

        //プレイヤーロスト
        if (col2D.tag == "Drop")
            _isLocatePlayer = false;
    }
}
