using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    private float _timer = 0;
    Animator _animator;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if(_animator.GetBool("touch"))
        {
            _timer += Time.deltaTime;
            if (_timer >= 2.0f)
            {
                AudioManager.Instance.PlayBGM("PlayBGM");
                SceneManager.LoadScene("PlayScene");
            }
        }

        if (Input.GetMouseButtonDown(0) && !_animator.GetBool("touch"))
        {
            AudioManager.Instance.PlaySE("Enter");
            _animator.SetBool("touch", true);
        }
    }
}
