using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class FruitLaunch : MonoBehaviour
{
    [SerializeField]
    private Transform _launchPos;
    private GameObject _target;
    [SerializeField]
    private List<GameObject> _projectile;
    private GameObject _fruit;
    private int _proj;
    private int _chance;
    public bool StartGame = false;
    private bool coroutineStarted = false;

    private void RandomProjecties()
    {
        _proj = Random.Range(0, _projectile.Count);
        _fruit = Instantiate(_projectile[_proj], _launchPos.position, Quaternion.identity);
        StartCoroutine(DestroyFullFruit(_fruit));
    }

    void Update()
    {
        if (StartGame && !coroutineStarted)
        {
            StartCoroutine(FruitNinja());
            coroutineStarted = true;
        }
    }
    IEnumerator FruitNinja()
    {
        while(StartGame)
        {
            yield return new WaitForSeconds(1f);
            _target = GameObject.FindGameObjectWithTag("Player");
            transform.LookAt(_target.transform.position + new Vector3(0.0f, 100f, 0f));
            _chance = Random.Range(0, 4);
            if (_chance == 1)
            {
                RandomProjecties();

            }
        }
        yield return null;
    }

    IEnumerator DestroyFullFruit(GameObject fruit)
    {
        yield return new WaitForSeconds(5f);
        Destroy(fruit);
    }

}
