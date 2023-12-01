using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEscena : MonoBehaviour
{
    // Nombre de la escena que quieres cargar
    public string nombreDeEscena;

    // Este método se llama cuando se presiona el botón
    public void CargarNuevaEscena()
    {
        // Cargar la escena con el nombre especificado
        SceneManager.LoadScene(nombreDeEscena);
    }
}

