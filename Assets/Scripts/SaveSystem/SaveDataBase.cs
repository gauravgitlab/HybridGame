
[System.Serializable]
public abstract class SaveDataBase
{
    public string m_version = "1.0";
}

public interface ICustomSerializable
{
    void PrepareForSave();
    void RestoreAfterLoad();
}