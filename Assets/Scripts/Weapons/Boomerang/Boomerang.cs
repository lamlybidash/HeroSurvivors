using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boomerang : Weapons
{
    //Class phục vụ tính top n enemy xa nhất
    private class EnemyDistance
    {
        public GameObject enemy;
        public float distanceSqr;
    }

    [SerializeField] private EnemyController _ec;
    private List<GameObject> _enemies;
    private List<GameObject> _enemiesFarthest;
    private Vector3 _target;
    private List<ProjectileBoomerang> _projectileBoomerangs = new List<ProjectileBoomerang>();
    private List<ProjectileBoomerang> _listBMReady = new List<ProjectileBoomerang>();
    private readonly object _lock = new object();
    private Coroutine _BoomerangC = null;

    private int i;
    private void Start()
    {
    }


    //Throw All
    private IEnumerator ThrowBoomerang()
    {
        while (true)
        {
            while(_listBMReady.Count == 0)
            {
                yield return null;
            }
            SoundManager.instance.PlayOneSound(ac);
            lock (_lock)
            {
                FindNTargetFarthest(_listBMReady.Count);
                i = 0;
                foreach (ProjectileBoomerang e in _listBMReady)
                {
                    e.gameObject.SetActive(true);
                    e.ThrowBoomerang(_enemiesFarthest[i].transform.position);
                    i++;
                }
                _listBMReady.Clear();
            }
            yield return new WaitForSeconds(countdown);
        }
    }    
    public void AddBoomerangReady(ProjectileBoomerang x)
    {
        lock (_lock)
        {
            _listBMReady.Add(x);
        }
    }
    public void ThrowAgain(ProjectileBoomerang x)
    {
        lock (_lock)
        {
            SoundManager.instance.PlayOneSound(ac);
            _enemies = _ec.GetListEnemyActive();
            if (_enemies == null || _enemies.Count == 0)
            {
                _enemiesFarthest = new List<GameObject>();
                return;
            }
            x.ThrowBoomerang(_enemies[Random.Range(0, _enemies.Count)].transform.position);
        }
    }
    public override void SetUpStartGame(Character charx)
    {
        base.SetUpStartGame(charx);
        AddProjectile(projectile);
        SetUpDataProjectile();
        if (_BoomerangC == null && gameObject.activeInHierarchy == true)
        {
            _BoomerangC = StartCoroutine(ThrowBoomerang());
        }
    }
    private void SetUpDataProjectile()
    {
        int j;
        for (j = 0; j < _projectileBoomerangs.Count; j++)
        {
            _projectileBoomerangs[j].SetUpData(this);
            _projectileBoomerangs[j].SetPlayer(player);
        }
    }
    private void AddProjectile(int count)
    {
        int j;
        for (j = 1; j <= count; j++)
        {
            //Thêm tia
            GameObject newPro = Instantiate(projectilePf);
            ProjectileBoomerang testscript = newPro.GetComponent<ProjectileBoomerang>();
            newPro.transform.parent = transform;
            _projectileBoomerangs.Add(newPro.GetComponent<ProjectileBoomerang>());
            _listBMReady.Add(newPro.GetComponent<ProjectileBoomerang>());
        }
    }
    public override void LevelUp(int attributef, float amountf)
    {
        base.LevelUp(attributef, amountf); //level++
        foreach (Weapons x in _projectileBoomerangs)
        {
            Destroy(x.gameObject);
        }
        foreach (var x in _listBMReady)
        {
            x.gameObject.SetActive(false);
        }
        _listBMReady.Clear();
        _projectileBoomerangs.Clear();
        AddProjectile(projectile);
        SetUpDataProjectile();
        StopCoroutine(_BoomerangC);
        _BoomerangC = StartCoroutine(ThrowBoomerang());
    }
    public override void ResetWeapon()
    {
        base.ResetWeapon();
        foreach (Weapons x in _projectileBoomerangs)
        {
            Destroy(x.gameObject);
        }
        _projectileBoomerangs.Clear();
        _listBMReady.Clear();
        _BoomerangC = null;
    }
    protected override void UpdateStat()
    {
        base.UpdateStat();
        SetUpDataProjectile();
    }
    //Giải thuật tìm n enemy xa nhất
    private void FindNTargetFarthest(int n)
    {
        if(n == 0)
        {
            return;
        }    
        _enemies = _ec.GetListEnemyActive();
        if (_enemies == null || _enemies.Count == 0)
        {
            _enemiesFarthest = new List<GameObject>();
            return;
        }        
        // Mảng lưu 5 enemy xa nhất
        EnemyDistance[] topFar = new EnemyDistance[n];
        float minDistanceSqr = float.MinValue; // Khoảng cách nhỏ nhất trong top 5
        int minIndex = -1; // Vị trí của khoảng cách nhỏ nhất

        foreach (GameObject enemy in _enemies)
        {
            if (!enemy.activeInHierarchy)
            {
                continue; // Bỏ qua enemy đã bị chết
            }

            float distanceSqr = (enemy.transform.position - player.transform.position).sqrMagnitude;

            // Nếu mảng chưa đầy, thêm vào
            if (topFar[n - 1] == null)
            {
                for (int i = 0; i < n; i++)
                {
                    if (topFar[i] == null)
                    {
                        topFar[i] = new EnemyDistance { enemy = enemy, distanceSqr = distanceSqr };
                        if (distanceSqr < minDistanceSqr || minIndex == -1)
                        {
                            minDistanceSqr = distanceSqr;
                            minIndex = i;
                        }
                        break;
                    }
                }
            }
            // Nếu mảng đầy, thay thế nếu khoảng cách lớn hơn minDistanceSqr
            else
            {
                if (distanceSqr > minDistanceSqr)
                {
                    topFar[minIndex] = new EnemyDistance { enemy = enemy, distanceSqr = distanceSqr };
                    // Cập nhật minDistanceSqr và minIndex
                    minDistanceSqr = float.MaxValue;
                    for (int i = 0; i < n; i++)
                    {
                        if (topFar[i].distanceSqr < minDistanceSqr)
                        {
                            minDistanceSqr = topFar[i].distanceSqr;
                            minIndex = i;
                        }
                    }
                }
            }
        }

        // Tạo danh sách kết quả
        _enemiesFarthest = new List<GameObject>();
        int iFirstNull = -1;
        for (int i = 0; i < n; i++)
        {
            if (topFar[i] != null)
            {
                _enemiesFarthest.Add(topFar[i].enemy);
            }
            else // Lặp lại nếu số lượng enemy trên sân không đủ n
            {
                if(iFirstNull == -1)
                {
                    iFirstNull = i;
                }
                _enemiesFarthest.Add(topFar[i % iFirstNull].enemy);
            }
        }
    }
}