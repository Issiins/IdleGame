using System.Text;
using TMPro;
using UnityEngine;


namespace Interaction
{
    public class PointUiController : MonoBehaviour
    {
        [SerializeField] private PointController pointController;
        [SerializeField] private TMP_Text pointText;
        [SerializeField] private bool debug = true;

        private readonly StringBuilder _builder = new();
        private const string Text = "Insanity points: ";
        private const string Format = "N0";
        public void UpdateValue(float points)
        {
            _builder.Clear();
            _builder.Append(Text);
            _builder.Append(points.ToString(Format));
            pointText.text = _builder.ToString();
        }
        public void UpdateValue()
        {
            _builder.Clear();
            _builder.Append(Text);
            _builder.Append(pointController.GetPoints().ToString(Format));
            pointText.text = _builder.ToString();
        }
    }
}