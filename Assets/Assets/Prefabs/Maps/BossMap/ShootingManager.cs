using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public List<ProjectileShooter> corners;
    public List<ProjectileShooter> top;
    public List<ProjectileShooter> right;
    public List<ProjectileShooter> bottom;
    public List<ProjectileShooter> left;

    public float nekitHealth = 100;
    public Transform nekitHealthBar;
    public static ShootingManager Shared { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Shared = this;
        StopAll();
    }

    // Update is called once per frame
    void Update()
    {
        // Update Nekit HP UI
        var scale = nekitHealthBar.localScale;
        scale.x = nekitHealth / 100;
        nekitHealthBar.localScale = scale;
    }

    public static void StartBossFight()
    {
        Shared.StartCoroutine(Shared.ShootingCycle());
    }

    IEnumerator ShootingCycle()
    {
        while (isActiveAndEnabled)
        {
            StopAll();
            yield return new WaitForSeconds(2);
            Shoot(corners);
            yield return new WaitForSeconds(3);
            StopAll();
            yield return new WaitForSeconds(2);
            Shoot(top);
            Shoot(bottom);
            yield return new WaitForSeconds(3);

            StopAll();
            yield return new WaitForSeconds(2);
            yield return ShootCircleOnce();
        }
    }

    public void StopAll()
    {
        corners.ForEach(c => c.enabled = false);
        top.ForEach(c => c.enabled = false);
        right.ForEach(c => c.enabled = false);
        bottom.ForEach(c => c.enabled = false);
        left.ForEach(c => c.enabled = false);
    }

    public void Shoot(List<ProjectileShooter> list)
    {
        list.ForEach(c => c.enabled = true);
    }

    public IEnumerator ShootCircleOnce()
    {
        yield return ShootWaveOnce(top);
        yield return ShootWaveOnce(right);
        yield return ShootWaveOnce(bottom);
        yield return ShootWaveOnce(left);
    }

    public IEnumerator ShootOnce(List<ProjectileShooter> list)
    {
        foreach (var shooter in list)
        {
            list.ForEach(c => c.enabled = true);
            yield return new WaitForSeconds(0.1f);
            list.ForEach(c => c.enabled = false);
        }
    }

    public IEnumerator ShootWaveOnce(List<ProjectileShooter> list, bool reverced = false, float delay = 0.1f)
    {
        var shooters = reverced ? list.AsEnumerable().Reverse() : list;
        foreach (var shooter in list)
        {
            shooter.enabled = true;
            yield return new WaitForSeconds(delay);
            shooter.enabled = false;
        }
    }
}
