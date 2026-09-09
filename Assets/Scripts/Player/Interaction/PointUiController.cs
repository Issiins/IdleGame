using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Player.Upgrade;
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
        private const string Text = "Current point count: ";
        private const string Format = "N0";
        public void UpdateValue()
        {
            _builder.Clear();
            _builder.Append(Text);
            _builder.Append(pointController.GetPoints().ToString(Format));
            pointText.text = _builder.ToString();
            if(debug) Debug.Log("Point text after update: \n" + pointText.text);
        }
    }
}