public interface ISavable
{
    void Save(ref GameData data);
    void Load(GameData data);
}