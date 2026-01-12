using TMPro;
using UnityEngine;

namespace Shop
{
    public class LastScoreReached : MonoBehaviour,ISavable
    {
       [SerializeField] TextMeshProUGUI scoreValueText;
       public void Save(ref GameData data)
       {
           
       }

       public void Load(GameData data)
       {
           scoreValueText.SetText(data.LastScore.ToString());
       }
    }
}
