using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSpells : MonoBehaviour
{

    [Header("Ability 1")]
    public ParticleSystem spell1;
    public Animator spell1ColliderAnimator;
    private AnimationClip spell1Colliderclip;
    public Spell ability1 = new Spell("Magical", 50, 50, 10, 0, 3f);

    [Header("Ability 2")]
    public ParticleSystem spell2;
    public Animator spell2ColliderAnimator;
    private AnimationClip spell2Colliderclip;
    public Spell ability2 = new Spell("Magical", 3, 60, 12, 0.5f, 10f);

    [Header("Ability 3")]
    public ParticleSystem spell3;
    public Animator spell3ColliderAnimator;

    private AnimationClip spell3Colliderclip;
    public Spell ability3 = new Spell("Magical", 0, 20, 12, 10, 10f);

    [Header("Ability 4")]
    public Spell ability4 = new Spell("None", 0, 0, 0, 0, 0);


    public Abilities abilitiesScript;
    public GameObject spell3Particle;
    public GameObject spell3Collider;
    public GameObject ability3Indicator;



    void Start()
    {
        foreach (AnimationClip clip in spell1ColliderAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Support Spell 1 Collider") spell1Colliderclip = clip;
        }
        foreach (AnimationClip clip in spell2ColliderAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Support Spell 2 Collider") spell2Colliderclip = clip;
        }
        foreach (AnimationClip clip in spell3ColliderAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Support Spell 3 Collider") spell3Colliderclip = clip;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Ability1() //skill animasyonunda çaðýrýlýyor
    {
        StartCoroutine(Ability1SetParent());
        spell1.Play();
        abilitiesScript.PosAbility1();
    }
    IEnumerator Ability1SetParent()
    {
        spell1ColliderAnimator.SetBool("Skill1", true);
        yield return new WaitForSecondsRealtime(spell1Colliderclip.length - 0.1f);
        spell1ColliderAnimator.SetBool("Skill1", false);
    }

    public void Ability2()
    {
        StartCoroutine(Ability2SetParent());
        spell2.Play();
        abilitiesScript.PosAbility2();
    }
    IEnumerator Ability2SetParent()
    {
        spell2ColliderAnimator.SetBool("Skill2", true);
        yield return new WaitForSecondsRealtime(spell2Colliderclip.length - 0.1f);
        spell2ColliderAnimator.SetBool("Skill2", false);
    }
    public void Ability3()
    {
        StartCoroutine(Ability3SetParent());
        spell3.Play();
        abilitiesScript.PosAbility3();
    }
    IEnumerator Ability3SetParent()
    {
        abilitiesScript.spell3CanvasLastPos.y += 1;
        spell3Particle.transform.SetParent(null);
        spell3Particle.transform.position = abilitiesScript.spell3CanvasLastPos;
        spell3Collider.transform.SetParent(null);
        spell3Collider.transform.position = abilitiesScript.spell3CanvasLastPos;

        spell3ColliderAnimator.SetBool("Skill3", true);
        yield return new WaitForSecondsRealtime(spell3Colliderclip.length - 0.1f);
        spell3ColliderAnimator.SetBool("Skill3", false);

        spell3Particle.transform.SetParent(ability3Indicator.transform);
        spell3Collider.transform.SetParent(ability3Indicator.transform);
        spell3Particle.transform.position = ability3Indicator.transform.position;
        spell3Collider.transform.position = ability3Indicator.transform.position;
    }
}
