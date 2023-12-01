using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEscena : MonoBehaviour
{
    // Nombre de la escena que quieres cargar
    public string nombreDeEscena;

    // Este m�todo se llama cuando se presiona el bot�n
    public void CargarNuevaEscena()
    {
        // Cargar la escena con el nombre especificado
        SceneManager.LoadScene(nombreDeEscena);
    }
}

