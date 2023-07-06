using UnityEngine;

// CHAT GPT GENERATED STUFF
public class WeaponAim : MonoBehaviour
{
    public Transform weaponEnd; // Ko�c�wka broni
    public Transform projectileSpawnPoint; // Miejsce wystrza�u pocisku
    public float rotationSpeed; // Pr�dko�� obrotu broni
    public GameObject projectilePrefab; // Prefab pocisku
    public float projectileSpeed; // Pr�dko�� pocisku

    public AnimationClip recoilAnimation; // Animacja ruchu do g�ry
    public GameObject muzzleFlashPrefab; // Prefab efektu wystrza�u

    private bool isFiring = false;

    void FixedUpdate()
    {
        // Pobierz pozycj� kursora myszki w przestrzeni �wiata
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        // Sprawd�, czy promie� z myszki przecina obiekty w scenie
        if (Physics.Raycast(mouseRay, out hit))
        {
            // Pobierz punkt przeci�cia promienia z trafionym obiektem
            Vector3 targetPosition = hit.point;

            // Oblicz kierunek celowania
            Vector3 direction = targetPosition - weaponEnd.position;

            // Oblicz quaternion reprezentuj�cy celowanie
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Obr�� ko�c�wk� broni w kierunku celu z zadan� pr�dko�ci�
            weaponEnd.rotation = Quaternion.RotateTowards(weaponEnd.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Obs�uga wystrza�u po wci�ni�ciu lewego przycisku myszy
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
        
        // Odtw�rz animacj� ruchu do g�ry
        if (recoilAnimation != null)
        {
            GetComponent<Animation>().Play(recoilAnimation.name);
        }

        // Wygeneruj efekt wystrza�u
        if (muzzleFlashPrefab != null)
        {
            Instantiate(muzzleFlashPrefab, weaponEnd.position, weaponEnd.rotation);
        }

        var hit = GameManager.Instance.SelectedMole;
        // Wystrza� pocisku
        if (hit is not null)
        {
            Bullet projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation).AddComponent<Bullet>();
            StartCoroutine(projectile.Shoot(hit.transform.position, projectileSpeed));
        }

        isFiring = false;
    }
}