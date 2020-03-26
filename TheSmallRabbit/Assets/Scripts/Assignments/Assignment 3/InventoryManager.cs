using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{ //TODO: Refactor all inputs to use GetButtonDown or GetButton instead of getAxis
    public static InventoryManager instance;
    private ItemScriptableObject[] items;
    private int equippedSlot = 0;
    private int nextIndex = 0;
    private bool canFire = true;
    public Image[] icons;
    public GameObject hotbar;

    public Text goldText;
    private int goldNum = 10;

    void Start()
    {
        instance = GetComponent<InventoryManager>();
        items = new ItemScriptableObject[10];
        ItemToEquip();
        goldText.text = goldNum.ToString();
    }

    private void Update()
    {
        if (goldText.text != goldNum.ToString())
        {
            goldText.text = goldNum.ToString();
        }
    }

    public void AddItemToInventory(ItemScriptableObject itemData)
    {
        items[nextIndex] = itemData;
        icons[nextIndex].sprite = items[nextIndex].icon;
        nextIndex++;
    }

    public ItemScriptableObject ItemToEquip()
    {

        for (int i = 0; i < hotbar.transform.childCount; i++)
        {
            if (i != equippedSlot )
            {
                hotbar.transform.GetChild(i).GetComponent<Outline>().effectColor = Color.black;
            }
            else
            {
                hotbar.transform.GetChild(equippedSlot).GetComponent<Outline>().effectColor = Color.red;
            }
        }

        if (nextIndex >= equippedSlot)
        {
            return items[equippedSlot];
        }
        return null;
    }

    IEnumerator ItemCooldown()
    {
        yield return new WaitForSeconds(items[equippedSlot].cooldownTime);
        canFire = true;
    }

    public void CoolDown()
    {
        StartCoroutine(ItemCooldown());
    }

    public ItemScriptableObject EquipItem()
    {
        for (int i = 48; i <= 57; i++)
        {
            KeyCode current = (KeyCode)i;
            if (Input.GetKeyDown(current))
            {
                if (i == 48)
                {
                    equippedSlot = 9;
                }
                else
                {
                    equippedSlot = i - 49;
                }

                return items[equippedSlot];
            }
        }
        return ItemToEquip();
    }

    public void UseItem(GameObject owner) //TODO: Transfer UseItem functionality to ToggleableItem and RepeatingItem functions
    {

        if (items[equippedSlot] != null && !items[equippedSlot].toggles)
        {
            RepeatingItem(owner);
        }
        else if (items[equippedSlot] != null && items[equippedSlot].toggles)
        {
            ToggleableItem(owner);
        }
        //Play item specific animation
    }


    private void ToggleableItem(GameObject owner)
    {
        if (canFire && Input.GetButtonDown("Fire1")) //TODO: Fix bobber duplication
        {
            CreateProjectile(owner);
        }
        else if(!canFire && Input.GetButtonDown("Fire1"))
        {
            owner.GetComponent<Animator>().SetBool("UseItem", true);
            canFire = true;
        }
        else
        {
            owner.GetComponent<Animator>().SetBool("UseItem", false);
        }
    }

    private void RepeatingItem(GameObject owner)
    {
        if (canFire) //TODO: Fix bobber duplication
        {
            CreateProjectile(owner);
            CoolDown();
        }
        else
        {
            owner.GetComponent<Animator>().SetBool("UseItem", false);
        }
                
    }

    private void CreateProjectile(GameObject owner)
    {
        owner.GetComponent<Animator>().SetBool("UseItem", true);
        canFire = false;
        if (items[equippedSlot].projectilePrefab != null)
        {
            GameObject projectile = Instantiate(items[equippedSlot].projectilePrefab, owner.transform.position + owner.transform.forward, owner.transform.rotation);
            projectile.GetComponent<Projectile>().SetOwner(owner);
        }
    }

    public void AddGold(int amount)
    {
        goldNum += amount;
    }

    public int CheckGold()
    {
        return goldNum;
    }

    public void RemoveGold(int amount)
    {
        goldNum -= amount;
    }
}
