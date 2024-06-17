using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public struct MyHud
{
    public Text my_score;
    private int my_int_score;

    public Text my_line;
    private int my_int_line;

    public Text my_level;
    public int my_int_level { get; private set; }

    public float my_speed { get; set; }
    private int my_counter_line;

    public void AddScore(int _score)
    {
        my_int_score += _score;
        my_score.text = my_int_score.ToString();
    }

    public void AddLine(int _line)
    {
        my_int_line += _line;
        my_line.text = my_int_line.ToString();
        my_counter_line += _line;
        if (my_counter_line > 9)
            AddLevel(1);

        my_counter_line = my_counter_line % 10;
    }

    public void AddLevel(int _level)
    {
        my_int_level += _level;
        my_level.text = my_int_level.ToString();
        my_speed -= my_int_level > 5 ? 0.02f : 0.05f;
    }
}

public class my_main : MonoBehaviour
{
    private bool is_stop_move = false;
    private bool is_game_over = false;
    private const int wid = 13, hei = 21;
    private const float step_tetrino = 1;
    private float my_curr_time = 0;
    private Object pref_tetrino;
    private GameObject pref_tetrino_L;
    private my_tetrino_element[,] array_tetrino;
    private my_tetrino_figure test_figure;
    private FigureTetrino my_figure_random;
    private my_tittle_figure my_tittle;
    

    private MyHud my_hud;

    private GameObject my_main_camera;
    private GameObject my_3d_camera;
    public GameObject pLost;


    private void Start()
    {
        array_tetrino = new my_tetrino_element[wid, hei];
        pref_tetrino = Resources.Load("my_prefab/my_tetrino_o");
        pref_tetrino_L = Resources.Load("my_prefab/my_tetrino_L") as GameObject;
        my_tittle = FindObjectOfType<my_tittle_figure>();
        

        my_main_camera = GameObject.FindGameObjectWithTag("MainCamera");
        my_3d_camera = GameObject.FindGameObjectWithTag("my_camera_3d");
        

        MyOnSwitchCamera(false);

        my_hud.my_score = GameObject.FindGameObjectWithTag("my_score").GetComponent<Text>();
        my_hud.my_line = GameObject.FindGameObjectWithTag("my_line").GetComponent<Text>();
        my_hud.my_level = GameObject.FindGameObjectWithTag("my_level").GetComponent<Text>();
        my_hud.my_speed = 0.5f;
        my_hud.AddLevel(1);

        my_figure_random = CreateRandomFigure();
        CreateFigure(my_figure_random);
        my_figure_random = CreateRandomFigure();
        my_tittle.GetComponentInChildren<my_tetrino_data>().MyInitialized(my_figure_random);


        for (int y = 0; y < hei; y++)
        {
            for (int x = 0; x < wid; x++)
            {
                GameObject go = Instantiate(pref_tetrino, new Vector3(x * step_tetrino, y * step_tetrino, 0), 
                    Quaternion.identity) as GameObject;
                array_tetrino[x, y] = go.GetComponent<my_tetrino_element>();
            }
        }
    }

    private FigureTetrino CreateRandomFigure()
    {
        return (FigureTetrino)Random.Range(0, 5);
    }
    private void CreateFigure(FigureTetrino _figure)
    {
        test_figure = Instantiate(pref_tetrino_L, new Vector3(step_tetrino * 6, step_tetrino * (hei - 2), 0),
            Quaternion.identity).GetComponent<my_tetrino_figure>();
        test_figure.GetComponentInChildren<my_tetrino_data>().MyInitialized(_figure);

        if (my_hud.my_int_level < 3)
            test_figure.GetComponentInChildren<my_tetrino_data>().SetColor(Random.ColorHSV(0.4f, 0.6f, 1, 1, 1, 1, 1, 1));
        else if(my_hud.my_int_level < 5)
            test_figure.GetComponentInChildren<my_tetrino_data>().SetColor(Random.ColorHSV(0.5f, 0.7f, 1, 1, 1, 1, 1, 1));
        else if(my_hud.my_int_level < 7)
            test_figure.GetComponentInChildren<my_tetrino_data>().SetColor(Random.ColorHSV(0.8f, 1.0f, 1, 1, 1, 1, 1, 1));
        else if(my_hud.my_int_level < 10)
            test_figure.GetComponentInChildren<my_tetrino_data>().SetColor(Random.ColorHSV(0.2f, 0.4f, 1, 1, 1, 1, 1, 1));
        else if(my_hud.my_int_level >= 10)
            test_figure.GetComponentInChildren<my_tetrino_data>().SetColor(Random.ColorHSV(0.3f, 0.5f, 1, 1, 1, 1, 1, 1));

        is_stop_move = false;        

        StartCoroutine(my_update(my_hud.my_speed));
    }

