using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    //las variables del tipo static pueden ser llamadas desde distintas escenas. 
    public static MainManager Instance;
    public Color TeamColor;

    //El siguiente patr�n de dise�o es un singleton. Evita que se cree otro objeto si detecta que ya hay uno creado con anterioridad. 
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    //convertir datos a json para que persistan entre sesiones. Los datos se guardan(serializan) en un archivo json para su posterior uso. 
    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    //M�todo que guarda los datos:
    public void SaveColor()
    {
        //creo una instancia de SaveData que guardar� el color guardado enel MainManager:
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        //transformo la instancia data en un string json:
        string json = JsonUtility.ToJson(data);

        //Guardo el string en un archivo: 
        // m�todo        ( m�todo que crea una carpeta  + nombre del archivo, string que se guardar� en el archivo);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    //M�todo que carga los datos:
    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            //...si el archivo existe, lo transforma en una instancia de la clase SaveData:
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //finalmente, setea la variable TeamColor con el color que trae de data. 
            TeamColor = data.TeamColor;
        }
    }
}
