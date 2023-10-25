using UnityEngine;

namespace GBS.Data.Error
{
    public class GBSErrorData
    {
        public Color Color { get; set; }

        public GBSErrorData() 
        { 
            GenerateRandomColor();
        }

        public void GenerateRandomColor()
        {
            Color = new Color32(
                (byte) Random.Range(65, 256),
                (byte) Random.Range(65, 176),
                (byte) Random.Range(65, 176),
                255
            );
        }
    }
}
