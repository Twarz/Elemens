using UnityEngine;

/// <summary>
/// Crée des projectiles
/// </summary>
public class WeaponScript : MonoBehaviour
{
  //--------------------------------
  // 1 - Designer variables
  //--------------------------------

  /// <summary>
  /// Prefab du projectile
  /// </summary>
  public Transform shotPrefab;

  /// <summary>
  /// Temps de rechargement entre deux tirs
  /// </summary>
  public float shootingRate = 0.25f;

  //--------------------------------
  // 2 - Rechargement
  //--------------------------------

  private float shootCooldown;

  void Start()
  {
    shootCooldown = 0f;
  }

  void Update()
  {
    if (shootCooldown > 0)
    {
      shootCooldown -= Time.deltaTime;
    }
    
    // 5 - Tir
    bool shoot = Input.GetButtonDown("Fire1");
    shoot |= Input.GetButtonDown("Fire2");
    // Astuce pour ceux sous Mac car Ctrl + flèches est utilisé par le système

    if (shoot)
    {
      WeaponScript weapon = GetComponent<WeaponScript>();
      if (weapon != null)
      {
        // false : le joueur n'est pas un ennemi
        weapon.Attack(false);
      }
    }
  }

  //--------------------------------
  // 3 - Tirer depuis un autre script
  //--------------------------------

  /// <summary>
  /// Création d'un projectile si possible
  /// </summary>
  public void Attack(bool isEnemy)
  {
    if (CanAttack)
    {
      shootCooldown = shootingRate;

      // Création d'un objet copie du prefab
      var shotTransform = Instantiate(shotPrefab) as Transform;

      // Position
      shotTransform.position = transform.position;

      // Propriétés du script
      ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
      if (shot != null)
      {
        shot.isEnemyShot = isEnemy;
      }

      // On saisit la direction pour le mouvement
      MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
      if (move != null)
      {
        move.direction = this.transform.right; // ici la droite sera le devant de notre objet
      }
    }
  }

  /// <summary>
  /// L'arme est chargée ?
  /// </summary>
  public bool CanAttack
  {
    get
    {
      return shootCooldown <= 0f;
    }
  }
}


