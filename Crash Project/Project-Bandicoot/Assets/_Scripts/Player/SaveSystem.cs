
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveSystem
{

    public static void savePlayer(Crash player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.SaveData";
        FileStream stream = new FileStream(path, FileMode.Create);

        CrashData data = new CrashData(player);


        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CrashData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.SaveData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CrashData data = formatter.Deserialize(stream) as CrashData;
            stream.Close();
            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}