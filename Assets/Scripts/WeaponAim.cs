using UnityEngine;

// CHAT GPT GENERATED STUFF
public class WeaponAim : MonoBehaviour
{
    public Transform weaponEnd; // Ko�c�wka broni
    public Transform projectileSpawnPoint; // Miejsce wystrza�u pocisku
    public float rotationSpeed; // Pr�dko�� obrotu broni
    public GameObject projectilePrefab; // Prefab pocisku
    public float projectileSpeed; // Pr�dko�� pocisku

    public GameObject muzzleFlashPrefab; // Prefab efektu wystrza�u
    private RaycastHit _hit;
    private Animator AnimR;

    private bool isFiring = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            //isFiring = true;
            Fire();
    }

    private void FixedUpdate()
    {
        // Pobierz pozycj� kursora myszki w przestrzeni �wiata
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Sprawd�, czy promie� z myszki przecina obiekty w scenie
        if (Physics.Raycast(mouseRay, out _hit))
        {
            Debug.DrawLine(_hit.point, Camera.main.transform.position, Color.red);
            // Pobierz punkt przeci�cia promienia z trafionym obiektem
            var targetPosition = _hit.point;

            // Oblicz kierunek celowania
            var direction = targetPosition - weaponEnd.position;

            // Oblicz quaternion reprezentuj�cy celowanie
            var targetRotation = Quaternion.LookRotation(direction);

            // Obr�� ko�c�wk� broni w kierunku celu z zadan� pr�dko�ci�
            weaponEnd.rotation =
                Quaternion.RotateTowards(weaponEnd.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void Fire()
    {
        // Odtw�rz animacj� ruchu do g�ry
        {
            AnimR = GetComponent<Animator>();
            AnimR.SetTrigger("Recoil");
        }

        // Wygeneruj efekt wystrza�u
        if (muzzleFlashPrefab != null) Instantiate(muzzleFlashPrefab, weaponEnd.position, weaponEnd.rotation);

        // Wystrza� pocisku
        var bulletGo = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        var bullet = bulletGo.AddComponent<Bullet>();
        StartCoroutine(bullet.Shoot(_hit.point, projectileSpeed));

        //isFiring = false;
    }
}