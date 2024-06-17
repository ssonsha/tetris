using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyDirectionTetrino:int { LEFT = -1, RIGHT = 1, DOWN }

public class my_tetrino_figure : MonoBehaviour
{
    public my_tetrino_segment[] get_segments()
    {
        return GetComponentsInChildren<my_tetrino_segment>();
    }

    public void MySetDirection(MyDirectionTetrino _dir)
    {
        transform.Translate((int)_dir, 0, 0);
    }

    public void MyDropTetrino(bool _isPositive)
    {
        if (_isPositive)
            transform.Translate(0, -1, 0);
        else
            transform.Translate(0, 1, 0);
    }
}