    private IEnumerator my_update(float _time)
    {
        while(!is_stop_move)
        {
            yield return new WaitForSeconds(_time);
            test_figure.transform.Translate(new Vector3(0, -step_tetrino, 0));
            is_stop_move = check_pre_intersect(test_figure);
        }

        AddToArray();
        Destroy(test_figure.gameObject);
        RemoveFullLinies();

        if (!IsGameOver())
        {
            CreateFigure(my_figure_random);
            my_figure_random = CreateRandomFigure();
            my_tittle.GetComponentInChildren<my_tetrino_data>().MyInitialized(my_figure_random);
        }
        else
        {
            is_game_over = true;
            my_3d_camera.SetActive(false);
            my_main_camera.SetActive(false);
            my_tittle.gameObject.SetActive(false);
            pLost.SetActive(true);
            



            my_hud_visibility[] gObjects = FindObjectsOfType<my_hud_visibility>();
            for (int ind = 0; ind < gObjects.Length; ind++)
                gObjects[ind].gameObject.SetActive(false);
        }
    }

    public void MyOnSwitchCamera(bool _is3D)
    {
        my_main_camera.SetActive(!_is3D);
        my_3d_camera.SetActive(_is3D);
    }

    public void MyOnPause(bool _isPause)
    {
        Time.timeScale = _isPause ? 0 : 1;
    }

    public void MyOnStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void RemoveFullLinies()
    {
        int[] remove_line = CheckFullLines();

        for (int ind = 0; ind < remove_line.Length; ind++)
        {
            for (int x = 0; x < wid; x++)
            {
                array_tetrino[x, remove_line[ind]].set_tetrino_active(false);
            }

            my_hud.AddScore(remove_line.Length == 4 ? 750 : 350);
        }

        if(remove_line.Length != 0)
        {

            int[] empty_line = CheckEmptyLines();

            my_hud.AddLine(remove_line.Length);

            bool[,] arr_new_tetrino = new bool[wid, hei];

            int start_y = 0;

            for (int y = 0; y < hei; y++)
            {
                if (SkipTheLine(ref empty_line, y))
                    continue;

                for (int x = 0; x < wid; x++)
                {
                    arr_new_tetrino[x, start_y] = array_tetrino[x, y].get_isActive_tetrino();
                }

                start_y++;
            }

            SetNewTetrinoArray(ref arr_new_tetrino);
        }
    }

    private void SetNewTetrinoArray(ref bool[,] _arr_new)
    {
        for (int y = 0; y < hei; y++)
            for (int x = 0; x < wid; x++)
                array_tetrino[x, y].set_tetrino_active(_arr_new[x, y]);
    }


    private void Update()
    {
        if (test_figure)
        {
            if (Input.GetButtonDown("RotateTetrino"))
            {
                test_figure.GetComponentInChildren<my_tetrino_data>().MyRotation(true);
                if (check_intersect(test_figure))
                    test_figure.GetComponentInChildren<my_tetrino_data>().MyRotation(false);
            }

            if (Input.GetButton("LeftTetrino"))
            {
                MyInputPress(MyDirectionTetrino.LEFT, 0.1f);
            }
            else if (Input.GetButton("RightTetrino"))
            {
                MyInputPress(MyDirectionTetrino.RIGHT, 0.1f);
            }

            if (Input.GetButton("DropTetrino"))
            {
                MyInputPress(MyDirectionTetrino.DOWN, 0.04f);
            }

            if (Input.GetButtonDown("LeftTetrino"))
            {
                my_curr_time = 0;
                test_figure.MySetDirection(MyDirectionTetrino.LEFT);
                if (check_intersect(test_figure))
                    test_figure.MySetDirection(MyDirectionTetrino.RIGHT);
            }
            else if (Input.GetButtonDown("RightTetrino"))
            {
                my_curr_time = 0;
                test_figure.MySetDirection(MyDirectionTetrino.RIGHT);
                if (check_intersect(test_figure))
                    test_figure.MySetDirection(MyDirectionTetrino.LEFT);
            }

            if (Input.GetButtonUp("LeftTetrino") || Input.GetButtonUp("RightTetrino"))
                my_curr_time = 0;
        }

           
  
    }

