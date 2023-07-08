using System;
using UnityEngine;

// CHAT GPT GENERATED STUFF
public class WeaponAim : MonoBehaviour
{
    public Transform weaponEnd; // Koñcówka broni
    public Transform projectileSpawnPoint; // Miejsce wystrza³u pocisku
    public float rotationSpeed; // Prêdkoœæ obrotu broni
    public GameObject projectilePrefab; // Prefab pocisku
    public float projectileSpeed; // Prêdkoœæ pocisku

    public GameObject muzzleFlashPrefab; // Prefab efektu wystrza³u

    private bool isFiring = false;
    private Animator AnimR;
    private RaycastHit _hit;

    void FixedUpdate()
    {
        // Pobierz pozycjê kursora myszki w przestrzeni œwiata
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // SprawdŸ, czy promieñ z myszki przecina obiekty w scenie
        if (Physics.Raycast(mouseRay, out _hit))
        {
            Debug.DrawLine(_hit.point, Camera.main.transform.position, Color.red);
            // Pobierz punkt przeciêcia promienia z trafionym obiektem
            Vector3 targetPosition = _hit.point;

            // Oblicz kierunek celowania
            Vector3 direction = targetPosition - weaponEnd.position;

            // Oblicz quaternion reprezentuj¹cy celowanie
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Obróæ koñcówkê broni w kierunku celu z zadan¹ prêdkoœci¹
            weaponEnd.rotation = Quaternion.RotateTowards(weaponEnd.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //isFiring = true;
            Fire();
        }
    }

    void Fire()
    {
        // Odtwórz animacjê ruchu do góry
        {
            AnimR = GetComponent<Animator>();
            AnimR.SetTrigger("Recoil");
        }

        // Wygeneruj efekt wystrza³u
        if (muzzleFlashPrefab != null)
        {
            Instantiate(muzzleFlashPrefab, weaponEnd.position, weaponEnd.rotation);
        }
        
        // Wystrza³ pocisku
        var bulletGo = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        var bullet = bulletGo.AddComponent<Bullet>();
        StartCoroutine(bullet.Shoot(_hit.point, projectileSpeed));
        
        //isFiring = false;
    }
}