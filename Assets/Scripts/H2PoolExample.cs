using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class H2PoolExample : MonoBehaviour
{
    [SerializeField] private int poolCount = 3; //дефолтное количество элементов в пуле
    [SerializeField] private bool autoPlus = false; //дефолтная возможность или невозможность авто расширения пула
    [SerializeField] private H2Cube h2CubePrefab; //ссылка на префаб объекта пула

    [SerializeField] private Pool<H2Cube> _pool1;

    private void Start()
    {
        _pool1 = new Pool<H2Cube>(h2CubePrefab, poolCount, this.transform);
        _pool1.autoPlus = this.autoPlus;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.CreateCube();
        }
    }

    private void CreateCube()
    {
        var rx = Random.Range(-5f, 5f);
        var rz = Random.Range(-5f, 5f);
        var ry = 0;
        var pos = new Vector3(rx,ry,rz);
        var cube = this._pool1.GetFreeElement();
        cube.transform.position = pos;

    }
    
}
