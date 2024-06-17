using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FigureTetrino { L, T, I, Z, O }

public class my_tetrino_data : MonoBehaviour
{
    private GameObject pref_cube;
    private GameObject[] my_tetrino_array;
    private int my_rotation;
    private FigureTetrino my_type;
    public Color my_color { get; private set; }
    private void Awake()
    {
        pref_cube = Resources.Load("my_prefab/my_cube") as GameObject;
        my_tetrino_array = new GameObject[4];
        my_rotation = 0;
    }

    public GameObject[] get_tetrino_array
    {
        get { return my_tetrino_array; }
    }

    public void SetColor(Color _color)
    {
        for (int ind = 0; ind < transform.childCount; ind++)
        {
            GameObject go = transform.GetChild(ind).gameObject;
            Material mat = go.GetComponent<MeshRenderer>().material;
            mat.color = _color;
            my_color = _color;
        }
    }

    public void MyRotation(bool _isPositive)
    {
        if (_isPositive)
        {
            my_rotation++;
            my_rotation = my_rotation % 4;
        }
        else
        {
            my_rotation--;
            if (my_rotation < 0)
                my_rotation = 3;
        }

        MyRotationType(my_type, my_rotation);
    }

    private void MyRotationType(FigureTetrino _figure, int _rot)
    {
        switch(_rot)
        {
            case 0:
                if (_figure == FigureTetrino.L)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(0, 1, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(0, -1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(1, -1, 0);
                }
                else if (_figure == FigureTetrino.T)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(0, 1, 0);
                }
                else if (_figure == FigureTetrino.I)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(0, -1, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(0, 1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(0, -2, 0);
                }
                else if (_figure == FigureTetrino.Z)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(0, 1, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, 1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(1, 0, 0);
                }
                else if (_figure == FigureTetrino.O)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, -1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(0, -1, 0);
                }
                break;
            case 1:
                if (_figure == FigureTetrino.L)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(1, 1, 0);
                }
                else if (_figure == FigureTetrino.T)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(0, 1, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(0, -1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(-1, 0, 0);
                }
                else if (_figure == FigureTetrino.I)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(2, 0, 0);
                }
                else if (_figure == FigureTetrino.Z)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, -1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(0, 1, 0);
                }
                else if (_figure == FigureTetrino.O)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, -1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(0, -1, 0);
                }
                break;
            case 2:
                if (_figure == FigureTetrino.L)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(0, 1, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(0, -1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(-1, 1, 0);
                }
                else if (_figure == FigureTetrino.T)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(0, -1, 0);
                }
                else if (_figure == FigureTetrino.I)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(0, -1, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(0, 1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(0, -2, 0);
                }
                else if (_figure == FigureTetrino.Z)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(0, 1, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, 1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(1, 0, 0);
                }
                else if (_figure == FigureTetrino.O)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, -1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(0, -1, 0);
                }
                break;
            case 3:
                if (_figure == FigureTetrino.L)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(-1, -1, 0);
                }
                else if (_figure == FigureTetrino.T)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(0, 1, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(0, -1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(1, 0, 0);
                }
                else if (_figure == FigureTetrino.I)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(2, 0, 0);
                }
                else if (_figure == FigureTetrino.Z)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, -1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(0, 1, 0);
                }
                else if (_figure == FigureTetrino.O)
                {
                    my_tetrino_array[0].transform.localPosition = new Vector3(0, 0, 0);
                    my_tetrino_array[1].transform.localPosition = new Vector3(-1, 0, 0);
                    my_tetrino_array[2].transform.localPosition = new Vector3(-1, -1, 0);
                    my_tetrino_array[3].transform.localPosition = new Vector3(0, -1, 0);
                }
                break;
        }
    }

    public void MyInitialized(FigureTetrino _tetrino)
    {
        for (int ind = 0; ind < transform.childCount; ind++)
            Destroy(transform.GetChild(ind).gameObject);
        
        switch(_tetrino)
        {
            case FigureTetrino.L :
                my_type = _tetrino;
                GameObject obL = Instantiate(pref_cube, new Vector3(), Quaternion.identity);
                obL.AddComponent<my_tetrino_segment>();
                obL.transform.SetParent(transform, false);

                GameObject obL2 = Instantiate(pref_cube, new Vector3(0, 1, 0), Quaternion.identity);
                obL2.AddComponent<my_tetrino_segment>();
                obL2.transform.SetParent(transform, false);

                GameObject obL3 = Instantiate(pref_cube, new Vector3(0, -1, 0), Quaternion.identity);
                obL3.AddComponent<my_tetrino_segment>();
                obL3.transform.SetParent(transform, false);

                GameObject obL4 = Instantiate(pref_cube, new Vector3(1, -1, 0), Quaternion.identity);
                obL4.AddComponent<my_tetrino_segment>();
                obL4.transform.SetParent(transform, false);

                for (int ind = 0; ind < my_tetrino_array.Length; ind++)
                    my_tetrino_array[ind] = transform.GetChild(ind).gameObject;
                break;

            case FigureTetrino.T:
                my_type = _tetrino;
                GameObject obT = Instantiate(pref_cube, new Vector3(), Quaternion.identity);
                obT.AddComponent<my_tetrino_segment>();
                obT.transform.SetParent(transform, false);

                GameObject obT2 = Instantiate(pref_cube, new Vector3(1, 0, 0), Quaternion.identity);
                obT2.AddComponent<my_tetrino_segment>();
                obT2.transform.SetParent(transform, false);

                GameObject obT3 = Instantiate(pref_cube, new Vector3(-1, 0, 0), Quaternion.identity);
                obT3.AddComponent<my_tetrino_segment>();
                obT3.transform.SetParent(transform, false);

                GameObject obT4 = Instantiate(pref_cube, new Vector3(0, 1, 0), Quaternion.identity);
                obT4.AddComponent<my_tetrino_segment>();
                obT4.transform.SetParent(transform, false);

                for (int ind = 0; ind < my_tetrino_array.Length; ind++)
                    my_tetrino_array[ind] = transform.GetChild(ind).gameObject;
                break;

            case FigureTetrino.I:
                my_type = _tetrino;
                GameObject obI = Instantiate(pref_cube, new Vector3(), Quaternion.identity);
                obI.AddComponent<my_tetrino_segment>();
                obI.transform.SetParent(transform, false);

                GameObject obI2 = Instantiate(pref_cube, new Vector3(0, -1, 0), Quaternion.identity);
                obI2.AddComponent<my_tetrino_segment>();
                obI2.transform.SetParent(transform, false);

                GameObject obI3 = Instantiate(pref_cube, new Vector3(0, 1, 0), Quaternion.identity);
                obI3.AddComponent<my_tetrino_segment>();
                obI3.transform.SetParent(transform, false);

                GameObject obI4 = Instantiate(pref_cube, new Vector3(0, 2, 0), Quaternion.identity);
                obI4.AddComponent<my_tetrino_segment>();
                obI4.transform.SetParent(transform, false);

                for (int ind = 0; ind < my_tetrino_array.Length; ind++)
                    my_tetrino_array[ind] = transform.GetChild(ind).gameObject;
                break;

            case FigureTetrino.Z:
                my_type = _tetrino;
                GameObject obZ = Instantiate(pref_cube, new Vector3(), Quaternion.identity);
                obZ.AddComponent<my_tetrino_segment>();
                obZ.transform.SetParent(transform, false);

                GameObject obZ2 = Instantiate(pref_cube, new Vector3(0, 1, 0), Quaternion.identity);
                obZ2.AddComponent<my_tetrino_segment>();
                obZ2.transform.SetParent(transform, false);

                GameObject obZ3 = Instantiate(pref_cube, new Vector3(-1, 1, 0), Quaternion.identity);
                obZ3.AddComponent<my_tetrino_segment>();
                obZ3.transform.SetParent(transform, false);

                GameObject obZ4 = Instantiate(pref_cube, new Vector3(1, 0, 0), Quaternion.identity);
                obZ4.AddComponent<my_tetrino_segment>();
                obZ4.transform.SetParent(transform, false);

                for (int ind = 0; ind < my_tetrino_array.Length; ind++)
                    my_tetrino_array[ind] = transform.GetChild(ind).gameObject;
                break;

            case FigureTetrino.O:
                my_type = _tetrino;
                GameObject obO = Instantiate(pref_cube, new Vector3(), Quaternion.identity);
                obO.AddComponent<my_tetrino_segment>();
                obO.transform.SetParent(transform, false);

                GameObject obO2 = Instantiate(pref_cube, new Vector3(-1, 0, 0), Quaternion.identity);
                obO2.AddComponent<my_tetrino_segment>();
                obO2.transform.SetParent(transform, false);

                GameObject obO3 = Instantiate(pref_cube, new Vector3(-1, -1, 0), Quaternion.identity);
                obO3.AddComponent<my_tetrino_segment>();
                obO3.transform.SetParent(transform, false);

                GameObject obO4 = Instantiate(pref_cube, new Vector3(0, -1, 0), Quaternion.identity);
                obO4.AddComponent<my_tetrino_segment>();
                obO4.transform.SetParent(transform, false);

                for (int ind = 0; ind < my_tetrino_array.Length; ind++)
                    my_tetrino_array[ind] = transform.GetChild(ind).gameObject;
                break;
        }
    }
}
