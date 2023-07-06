using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponAim : MonoBehaviour
{
    public Transform weaponEnd; // Ko�c�wka broni
    public Transform projectileSpawnPoint; // Miejsce wystrza�u pocisku
    public float rotationSpeed = 10f; // Pr�dko�� obrotu broni
    public GameObject projectilePrefab; // Prefab pocisku
    public float projectileSpeed = 10f; // Pr�dko�� pocisku

    public AnimationClip recoilAnimation; // Animacja ruchu do g�ry
    public GameObject muzzleFlashPrefab; // Prefab efektu wystrza�u

    private bool isFiring = false;

    void Update()
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
            weaponEnd.rotation = Quaternion.RotateTowards(weaponEnd.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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

        // Wystrza� pocisku
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            if (projectileRb != null)
            {
                projectileRb.velocity = projectile.transform.forward * projectileSpeed;
            }
        }

        // Zako�cz wystrza�
        isFiring = false;
    }
}