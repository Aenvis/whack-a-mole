using UnityEngine;

// CHAT GPT GENERATED STUFF
public class WeaponAim : MonoBehaviour
{
    public Transform weaponEnd; // Koñcówka broni
    public Transform projectileSpawnPoint; // Miejsce wystrza³u pocisku
    public float rotationSpeed; // Prêdkoœæ obrotu broni
    public GameObject projectilePrefab; // Prefab pocisku
    public float projectileSpeed; // Prêdkoœæ pocisku

    public AnimationClip recoilAnimation; // Animacja ruchu do góry
    public GameObject muzzleFlashPrefab; // Prefab efektu wystrza³u

    private bool isFiring = false;

    void FixedUpdate()
    {
        // Pobierz pozycjê kursora myszki w przestrzeni œwiata
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        // SprawdŸ, czy promieñ z myszki przecina obiekty w scenie
        if (Physics.Raycast(mouseRay, out hit))
        {
            // Pobierz punkt przeciêcia promienia z trafionym obiektem
            Vector3 targetPosition = hit.point;

            // Oblicz kierunek celowania
            Vector3 direction = targetPosition - weaponEnd.position;

            // Oblicz quaternion reprezentuj¹cy celowanie
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Obróæ koñcówkê broni w kierunku celu z zadan¹ prêdkoœci¹
            weaponEnd.rotation = Quaternion.RotateTowards(weaponEnd.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Obs³uga wystrza³u po wciœniêciu lewego przycisku myszy
        if (Input.GetMouseButtonDown(0) && !isFiring)
        {
            isFiring = true;
            Fire();
        }
    }

    void Fire()
    {
        // if (!_hit.transform.CompareTag("Mole"))
        // {
        //     isFiring = false;
        //     return;
        // }
        
        // Odtwórz animacjê ruchu do góry
        if (recoilAnimation != null)
        {
            GetComponent<Animation>().Play(recoilAnimation.name);
        }

        // Wygeneruj efekt wystrza³u
        if (muzzleFlashPrefab != null)
        {
            Instantiate(muzzleFlashPrefab, weaponEnd.position, weaponEnd.rotation);
        }

        var hit = GameManager.Instance.SelectedMole;
        // Wystrza³ pocisku
        if (hit is not null)
        {
            Bullet projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation).AddComponent<Bullet>();
            StartCoroutine(projectile.Shoot(hit.transform.position, projectileSpeed));
        }

        isFiring = false;
    }
}