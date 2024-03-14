using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class FruitLaunch : MonoBehaviour
{
    [SerializeField]
    private Transform _launchPos;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private List<GameObject> _projectile;
    [SerializeField]
    private Collider _targetCollider;
    private GameObject _fruit;
    private int _proj;
    private bool StartGame = false;

    private void RandomProjecties()
    {
        _proj = Random.Range(0, _projectile.Count);
        _fruit = Instantiate(_projectile[_proj], _launchPos.position, Quaternion.identity);
    }

    void Update()
    {
        if (_targetCollider.isTrigger && StartGame == false)
        {
            StartGame = true;
            StartCoroutine(FruitNinja());
        }
    }

    IEnumerator FruitNinja()
    {
        while(StartGame)
        {
            yield return new WaitForSeconds(1f);
            RandomProjecties();
        }
        yield return null;
    }

}
