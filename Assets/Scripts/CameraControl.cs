using ScriptableObjectVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private FloatReference _cameraBound;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
            return;

        float playerX = _player.transform.position.x;
        float xDiff = transform.position.x - playerX;

        if (xDiff > _cameraBound)
            transform.position = new Vector3(playerX + _cameraBound, transform.position.y, -10);
        else if (xDiff < - _cameraBound)
            transform.position = new Vector3(playerX - _cameraBound, transform.position.y, -10);
    }
}
