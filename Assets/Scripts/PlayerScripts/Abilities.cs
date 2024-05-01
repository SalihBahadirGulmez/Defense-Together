using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{ 
    [Header("Ability 1")]
    public Image abilityImage1;
    public Text abilityText1;
    public KeyCode ability1Key = KeyCode.Q;
    public float ability1Cooldown = 5;

    public int ability1manaCost;
    public Canvas ability1Canvas;
    public Image ability1Indicator;

    [Header("Ability 2")]
    public Image abilityImage2;
    public Text abilityText2;
    public KeyCode ability2Key = KeyCode.W;
    public float ability2Cooldown = 7;

    public int ability2manaCost;
    public Canvas ability2Canvas;
    public Image ability2Indicator;

    [Header("Ability 3")]
    public Image abilityImage3;
    public Text abilityText3;
    public KeyCode ability3Key = KeyCode.E;
    public float ability3Cooldown = 10;

    public int ability3manaCost;
    public Canvas ability3Canvas;
    public Image ability3Indicator;
    public Image ability3RangeIndicator;
    public float maxAbility3Distance = 2.5f;

    [Header("Ability 4")]
    public Image abilityImage4;
    public Text abilityText4;
    public KeyCode ability4Key = KeyCode.R;
    public float ability4Cooldown;
    public int ability4manaCost;


    private bool isAbility1Cooldown = false;
    private bool isAbility2Cooldown = false;
    private bool isAbility3Cooldown = false;

    private float currentAbility1Cooldown;
    private float currentAbility2Cooldown;
    private float currentAbility3Cooldown;

    private Vector3 position;
    private RaycastHit hit;
    private Ray ray;

    private Stats statsScript;
    public Text manaText2d;

    private RecievedMovement recieveMovementSicript;

    private int layerMask;
    private int layerNumber = 3;


    public Vector3 spell3CanvasLastPos;


    public bool canUseSpell = true;
    void Start()
    {
        layerMask = 1 << layerNumber;

        statsScript = GetComponent<Stats>();
        recieveMovementSicript = GetComponent<RecievedMovement>();

        abilityImage1 = GameObject.Find("Ability 1 Icon (GREYED)").GetComponent<Image>();
        abilityImage2 = GameObject.Find("Ability 2 Icon (GREYED)").GetComponent<Image>();
        abilityImage3 = GameObject.Find("Ability 3 Icon (GREYED)").GetComponent<Image>();

        abilityText1 = GameObject.Find("Ability 1 Text").GetComponent<Text>();
        abilityText2 = GameObject.Find("Ability 2 Text").GetComponent<Text>();
        abilityText3 = GameObject.Find("Ability 3 Text").GetComponent<Text>();

        ability1Canvas = GameObject.Find("Ability 1 Indicator").GetComponent<Canvas>();
        ability2Canvas = GameObject.Find("Ability 2 Indicator").GetComponent<Canvas>();
        ability3Canvas = GameObject.Find("Ability 3 Indicator").GetComponent<Canvas>();


        ability1Indicator = GameObject.Find("Ability 1 Indicator Image").GetComponent<Image>();
        ability2Indicator = GameObject.Find("Ability 2 Indicator Image").GetComponent<Image>();
        ability3Indicator = GameObject.Find("Ability 3 Range Indicator Image").GetComponent<Image>();


        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;

        abilityText1.text = "";
        abilityText2.text = "";
        abilityText3.text = "";

        ability1Canvas.enabled = false;
        ability2Canvas.enabled = false;
        ability3Canvas.enabled = false;

        ability1Indicator.enabled= false;
        ability2Indicator.enabled= false;
        ability3Indicator.enabled= false;

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (canUseSpell)
        {
            Ability1Input();
            Ability2Input();
            Ability3Input();
        }

        AbilityCooldown(ability1Cooldown, ability1manaCost, ref currentAbility1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);
        AbilityCooldown(ability2Cooldown, ability2manaCost, ref currentAbility2Cooldown, ref isAbility2Cooldown, abilityImage2, abilityText2);
        AbilityCooldown(ability3Cooldown, ability3manaCost, ref currentAbility3Cooldown, ref isAbility3Cooldown, abilityImage3, abilityText3);

        Ability1Canvas();
        Ability2Canvas();

        if(ability3Canvas.enabled) Ability3Canvas();


    }

    private void Ability1Canvas()
    {
        if (ability1Indicator.enabled)
        {
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            Quaternion ab1Canvas = Quaternion.LookRotation(position - transform.position);
            ab1Canvas.eulerAngles = new Vector3(0, ab1Canvas.eulerAngles.y, ab1Canvas.eulerAngles.z);

            ability1Canvas.transform.rotation = Quaternion.Lerp(ab1Canvas, ability1Canvas.transform.rotation, 0); 
        }
    }
    private void Ability2Canvas()
    {
        if (ability2Indicator.enabled)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            Quaternion ab2Canvas = Quaternion.LookRotation(position - transform.position);
            ab2Canvas.eulerAngles = new Vector3(0, ab2Canvas.eulerAngles.y, ab2Canvas.eulerAngles.z);

            ability2Canvas.transform.rotation = Quaternion.Lerp(ab2Canvas, ability2Canvas.transform.rotation, 0);
        }
    }
    private void Ability3Canvas()
    {
        //int layerMask = ~LayerMask.GetMask("Player");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if(hit.collider.gameObject != this.gameObject)
            {
                position = hit.point;
            }
        }

        Vector3 hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxAbility3Distance);

        Vector3 newHitPos = transform.position + hitPosDir * distance;
        ability3Canvas.transform.position = newHitPos;
    }

    private void Ability1Input()
    {
        
        if (Input.GetKeyDown(ability1Key) && !isAbility1Cooldown)
        {
            ability1Canvas.enabled = true;
            ability1Indicator.enabled = true;

            ability2Canvas.enabled = false;
            ability2Indicator.enabled = false;
            ability3Canvas.enabled = false;
            ability3Indicator.enabled = false;

        }
        if (ability1Indicator.enabled && Input.GetMouseButtonDown(1))
        {
            ability1Canvas.enabled = false;
            ability1Indicator.enabled = false;

            ability2Canvas.enabled = false;
            ability2Indicator.enabled = false;
            ability3Canvas.enabled = false;
            ability3Indicator.enabled = false;
        }

        if (ability1Indicator.enabled && Input.GetMouseButtonDown(0) && ability1manaCost <= statsScript.character.currentMana)
        {
            ability1Canvas.enabled = false;
            ability1Indicator.enabled = false;
            transform.rotation = ability1Canvas.transform.rotation;
            ability1Canvas.transform.rotation = transform.rotation;
            recieveMovementSicript.Spell("Skill1");
        }

    }
    private void Ability2Input()
    {
        if (Input.GetKeyDown(ability2Key) && !isAbility2Cooldown)
        {
            ability2Canvas.enabled = true;
            ability2Indicator.enabled = true;

            ability1Canvas.enabled = false;
            ability1Indicator.enabled = false;
            ability3Canvas.enabled = false;
            ability3Indicator.enabled = false;

        }
        if (ability2Indicator.enabled && Input.GetMouseButtonDown(1))
        {
            ability1Canvas.enabled = false;
            ability1Indicator.enabled = false;

            ability2Canvas.enabled = false;
            ability2Indicator.enabled = false;
            ability3Canvas.enabled = false;
            ability3Indicator.enabled = false;
        }
        if (ability2Indicator.enabled && Input.GetMouseButtonDown(0) && ability2manaCost <= statsScript.character.currentMana)
        {
            ability2Canvas.enabled = false;
            ability2Indicator.enabled = false;
            transform.rotation = ability2Canvas.transform.rotation;
            ability2Canvas.transform.rotation = transform.rotation;
            recieveMovementSicript.Spell("Skill2");
        }
    }
    private void Ability3Input()
    {
        if (Input.GetKeyDown(ability3Key) && !isAbility3Cooldown)
        {

            ability3Canvas.enabled = true;
            ability3Indicator.enabled = true;

            ability2Canvas.enabled = false;
            ability2Indicator.enabled = false;
            ability1Canvas.enabled = false;
            ability1Indicator.enabled = false;

        }
        if (ability3Indicator.enabled && Input.GetMouseButtonDown(1))
        {
            ability1Canvas.enabled = false;
            ability1Indicator.enabled = false;

            ability2Canvas.enabled = false;
            ability2Indicator.enabled = false;
            ability3Canvas.enabled = false;
            ability3Indicator.enabled = false;
        }
        if (ability3Indicator.enabled && Input.GetMouseButtonDown(0) && ability3manaCost <= statsScript.character.currentMana)
        {
            spell3CanvasLastPos = ability3Canvas.transform.position;
            ability3Canvas.enabled = false;
            ability3Indicator.enabled = false;
            transform.LookAt(ability3Canvas.transform.position, Vector3.up);
            recieveMovementSicript.Spell("Skill3");
        }
    }

    private void AbilityCooldown(float abilityCooldown, float abilityManaCost, ref float currentCooldown, ref bool isCooldown, Image skillImage, Text skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;
            }
            if (skillImage != null)
            {
                skillImage.color = Color.grey;
                skillImage.fillAmount = currentCooldown / abilityCooldown;
            } 
            if (skillText != null)
            {
                skillText.text = Mathf.Ceil(currentCooldown).ToString();
            }           
        }
        else
        {
            if (statsScript.character.currentMana >= abilityManaCost)
            {
                if (skillImage != null)
                {
                    skillImage.color = Color.grey;
                    skillImage.fillAmount = 0;
                }
                if (skillText != null)
                {
                    skillText.text = " ";
                }
            }
            else
            {
                if (skillImage != null)
                {
                    skillImage.color = Color.red;
                    skillImage.fillAmount = 1;
                }
                if (skillText != null)
                {
                    skillText.text = "X";
                }
            }
        }
    }

    public void PosAbility1() 
    {
        isAbility1Cooldown = true;
        currentAbility1Cooldown = ability1Cooldown;
        statsScript.ManaLose(ability1manaCost);
        manaText2d.text = statsScript.character.currentMana.ToString() + "/" + statsScript.character.mana.ToString();
    }

    public void PosAbility2()
    {
        isAbility2Cooldown = true;
        currentAbility2Cooldown = ability2Cooldown;
        statsScript.ManaLose(ability2manaCost);
        manaText2d.text = statsScript.character.currentMana.ToString() + "/" + statsScript.character.mana.ToString();
    }
    public void PosAbility3()
    {
        isAbility3Cooldown = true;
        currentAbility3Cooldown = ability3Cooldown;
        statsScript.ManaLose(ability3manaCost);
        manaText2d.text = statsScript.character.currentMana.ToString() + "/" + statsScript.character.mana.ToString();
    }

}
