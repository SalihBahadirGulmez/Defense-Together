using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperSpells : MonoBehaviour
{
    [Header("Ability 1")]
    public ParticleSystem spell1;
    public Collider spell1ColliderItself;
    public GameObject spell1Particle;
    public GameObject spell1Collider;
    public Transform sniperTransform;
    public Spell ability1 = new Spell("Magical", 50, 10, 10, 0.5f, 10f);

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

    public GameObject ability3Indicator;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Ability1() //skill animasyonunda çaðýrýlýyor
    {
        StartCoroutine(Ability1SetParent());
        spell1.Play();
        abilitiesScript.PosAbility3();
    }
    IEnumerator Ability1SetParent()
    {

        spell1Particle.transform.position = abilitiesScript.spell3CanvasLastPos;
        spell1Collider.transform.position = abilitiesScript.spell3CanvasLastPos;


        spell1Particle.transform.SetParent(null);
        spell1Collider.transform.SetParent(null);

        spell1Collider.GetComponent<Collider>().enabled = true;
        yield return new WaitForSecondsRealtime(ability1.duration + 0.1f);
        spell1Collider.GetComponent<Collider>().enabled = false;

        spell1Particle.transform.SetParent(ability3Indicator.transform);
        spell1Collider.transform.SetParent(ability3Indicator.transform);
        spell1Particle.transform.position = ability3Indicator.transform.position;
        spell1Collider.transform.position = ability3Indicator.transform.position;

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
        spell3ColliderAnimator.SetBool("Skill3", true);
        yield return new WaitForSecondsRealtime(spell3Colliderclip.length - 0.1f);
        spell3ColliderAnimator.SetBool("Skill3", false);
    }
}
