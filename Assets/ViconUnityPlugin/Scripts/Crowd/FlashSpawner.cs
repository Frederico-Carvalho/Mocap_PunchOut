using UnityEngine;

public class FlashSpawner : MonoBehaviour
{
    [System.Serializable]
    public class FlashZone
    {
        public Transform zoneCenter;
        public Vector3 zoneSize = new Vector3(6f, 3f, 2f); // largura, altura, profundidade
    }

    [SerializeField] private GameObject flashPrefab;
    [SerializeField] private FlashZone[] zones;
    [SerializeField] private float minInterval = 0.1f;
    [SerializeField] private float maxInterval = 0.4f;
    [SerializeField] private int minFlashesPerBurst = 1;
    [SerializeField] private int maxFlashesPerBurst = 4;

    [Header("Som")]
    [SerializeField] private AudioClip[] flashSounds;
    [SerializeField] private float soundVolume = 0.6f;
    [SerializeField] private Vector2 pitchRange = new Vector2(0.9f, 1.1f);

    private float timer;
    private float nextInterval;

    private void Start()
    {
        SetNextInterval();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= nextInterval)
        {
            int count = Random.Range(minFlashesPerBurst, maxFlashesPerBurst + 1);
            for (int i = 0; i < count; i++)
            {
                SpawnFlash();
            }
            timer = 0f;
            SetNextInterval();
        }
    }

    private void SetNextInterval()
    {
        nextInterval = Random.Range(minInterval, maxInterval);
    }

    private void SpawnFlash()
    {
        if (zones.Length == 0) return;

        FlashZone zone = zones[Random.Range(0, zones.Length)];

        float x = Random.Range(-zone.zoneSize.x / 2f, zone.zoneSize.x / 2f);
        float y = Random.Range(0f, zone.zoneSize.y); // sempre para cima do centro da zona
        float z = Random.Range(-zone.zoneSize.z / 2f, zone.zoneSize.z / 2f);

        // TransformPoint respeita a posição E rotação da zona, útil para bancadas anguladas
        Vector3 spawnPos = zone.zoneCenter.TransformPoint(new Vector3(x, y, z));

        Debug.Log($"Zona: {zone.zoneCenter.name} | Centro: {zone.zoneCenter.position} | Scale: {zone.zoneCenter.lossyScale} | Offset local: {new Vector3(x, y, z)} | Spawn final: {spawnPos}");

        Instantiate(flashPrefab, spawnPos, Quaternion.identity);
        PlayFlashSound(spawnPos);
    }

    private void PlayFlashSound(Vector3 position)
    {
        if (flashSounds == null || flashSounds.Length == 0) return;

        AudioClip clip = flashSounds[Random.Range(0, flashSounds.Length)];
        if (clip == null) return;

        // Cria um AudioSource temporário para permitir variar o pitch (PlayClipAtPoint sozinho não suporta pitch)
        GameObject tempAudio = new GameObject("FlashSound_Temp");
        tempAudio.transform.position = position;

        AudioSource source = tempAudio.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = soundVolume;
        source.pitch = Random.Range(pitchRange.x, pitchRange.y);
        source.spatialBlend = 1f; // som 3D
        source.Play();

        Destroy(tempAudio, clip.length / source.pitch);
    }

    private void OnDrawGizmos()
    {
        if (zones == null) return;
        Gizmos.color = Color.yellow;
        foreach (var zone in zones)
        {
            if (zone.zoneCenter == null) continue;
            Gizmos.matrix = zone.zoneCenter.localToWorldMatrix;
            Vector3 boxCenter = new Vector3(0f, zone.zoneSize.y / 2f, 0f);
            Gizmos.DrawWireCube(boxCenter, zone.zoneSize);
        }
    }
}