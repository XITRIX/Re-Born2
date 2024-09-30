using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public float damageToNekit = 17;
    
    public List<ProjectileShooter> corners;
    public List<ProjectileShooter> top;
    public List<ProjectileShooter> right;
    public List<ProjectileShooter> bottom;
    public List<ProjectileShooter> left;
    
    public List<ToggleItemExtra> toggles;

    public float nekitHealth = 100;
    public Transform nekitHealthBar;
    public GameObject nekitHealthBarObject;
    public CharacterScript nekitModel;
    
    public GameObject nekitShieldObject;
    public static ShootingManager Shared { get; private set; }

    private bool _death;
    private int _phase;
    private int[] _enabledToggles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Shared = this;
        nekitHealthBarObject.SetActive(false);
        HideAllToggles();
        StopAll();
    }

    // Update is called once per frame
    void Update()
    {
        // Update Nekit HP UI
        var scale = nekitHealthBar.localScale;
        scale.x = nekitHealth / 100;
        nekitHealthBar.localScale = scale;

        // Update Nekit shield
        var nekitAlive = _phase == 0 || nekitHealth > 0;
        nekitShieldObject.SetActive(!CanHitNekit() && nekitAlive);

        if (!_death && nekitHealth > 0 && GlobalDirector.Shared.health[PlayerInputScript.Shared.ActiveCharacter.characterModel] <= 0)
        {
            _death = true;
            
            HideAllToggles();
        
            StopAll();
            StopAllCoroutines();
            
            EventBus.Trigger("InteractionEvent", "GameOver");
        }
    }

    public static void StartBossFight()
    {
        Shared.StartCoroutine(Shared.StartBossFightCoroutine());
    }

    IEnumerator StartBossFightCoroutine()
    {
        nekitHealthBarObject.SetActive(true);

        while (nekitHealth < 100)
        {
            nekitHealth += 1;
            yield return new WaitForSeconds(0.03f);
        }

        StartPhase();
    }

    IEnumerator ShootingCycle()
    {
        while (isActiveAndEnabled)
        {
            StopAll();
            yield return new WaitForSeconds(2);

            switch (_phase)
            {
                case 0:
                    yield return Phase1();
                    break;
                case 1:
                    yield return Phase2();
                    break;
                case 2:
                    yield return Phase3();
                    break;
                case 3:
                    yield return Phase4();
                    break;
                case 4:
                    yield return Phase5();
                    break;
                case 5:
                    yield return Phase6();
                    break;
            }
        }
    }

    public IEnumerator StartPhaseCoroutine()
    {
        yield return new WaitForSeconds(5);
        
        switch (_phase)
        {
            case 0:
                EnableToggles(new[] { 0, 1 });
                break;
            case 1:
                EnableToggles(new[] { 0, 1, 5 });
                break;
            case 2:
                EnableToggles(new[] { 2, 6, 7 });
                break;
            case 3:
                EnableToggles(new[] { 0, 1, 6, 7 });
                break;
            case 4:
                EnableToggles(new[] { 0, 1, 2, 5, 6, 7 });
                break;
            case 5:
                EnableToggles(new[] { 0, 1, 2, 3, 4, 5, 6, 7 });
                break;
        }
    }

    public IEnumerator Phase1()
    {
        Shoot(corners);
        yield return new WaitForSeconds(3);
    }

    public IEnumerator Phase2()
    {
        yield return ShootOnce(top);
        yield return ShootOnce(right);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(right);
        yield return ShootOnce(bottom);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(bottom);
        yield return ShootOnce(left);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(left);
        yield return ShootOnce(top);
    }

    public IEnumerator Phase3()
    {
        yield return ShootOnce(top);
        yield return ShootOnce(right);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(right);
        yield return ShootOnce(bottom);
        yield return ShootOnce(corners);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(bottom);
        yield return ShootOnce(left);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(left);
        yield return ShootOnce(top);
        yield return ShootOnce(corners);
    }

    public IEnumerator Phase4()
    {
        yield return ShootOnce(top);
        yield return ShootWaveOnce(right);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(right);
        yield return ShootWaveOnce(bottom);
        yield return ShootOnce(corners);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(bottom);
        yield return ShootWaveOnce(left);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(left);
        yield return ShootWaveOnce(top);
        yield return ShootOnce(corners);
    }

    public IEnumerator Phase5()
    {
        yield return ShootOnce(top);
        yield return ShootWaveOnce(right);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(right);
        yield return ShootWaveOnce(bottom);
        yield return ShootOnce(corners);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(bottom);
        yield return ShootWaveOnce(left);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(left);
        yield return ShootWaveOnce(top);
        yield return ShootOnce(corners);
        yield return new WaitForSeconds(2);
        yield return ShootOnce(corners);
        yield return ShootCircleOnce();
    }

    public IEnumerator Phase6()
    {
        Shoot(corners);
        yield return new WaitForSeconds(3);
        StopAll();
        yield return new WaitForSeconds(2);
        Shoot(top);
        Shoot(bottom);
        yield return new WaitForSeconds(3);
        StopAll();
        Shoot(left);
        Shoot(right);
        yield return new WaitForSeconds(3);
        StopAll();
        yield return new WaitForSeconds(2);
        yield return ShootCircleOnce();
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

    public void HideAllToggles()
    {
        _enabledToggles = null;
        toggles.ForEach(t => t.transform.parent.gameObject.SetActive(false));
    }

    public void EnableToggles(int[] indexes)
    {
        HideAllToggles();
        _enabledToggles = indexes;
        foreach (var index in indexes)
        {
            toggles[index].transform.parent.gameObject.SetActive(true);
            toggles[index].isEnabled = false;
            toggles[index].UpdateSprite();
        }
    }

    public bool CanHitNekit()
    {
        return _enabledToggles != null && _enabledToggles.All(x => toggles[x].isEnabled);
    }

    public static void HitNekit()
    {
        if (!Shared.CanHitNekit()) return;
        Shared._HitNekit();
    }
    
    private void _HitNekit()
    {
        Debug.Log("Hit Nekit");
        nekitModel.HitAnimation();
        nekitHealth -= damageToNekit;
        _phase += 1;
        HideAllToggles();
        
        StopAll();
        StopAllCoroutines();

        StartCoroutine(nekitHealth > 0 ? StartMidPhase() : StartWinPhase());
    }

    public IEnumerator StartMidPhase()
    {
        yield return new WaitForSeconds(5);

        if (_phase == 1) 
            yield return ShowMessage(nekitModel.characterModel, "Ах ты мелкое говно ... Ну ничего, теперь я буду серьёзнее");
        
        if (_phase == 4) 
            yield return ShowMessage(nekitModel.characterModel, "СУКА-СУКА-СУКА-СУКА!!! ИДИТЕ НАХУЙ!!! Я НЕ ПРОИГРАЮ!!!");
        
        StartPhase();
    }
    
    public IEnumerator StartWinPhase()
    {
        yield return new WaitForSeconds(5);
        EventBus.Trigger("InteractionEvent", "NekitKilled");
    }

    public void StartPhase()
    {
        StartCoroutine(StartPhaseCoroutine());
        StartCoroutine(ShootingCycle());
    }
    
    private IEnumerator ShowMessage(CharacterScriptableObject character, string message)
    {
        GlobalDirector.ShowDialog();
        UIDialogMessage.OpenMessageView();
        yield return UIDialogMessage.SetMessage(character.avatar, character.charName, message);
        
        yield return new WaitUntil(() => UIDialogMessage.Shared.Submit);
        yield return new WaitForSeconds(0.1f);
        
        UIDialogMessage.CloseMessageView();
        GlobalDirector.CloseDialog();
    }
}
