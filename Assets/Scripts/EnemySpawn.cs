using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject prefab;

    public float spawnRate = 2f;
  
    public float speed = 3f;

    public float destructionLimitX = -12f;

    // Limites de spawn no cenário
    public float limitX = 8f;
    public float limitY = 4f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }
    private void Spawn()
    {
        // Define posição de spawn (à direita da tela)
        float x = limitX;
        float y = Random.Range(-limitY, limitY);
        Vector2 posicaoAleatoria = new Vector2(x, y);

        // Instancia o inimigo
        GameObject enemy = Instantiate(prefab, posicaoAleatoria, Quaternion.identity);

        // Inicia o movimento automático (corrotina)
        StartCoroutine(MoveEnemy(enemy));
    }

    IEnumerator MoveEnemy(GameObject enemy)
    {
        while (enemy != null)
        {
            // Move o inimigo da direita para a esquerda
            enemy.transform.Translate(Vector2.left * speed * Time.deltaTime);

            // Se o inimigo sair do limite visível, destrói o objeto
            if (enemy.transform.position.x < destructionLimitX)
            {
                Destroy(enemy);
                yield break; // Sai da corrotina
            }

            yield return null; // Espera o próximo frame
        }
    }
}
