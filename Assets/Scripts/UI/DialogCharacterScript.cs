using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class DialogCharacterScript: MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Image _image;
        private bool _flipped;
        private Vector2 _position;
        private float _blackMask = 0;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Set(CharacterScriptableObject model)
        {
            _image.sprite = model.body;
        }

        public void Flip()
        {
            _flipped = !_flipped;
            var rotation = _rectTransform.rotation;
            var rotationAngles = rotation.eulerAngles;
            
            rotationAngles.y = _flipped ? 180 : 0;
            
            rotation.eulerAngles = rotationAngles;
            _rectTransform.rotation = rotation;
        }

        public IEnumerator Flip(float seconds)
        {
            if (seconds == 0)
                Flip();
            else
            {
                _flipped = !_flipped;

                var startTime = Time.time;
                var start = _flipped ? 0 : 180;
                var target = _flipped ? 180 : 0;
                while (true)
                {
                    var timePass = Time.time - startTime;

                    var rotation = _rectTransform.rotation;
                    var rotationAngles = rotation.eulerAngles;

                    rotationAngles.y = Mathf.Lerp(start, target, timePass / seconds);

                    rotation.eulerAngles = rotationAngles;
                    _rectTransform.rotation = rotation;

                    if (timePass >= seconds) break;
                    yield return null;
                }
            }
        }

        public IEnumerator SetPosition(Vector2 position, float seconds)
        {
            if (seconds == 0)
                Position = position;
            else
            {
                var startTime = Time.time;
                var start = _position;
                while (true)
                {
                    var timePass = Time.time - startTime;
                    Position = Vector2.Lerp(start, position, timePass / seconds);

                    if (timePass >= seconds) break;
                    yield return null;
                }
            }
        }

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                var parentSize = transform.parent.GetComponent<RectTransform>().rect.size;
                // Debug.Log(parentSize);
                var newPosition = new Vector2(parentSize.x / 2 * value.x, parentSize.y * value.y);
                _rectTransform.anchoredPosition = newPosition;
            }
        }
        
        public float BlackMask
        {
            get => _blackMask;
            set
            {
                _blackMask = value;
                var color = 1 - _blackMask;
                _image.color = new Color(color, color, color, Alpha);
            }
        }

        public float Alpha
        {
            get => _image.color.a;
            set => _image.color = _image.color.WithAlpha(value);
        }
    }
}