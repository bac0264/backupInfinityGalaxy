namespace Sirenix.OdinInspector.Demos
{
    using UnityEngine;
    using Sirenix.OdinInspector;

    // Inheriting from SerializedMonoBehaviour is only needed if you want Odin to serialize the multi-dimensional arrays for you.
    // If you prefer doing that yourself, you can still make Odin show them in the inspector using the ShowInInspector attribute.
    public class TwoDimensionalArrayExamples : SerializedMonoBehaviour
    {
        public bool[,] BooleanMatrix = new bool[10, 6];
        public bool[,] s = new bool[10, 6];
        [TableMatrix(SquareCells = true)]
        public GameObject[,] TextureMatrix = new GameObject[5, 5];
        [TableMatrix(SquareCells = true)]
        public GameObject[,] e = new GameObject[5, 5];
        public InfoMessageType[,] EnumMatrix = new InfoMessageType[4, 4];

        public string[,] StringMatrix = new string[4, 4];

        // And so on...
        // Check out the TableMatrix examples for more customization options.
    }
}