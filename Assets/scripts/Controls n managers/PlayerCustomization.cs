using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCustomization : MonoBehaviour
{
    public static Transform searched;

    public static void SetSkills(Creature playerData, GameObject playerObject, ref Skill skill)
    {
        if (playerData.MainPlayerClass == MainPlayerClasses.Barbarian)
        {
            skill = playerObject.AddComponent<SimpleMeleeHit1h>();
            skill.SetData(playerData, playerData.AnimationManager.HitAnimation);
        }

        if (playerData.CreatureType == CreatureTypes.Skeleton)
        {
            skill = playerObject.AddComponent<SimpleMeleeHit1h>();
            skill.SetData(playerData, playerData.AnimationManager.HitAnimation);
        }
    }

    public static EffectsManager GetEffectsManager(Creature playerData)
    {
        return Instantiate(Resources.Load<GameObject>("full effects"), playerData.PlayerTransform.position, Quaternion.identity, playerData.PlayerTransform).GetComponent<EffectsManager>();
    }

    public static GameObject InitPlayerData(Creature playerData, GameObject player)
    {
        playerData.SetTransform(player.transform);
        playerData.SetRigidbody(player.GetComponent<Rigidbody>());
        playerData.SetWeaponTriggerMelee(Weapon.CreateMeleeWeaponTrigger(playerData));
        playerData.SetEffectsManager(GetEffectsManager(playerData));

        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            playerData.SetNavMeshAgent(agent);
        }

        int skinID = GetIDForPlayerSkin(playerData);

        GameObject skin = Instantiate(AssetManager.GetPlayerSkinByID(skinID), player.transform.position, Quaternion.identity, player.transform);
        playerData.SetAnimationManager(skin.AddComponent<AnimationManager>());
        playerData.AnimationManager.SetData(skin.GetComponent<Animator>(), playerData);
        
        searched = null;

        ReturnChildOfParent(skin.transform, "hand_container_right");
        playerData.SetRightHandContainerTransform(searched);
        searched = null;

        ReturnChildOfParent(skin.transform, "hand_container_left");
        playerData.SetLeftHandContainerTransform(searched);
        searched = null;

        return skin;
    }

    public static int GetIDForPlayerSkin(Creature playerData)
    {
        if (playerData.MainPlayerClass == MainPlayerClasses.Barbarian)
        {
            return 1;
        }

        if (playerData.CreatureType == CreatureTypes.Skeleton)
        {
            return 2;
        }
        return 0;
    }

    public static void SetWeaponSkins(Creature mainPlayerEntity, ref GameObject mainWeapon, ref GameObject secondWeapon)
    {
        if (mainPlayerEntity.MainInventory.MainWeapon != null)
        {
            mainWeapon = Instantiate(AssetManager.GetWeaponByID(mainPlayerEntity.MainInventory.MainWeapon.WeaponSkin),
                Vector3.zero, Quaternion.identity, mainPlayerEntity.RightHandContainer);
            mainWeapon.transform.localPosition = mainPlayerEntity.MainInventory.MainWeapon.LocalPosition;
            mainWeapon.transform.localEulerAngles = mainPlayerEntity.MainInventory.MainWeapon.LocalRotation;
            mainWeapon.transform.localScale = mainPlayerEntity.MainInventory.MainWeapon.LocalScale;
        }

        if (mainPlayerEntity.MainInventory.SecondWeapon != null)
        {
            secondWeapon = Instantiate(AssetManager.GetWeaponByID(mainPlayerEntity.MainInventory.SecondWeapon.WeaponSkin),
                Vector3.zero, Quaternion.identity, mainPlayerEntity.LeftHandContainer);
            secondWeapon.transform.localPosition = mainPlayerEntity.MainInventory.MainWeapon.LocalPosition;
            secondWeapon.transform.localEulerAngles = mainPlayerEntity.MainInventory.MainWeapon.LocalRotation;
            secondWeapon.transform.localScale = mainPlayerEntity.MainInventory.MainWeapon.LocalScale;
        }
    }

    public static void ReturnChildOfParent(Transform parent, string _name)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).name == _name)
            {
                searched = parent.GetChild(i);
            }
            else if (parent.GetChild(i).childCount > 0)
            {
                ReturnChildOfParent(parent.GetChild(i), _name);
            }
        }
    }
}