    private void MyInputPress(MyDirectionTetrino _dir, float _time)
    {
        my_curr_time += Time.deltaTime;
        if(my_curr_time > _time)
        {
            my_curr_time = 0;

            if(_dir == MyDirectionTetrino.LEFT)
            {
                test_figure.MySetDirection(MyDirectionTetrino.LEFT);
                if (check_intersect(test_figure))
                    test_figure.MySetDirection(MyDirectionTetrino.RIGHT);
            }
            else if (_dir == MyDirectionTetrino.RIGHT)
            {
                test_figure.MySetDirection(MyDirectionTetrino.RIGHT);
                if (check_intersect(test_figure))
                    test_figure.MySetDirection(MyDirectionTetrino.LEFT);
            }
            else if (_dir == MyDirectionTetrino.DOWN)
            {
                test_figure.MyDropTetrino(true);
                if (check_intersect(test_figure))
                    test_figure.MyDropTetrino(false);
            }
        }
    }

    private void AddToArray()
    {
        GameObject[] go = test_figure.GetComponentInChildren<my_tetrino_data>().get_tetrino_array;
        for (int ind = 0; ind < go.Length; ind++)
        {
            int x = (int)go[ind].transform.position.x;
            int y = (int)go[ind].transform.position.y;

            array_tetrino[x, y].set_tetrino_active(true);
            array_tetrino[x, y].set_color(test_figure.GetComponentInChildren<my_tetrino_data>().my_color);
        }
    }

    private bool IsGameOver()
    {
        for (int x = 0; x < wid; x++)
        {
            if (array_tetrino[x, hei - 3].get_isActive_tetrino())
                return true;
        }
        return false;
    }

    private bool SkipTheLine(ref int[] _empty_line, int _y)
    {
        for (int ind = 0; ind < _empty_line.Length; ind++)
        {
            if (_y == _empty_line[ind])
                return true;
        }

        return false;
    }

    private int[] CheckEmptyLines()
    {
        List<int> lines = new List<int>();

        for (int y = 0; y < hei; y++)
        {
            int count_line_x = 0;

            for (int x = 0; x < wid; x++)
            {
                if (array_tetrino[x, y].get_isActive_tetrino())
                    break;
                else
                    count_line_x++;
            }

            if (count_line_x == wid)
                lines.Add(y);
        }

        return lines.ToArray();
    }

    private int[] CheckFullLines()
    {
        List<int> arr = new List<int>();

        for (int y = 0; y < hei; y++)
        {
            int count_line_x = 0;

            for (int x = 0; x < wid; x++)
            {
                if (array_tetrino[x, y].get_isActive_tetrino())
                    count_line_x++;
                else
                    break;
            }

            if (count_line_x == wid)
                arr.Add(y);
        }

        return arr.ToArray();
    }

    private bool check_intersect(my_tetrino_figure _figure)
    {
        for (int ind = 0; ind < _figure.get_segments().Length; ind++)
        {
            int x = (int)_figure.get_segments()[ind].transform.position.x;
            int y = (int)_figure.get_segments()[ind].transform.position.y;

            bool is_intersect = IsIntersect(x, y);

            if (is_intersect)
                return is_intersect;
        }

        return false;
    }

    private bool check_pre_intersect(my_tetrino_figure _figure)
    {
        for (int ind = 0; ind < _figure.get_segments().Length; ind++)
        {
            int x = (int)_figure.get_segments()[ind].transform.position.x;
            int y = (int)_figure.get_segments()[ind].transform.position.y;
            
            bool is_intersect = IsIntersect(x, y);

            if (is_intersect)
            {
                _figure.transform.Translate(new Vector3(0, step_tetrino, 0));
                return is_intersect;
            }
        }

        return false;
    }

    private bool IsIntersect(int _x, int _y)
    {
        try
        {
            if (array_tetrino[_x, _y].get_isActive_tetrino())
                return true;

        }
        catch (System.Exception e) { return true; }

        return false;
    }
}
