using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TankSpells : MonoBehaviour
{
    [Header("Ability 1")]
    public ParticleSystem spell1;
    public Animator spell1ColliderAnimator;
    private AnimationClip spell1Colliderclip;
    public Spell ability1 = new Spell("Physical", 50, 30, 10, 0, 0f);


    [Header("Ability 2")]
    public Spell ability2 = new Spell("None", 0, 0, 0, 0, 0f);

    [Header("Ability 3")]
    public ParticleSystem spell3;
    public Animator spell3ColliderAnimator;
    private AnimationClip spell3Colliderclip;
    public Spell ability3 = new Spell("None", 30, 20, 12, 10, 10f);


    [Header("Ability 4")]
    public Spell ability4 = new Spell("None", 0, 0, 0, 0, 0f);


    public Abilities abilitiesScript;
    public GameObject spell3Particle;
    public GameObject spell3ColliderPosition;
    public GameObject ability3Indicator;

    void Start()    
    {
        foreach(AnimationClip clip in spell1ColliderAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Tank Spell 1 Collider") spell1Colliderclip = clip;
        }
        foreach (AnimationClip clip in spell3ColliderAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Tank Spell 3 Collider") spell3Colliderclip = clip;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Ability1() //skill animasyonynda çaðýrýlýyor
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
        spell3ColliderPosition.transform.SetParent(null);
        spell3ColliderPosition.transform.position = abilitiesScript.spell3CanvasLastPos;

        spell3ColliderAnimator.SetBool("Skill3", true);
        yield return new WaitForSecondsRealtime(spell3Colliderclip.length + 0.1f);
        spell3ColliderAnimator.SetBool("Skill3", false);

        spell3Particle.transform.SetParent(ability3Indicator.transform);
        spell3ColliderPosition.transform.SetParent(ability3Indicator.transform);
        spell3Particle.transform.position = ability3Indicator.transform.position;
        spell3ColliderPosition.transform.position = ability3Indicator.transform.position;
    }
}
 