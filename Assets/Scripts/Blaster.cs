using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponAim : MonoBehaviour
{
    public Transform weaponEnd; // Koñcówka broni
    public Transform projectileSpawnPoint; // Miejsce wystrza³u pocisku
    public float rotationSpeed = 10f; // Prêdkoœæ obrotu broni
    public GameObject projectilePrefab; // Prefab pocisku
    public float projectileSpeed = 10f; // Prêdkoœæ pocisku

    public AnimationClip recoilAnimation; // Animacja ruchu do góry
    public GameObject muzzleFlashPrefab; // Prefab efektu wystrza³u

    private bool isFiring = false;

    void Update()
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
            weaponEnd.rotation = Quaternion.RotateTowards(weaponEnd.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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

        // Wystrza³ pocisku
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            if (projectileRb != null)
            {
                projectileRb.velocity = projectile.transform.forward * projectileSpeed;
            }
        }

        // Zakoñcz wystrza³
        isFiring = false;
    }
}